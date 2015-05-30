using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Antlr4.Runtime.Tree;
using WrightsAtHome.BackEnd.Domain.Services.Trigger.Parser.Generated;

namespace WrightsAtHome.BackEnd.Domain.Services.Trigger.Parser
{
    public class TriggerListener : TriggerBaseListener
    {
        private readonly ITriggerHelpers helpers;
        private readonly ParseTreeProperty<Expression> expressions = new ParseTreeProperty<Expression>();
        private readonly List<ParameterExpression> parameters = new List<ParameterExpression>();

        private readonly Dictionary<string, Func<Expression, Expression, BinaryExpression>> BinaryOperatorMap = new Dictionary<string, Func<Expression, Expression, BinaryExpression>>()
        {
            { "*", Expression.MultiplyChecked },
            { "/", Expression.Divide },
            { "+", Expression.AddChecked },
            { "-", Expression.SubtractChecked },
            { "<", Expression.LessThan },
            { "<=", Expression.LessThanOrEqual },
            { "=", Expression.Equal },
            { "!=", Expression.NotEqual },
            { "<>", Expression.NotEqual },
            { ">", Expression.GreaterThan },
            { ">=", Expression.GreaterThanOrEqual },
            { "and", Expression.AndAlso },
            { "or", Expression.OrElse },
        };

        public Expression Result { get; private set; }

        public List<ParameterExpression> Parameters { get { return parameters; } }

        public TriggerListener(ITriggerHelpers helpers)
        {
            this.helpers = helpers;
        }

        public override void ExitAtExp(TriggerParser.AtExpContext context)
        {
            var timeVal = DateTime.Parse(context.TIMECONST().GetText());

            Result = Expression.GreaterThanOrEqual(CallHelperMethod("GetCurrentTime"),
                                                   Expression.Constant(timeVal));
        }

        public override void ExitAfterExp(TriggerParser.AfterExpContext context)
        {
            Result = Expression.GreaterThanOrEqual(
                                                    Expression.SubtractChecked(
                                                                               CallHelperMethod("GetCurrentTime"),
                                                                               parameters[0]),
                                                    expressions.Get(context.duration()));
        }

        public override void ExitWhenExp(TriggerParser.WhenExpContext context)
        {
            Result = expressions.Get(context.expr());
        }

        public override void ExitDuration(TriggerParser.DurationContext context)
        {
            int interval = int.Parse(context.INT().GetText());

            var t = context.timePeriod().HOURS() != null ? new TimeSpan(interval, 0, 0) : new TimeSpan(0, interval, 0);

            expressions.Put(context, Expression.Constant(t));
        }

        public override void ExitNegation(TriggerParser.NegationContext context)
        {
            expressions.Put(context, Expression.NegateChecked(expressions.Get(context.expr())));
        }

        public override void ExitBinary(TriggerParser.BinaryContext context)
        {
            var left = expressions.Get(context.GetChild(0));
            var right = expressions.Get(context.GetChild(2));
            var opMaker = BinaryOperatorMap[context.GetChild(1).GetText()];

            expressions.Put(context, opMaker(left, right));
        }

        public override void ExitNot(TriggerParser.NotContext context)
        {
            expressions.Put(context, Expression.Not(expressions.Get(context.expr())));
        }

        public override void ExitIdentifier(TriggerParser.IdentifierContext context)
        {
            string identifier = context.ID().GetText();

            if (identifier == "now")
            {
                expressions.Put(context, CallHelperMethod("GetCurrentTime"));
            }
            else
            {
                if (IsValidIdentifier(identifier))
                {
                    var constant = Expression.Constant(identifier);
                    expressions.Put(context, Expression.Call(Expression.Constant(helpers), helpers.GetType().GetMethod("GetNumericSensorReading"), constant));
                }
            }
        }

        public override void ExitAtom(TriggerParser.AtomContext context)
        {
            if (context.DECIMAL() != null)
            {
                decimal value = decimal.Parse(context.DECIMAL().GetText());
                expressions.Put(context, Expression.Constant(value));
            }
            else if (context.INT() != null)
            {
                decimal value = decimal.Parse(context.INT().GetText());
                expressions.Put(context, Expression.Constant(value));
            }
            else if (context.STRING() != null)
            {
                expressions.Put(context, Expression.Constant(context.STRING().GetText()));
            }
        }

        public override void ExitParen(TriggerParser.ParenContext context)
        {
            expressions.Put(context, expressions.Get(context.expr()));
        }

        private Expression CallHelperMethod(string methodName)
        {
            return Expression.Call(Expression.Constant(helpers), helpers.GetType().GetMethod(methodName));
        }

        private bool IsValidIdentifier(string identifier)
        {
            if (identifier != "BadOne")
            {
                return true;
            }

            return false;
        }
    }
}
