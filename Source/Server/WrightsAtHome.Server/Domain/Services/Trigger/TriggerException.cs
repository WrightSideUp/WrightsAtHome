using System;
using Antlr4.Runtime;

namespace WrightsAtHome.Server.Domain.Services.Trigger
{
    public class TriggerException : Exception
    {
        public IToken OffendingSymbol { get; private set; }

        public int Line { get; private set; }

        public int CharPositionInLine { get; private set; }

        public RecognitionException RecognitionException { get; private set; }

        public TriggerException(IToken offendingSymbol, int line, int charPositionInLine, string msg,
            RecognitionException e) : base(msg)
        {
            OffendingSymbol = offendingSymbol;
            Line = line;
            CharPositionInLine = charPositionInLine;
            RecognitionException = e;
        }
    }
}
