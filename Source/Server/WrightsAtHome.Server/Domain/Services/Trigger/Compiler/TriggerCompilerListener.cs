using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Antlr4.Runtime.Tree;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Trigger.Parser;
using WrightsAtHome.Server.Domain.Services.Trigger.Parser.Generated;

namespace WrightsAtHome.Server.Domain.Services.Trigger.Compiler
{
    public class TriggerCompilerListener : TriggerBaseListener, ITriggerParseListener
    {
        private readonly ITriggerHelpers helpers;
        private readonly ParseTreeProperty<Expression> expressions = new ParseTreeProperty<Expression>();
        private readonly List<ParameterExpression> parameters = new List<ParameterExpression>();
        private readonly Dictionary<string, string> referencedSensors = new Dictionary<string, string>(); 

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

        public TriggerType TriggerType { get; private set; }

        public DateTime AtTime { get; private set; }

        public TimeSpan AfterDelay { get; private set; }

        public Dictionary<string, string> ReferencedSensors { get { return referencedSensors; }  } 
        
        public List<ParameterExpression> Parameters { get { return parameters; } }

        public TriggerCompilerListener(ITriggerHelpers helpers)
        {
            this.helpers = helpers;
        }

        public void Initialize()
        {
            Parameters.Clear();
            Parameters.Add(Expression.Parameter(typeof(DateTime), "StartTime"));
            AtTime = DateTime.MaxValue;
            AfterDelay = TimeSpan.MaxValue;
            ReferencedSensors.Clear();
        }

        public override void ExitAtExp(TriggerParser.AtExpContext context)
        {
            var timeVal = DateTime.Parse(context.TIMECONST().GetText());

            Result = Expression.GreaterThanOrEqual(CallHelperMethod("GetCurrentTime"),
                                                   Expression.Constant(timeVal));

            AtTime = timeVal;
            TriggerType = TriggerType.At;
        }


        public override void ExitAfterExp(TriggerParser.AfterExpContext context)
        {
            Result = Expression.GreaterThanOrEqual(
                                                    Expression.SubtractChecked(
                                                                               CallHelperMethod("GetCurrentTime"),
                                                                               parameters[0]),
                                                    expressions.Get(context.duration()));
            TriggerType = TriggerType.After;
        }

        public override void ExitWhenExp(TriggerParser.WhenExpContext context)
        {
            Result = expressions.Get(context.expr());
            TriggerType = TriggerType.When;
        }

        public override void ExitDuration(TriggerParser.DurationContext context)
        {
            int interval = int.Parse(context.INT().GetText());

            var t = context.timePeriod().HOURS() != null ? new TimeSpan(interval, 0, 0) : new TimeSpan(0, interval, 0);

            expressions.Put(context, Expression.Constant(t));

            AfterDelay = t;
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

        public override void ExitCurrentTime(TriggerParser.CurrentTimeContext context)
        {
            expressions.Put(context, CallHelperMethod("GetCurrentTime"));
        }

        public override void ExitTimeConst(TriggerParser.TimeConstContext context)
        {
            var timeVal = DateTime.Parse(context.TIMECONST().GetText());
            expressions.Put(context, Expression.Constant(timeVal));
        }

        public override void ExitIdentifier(TriggerParser.IdentifierContext context)
        {
            string identifier = context.ID().GetText();

            if (IsValidIdentifier(identifier))
            {
                var constant = Expression.Constant(identifier);
                expressions.Put(context,
                    Expression.Call(Expression.Constant(helpers), helpers.GetType().GetMethod("GetNumericSensorReading"),
                        constant));
            }
            else
            {
                var token = context.ID().Symbol;
                throw new TriggerException(token, token.StartIndex, identifier.Length, string.Format("'{0}' is not a valid sensor name", identifier), null);
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
            string actualName;

            if (helpers.TryGetSensorName(identifier, out actualName))
            {
                ReferencedSensors[identifier] = actualName;
                return true;
            }

            return false;
        }
    }
}
