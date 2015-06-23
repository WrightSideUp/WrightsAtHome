using System;
using Antlr4.Runtime;

namespace WrightsAtHome.Server.Domain.Services.Trigger
{
    public class TriggerException : Exception
    {
        public IToken OffendingSymbol { get; private set; }

        public int StartIndex { get; private set; }

        public int Length { get; private set; }

        public RecognitionException RecognitionException { get; private set; }

        public TriggerException(IToken offendingSymbol, int startIndex, int length, string msg,
            RecognitionException e) : base(msg)
        {
            OffendingSymbol = offendingSymbol;
            StartIndex = startIndex;
            Length = length;
            RecognitionException = e;
        }
    }
}
