using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Trigger.Parser;
using WrightsAtHome.Server.Domain.Services.Trigger.Parser.Generated;

namespace WrightsAtHome.Server.Domain.Services.Trigger
{
    public interface ITriggerCompiler
    {
        TriggerInfo CompileTrigger(DeviceTrigger trigger);
    }

    public class TriggerCompiler : ITriggerCompiler
    {
        private readonly ITriggerHelpers helpers;

        public TriggerCompiler(ITriggerHelpers helpers)
        {
            this.helpers = helpers;
        }

        public TriggerInfo CompileTrigger(DeviceTrigger trigger)
        {
            var parser = SetupParser(trigger.TriggerText);

            var listener = new TriggerListener(helpers);
            listener.Parameters.Clear();
            listener.Parameters.Add(Expression.Parameter(typeof(DateTime), "StartTime"));

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
                string originalToken = trigger.TriggerText.Substring(ex.OffendingSymbol.StartIndex,
                    ex.OffendingSymbol.StopIndex - ex.OffendingSymbol.StartIndex + 1);

                throw new TriggerException(ex.OffendingSymbol, ex.Line, ex.CharPositionInLine,
                    ex.Message.Replace(ex.OffendingSymbol.Text, originalToken), ex.RecognitionException);
            }

            var result = new TriggerInfo
            {
                Trigger = trigger,
                TriggerType = listener.TriggerType,
                TriggerText = trigger.TriggerText,
                TriggerStartTime = listener.AtTime,
                TriggerAfterDelay = listener.AfterDelay,
                EventDescription = GetEventDescription(trigger, listener.TriggerType, listener.ReferencedSensors)
            };

            if (result.TriggerType == TriggerType.After)
            {
                var lambda = Expression.Lambda<Func<DateTime, bool>>(listener.Result, listener.Parameters);
                result.AfterFunction = lambda.Compile();
            }
            else
            {
                var lambda = Expression.Lambda<Func<bool>>(listener.Result);
                result.AtOrWhenFunction = lambda.Compile();
            }

            return result;
        }

        private TriggerParser SetupParser(string input)
        {
            var errors = new TriggerErrors();

            var stream = new AntlrInputStream(input.ToLower());

            var lexer = new TriggerLexer(stream);

            var tokens = new CommonTokenStream(lexer);

            var parser = new TriggerParser(tokens);
            parser.RemoveErrorListeners();
            parser.AddErrorListener(errors);

            return parser;
        }

        private string GetEventDescription(DeviceTrigger trigger, TriggerType triggerType, Dictionary<string, string> sensors)
        {
            var result = trigger.ToState != null
                ? "Turn " + trigger.ToState.Name + " " + trigger.TriggerText.ToLower()
                : trigger.TriggerText.ToLower();

            if (triggerType == TriggerType.After)
            {
                result = result.Replace("after ", "AFTER ");
            }
            else if (triggerType == TriggerType.At)
            {
                result = result.Replace("at ", "AT ");
            }
            else if (triggerType == TriggerType.When)
            {
                result = result.Replace("when ", "WHEN ");
            }

            foreach (var pair in sensors)
            {
                result = result.Replace(pair.Key, pair.Value);
            }
            
            return result;
        }
    }
}
