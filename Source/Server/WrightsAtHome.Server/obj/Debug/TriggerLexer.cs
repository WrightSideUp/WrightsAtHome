//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Projects\WrightsAtHome\Source\Server\WrightsAtHome.Server\Domain\Services\Trigger\Parser\Grammar\Trigger.g4 by ANTLR 4.3

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace WrightsAtHome.Server.Domain.Services.Trigger.Parser.Grammar {
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.3")]
[System.CLSCompliant(false)]
public partial class TriggerLexer : Lexer {
	public const int
		T__1=1, T__0=2, AFTER=3, WHEN=4, AT=5, MUL=6, DIV=7, PLUS=8, MINUS=9, 
		LESSTHAN=10, LESSEQUAL=11, EQUAL=12, NOTEQUAL=13, GREATERTHAN=14, GREATEREQUAL=15, 
		AND=16, OR=17, NOT=18, MINUTES=19, HOURS=20, TIMECONST=21, STRING=22, 
		DECIMAL=23, INT=24, CURRENTTIMEFUNC=25, ID=26, WS=27, ErrorChar=28;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] tokenNames = {
		"'\\u0000'", "'\\u0001'", "'\\u0002'", "'\\u0003'", "'\\u0004'", "'\\u0005'", 
		"'\\u0006'", "'\\u0007'", "'\b'", "'\t'", "'\n'", "'\\u000B'", "'\f'", 
		"'\r'", "'\\u000E'", "'\\u000F'", "'\\u0010'", "'\\u0011'", "'\\u0012'", 
		"'\\u0013'", "'\\u0014'", "'\\u0015'", "'\\u0016'", "'\\u0017'", "'\\u0018'", 
		"'\\u0019'", "'\\u001A'", "'\\u001B'", "'\\u001C'"
	};
	public static readonly string[] ruleNames = {
		"T__1", "T__0", "AFTER", "WHEN", "AT", "MUL", "DIV", "PLUS", "MINUS", 
		"LESSTHAN", "LESSEQUAL", "EQUAL", "NOTEQUAL", "GREATERTHAN", "GREATEREQUAL", 
		"AND", "OR", "NOT", "MINUTES", "HOURS", "TIMECONST", "HOURPART", "MINSSECS", 
		"AMPM", "STRING", "ESCAPED_QUOTE", "DECIMAL", "INT", "DIGIT", "CURRENTTIMEFUNC", 
		"ID", "LETTER", "WS", "ErrorChar"
	};


	public TriggerLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	public override string GrammarFileName { get { return "Trigger.g4"; } }

	public override string[] TokenNames { get { return tokenNames; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\x1E\xED\b\x1\x4"+
		"\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b"+
		"\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4"+
		"\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15"+
		"\t\x15\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A"+
		"\x4\x1B\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 "+
		"\t \x4!\t!\x4\"\t\"\x4#\t#\x3\x2\x3\x2\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\x3"+
		"\x4\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\x6\x3\a\x3"+
		"\a\x3\b\x3\b\x3\t\x3\t\x3\n\x3\n\x3\v\x3\v\x3\f\x3\f\x3\f\x3\r\x3\r\x3"+
		"\xE\x3\xE\x3\xE\x3\xF\x3\xF\x3\x10\x3\x10\x3\x10\x3\x11\x3\x11\x3\x11"+
		"\x3\x11\x3\x12\x3\x12\x3\x12\x3\x13\x3\x13\x3\x13\x3\x13\x3\x14\x3\x14"+
		"\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x5\x14\x84\n\x14\x3\x15\x3"+
		"\x15\x3\x15\x3\x15\x3\x15\x3\x15\x5\x15\x8C\n\x15\x3\x16\x3\x16\x3\x16"+
		"\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x5\x16\x97\n\x16\x3\x16\x5"+
		"\x16\x9A\n\x16\x3\x16\x3\x16\x5\x16\x9E\n\x16\x3\x17\x3\x17\x3\x17\x5"+
		"\x17\xA3\n\x17\x3\x18\x3\x18\x3\x18\x3\x18\x3\x19\x3\x19\x3\x19\x3\x19"+
		"\x5\x19\xAD\n\x19\x3\x1A\x3\x1A\x3\x1A\a\x1A\xB2\n\x1A\f\x1A\xE\x1A\xB5"+
		"\v\x1A\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3\x1C\x3\x1C"+
		"\x3\x1D\x6\x1D\xC1\n\x1D\r\x1D\xE\x1D\xC2\x3\x1E\x3\x1E\x3\x1F\x3\x1F"+
		"\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F"+
		"\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x5\x1F\xD9\n\x1F\x3 \x3 \x3"+
		" \a \xDE\n \f \xE \xE1\v \x3!\x3!\x3\"\x6\"\xE6\n\"\r\"\xE\"\xE7\x3\""+
		"\x3\"\x3#\x3#\x3\xB3\x2\x2$\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2\x6\v\x2\a\r"+
		"\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE\x1B\x2\xF"+
		"\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2\x16+\x2\x17"+
		"-\x2\x2/\x2\x2\x31\x2\x2\x33\x2\x18\x35\x2\x2\x37\x2\x19\x39\x2\x1A;\x2"+
		"\x2=\x2\x1B?\x2\x1C\x41\x2\x2\x43\x2\x1D\x45\x2\x1E\x3\x2\t\x3\x2\x32"+
		"\x34\x3\x2\x33;\x3\x2\x32\x37\x3\x2\x32;\x4\x2\f\f\xF\xF\x4\x2\x43\\\x63"+
		"|\x5\x2\v\f\xF\xF\"\"\xF6\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3"+
		"\x2\x2\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3"+
		"\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2"+
		"\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2"+
		"\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2"+
		"\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2\x33\x3\x2\x2\x2\x2"+
		"\x37\x3\x2\x2\x2\x2\x39\x3\x2\x2\x2\x2=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2"+
		"\x43\x3\x2\x2\x2\x2\x45\x3\x2\x2\x2\x3G\x3\x2\x2\x2\x5I\x3\x2\x2\x2\a"+
		"K\x3\x2\x2\x2\tQ\x3\x2\x2\x2\vV\x3\x2\x2\x2\rY\x3\x2\x2\x2\xF[\x3\x2\x2"+
		"\x2\x11]\x3\x2\x2\x2\x13_\x3\x2\x2\x2\x15\x61\x3\x2\x2\x2\x17\x63\x3\x2"+
		"\x2\x2\x19\x66\x3\x2\x2\x2\x1Bh\x3\x2\x2\x2\x1Dk\x3\x2\x2\x2\x1Fm\x3\x2"+
		"\x2\x2!p\x3\x2\x2\x2#t\x3\x2\x2\x2%w\x3\x2\x2\x2\'{\x3\x2\x2\x2)\x85\x3"+
		"\x2\x2\x2+\x9D\x3\x2\x2\x2-\xA2\x3\x2\x2\x2/\xA4\x3\x2\x2\x2\x31\xAC\x3"+
		"\x2\x2\x2\x33\xAE\x3\x2\x2\x2\x35\xB8\x3\x2\x2\x2\x37\xBB\x3\x2\x2\x2"+
		"\x39\xC0\x3\x2\x2\x2;\xC4\x3\x2\x2\x2=\xD8\x3\x2\x2\x2?\xDA\x3\x2\x2\x2"+
		"\x41\xE2\x3\x2\x2\x2\x43\xE5\x3\x2\x2\x2\x45\xEB\x3\x2\x2\x2GH\a*\x2\x2"+
		"H\x4\x3\x2\x2\x2IJ\a+\x2\x2J\x6\x3\x2\x2\x2KL\a\x63\x2\x2LM\ah\x2\x2M"+
		"N\av\x2\x2NO\ag\x2\x2OP\at\x2\x2P\b\x3\x2\x2\x2QR\ay\x2\x2RS\aj\x2\x2"+
		"ST\ag\x2\x2TU\ap\x2\x2U\n\x3\x2\x2\x2VW\a\x63\x2\x2WX\av\x2\x2X\f\x3\x2"+
		"\x2\x2YZ\a,\x2\x2Z\xE\x3\x2\x2\x2[\\\a\x31\x2\x2\\\x10\x3\x2\x2\x2]^\a"+
		"-\x2\x2^\x12\x3\x2\x2\x2_`\a/\x2\x2`\x14\x3\x2\x2\x2\x61\x62\a>\x2\x2"+
		"\x62\x16\x3\x2\x2\x2\x63\x64\a>\x2\x2\x64\x65\a?\x2\x2\x65\x18\x3\x2\x2"+
		"\x2\x66g\a?\x2\x2g\x1A\x3\x2\x2\x2hi\a>\x2\x2ij\a@\x2\x2j\x1C\x3\x2\x2"+
		"\x2kl\a@\x2\x2l\x1E\x3\x2\x2\x2mn\a@\x2\x2no\a?\x2\x2o \x3\x2\x2\x2pq"+
		"\a\x63\x2\x2qr\ap\x2\x2rs\a\x66\x2\x2s\"\x3\x2\x2\x2tu\aq\x2\x2uv\at\x2"+
		"\x2v$\x3\x2\x2\x2wx\ap\x2\x2xy\aq\x2\x2yz\av\x2\x2z&\x3\x2\x2\x2{|\ao"+
		"\x2\x2|}\ak\x2\x2}~\ap\x2\x2~\x7F\aw\x2\x2\x7F\x80\av\x2\x2\x80\x81\a"+
		"g\x2\x2\x81\x83\x3\x2\x2\x2\x82\x84\au\x2\x2\x83\x82\x3\x2\x2\x2\x83\x84"+
		"\x3\x2\x2\x2\x84(\x3\x2\x2\x2\x85\x86\aj\x2\x2\x86\x87\aq\x2\x2\x87\x88"+
		"\aw\x2\x2\x88\x89\at\x2\x2\x89\x8B\x3\x2\x2\x2\x8A\x8C\au\x2\x2\x8B\x8A"+
		"\x3\x2\x2\x2\x8B\x8C\x3\x2\x2\x2\x8C*\x3\x2\x2\x2\x8D\x8E\x5-\x17\x2\x8E"+
		"\x8F\x5/\x18\x2\x8F\x9E\x3\x2\x2\x2\x90\x91\x5-\x17\x2\x91\x92\x5/\x18"+
		"\x2\x92\x93\x5/\x18\x2\x93\x9E\x3\x2\x2\x2\x94\x96\x5-\x17\x2\x95\x97"+
		"\x5/\x18\x2\x96\x95\x3\x2\x2\x2\x96\x97\x3\x2\x2\x2\x97\x99\x3\x2\x2\x2"+
		"\x98\x9A\x5/\x18\x2\x99\x98\x3\x2\x2\x2\x99\x9A\x3\x2\x2\x2\x9A\x9B\x3"+
		"\x2\x2\x2\x9B\x9C\x5\x31\x19\x2\x9C\x9E\x3\x2\x2\x2\x9D\x8D\x3\x2\x2\x2"+
		"\x9D\x90\x3\x2\x2\x2\x9D\x94\x3\x2\x2\x2\x9E,\x3\x2\x2\x2\x9F\xA0\a\x33"+
		"\x2\x2\xA0\xA3\t\x2\x2\x2\xA1\xA3\t\x3\x2\x2\xA2\x9F\x3\x2\x2\x2\xA2\xA1"+
		"\x3\x2\x2\x2\xA3.\x3\x2\x2\x2\xA4\xA5\a<\x2\x2\xA5\xA6\t\x4\x2\x2\xA6"+
		"\xA7\t\x5\x2\x2\xA7\x30\x3\x2\x2\x2\xA8\xA9\a\x63\x2\x2\xA9\xAD\ao\x2"+
		"\x2\xAA\xAB\ar\x2\x2\xAB\xAD\ao\x2\x2\xAC\xA8\x3\x2\x2\x2\xAC\xAA\x3\x2"+
		"\x2\x2\xAD\x32\x3\x2\x2\x2\xAE\xB3\a$\x2\x2\xAF\xB2\x5\x35\x1B\x2\xB0"+
		"\xB2\n\x6\x2\x2\xB1\xAF\x3\x2\x2\x2\xB1\xB0\x3\x2\x2\x2\xB2\xB5\x3\x2"+
		"\x2\x2\xB3\xB4\x3\x2\x2\x2\xB3\xB1\x3\x2\x2\x2\xB4\xB6\x3\x2\x2\x2\xB5"+
		"\xB3\x3\x2\x2\x2\xB6\xB7\a$\x2\x2\xB7\x34\x3\x2\x2\x2\xB8\xB9\a^\x2\x2"+
		"\xB9\xBA\a$\x2\x2\xBA\x36\x3\x2\x2\x2\xBB\xBC\x5\x39\x1D\x2\xBC\xBD\a"+
		"\x30\x2\x2\xBD\xBE\x5\x39\x1D\x2\xBE\x38\x3\x2\x2\x2\xBF\xC1\x5;\x1E\x2"+
		"\xC0\xBF\x3\x2\x2\x2\xC1\xC2\x3\x2\x2\x2\xC2\xC0\x3\x2\x2\x2\xC2\xC3\x3"+
		"\x2\x2\x2\xC3:\x3\x2\x2\x2\xC4\xC5\t\x5\x2\x2\xC5<\x3\x2\x2\x2\xC6\xC7"+
		"\ap\x2\x2\xC7\xC8\aq\x2\x2\xC8\xD9\ay\x2\x2\xC9\xCA\a\x65\x2\x2\xCA\xCB"+
		"\aw\x2\x2\xCB\xCC\at\x2\x2\xCC\xCD\at\x2\x2\xCD\xCE\ag\x2\x2\xCE\xCF\a"+
		"p\x2\x2\xCF\xD0\av\x2\x2\xD0\xD1\av\x2\x2\xD1\xD2\ak\x2\x2\xD2\xD3\ao"+
		"\x2\x2\xD3\xD9\ag\x2\x2\xD4\xD5\av\x2\x2\xD5\xD6\ak\x2\x2\xD6\xD7\ao\x2"+
		"\x2\xD7\xD9\ag\x2\x2\xD8\xC6\x3\x2\x2\x2\xD8\xC9\x3\x2\x2\x2\xD8\xD4\x3"+
		"\x2\x2\x2\xD9>\x3\x2\x2\x2\xDA\xDF\x5\x41!\x2\xDB\xDE\x5\x41!\x2\xDC\xDE"+
		"\x5;\x1E\x2\xDD\xDB\x3\x2\x2\x2\xDD\xDC\x3\x2\x2\x2\xDE\xE1\x3\x2\x2\x2"+
		"\xDF\xDD\x3\x2\x2\x2\xDF\xE0\x3\x2\x2\x2\xE0@\x3\x2\x2\x2\xE1\xDF\x3\x2"+
		"\x2\x2\xE2\xE3\t\a\x2\x2\xE3\x42\x3\x2\x2\x2\xE4\xE6\t\b\x2\x2\xE5\xE4"+
		"\x3\x2\x2\x2\xE6\xE7\x3\x2\x2\x2\xE7\xE5\x3\x2\x2\x2\xE7\xE8\x3\x2\x2"+
		"\x2\xE8\xE9\x3\x2\x2\x2\xE9\xEA\b\"\x2\x2\xEA\x44\x3\x2\x2\x2\xEB\xEC"+
		"\v\x2\x2\x2\xEC\x46\x3\x2\x2\x2\x11\x2\x83\x8B\x96\x99\x9D\xA2\xAC\xB1"+
		"\xB3\xC2\xD8\xDD\xDF\xE7\x3\b\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace WrightsAtHome.Server.Domain.Services.Trigger.Parser.Grammar
