using Antlr4.Runtime;

namespace WrightsAtHome.Server.Domain.Services.Trigger.Compiler
{
    class TriggerCompilerErrors : BaseErrorListener
    {
        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            base.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e);

            throw new TriggerException(offendingSymbol, offendingSymbol.StartIndex, offendingSymbol.Text.Length, msg, e);
        }
    }
}
