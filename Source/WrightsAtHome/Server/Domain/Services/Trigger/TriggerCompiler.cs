using System;
using System.Linq.Expressions;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using WrightsAtHome.Server.Domain.Services.Trigger.Parser;
using WrightsAtHome.Server.Domain.Services.Trigger.Parser.Generated;

namespace WrightsAtHome.Server.Domain.Services.Trigger
{
    public interface ITriggerCompiler
    {
        bool IsAfterTrigger(string trigger);

        Func<bool> CompileAtOrWhenTrigger(string trigger);

        Func<DateTime, bool> CompileAfterTrigger(string trigger);
    }

    public class TriggerCompiler : ITriggerCompiler
    {
        private readonly ITriggerHelpers helpers;

        public TriggerCompiler(ITriggerHelpers helpers)
        {
            this.helpers = helpers;
        }

        public bool IsAfterTrigger(string trigger)
        {
            return trigger.ToLower().Trim().StartsWith("after ");
        }

        public Func<bool> CompileAtOrWhenTrigger(string trigger)
        {
            if (IsAfterTrigger(trigger))
            {
                throw new ArgumentException("AFTER trigger sent to CompileAtOrWhenTrigger", "trigger");
            }
            
            var parser = SetupParser(trigger);

            var tree = parser.trigger();

            var walker = new ParseTreeWalker();

            var listener = new TriggerListener(helpers);
            listener.Parameters.Clear();

            walker.Walk(listener, tree);

            var lambda = Expression.Lambda<Func<bool>>(listener.Result);
            var func = lambda.Compile();

            return func;
        }
        public Func<DateTime, bool> CompileAfterTrigger(string trigger)
        {
            var parser = SetupParser(trigger);

            var tree = parser.endTrigger();

            var walker = new ParseTreeWalker();

            var listener = new TriggerListener(helpers);
            listener.Parameters.Clear();
            listener.Parameters.Add(Expression.Parameter(typeof(DateTime), "StartTime"));

            walker.Walk(listener, tree);

            var lambda = Expression.Lambda<Func<DateTime, bool>>(listener.Result, listener.Parameters);
            var func = lambda.Compile();

            return func;
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
    }
}
