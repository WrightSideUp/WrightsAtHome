using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using WrightsAtHome.Server.Domain.Services.Trigger.Parser.Generated;

namespace WrightsAtHome.Server.Domain.Services.Trigger.Parser
{
    public class TriggerParseHelper
    {
        private readonly ITriggerParseListener listener;
        private readonly BaseErrorListener errorListener;

        public TriggerParseHelper(ITriggerParseListener listener, BaseErrorListener errorListener)
        {
            this.listener = listener;
            this.errorListener = errorListener;
        }

        public void ParseTrigger(string triggerText)
        {
            var parser = SetupParser(triggerText);
            listener.Initialize();

            try
            {
                var tree = parser.trigger();

                var walker = new ParseTreeWalker();

                walker.Walk(listener, tree);
            }
            catch (TriggerException ex)
            {
                // The parser works on an all-lowercase version of the trigger.  Error messages should 
                // contain invalid tokens in the original form.
                // Get the original token from the non-toLower()ed string and replace it in the 
                // exception message
                string originalToken = triggerText.Substring(ex.OffendingSymbol.StartIndex,
                    ex.OffendingSymbol.StopIndex - ex.OffendingSymbol.StartIndex + 1);

                throw new TriggerException(ex.OffendingSymbol, ex.StartIndex, ex.Length,
                    ex.Message.Replace(ex.OffendingSymbol.Text, originalToken), ex.RecognitionException);
            }
            
        }

        private TriggerParser SetupParser(string input)
        {
            var stream = new AntlrInputStream(input.ToLower());

            var lexer = new TriggerLexer(stream);

            var tokens = new CommonTokenStream(lexer);

            var parser = new TriggerParser(tokens);
            parser.RemoveErrorListeners();
            parser.AddErrorListener(errorListener);

            return parser;
        }
    }
}