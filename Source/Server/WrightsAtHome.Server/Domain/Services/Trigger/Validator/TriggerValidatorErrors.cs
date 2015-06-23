using System.Collections.Generic;
using Antlr4.Runtime;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.Domain.Services.Trigger.Validator
{
    public class TriggerValidatorErrors : BaseErrorListener
    {
        private readonly string originalText;

        public List<TriggerValidationError> Errors { get; private set; }
        
        public TriggerValidatorErrors(string originalText)
        {
            this.originalText = originalText;
            Errors = new List<TriggerValidationError>();
        }

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg,
            RecognitionException e)
        {
            string originalToken = originalText.Substring(offendingSymbol.StartIndex,
                offendingSymbol.StopIndex - offendingSymbol.StartIndex + 1);
            
            Errors.Add(new TriggerValidationError
            {
                BadToken = originalToken,
                StartIndex = offendingSymbol.StartIndex,
                Length = originalToken.Length,
                ErrorMessage = GetErrorMessage(msg, offendingSymbol, originalToken, e)
            });
                        
            base.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e);
        }

        private string GetErrorMessage(string msg, IToken offendingSymbol, string originalToken, RecognitionException e)
        {
            if (e is NoViableAltException)
            {
                return string.Format("'{0}' is not a valid input", originalToken);
            }
            else
            {
                return msg.Replace(offendingSymbol.Text, originalToken);
            }
        }
    }
}