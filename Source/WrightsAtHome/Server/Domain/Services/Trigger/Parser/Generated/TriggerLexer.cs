//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Projects\Trigger\TriggerFramework\Domain\Services\Trigger\Parser\Grammar\Trigger.g4 by ANTLR 4.3

// Unreachable code detected

using System;

#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace WrightsAtHome.Server.Domain.Services.Trigger.Parser.Generated
{
    using Antlr4.Runtime;
    using Antlr4.Runtime.Atn;
    using Antlr4.Runtime.Misc;
    using DFA = Antlr4.Runtime.Dfa.DFA;

    [System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.3")]
    public partial class TriggerLexer : Lexer
    {
        public const int
            T__1 = 1, T__0 = 2, AFTER = 3, WHEN = 4, AT = 5, MUL = 6, DIV = 7, PLUS = 8, MINUS = 9,
            LESSTHAN = 10, LESSEQUAL = 11, EQUAL = 12, NOTEQUAL = 13, GREATERTHAN = 14, GREATEREQUAL = 15,
            AND = 16, OR = 17, NOT = 18, MINUTES = 19, HOURS = 20, TIMECONST = 21, STRING = 22,
            DECIMAL = 23, INT = 24, ID = 25, WS = 26, ErrorChar = 27;
        public static string[] modeNames = {
        "DEFAULT_MODE"
    };

        public static readonly string[] tokenNames = {
        "'\\u0000'", "'\\u0001'", "'\\u0002'", "'\\u0003'", "'\\u0004'", "'\\u0005'",
        "'\\u0006'", "'\\u0007'", "'\b'", "'\t'", "'\n'", "'\\u000B'", "'\f'",
        "'\r'", "'\\u000E'", "'\\u000F'", "'\\u0010'", "'\\u0011'", "'\\u0012'",
        "'\\u0013'", "'\\u0014'", "'\\u0015'", "'\\u0016'", "'\\u0017'", "'\\u0018'",
        "'\\u0019'", "'\\u001A'", "'\\u001B'"
    };
        public static readonly string[] ruleNames = {
        "T__1", "T__0", "AFTER", "WHEN", "AT", "MUL", "DIV", "PLUS", "MINUS",
        "LESSTHAN", "LESSEQUAL", "EQUAL", "NOTEQUAL", "GREATERTHAN", "GREATEREQUAL",
        "AND", "OR", "NOT", "MINUTES", "HOURS", "TIMECONST", "HOURPART", "MINSSECS",
        "AMPM", "STRING", "ESCAPED_QUOTE", "DECIMAL", "INT", "DIGIT", "ID", "LETTER",
        "WS", "ErrorChar"
    };


        public TriggerLexer(ICharStream input)
            : base(input)
        {
            _interp = new LexerATNSimulator(this, _ATN);
        }

        public override string GrammarFileName { get { return "Trigger.g4"; } }
        [Obsolete("Call this one!")]
        public override string[] TokenNames { get { return tokenNames; } }

        public override string[] RuleNames { get { return ruleNames; } }

        public override string[] ModeNames { get { return modeNames; } }

        public override string SerializedAtn { get { return _serializedATN; } }

        public static readonly string _serializedATN =
            "\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\x1D\xDA\b\x1\x4" +
            "\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b" +
            "\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4" +
            "\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15" +
            "\t\x15\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A" +
            "\x4\x1B\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 " +
            "\t \x4!\t!\x4\"\t\"\x3\x2\x3\x2\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\x3\x4\x3" +
            "\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x3" +
            "\b\x3\b\x3\t\x3\t\x3\n\x3\n\x3\v\x3\v\x3\f\x3\f\x3\f\x3\r\x3\r\x3\xE\x3" +
            "\xE\x3\xE\x3\xE\x5\xEk\n\xE\x3\xF\x3\xF\x3\x10\x3\x10\x3\x10\x3\x11\x3" +
            "\x11\x3\x11\x3\x11\x3\x12\x3\x12\x3\x12\x3\x13\x3\x13\x3\x13\x3\x13\x3" +
            "\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x5\x14\x85\n\x14" +
            "\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x5\x15\x8D\n\x15\x3\x16\x3" +
            "\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x5\x16\x98\n\x16" +
            "\x3\x16\x5\x16\x9B\n\x16\x3\x16\x3\x16\x5\x16\x9F\n\x16\x3\x17\x3\x17" +
            "\x3\x17\x5\x17\xA4\n\x17\x3\x18\x3\x18\x3\x18\x3\x18\x3\x19\x3\x19\x3" +
            "\x19\x3\x19\x5\x19\xAE\n\x19\x3\x1A\x3\x1A\x3\x1A\a\x1A\xB3\n\x1A\f\x1A" +
            "\xE\x1A\xB6\v\x1A\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3" +
            "\x1C\x3\x1C\x3\x1D\x6\x1D\xC2\n\x1D\r\x1D\xE\x1D\xC3\x3\x1E\x3\x1E\x3" +
            "\x1F\x3\x1F\x3\x1F\a\x1F\xCB\n\x1F\f\x1F\xE\x1F\xCE\v\x1F\x3 \x3 \x3!" +
            "\x6!\xD3\n!\r!\xE!\xD4\x3!\x3!\x3\"\x3\"\x3\xB4\x2\x2#\x3\x2\x3\x5\x2" +
            "\x4\a\x2\x5\t\x2\x6\v\x2\a\r\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17" +
            "\x2\r\x19\x2\xE\x1B\x2\xF\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14" +
            "\'\x2\x15)\x2\x16+\x2\x17-\x2\x2/\x2\x2\x31\x2\x2\x33\x2\x18\x35\x2\x2" +
            "\x37\x2\x19\x39\x2\x1A;\x2\x2=\x2\x1B?\x2\x2\x41\x2\x1C\x43\x2\x1D\x3" +
            "\x2\t\x3\x2\x32\x34\x3\x2\x33;\x3\x2\x32\x37\x3\x2\x32;\x4\x2\f\f\xF\xF" +
            "\x4\x2\x43\\\x63|\x5\x2\v\f\xF\xF\"\"\xE2\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2" +
            "\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2" +
            "\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3" +
            "\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2\x2" +
            "\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2\x2" +
            "%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2\x33" +
            "\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2\x2\x2\x2=\x3\x2\x2\x2\x2" +
            "\x41\x3\x2\x2\x2\x2\x43\x3\x2\x2\x2\x3\x45\x3\x2\x2\x2\x5G\x3\x2\x2\x2" +
            "\aI\x3\x2\x2\x2\tO\x3\x2\x2\x2\vT\x3\x2\x2\x2\rW\x3\x2\x2\x2\xFY\x3\x2" +
            "\x2\x2\x11[\x3\x2\x2\x2\x13]\x3\x2\x2\x2\x15_\x3\x2\x2\x2\x17\x61\x3\x2" +
            "\x2\x2\x19\x64\x3\x2\x2\x2\x1Bj\x3\x2\x2\x2\x1Dl\x3\x2\x2\x2\x1Fn\x3\x2" +
            "\x2\x2!q\x3\x2\x2\x2#u\x3\x2\x2\x2%x\x3\x2\x2\x2\'|\x3\x2\x2\x2)\x86\x3" +
            "\x2\x2\x2+\x9E\x3\x2\x2\x2-\xA3\x3\x2\x2\x2/\xA5\x3\x2\x2\x2\x31\xAD\x3" +
            "\x2\x2\x2\x33\xAF\x3\x2\x2\x2\x35\xB9\x3\x2\x2\x2\x37\xBC\x3\x2\x2\x2" +
            "\x39\xC1\x3\x2\x2\x2;\xC5\x3\x2\x2\x2=\xC7\x3\x2\x2\x2?\xCF\x3\x2\x2\x2" +
            "\x41\xD2\x3\x2\x2\x2\x43\xD8\x3\x2\x2\x2\x45\x46\a*\x2\x2\x46\x4\x3\x2" +
            "\x2\x2GH\a+\x2\x2H\x6\x3\x2\x2\x2IJ\a\x63\x2\x2JK\ah\x2\x2KL\av\x2\x2" +
            "LM\ag\x2\x2MN\at\x2\x2N\b\x3\x2\x2\x2OP\ay\x2\x2PQ\aj\x2\x2QR\ag\x2\x2" +
            "RS\ap\x2\x2S\n\x3\x2\x2\x2TU\a\x63\x2\x2UV\av\x2\x2V\f\x3\x2\x2\x2WX\a" +
            ",\x2\x2X\xE\x3\x2\x2\x2YZ\a\x31\x2\x2Z\x10\x3\x2\x2\x2[\\\a-\x2\x2\\\x12" +
            "\x3\x2\x2\x2]^\a/\x2\x2^\x14\x3\x2\x2\x2_`\a>\x2\x2`\x16\x3\x2\x2\x2\x61" +
            "\x62\a>\x2\x2\x62\x63\a?\x2\x2\x63\x18\x3\x2\x2\x2\x64\x65\a?\x2\x2\x65" +
            "\x1A\x3\x2\x2\x2\x66g\a#\x2\x2gk\a?\x2\x2hi\a>\x2\x2ik\a@\x2\x2j\x66\x3" +
            "\x2\x2\x2jh\x3\x2\x2\x2k\x1C\x3\x2\x2\x2lm\a@\x2\x2m\x1E\x3\x2\x2\x2n" +
            "o\a@\x2\x2op\a?\x2\x2p \x3\x2\x2\x2qr\a\x63\x2\x2rs\ap\x2\x2st\a\x66\x2" +
            "\x2t\"\x3\x2\x2\x2uv\aq\x2\x2vw\at\x2\x2w$\x3\x2\x2\x2xy\ap\x2\x2yz\a" +
            "q\x2\x2z{\av\x2\x2{&\x3\x2\x2\x2|}\ao\x2\x2}~\ak\x2\x2~\x7F\ap\x2\x2\x7F" +
            "\x80\aw\x2\x2\x80\x81\av\x2\x2\x81\x82\ag\x2\x2\x82\x84\x3\x2\x2\x2\x83" +
            "\x85\au\x2\x2\x84\x83\x3\x2\x2\x2\x84\x85\x3\x2\x2\x2\x85(\x3\x2\x2\x2" +
            "\x86\x87\aj\x2\x2\x87\x88\aq\x2\x2\x88\x89\aw\x2\x2\x89\x8A\at\x2\x2\x8A" +
            "\x8C\x3\x2\x2\x2\x8B\x8D\au\x2\x2\x8C\x8B\x3\x2\x2\x2\x8C\x8D\x3\x2\x2" +
            "\x2\x8D*\x3\x2\x2\x2\x8E\x8F\x5-\x17\x2\x8F\x90\x5/\x18\x2\x90\x9F\x3" +
            "\x2\x2\x2\x91\x92\x5-\x17\x2\x92\x93\x5/\x18\x2\x93\x94\x5/\x18\x2\x94" +
            "\x9F\x3\x2\x2\x2\x95\x97\x5-\x17\x2\x96\x98\x5/\x18\x2\x97\x96\x3\x2\x2" +
            "\x2\x97\x98\x3\x2\x2\x2\x98\x9A\x3\x2\x2\x2\x99\x9B\x5/\x18\x2\x9A\x99" +
            "\x3\x2\x2\x2\x9A\x9B\x3\x2\x2\x2\x9B\x9C\x3\x2\x2\x2\x9C\x9D\x5\x31\x19" +
            "\x2\x9D\x9F\x3\x2\x2\x2\x9E\x8E\x3\x2\x2\x2\x9E\x91\x3\x2\x2\x2\x9E\x95" +
            "\x3\x2\x2\x2\x9F,\x3\x2\x2\x2\xA0\xA1\a\x33\x2\x2\xA1\xA4\t\x2\x2\x2\xA2" +
            "\xA4\t\x3\x2\x2\xA3\xA0\x3\x2\x2\x2\xA3\xA2\x3\x2\x2\x2\xA4.\x3\x2\x2" +
            "\x2\xA5\xA6\a<\x2\x2\xA6\xA7\t\x4\x2\x2\xA7\xA8\t\x5\x2\x2\xA8\x30\x3" +
            "\x2\x2\x2\xA9\xAA\a\x63\x2\x2\xAA\xAE\ao\x2\x2\xAB\xAC\ar\x2\x2\xAC\xAE" +
            "\ao\x2\x2\xAD\xA9\x3\x2\x2\x2\xAD\xAB\x3\x2\x2\x2\xAE\x32\x3\x2\x2\x2" +
            "\xAF\xB4\a$\x2\x2\xB0\xB3\x5\x35\x1B\x2\xB1\xB3\n\x6\x2\x2\xB2\xB0\x3" +
            "\x2\x2\x2\xB2\xB1\x3\x2\x2\x2\xB3\xB6\x3\x2\x2\x2\xB4\xB5\x3\x2\x2\x2" +
            "\xB4\xB2\x3\x2\x2\x2\xB5\xB7\x3\x2\x2\x2\xB6\xB4\x3\x2\x2\x2\xB7\xB8\a" +
            "$\x2\x2\xB8\x34\x3\x2\x2\x2\xB9\xBA\a^\x2\x2\xBA\xBB\a$\x2\x2\xBB\x36" +
            "\x3\x2\x2\x2\xBC\xBD\x5\x39\x1D\x2\xBD\xBE\a\x30\x2\x2\xBE\xBF\x5\x39" +
            "\x1D\x2\xBF\x38\x3\x2\x2\x2\xC0\xC2\x5;\x1E\x2\xC1\xC0\x3\x2\x2\x2\xC2" +
            "\xC3\x3\x2\x2\x2\xC3\xC1\x3\x2\x2\x2\xC3\xC4\x3\x2\x2\x2\xC4:\x3\x2\x2" +
            "\x2\xC5\xC6\t\x5\x2\x2\xC6<\x3\x2\x2\x2\xC7\xCC\x5? \x2\xC8\xCB\x5? \x2" +
            "\xC9\xCB\x5;\x1E\x2\xCA\xC8\x3\x2\x2\x2\xCA\xC9\x3\x2\x2\x2\xCB\xCE\x3" +
            "\x2\x2\x2\xCC\xCA\x3\x2\x2\x2\xCC\xCD\x3\x2\x2\x2\xCD>\x3\x2\x2\x2\xCE" +
            "\xCC\x3\x2\x2\x2\xCF\xD0\t\a\x2\x2\xD0@\x3\x2\x2\x2\xD1\xD3\t\b\x2\x2" +
            "\xD2\xD1\x3\x2\x2\x2\xD3\xD4\x3\x2\x2\x2\xD4\xD2\x3\x2\x2\x2\xD4\xD5\x3" +
            "\x2\x2\x2\xD5\xD6\x3\x2\x2\x2\xD6\xD7\b!\x2\x2\xD7\x42\x3\x2\x2\x2\xD8" +
            "\xD9\v\x2\x2\x2\xD9\x44\x3\x2\x2\x2\x11\x2j\x84\x8C\x97\x9A\x9E\xA3\xAD" +
            "\xB2\xB4\xC3\xCA\xCC\xD4\x3\b\x2\x2";
        public static readonly ATN _ATN =
            new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
    }
} // namespace WrightsAtHome.Server.Domain.Services.Trigger.Parser.Generated
