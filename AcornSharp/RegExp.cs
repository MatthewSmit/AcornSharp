using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp
{
    internal sealed class RegExpValidationState
    {
        private readonly Parser parser;
        public readonly string validFlags;
        public string source;
        public string flags;
        public int start;
        public bool switchU;
        public bool switchN;
        public int pos;
        public int lastIntValue;
        public string lastStringValue;
        public bool lastAssertionIsQuantifiable;
        public int numCapturingParens;
        public int maxBackReference;
        public readonly List<string> groupNames;
        public readonly List<string> backReferenceNames;

        public RegExpValidationState([NotNull] Parser parser)
        {
            this.parser = parser;
            validFlags = $"gim{(parser.Options.EcmaVersion >= 6 ? "uy" : "")}{(parser.Options.EcmaVersion >= 9 ? "s" : "")}";
            source = "";
            flags = "";
            start = 0;
            switchU = false;
            switchN = false;
            pos = 0;
            lastIntValue = 0;
            lastStringValue = "";
            lastAssertionIsQuantifiable = false;
            numCapturingParens = 0;
            maxBackReference = 0;
            groupNames = new List<string>();
            backReferenceNames = new List<string>();
        }

        public void Reset(int start, string pattern, [NotNull] string flags)
        {
            var unicode = flags.IndexOf("u", StringComparison.Ordinal) != -1;
            this.start = start;
            source = pattern + "";
            this.flags = flags;
            switchU = unicode && parser.Options.EcmaVersion >= 6;
            switchN = unicode && parser.Options.EcmaVersion >= 9;
        }

        public void Raise(string message)
        {
            parser.RaiseRecoverable(start, $"Invalid regular expression: /{source}/: {message}");
        }

        // If u flag is given, this returns the code point at the index (it combines a surrogate pair).
        // Otherwise, this returns the code unit of the index (can be a part of a surrogate pair).
        private int At(int i)
        {
            var s = source;
            var l = s.Length;
            if (i >= l)
            {
                return -1;
            }

            var c = s[i];
            if (!switchU || c <= 0xD7FF || c >= 0xE000 || i + 1 >= l)
            {
                return c;
            }

            return (c << 10) + s[i + 1] - 0x35FDC00;
        }

        private int NextIndex(int i)
        {
            var s = source;
            var l = s.Length;
            if (i >= l)
            {
                return l;
            }

            var c = s[i];
            if (!switchU || c <= 0xD7FF || c >= 0xE000 || i + 1 >= l)
            {
                return i + 1;
            }

            return i + 2;
        }

        public int Current()
        {
            return At(pos);
        }

        public int Lookahead()
        {
            return At(NextIndex(pos));
        }

        public void Advance()
        {
            pos = NextIndex(pos);
        }

        public bool Eat(int ch)
        {
            if (Current() == ch)
            {
                Advance();
                return true;
            }

            return false;
        }
    }

    public sealed partial class Parser
    {
        /**
         * Validate the flags part of a given RegExpLiteral.
         *
         * @param {RegExpValidationState} state The state to validate RegExp.
         * @returns {void}
         */
        private void ValidateRegExpFlags([NotNull] RegExpValidationState state)
        {
            var validFlags = state.validFlags;
            var flags = state.flags;

            for (var i = 0; i < flags.Length; i++)
            {
                var flag = flags[i];
                if (validFlags.IndexOf(flag) == -1)
                {
                    Raise(state.start, "Invalid regular expression flag");
                }

                if (flags.IndexOf(flag, i + 1) > -1)
                {
                    Raise(state.start, "Duplicate regular expression flag");
                }
            }
        }

        /**
         * Validate the pattern part of a given RegExpLiteral.
         *
         * @param {RegExpValidationState} state The state to validate RegExp.
         * @returns {void}
         */
        private void ValidateRegExpPattern([NotNull] RegExpValidationState state)
        {
            RegexpPattern(state);

            // The goal symbol for the parse is |Pattern[~U, ~N]|. If the result of
            // parsing contains a |GroupName|, reparse with the goal symbol
            // |Pattern[~U, +N]| and use this result instead. Throw a *SyntaxError*
            // exception if _P_ did not conform to the grammar, if any elements of _P_
            // were not matched by the parse, or if any Early Error conditions exist.
            if (!state.switchN && Options.EcmaVersion >= 9 && state.groupNames.Count > 0)
            {
                state.switchN = true;
                RegexpPattern(state);
            }
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-Pattern
        private void RegexpPattern([NotNull] RegExpValidationState state)
        {
            state.pos = 0;
            state.lastIntValue = 0;
            state.lastStringValue = "";
            state.lastAssertionIsQuantifiable = false;
            state.numCapturingParens = 0;
            state.maxBackReference = 0;
            state.groupNames.Clear();
            state.backReferenceNames.Clear();

            RegexpDisjunction(state);

            if (state.pos != state.source.Length)
            {
                // Make the same messages as V8.
                if (state.Eat(0x29 /* ) */))
                {
                    state.Raise("Unmatched ')'");
                }

                if (state.Eat(0x5D /* [ */) || state.Eat(0x7D /* } */))
                {
                    state.Raise("Lone quantifier brackets");
                }
            }

            if (state.maxBackReference > state.numCapturingParens)
            {
                state.Raise("Invalid escape");
            }

            foreach (var name in state.backReferenceNames)
            {
                if (state.groupNames.IndexOf(name) == -1)
                {
                    state.Raise("Invalid named capture referenced");
                }
            }
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-Disjunction
        private void RegexpDisjunction([NotNull] RegExpValidationState state)
        {
            RegexpAlternative(state);
            while (state.Eat(0x7C /* | */))
            {
                RegexpAlternative(state);
            }

            // Make the same message as V8.
            if (RegexpEatQuantifier(state, true))
            {
                state.Raise("Nothing to repeat");
            }

            if (state.Eat(0x7B /* { */))
            {
                state.Raise("Lone quantifier brackets");
            }
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-Alternative
        private void RegexpAlternative([NotNull] RegExpValidationState state)
        {
            while (state.pos < state.source.Length && RegexpEatTerm(state))
            {
            }
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-annexB-Term
        private bool RegexpEatTerm([NotNull] RegExpValidationState state)
        {
            if (RegexpEatAssertion(state))
            {
                // Handle `QuantifiableAssertion Quantifier` alternative.
                // `state.lastAssertionIsQuantifiable` is true if the last eaten Assertion
                // is a QuantifiableAssertion.
                if (state.lastAssertionIsQuantifiable && RegexpEatQuantifier(state))
                {
                    // Make the same message as V8.
                    if (state.switchU)
                    {
                        state.Raise("Invalid quantifier");
                    }
                }

                return true;
            }

            if (state.switchU ? RegexpEatAtom(state) : RegexpEatExtendedAtom(state))
            {
                RegexpEatQuantifier(state);
                return true;
            }

            return false;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-annexB-Assertion
        private bool RegexpEatAssertion([NotNull] RegExpValidationState state)
        {
            var start = state.pos;
            state.lastAssertionIsQuantifiable = false;

            // ^, $
            if (state.Eat(0x5E /* ^ */) || state.Eat(0x24 /* $ */))
            {
                return true;
            }

            // \b \B
            if (state.Eat(0x5C /* \ */))
            {
                if (state.Eat(0x42 /* B */) || state.Eat(0x62 /* b */))
                {
                    return true;
                }

                state.pos = start;
            }

            // Lookahead / Lookbehind
            if (state.Eat(0x28 /* ( */) && state.Eat(0x3F /* ? */))
            {
                var lookbehind = false;
                if (Options.EcmaVersion >= 9)
                {
                    lookbehind = state.Eat(0x3C /* < */);
                }

                if (state.Eat(0x3D /* = */) || state.Eat(0x21 /* ! */))
                {
                    RegexpDisjunction(state);
                    if (!state.Eat(0x29 /* ) */))
                    {
                        state.Raise("Unterminated group");
                    }

                    state.lastAssertionIsQuantifiable = !lookbehind;
                    return true;
                }
            }

            state.pos = start;
            return false;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-Quantifier
        private static bool RegexpEatQuantifier([NotNull] RegExpValidationState state, bool noError = false)
        {
            if (RegexpEatQuantifierPrefix(state, noError))
            {
                state.Eat(0x3F /* ? */);
                return true;
            }

            return false;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-QuantifierPrefix
        private static bool RegexpEatQuantifierPrefix([NotNull] RegExpValidationState state, bool noError)
        {
            return state.Eat(0x2A /* * */) ||
                   state.Eat(0x2B /* + */) ||
                   state.Eat(0x3F /* ? */) ||
                   RegexpEatBracedQuantifier(state, noError);
        }

        private static bool RegexpEatBracedQuantifier([NotNull] RegExpValidationState state, bool noError)
        {
            var start = state.pos;
            if (state.Eat(0x7B /* { */))
            {
                var max = -1;
                if (RegexpEatDecimalDigits(state))
                {
                    var min = state.lastIntValue;
                    if (state.Eat(0x2C /* , */) && RegexpEatDecimalDigits(state))
                    {
                        max = state.lastIntValue;
                    }

                    if (state.Eat(0x7D /* } */))
                    {
                        // SyntaxError in https://www.ecma-international.org/ecma-262/8.0/#sec-term
                        if (max != -1 && max < min && !noError)
                        {
                            state.Raise("numbers out of order in {} quantifier");
                        }

                        return true;
                    }
                }

                if (state.switchU && !noError)
                {
                    state.Raise("Incomplete quantifier");
                }

                state.pos = start;
            }

            return false;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-Atom
        private bool RegexpEatAtom([NotNull] RegExpValidationState state)
        {
            return RegexpEatPatternCharacters(state) ||
                   state.Eat(0x2E /* . */) ||
                   RegexpEatReverseSolidusAtomEscape(state) ||
                   RegexpEatCharacterClass(state) ||
                   RegexpEatUncapturingGroup(state) ||
                   RegexpEatCapturingGroup(state);
        }

        private bool RegexpEatReverseSolidusAtomEscape([NotNull] RegExpValidationState state)
        {
            var start = state.pos;
            if (state.Eat(0x5C /* \ */))
            {
                if (RegexpEatAtomEscape(state))
                {
                    return true;
                }

                state.pos = start;
            }

            return false;
        }

        private bool RegexpEatUncapturingGroup([NotNull] RegExpValidationState state)
        {
            var start = state.pos;
            if (state.Eat(0x28 /* ( */))
            {
                if (state.Eat(0x3F /* ? */) && state.Eat(0x3A /* : */))
                {
                    RegexpDisjunction(state);
                    if (state.Eat(0x29 /* ) */))
                    {
                        return true;
                    }

                    state.Raise("Unterminated group");
                }

                state.pos = start;
            }

            return false;
        }

        private bool RegexpEatCapturingGroup([NotNull] RegExpValidationState state)
        {
            if (state.Eat(0x28 /* ( */))
            {
                if (Options.EcmaVersion >= 9)
                {
                    RegexpGroupSpecifier(state);
                }
                else if (state.Current() == 0x3F /* ? */)
                {
                    state.Raise("Invalid group");
                }

                RegexpDisjunction(state);
                if (state.Eat(0x29 /* ) */))
                {
                    state.numCapturingParens += 1;
                    return true;
                }

                state.Raise("Unterminated group");
            }

            return false;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-annexB-ExtendedAtom
        private bool RegexpEatExtendedAtom([NotNull] RegExpValidationState state)
        {
            return state.Eat(0x2E /* . */) ||
                   RegexpEatReverseSolidusAtomEscape(state) ||
                   RegexpEatCharacterClass(state) ||
                   RegexpEatUncapturingGroup(state) ||
                   RegexpEatCapturingGroup(state) ||
                   RegexpEatInvalidBracedQuantifier(state) ||
                   RegexpEatExtendedPatternCharacter(state);
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-annexB-InvalidBracedQuantifier
        private static bool RegexpEatInvalidBracedQuantifier([NotNull] RegExpValidationState state)
        {
            if (RegexpEatBracedQuantifier(state, true))
            {
                state.Raise("Nothing to repeat");
            }

            return false;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-SyntaxCharacter
        private static bool RegexpEatSyntaxCharacter([NotNull] RegExpValidationState state)
        {
            var ch = state.Current();
            if (IsSyntaxCharacter(ch))
            {
                state.lastIntValue = ch;
                state.Advance();
                return true;
            }

            return false;
        }

        private static bool IsSyntaxCharacter(int ch)
        {
            return ch == 0x24 /* $ */ ||
                   ch >= 0x28 /* ( */ && ch <= 0x2B /* + */ ||
                   ch == 0x2E /* . */ ||
                   ch == 0x3F /* ? */ ||
                   ch >= 0x5B /* [ */ && ch <= 0x5E /* ^ */ ||
                   ch >= 0x7B /* { */ && ch <= 0x7D;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-PatternCharacter
        // But eat eager.
        private static bool RegexpEatPatternCharacters([NotNull] RegExpValidationState state)
        {
            var start = state.pos;
            int ch;
            while ((ch = state.Current()) != -1 && !IsSyntaxCharacter(ch))
            {
                state.Advance();
            }

            return state.pos != start;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-annexB-ExtendedPatternCharacter
        private static bool RegexpEatExtendedPatternCharacter([NotNull] RegExpValidationState state)
        {
            var ch = state.Current();
            if (
                ch != -1 &&
                ch != 0x24 /* $ */ &&
                !(ch >= 0x28 /* ( */ && ch <= 0x2B /* + */) &&
                ch != 0x2E /* . */ &&
                ch != 0x3F /* ? */ &&
                ch != 0x5B /* [ */ &&
                ch != 0x5E /* ^ */ &&
                ch != 0x7C /* | */
            )
            {
                state.Advance();
                return true;
            }

            return false;
        }

        // GroupSpecifier[U] ::
        //   [empty]
        //   `?` GroupName[?U]
        private static void RegexpGroupSpecifier([NotNull] RegExpValidationState state)
        {
            if (state.Eat(0x3F /* ? */))
            {
                if (RegexpEatGroupName(state))
                {
                    if (state.groupNames.IndexOf(state.lastStringValue) != -1)
                    {
                        state.Raise("Duplicate capture group name");
                    }

                    state.groupNames.Add(state.lastStringValue);
                    return;
                }

                state.Raise("Invalid group");
            }
        }

        // GroupName[U] ::
        //   `<` RegExpIdentifierName[?U] `>`
        // Note: this updates `state.lastStringValue` property with the eaten name.
        private static bool RegexpEatGroupName([NotNull] RegExpValidationState state)
        {
            state.lastStringValue = "";
            if (state.Eat(0x3C /* < */))
            {
                if (RegexpEatRegExpIdentifierName(state) && state.Eat(0x3E /* > */))
                {
                    return true;
                }

                state.Raise("Invalid capture group name");
            }

            return false;
        }

        // RegExpIdentifierName[U] ::
        //   RegExpIdentifierStart[?U]
        //   RegExpIdentifierName[?U] RegExpIdentifierPart[?U]
        // Note: this updates `state.lastStringValue` property with the eaten name.
        private static bool RegexpEatRegExpIdentifierName([NotNull] RegExpValidationState state)
        {
            state.lastStringValue = "";
            if (RegexpEatRegExpIdentifierStart(state))
            {
                state.lastStringValue += CodePointToString(state.lastIntValue);
                while (RegexpEatRegExpIdentifierPart(state))
                {
                    state.lastStringValue += CodePointToString(state.lastIntValue);
                }

                return true;
            }

            return false;
        }

        // RegExpIdentifierStart[U] ::
        //   UnicodeIDStart
        //   `$`
        //   `_`
        //   `\` RegExpUnicodeEscapeSequence[?U]
        private static bool RegexpEatRegExpIdentifierStart([NotNull] RegExpValidationState state)
        {
            var start = state.pos;
            var ch = state.Current();
            state.Advance();

            if (ch == 0x5C /* \ */ && RegexpEatRegExpUnicodeEscapeSequence(state))
            {
                ch = state.lastIntValue;
            }

            if (IsRegExpIdentifierStart(ch))
            {
                state.lastIntValue = ch;
                return true;
            }

            state.pos = start;
            return false;
        }

        private static bool IsRegExpIdentifierStart(int ch)
        {
            return Identifier.IsIdentifierStart(ch, true) || ch == 0x24 /* $ */ || ch == 0x5F; /* _ */
        }

        // RegExpIdentifierPart[U] ::
        //   UnicodeIDContinue
        //   `$`
        //   `_`
        //   `\` RegExpUnicodeEscapeSequence[?U]
        //   <ZWNJ>
        //   <ZWJ>
        private static bool RegexpEatRegExpIdentifierPart([NotNull] RegExpValidationState state)
        {
            var start = state.pos;
            var ch = state.Current();
            state.Advance();

            if (ch == 0x5C /* \ */ && RegexpEatRegExpUnicodeEscapeSequence(state))
            {
                ch = state.lastIntValue;
            }

            if (IsRegExpIdentifierPart(ch))
            {
                state.lastIntValue = ch;
                return true;
            }

            state.pos = start;
            return false;
        }

        private static bool IsRegExpIdentifierPart(int ch)
        {
            return Identifier.IsIdentifierChar(ch, true) || ch == 0x24 /* $ */ || ch == 0x5F /* _ */ || ch == 0x200C /* <ZWNJ> */ || ch == 0x200D; /* <ZWJ> */
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-annexB-AtomEscape
        private bool RegexpEatAtomEscape([NotNull] RegExpValidationState state)
        {
            if (RegexpEatBackReference(state) ||
                RegexpEatCharacterClassEscape(state) ||
                RegexpEatCharacterEscape(state) ||
                state.switchN && RegexpEatKGroupName(state))
            {
                return true;
            }

            if (state.switchU)
            {
                // Make the same message as V8.
                if (state.Current() == 0x63 /* c */)
                {
                    state.Raise("Invalid unicode escape");
                }

                state.Raise("Invalid escape");
            }

            return false;
        }

        private static bool RegexpEatBackReference([NotNull] RegExpValidationState state)
        {
            var start = state.pos;
            if (RegexpEatDecimalEscape(state))
            {
                var n = state.lastIntValue;
                if (state.switchU)
                {
                    // For SyntaxError in https://www.ecma-international.org/ecma-262/8.0/#sec-atomescape
                    if (n > state.maxBackReference)
                    {
                        state.maxBackReference = n;
                    }

                    return true;
                }

                if (n <= state.numCapturingParens)
                {
                    return true;
                }

                state.pos = start;
            }

            return false;
        }

        private static bool RegexpEatKGroupName([NotNull] RegExpValidationState state)
        {
            if (state.Eat(0x6B /* k */))
            {
                if (RegexpEatGroupName(state))
                {
                    state.backReferenceNames.Add(state.lastStringValue);
                    return true;
                }

                state.Raise("Invalid named reference");
            }

            return false;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-annexB-CharacterEscape
        private static bool RegexpEatCharacterEscape([NotNull] RegExpValidationState state)
        {
            return RegexpEatControlEscape(state) ||
                   RegexpEatCControlLetter(state) ||
                   RegexpEatZero(state) ||
                   RegexpEatHexEscapeSequence(state) ||
                   RegexpEatRegExpUnicodeEscapeSequence(state) ||
                   !state.switchU && RegexpEatLegacyOctalEscapeSequence(state) ||
                   RegexpEatIdentityEscape(state);
        }

        private static bool RegexpEatCControlLetter([NotNull] RegExpValidationState state)
        {
            var start = state.pos;
            if (state.Eat(0x63 /* c */))
            {
                if (RegexpEatControlLetter(state))
                {
                    return true;
                }

                state.pos = start;
            }

            return false;
        }

        private static bool RegexpEatZero([NotNull] RegExpValidationState state)
        {
            if (state.Current() == 0x30 /* 0 */ && !IsDecimalDigit(state.Lookahead()))
            {
                state.lastIntValue = 0;
                state.Advance();
                return true;
            }

            return false;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-ControlEscape
        private static bool RegexpEatControlEscape([NotNull] RegExpValidationState state)
        {
            var ch = state.Current();
            if (ch == 0x74 /* t */)
            {
                state.lastIntValue = 0x09; /* \t */
                state.Advance();
                return true;
            }

            if (ch == 0x6E /* n */)
            {
                state.lastIntValue = 0x0A; /* \n */
                state.Advance();
                return true;
            }

            if (ch == 0x76 /* v */)
            {
                state.lastIntValue = 0x0B; /* \v */
                state.Advance();
                return true;
            }

            if (ch == 0x66 /* f */)
            {
                state.lastIntValue = 0x0C; /* \f */
                state.Advance();
                return true;
            }

            if (ch == 0x72 /* r */)
            {
                state.lastIntValue = 0x0D; /* \r */
                state.Advance();
                return true;
            }

            return false;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-ControlLetter
        private static bool RegexpEatControlLetter([NotNull] RegExpValidationState state)
        {
            var ch = state.Current();
            if (IsControlLetter(ch))
            {
                state.lastIntValue = ch % 0x20;
                state.Advance();
                return true;
            }

            return false;
        }

        private static bool IsControlLetter(int ch)
        {
            return ch >= 0x41 /* A */ && ch <= 0x5A ||
                   ch >= 0x61 /* a */ && ch <= 0x7A;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-RegExpUnicodeEscapeSequence
        private static bool RegexpEatRegExpUnicodeEscapeSequence([NotNull] RegExpValidationState state)
        {
            var start = state.pos;

            if (state.Eat(0x75 /* u */))
            {
                if (RegexpEatFixedHexDigits(state, 4))
                {
                    var lead = state.lastIntValue;
                    if (state.switchU && lead >= 0xD800 && lead <= 0xDBFF)
                    {
                        var leadSurrogateEnd = state.pos;
                        if (state.Eat(0x5C /* \ */) && state.Eat(0x75 /* u */) && RegexpEatFixedHexDigits(state, 4))
                        {
                            var trail = state.lastIntValue;
                            if (trail >= 0xDC00 && trail <= 0xDFFF)
                            {
                                state.lastIntValue = (lead - 0xD800) * 0x400 + (trail - 0xDC00) + 0x10000;
                                return true;
                            }
                        }

                        state.pos = leadSurrogateEnd;
                        state.lastIntValue = lead;
                    }

                    return true;
                }

                if (
                    state.switchU &&
                    state.Eat(0x7B /* { */) &&
                    RegexpEatHexDigits(state) &&
                    state.Eat(0x7D /* } */) &&
                    IsValidUnicode(state.lastIntValue)
                )
                {
                    return true;
                }

                if (state.switchU)
                {
                    state.Raise("Invalid unicode escape");
                }

                state.pos = start;
            }

            return false;
        }

        private static bool IsValidUnicode(int ch)
        {
            return ch >= 0 && ch <= 0x10FFFF;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-annexB-IdentityEscape
        private static bool RegexpEatIdentityEscape([NotNull] RegExpValidationState state)
        {
            if (state.switchU)
            {
                if (RegexpEatSyntaxCharacter(state))
                {
                    return true;
                }

                if (state.Eat(0x2F /* / */))
                {
                    state.lastIntValue = 0x2F; /* / */
                    return true;
                }

                return false;
            }

            var ch = state.Current();
            if (ch != 0x63 /* c */ && (!state.switchN || ch != 0x6B /* k */))
            {
                state.lastIntValue = ch;
                state.Advance();
                return true;
            }

            return false;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-DecimalEscape
        private static bool RegexpEatDecimalEscape([NotNull] RegExpValidationState state)
        {
            state.lastIntValue = 0;
            var ch = state.Current();
            if (ch >= 0x31 /* 1 */ && ch <= 0x39 /* 9 */)
            {
                do
                {
                    state.lastIntValue = 10 * state.lastIntValue + (ch - 0x30 /* 0 */);
                    state.Advance();
                } while ((ch = state.Current()) >= 0x30 /* 0 */ && ch <= 0x39 /* 9 */);

                return true;
            }

            return false;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-CharacterClassEscape
        private bool RegexpEatCharacterClassEscape([NotNull] RegExpValidationState state)
        {
            var ch = state.Current();

            if (IsCharacterClassEscape(ch))
            {
                state.lastIntValue = -1;
                state.Advance();
                return true;
            }

            if (
                state.switchU &&
                Options.EcmaVersion >= 9 &&
                (ch == 0x50 /* P */ || ch == 0x70 /* p */)
            )
            {
                state.lastIntValue = -1;
                state.Advance();
                if (
                    state.Eat(0x7B /* { */) &&
                    RegexpEatUnicodePropertyValueExpression(state) &&
                    state.Eat(0x7D /* } */)
                )
                {
                    return true;
                }

                state.Raise("Invalid property name");
            }

            return false;
        }

        private static bool IsCharacterClassEscape(int ch)
        {
            return ch == 0x64 /* d */ ||
                   ch == 0x44 /* D */ ||
                   ch == 0x73 /* s */ ||
                   ch == 0x53 /* S */ ||
                   ch == 0x77 /* w */ ||
                   ch == 0x57;
        }

        // UnicodePropertyValueExpression ::
        //   UnicodePropertyName `=` UnicodePropertyValue
        //   LoneUnicodePropertyNameOrValue
        private static bool RegexpEatUnicodePropertyValueExpression([NotNull] RegExpValidationState state)
        {
            var start = state.pos;

            // UnicodePropertyName `=` UnicodePropertyValue
            if (RegexpEatUnicodePropertyName(state) && state.Eat(0x3D /* = */))
            {
                var name = state.lastStringValue;
                if (RegexpEatUnicodePropertyValue(state))
                {
                    var value = state.lastStringValue;
                    RegexpValidateUnicodePropertyNameAndValue(state, name, value);
                    return true;
                }
            }

            state.pos = start;

            // LoneUnicodePropertyNameOrValue
            if (RegexpEatLoneUnicodePropertyNameOrValue(state))
            {
                var nameOrValue = state.lastStringValue;
                RegexpValidateUnicodePropertyNameOrValue(state, nameOrValue);
                return true;
            }

            return false;
        }

        private static void RegexpValidateUnicodePropertyNameAndValue(RegExpValidationState state, [NotNull] string name, string value)
        {
            if (!UnicodePropertyData.Values.TryGetValue(name, out var values) || Array.IndexOf(values, value) == -1)
            {
                state.Raise("Invalid property name");
            }
        }

        private static void RegexpValidateUnicodePropertyNameOrValue([NotNull] RegExpValidationState state, string nameOrValue)
        {
            if (Array.IndexOf(UnicodePropertyData.Values["$LONE"], nameOrValue) == -1)
            {
                state.Raise("Invalid property name");
            }
        }

        // UnicodePropertyName ::
        //   UnicodePropertyNameCharacters
        private static bool RegexpEatUnicodePropertyName([NotNull] RegExpValidationState state)
        {
            int ch;
            state.lastStringValue = "";
            while (IsUnicodePropertyNameCharacter(ch = state.Current()))
            {
                state.lastStringValue += CodePointToString(ch);
                state.Advance();
            }

            return state.lastStringValue != "";
        }

        private static bool IsUnicodePropertyNameCharacter(int ch)
        {
            return IsControlLetter(ch) || ch == 0x5F; /* _ */
        }

        // UnicodePropertyValue ::
        //   UnicodePropertyValueCharacters
        private static bool RegexpEatUnicodePropertyValue([NotNull] RegExpValidationState state)
        {
            int ch;
            state.lastStringValue = "";
            while (IsUnicodePropertyValueCharacter(ch = state.Current()))
            {
                state.lastStringValue += CodePointToString(ch);
                state.Advance();
            }

            return state.lastStringValue != "";
        }

        private static bool IsUnicodePropertyValueCharacter(int ch)
        {
            return IsUnicodePropertyNameCharacter(ch) || IsDecimalDigit(ch);
        }

        // LoneUnicodePropertyNameOrValue ::
        //   UnicodePropertyValueCharacters
        private static bool RegexpEatLoneUnicodePropertyNameOrValue([NotNull] RegExpValidationState state)
        {
            return RegexpEatUnicodePropertyValue(state);
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-CharacterClass
        private bool RegexpEatCharacterClass([NotNull] RegExpValidationState state)
        {
            if (state.Eat(0x5B /* [ */))
            {
                state.Eat(0x5E /* ^ */);
                RegexpClassRanges(state);
                if (state.Eat(0x5D /* [ */))
                {
                    return true;
                }

                // Unreachable since it threw "unterminated regular expression" error before.
                state.Raise("Unterminated character class");
            }

            return false;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-ClassRanges
        // https://www.ecma-international.org/ecma-262/8.0/#prod-NonemptyClassRanges
        // https://www.ecma-international.org/ecma-262/8.0/#prod-NonemptyClassRangesNoDash
        private void RegexpClassRanges([NotNull] RegExpValidationState state)
        {
            while (RegexpEatClassAtom(state))
            {
                var left = state.lastIntValue;
                if (state.Eat(0x2D /* - */) && RegexpEatClassAtom(state))
                {
                    var right = state.lastIntValue;
                    if (state.switchU && (left == -1 || right == -1))
                    {
                        state.Raise("Invalid character class");
                    }

                    if (left != -1 && right != -1 && left > right)
                    {
                        state.Raise("Range out of order in character class");
                    }
                }
            }
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-ClassAtom
        // https://www.ecma-international.org/ecma-262/8.0/#prod-ClassAtomNoDash
        private bool RegexpEatClassAtom([NotNull] RegExpValidationState state)
        {
            var start = state.pos;

            int ch;
            if (state.Eat(0x5C /* \ */))
            {
                if (RegexpEatClassEscape(state))
                {
                    return true;
                }

                if (state.switchU)
                {
                    // Make the same message as V8.
                    ch = state.Current();
                    if (ch == 0x63 /* c */ || IsOctalDigit(ch))
                    {
                        state.Raise("Invalid class escape");
                    }

                    state.Raise("Invalid escape");
                }

                state.pos = start;
            }

            ch = state.Current();
            if (ch != 0x5D /* [ */)
            {
                state.lastIntValue = ch;
                state.Advance();
                return true;
            }

            return false;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-annexB-ClassEscape
        private bool RegexpEatClassEscape([NotNull] RegExpValidationState state)
        {
            var start = state.pos;

            if (state.Eat(0x62 /* b */))
            {
                state.lastIntValue = 0x08; /* <BS> */
                return true;
            }

            if (state.switchU && state.Eat(0x2D /* - */))
            {
                state.lastIntValue = 0x2D; /* - */
                return true;
            }

            if (!state.switchU && state.Eat(0x63 /* c */))
            {
                if (RegexpEatClassControlLetter(state))
                {
                    return true;
                }

                state.pos = start;
            }

            return RegexpEatCharacterClassEscape(state) ||
                   RegexpEatCharacterEscape(state);
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-annexB-ClassControlLetter
        private static bool RegexpEatClassControlLetter([NotNull] RegExpValidationState state)
        {
            var ch = state.Current();
            if (IsDecimalDigit(ch) || ch == 0x5F /* _ */)
            {
                state.lastIntValue = ch % 0x20;
                state.Advance();
                return true;
            }

            return false;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-HexEscapeSequence
        private static bool RegexpEatHexEscapeSequence([NotNull] RegExpValidationState state)
        {
            var start = state.pos;
            if (state.Eat(0x78 /* x */))
            {
                if (RegexpEatFixedHexDigits(state, 2))
                {
                    return true;
                }

                if (state.switchU)
                {
                    state.Raise("Invalid escape");
                }

                state.pos = start;
            }

            return false;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-DecimalDigits
        private static bool RegexpEatDecimalDigits([NotNull] RegExpValidationState state)
        {
            var start = state.pos;
            int ch;
            state.lastIntValue = 0;
            while (IsDecimalDigit(ch = state.Current()))
            {
                state.lastIntValue = 10 * state.lastIntValue + (ch - 0x30 /* 0 */);
                state.Advance();
            }

            return state.pos != start;
        }

        private static bool IsDecimalDigit(int ch)
        {
            return ch >= 0x30 /* 0 */ && ch <= 0x39; /* 9 */
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-HexDigits
        private static bool RegexpEatHexDigits([NotNull] RegExpValidationState state)
        {
            var start = state.pos;
            int ch;
            state.lastIntValue = 0;
            while (IsHexDigit(ch = state.Current()))
            {
                state.lastIntValue = 16 * state.lastIntValue + HexToInt(ch);
                state.Advance();
            }

            return state.pos != start;
        }

        private static bool IsHexDigit(int ch)
        {
            return ch >= 0x30 /* 0 */ && ch <= 0x39 ||
                   ch >= 0x41 /* A */ && ch <= 0x46 ||
                   ch >= 0x61 /* a */ && ch <= 0x66;
        }

        private static int HexToInt(int ch)
        {
            if (ch >= 0x41 /* A */ && ch <= 0x46 /* F */)
            {
                return 10 + (ch - 0x41 /* A */);
            }

            if (ch >= 0x61 /* a */ && ch <= 0x66 /* f */)
            {
                return 10 + (ch - 0x61 /* a */);
            }

            return ch - 0x30; /* 0 */
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-annexB-LegacyOctalEscapeSequence
        // Allows only 0-377(octal) i.e. 0-255(decimal).
        private static bool RegexpEatLegacyOctalEscapeSequence([NotNull] RegExpValidationState state)
        {
            if (RegexpEatOctalDigit(state))
            {
                var n1 = state.lastIntValue;
                if (RegexpEatOctalDigit(state))
                {
                    var n2 = state.lastIntValue;
                    if (n1 <= 3 && RegexpEatOctalDigit(state))
                    {
                        state.lastIntValue = n1 * 64 + n2 * 8 + state.lastIntValue;
                    }
                    else
                    {
                        state.lastIntValue = n1 * 8 + n2;
                    }
                }
                else
                {
                    state.lastIntValue = n1;
                }

                return true;
            }

            return false;
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-OctalDigit
        private static bool RegexpEatOctalDigit([NotNull] RegExpValidationState state)
        {
            var ch = state.Current();
            if (IsOctalDigit(ch))
            {
                state.lastIntValue = ch - 0x30; /* 0 */
                state.Advance();
                return true;
            }

            state.lastIntValue = 0;
            return false;
        }

        private static bool IsOctalDigit(int ch)
        {
            return ch >= 0x30 /* 0 */ && ch <= 0x37; /* 7 */
        }

        // https://www.ecma-international.org/ecma-262/8.0/#prod-Hex4Digits
        // https://www.ecma-international.org/ecma-262/8.0/#prod-HexDigit
        // And HexDigit HexDigit in https://www.ecma-international.org/ecma-262/8.0/#prod-HexEscapeSequence
        private static bool RegexpEatFixedHexDigits([NotNull] RegExpValidationState state, int length)
        {
            var start = state.pos;
            state.lastIntValue = 0;
            for (var i = 0; i < length; ++i)
            {
                var ch = state.Current();
                if (!IsHexDigit(ch))
                {
                    state.pos = start;
                    return false;
                }

                state.lastIntValue = 16 * state.lastIntValue + HexToInt(ch);
                state.Advance();
            }

            return true;
        }
    }
}