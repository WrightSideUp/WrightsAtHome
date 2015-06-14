//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Projects\Trigger\TriggerFramework\Domain\Services\Trigger\Parser\Grammar\Trigger.g4 by ANTLR 4.3

// Unreachable code detected

using System;

#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace WrightsAtHome.Server.Domain.Services.Trigger.Parser.Generated
{
    using Antlr4.Runtime;
    using Antlr4.Runtime.Atn;
    using Antlr4.Runtime.Misc;
    using Antlr4.Runtime.Tree;
    using System.Collections.Generic;
    using DFA = Antlr4.Runtime.Dfa.DFA;

    [System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.3")]
    public partial class TriggerParser : Parser
    {
        public const int
            T__1 = 1, T__0 = 2, AFTER = 3, WHEN = 4, AT = 5, MUL = 6, DIV = 7, PLUS = 8, MINUS = 9,
            LESSTHAN = 10, LESSEQUAL = 11, EQUAL = 12, NOTEQUAL = 13, GREATERTHAN = 14, GREATEREQUAL = 15,
            AND = 16, OR = 17, NOT = 18, MINUTES = 19, HOURS = 20, TIMECONST = 21, STRING = 22,
            DECIMAL = 23, INT = 24, ID = 25, WS = 26, ErrorChar = 27;
        public static readonly string[] tokenNames = {
        "<INVALID>", "'('", "')'", "'after'", "'when'", "'at'", "'*'", "'/'",
        "'+'", "'-'", "'<'", "'<='", "'='", "NOTEQUAL", "'>'", "'>='", "'and'",
        "'or'", "'not'", "MINUTES", "HOURS", "TIMECONST", "STRING", "DECIMAL",
        "INT", "ID", "WS", "ErrorChar"
    };
        public const int
            RULE_endTrigger = 0, RULE_trigger = 1, RULE_afterExp = 2, RULE_duration = 3,
            RULE_timePeriod = 4, RULE_atExp = 5, RULE_whenExp = 6, RULE_expr = 7;
        public static readonly string[] ruleNames = {
        "endTrigger", "trigger", "afterExp", "duration", "timePeriod", "atExp",
        "whenExp", "expr"
    };

        public override string GrammarFileName { get { return "Trigger.g4"; } }

        public override string[] TokenNames { get { return tokenNames; } }

        public override string[] RuleNames { get { return ruleNames; } }

        public override string SerializedAtn { get { return _serializedATN; } }

        public TriggerParser(ITokenStream input)
            : base(input)
        {
            _interp = new ParserATNSimulator(this, _ATN);
        }
        public partial class EndTriggerContext : ParserRuleContext
        {
            public AfterExpContext afterExp()
            {
                return GetRuleContext<AfterExpContext>(0);
            }
            public TriggerContext trigger()
            {
                return GetRuleContext<TriggerContext>(0);
            }
            public EndTriggerContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_endTrigger; } }
            public override void EnterRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.EnterEndTrigger(this);
            }
            public override void ExitRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.ExitEndTrigger(this);
            }
        }

        [RuleVersion(0)]
        public EndTriggerContext endTrigger()
        {
            EndTriggerContext _localctx = new EndTriggerContext(_ctx, State);
            EnterRule(_localctx, 0, RULE_endTrigger);
            try
            {
                State = 18;
                switch (_input.La(1))
                {
                    case AFTER:
                        EnterOuterAlt(_localctx, 1);
                        {
                            State = 16; afterExp();
                        }
                        break;
                    case WHEN:
                    case AT:
                        EnterOuterAlt(_localctx, 2);
                        {
                            State = 17; trigger();
                        }
                        break;
                    default:
                        throw new NoViableAltException(this);
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                _errHandler.ReportError(this, re);
                _errHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class TriggerContext : ParserRuleContext
        {
            public WhenExpContext whenExp()
            {
                return GetRuleContext<WhenExpContext>(0);
            }
            public AtExpContext atExp()
            {
                return GetRuleContext<AtExpContext>(0);
            }
            public TriggerContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_trigger; } }
            public override void EnterRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.EnterTrigger(this);
            }
            public override void ExitRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.ExitTrigger(this);
            }
        }

        [RuleVersion(0)]
        public TriggerContext trigger()
        {
            TriggerContext _localctx = new TriggerContext(_ctx, State);
            EnterRule(_localctx, 2, RULE_trigger);
            try
            {
                State = 22;
                switch (_input.La(1))
                {
                    case AT:
                        EnterOuterAlt(_localctx, 1);
                        {
                            State = 20; atExp();
                        }
                        break;
                    case WHEN:
                        EnterOuterAlt(_localctx, 2);
                        {
                            State = 21; whenExp();
                        }
                        break;
                    default:
                        throw new NoViableAltException(this);
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                _errHandler.ReportError(this, re);
                _errHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class AfterExpContext : ParserRuleContext
        {
            public DurationContext duration()
            {
                return GetRuleContext<DurationContext>(0);
            }
            public ITerminalNode AFTER() { return GetToken(TriggerParser.AFTER, 0); }
            public AfterExpContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_afterExp; } }
            public override void EnterRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.EnterAfterExp(this);
            }
            public override void ExitRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.ExitAfterExp(this);
            }
        }

        [RuleVersion(0)]
        public AfterExpContext afterExp()
        {
            AfterExpContext _localctx = new AfterExpContext(_ctx, State);
            EnterRule(_localctx, 4, RULE_afterExp);
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 24; Match(AFTER);
                    State = 25; duration();
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                _errHandler.ReportError(this, re);
                _errHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class DurationContext : ParserRuleContext
        {
            public TimePeriodContext timePeriod()
            {
                return GetRuleContext<TimePeriodContext>(0);
            }
            public ITerminalNode INT() { return GetToken(TriggerParser.INT, 0); }
            public DurationContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_duration; } }
            public override void EnterRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.EnterDuration(this);
            }
            public override void ExitRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.ExitDuration(this);
            }
        }

        [RuleVersion(0)]
        public DurationContext duration()
        {
            DurationContext _localctx = new DurationContext(_ctx, State);
            EnterRule(_localctx, 6, RULE_duration);
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 27; Match(INT);
                    State = 28; timePeriod();
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                _errHandler.ReportError(this, re);
                _errHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class TimePeriodContext : ParserRuleContext
        {
            public ITerminalNode HOURS() { return GetToken(TriggerParser.HOURS, 0); }
            public ITerminalNode MINUTES() { return GetToken(TriggerParser.MINUTES, 0); }
            public TimePeriodContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_timePeriod; } }
            public override void EnterRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.EnterTimePeriod(this);
            }
            public override void ExitRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.ExitTimePeriod(this);
            }
        }

        [RuleVersion(0)]
        public TimePeriodContext timePeriod()
        {
            TimePeriodContext _localctx = new TimePeriodContext(_ctx, State);
            EnterRule(_localctx, 8, RULE_timePeriod);
            int _la;
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 30;
                    _la = _input.La(1);
                    if (!(_la == MINUTES || _la == HOURS))
                    {
                        _errHandler.RecoverInline(this);
                    }
                    Consume();
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                _errHandler.ReportError(this, re);
                _errHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class AtExpContext : ParserRuleContext
        {
            public ITerminalNode AT() { return GetToken(TriggerParser.AT, 0); }
            public ITerminalNode TIMECONST() { return GetToken(TriggerParser.TIMECONST, 0); }
            public AtExpContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_atExp; } }
            public override void EnterRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.EnterAtExp(this);
            }
            public override void ExitRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.ExitAtExp(this);
            }
        }

        [RuleVersion(0)]
        public AtExpContext atExp()
        {
            AtExpContext _localctx = new AtExpContext(_ctx, State);
            EnterRule(_localctx, 10, RULE_atExp);
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 32; Match(AT);
                    State = 33; Match(TIMECONST);
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                _errHandler.ReportError(this, re);
                _errHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class WhenExpContext : ParserRuleContext
        {
            public ExprContext expr()
            {
                return GetRuleContext<ExprContext>(0);
            }
            public ITerminalNode WHEN() { return GetToken(TriggerParser.WHEN, 0); }
            public WhenExpContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_whenExp; } }
            public override void EnterRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.EnterWhenExp(this);
            }
            public override void ExitRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.ExitWhenExp(this);
            }
        }

        [RuleVersion(0)]
        public WhenExpContext whenExp()
        {
            WhenExpContext _localctx = new WhenExpContext(_ctx, State);
            EnterRule(_localctx, 12, RULE_whenExp);
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 35; Match(WHEN);
                    State = 36; expr(0);
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                _errHandler.ReportError(this, re);
                _errHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class ExprContext : ParserRuleContext
        {
            public ExprContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_expr; } }

            public ExprContext() { }
            public virtual void CopyFrom(ExprContext context)
            {
                base.CopyFrom(context);
            }
        }
        public partial class NotContext : ExprContext
        {
            public ITerminalNode NOT() { return GetToken(TriggerParser.NOT, 0); }
            public ExprContext expr()
            {
                return GetRuleContext<ExprContext>(0);
            }
            public NotContext(ExprContext context) { CopyFrom(context); }
            public override void EnterRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.EnterNot(this);
            }
            public override void ExitRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.ExitNot(this);
            }
        }
        public partial class IdentifierContext : ExprContext
        {
            public ITerminalNode ID() { return GetToken(TriggerParser.ID, 0); }
            public IdentifierContext(ExprContext context) { CopyFrom(context); }
            public override void EnterRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.EnterIdentifier(this);
            }
            public override void ExitRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.ExitIdentifier(this);
            }
        }
        public partial class NegationContext : ExprContext
        {
            public ExprContext expr()
            {
                return GetRuleContext<ExprContext>(0);
            }
            public ITerminalNode MINUS() { return GetToken(TriggerParser.MINUS, 0); }
            public NegationContext(ExprContext context) { CopyFrom(context); }
            public override void EnterRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.EnterNegation(this);
            }
            public override void ExitRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.ExitNegation(this);
            }
        }
        public partial class BinaryContext : ExprContext
        {
            public ITerminalNode GREATEREQUAL() { return GetToken(TriggerParser.GREATEREQUAL, 0); }
            public ExprContext expr(int i)
            {
                return GetRuleContext<ExprContext>(i);
            }
            public ITerminalNode OR() { return GetToken(TriggerParser.OR, 0); }
            public ITerminalNode MUL() { return GetToken(TriggerParser.MUL, 0); }
            public ITerminalNode EQUAL() { return GetToken(TriggerParser.EQUAL, 0); }
            public IReadOnlyList<ExprContext> expr()
            {
                return GetRuleContexts<ExprContext>();
            }
            public ITerminalNode GREATERTHAN() { return GetToken(TriggerParser.GREATERTHAN, 0); }
            public ITerminalNode LESSEQUAL() { return GetToken(TriggerParser.LESSEQUAL, 0); }
            public ITerminalNode LESSTHAN() { return GetToken(TriggerParser.LESSTHAN, 0); }
            public ITerminalNode AND() { return GetToken(TriggerParser.AND, 0); }
            public ITerminalNode PLUS() { return GetToken(TriggerParser.PLUS, 0); }
            public ITerminalNode MINUS() { return GetToken(TriggerParser.MINUS, 0); }
            public ITerminalNode NOTEQUAL() { return GetToken(TriggerParser.NOTEQUAL, 0); }
            public ITerminalNode DIV() { return GetToken(TriggerParser.DIV, 0); }
            public BinaryContext(ExprContext context) { CopyFrom(context); }
            public override void EnterRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.EnterBinary(this);
            }
            public override void ExitRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.ExitBinary(this);
            }
        }
        public partial class AtomContext : ExprContext
        {
            public ITerminalNode DECIMAL() { return GetToken(TriggerParser.DECIMAL, 0); }
            public ITerminalNode STRING() { return GetToken(TriggerParser.STRING, 0); }
            public ITerminalNode INT() { return GetToken(TriggerParser.INT, 0); }
            public AtomContext(ExprContext context) { CopyFrom(context); }
            public override void EnterRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.EnterAtom(this);
            }
            public override void ExitRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.ExitAtom(this);
            }
        }
        public partial class ParenContext : ExprContext
        {
            public ExprContext expr()
            {
                return GetRuleContext<ExprContext>(0);
            }
            public ParenContext(ExprContext context) { CopyFrom(context); }
            public override void EnterRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.EnterParen(this);
            }
            public override void ExitRule(IParseTreeListener listener)
            {
                ITriggerListener typedListener = listener as ITriggerListener;
                if (typedListener != null) typedListener.ExitParen(this);
            }
        }

        [RuleVersion(0)]
        public ExprContext expr()
        {
            return expr(0);
        }

        private ExprContext expr(int _p)
        {
            ParserRuleContext _parentctx = _ctx;
            int _parentState = State;
            ExprContext _localctx = new ExprContext(_ctx, _parentState);
            ExprContext _prevctx = _localctx;
            int _startState = 14;
            EnterRecursionRule(_localctx, 14, RULE_expr, _p);
            int _la;
            try
            {
                int _alt;
                EnterOuterAlt(_localctx, 1);
                {
                    State = 51;
                    switch (_input.La(1))
                    {
                        case MINUS:
                            {
                                _localctx = new NegationContext(_localctx);
                                _ctx = _localctx;
                                _prevctx = _localctx;

                                State = 39; Match(MINUS);
                                State = 40; expr(13);
                            }
                            break;
                        case NOT:
                            {
                                _localctx = new NotContext(_localctx);
                                _ctx = _localctx;
                                _prevctx = _localctx;
                                State = 41; Match(NOT);
                                State = 42; expr(6);
                            }
                            break;
                        case ID:
                            {
                                _localctx = new IdentifierContext(_localctx);
                                _ctx = _localctx;
                                _prevctx = _localctx;
                                State = 43; Match(ID);
                            }
                            break;
                        case INT:
                            {
                                _localctx = new AtomContext(_localctx);
                                _ctx = _localctx;
                                _prevctx = _localctx;
                                State = 44; Match(INT);
                            }
                            break;
                        case DECIMAL:
                            {
                                _localctx = new AtomContext(_localctx);
                                _ctx = _localctx;
                                _prevctx = _localctx;
                                State = 45; Match(DECIMAL);
                            }
                            break;
                        case STRING:
                            {
                                _localctx = new AtomContext(_localctx);
                                _ctx = _localctx;
                                _prevctx = _localctx;
                                State = 46; Match(STRING);
                            }
                            break;
                        case T__1:
                            {
                                _localctx = new ParenContext(_localctx);
                                _ctx = _localctx;
                                _prevctx = _localctx;
                                State = 47; Match(T__1);
                                State = 48; expr(0);
                                State = 49; Match(T__0);
                            }
                            break;
                        default:
                            throw new NoViableAltException(this);
                    }
                    _ctx.stop = _input.Lt(-1);
                    State = 73;
                    _errHandler.Sync(this);
                    _alt = Interpreter.AdaptivePredict(_input, 4, _ctx);
                    while (_alt != 2 && _alt != global::Antlr4.Runtime.Atn.ATN.InvalidAltNumber)
                    {
                        if (_alt == 1)
                        {
                            if (_parseListeners != null) TriggerExitRuleEvent();
                            _prevctx = _localctx;
                            {
                                State = 71;
                                switch (Interpreter.AdaptivePredict(_input, 3, _ctx))
                                {
                                    case 1:
                                        {
                                            _localctx = new BinaryContext(new ExprContext(_parentctx, _parentState));
                                            PushNewRecursionContext(_localctx, _startState, RULE_expr);
                                            State = 53;
                                            if (!(Precpred(_ctx, 12))) throw new FailedPredicateException(this, "Precpred(_ctx, 12)");
                                            State = 54;
                                            _la = _input.La(1);
                                            if (!(_la == MUL || _la == DIV))
                                            {
                                                _errHandler.RecoverInline(this);
                                            }
                                            Consume();
                                            State = 55; expr(13);
                                        }
                                        break;

                                    case 2:
                                        {
                                            _localctx = new BinaryContext(new ExprContext(_parentctx, _parentState));
                                            PushNewRecursionContext(_localctx, _startState, RULE_expr);
                                            State = 56;
                                            if (!(Precpred(_ctx, 11))) throw new FailedPredicateException(this, "Precpred(_ctx, 11)");
                                            State = 57;
                                            _la = _input.La(1);
                                            if (!(_la == PLUS || _la == MINUS))
                                            {
                                                _errHandler.RecoverInline(this);
                                            }
                                            Consume();
                                            State = 58; expr(12);
                                        }
                                        break;

                                    case 3:
                                        {
                                            _localctx = new BinaryContext(new ExprContext(_parentctx, _parentState));
                                            PushNewRecursionContext(_localctx, _startState, RULE_expr);
                                            State = 59;
                                            if (!(Precpred(_ctx, 10))) throw new FailedPredicateException(this, "Precpred(_ctx, 10)");
                                            State = 60;
                                            _la = _input.La(1);
                                            if (!(_la == LESSTHAN || _la == LESSEQUAL))
                                            {
                                                _errHandler.RecoverInline(this);
                                            }
                                            Consume();
                                            State = 61; expr(11);
                                        }
                                        break;

                                    case 4:
                                        {
                                            _localctx = new BinaryContext(new ExprContext(_parentctx, _parentState));
                                            PushNewRecursionContext(_localctx, _startState, RULE_expr);
                                            State = 62;
                                            if (!(Precpred(_ctx, 9))) throw new FailedPredicateException(this, "Precpred(_ctx, 9)");
                                            State = 63;
                                            _la = _input.La(1);
                                            if (!(_la == EQUAL || _la == NOTEQUAL))
                                            {
                                                _errHandler.RecoverInline(this);
                                            }
                                            Consume();
                                            State = 64; expr(10);
                                        }
                                        break;

                                    case 5:
                                        {
                                            _localctx = new BinaryContext(new ExprContext(_parentctx, _parentState));
                                            PushNewRecursionContext(_localctx, _startState, RULE_expr);
                                            State = 65;
                                            if (!(Precpred(_ctx, 8))) throw new FailedPredicateException(this, "Precpred(_ctx, 8)");
                                            State = 66;
                                            _la = _input.La(1);
                                            if (!(_la == GREATERTHAN || _la == GREATEREQUAL))
                                            {
                                                _errHandler.RecoverInline(this);
                                            }
                                            Consume();
                                            State = 67; expr(9);
                                        }
                                        break;

                                    case 6:
                                        {
                                            _localctx = new BinaryContext(new ExprContext(_parentctx, _parentState));
                                            PushNewRecursionContext(_localctx, _startState, RULE_expr);
                                            State = 68;
                                            if (!(Precpred(_ctx, 7))) throw new FailedPredicateException(this, "Precpred(_ctx, 7)");
                                            State = 69;
                                            _la = _input.La(1);
                                            if (!(_la == AND || _la == OR))
                                            {
                                                _errHandler.RecoverInline(this);
                                            }
                                            Consume();
                                            State = 70; expr(8);
                                        }
                                        break;
                                }
                            }
                        }
                        State = 75;
                        _errHandler.Sync(this);
                        _alt = Interpreter.AdaptivePredict(_input, 4, _ctx);
                    }
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                _errHandler.ReportError(this, re);
                _errHandler.Recover(this, re);
            }
            finally
            {
                UnrollRecursionContexts(_parentctx);
            }
            return _localctx;
        }

        public override bool Sempred(RuleContext _localctx, int ruleIndex, int predIndex)
        {
            switch (ruleIndex)
            {
                case 7: return expr_sempred((ExprContext)_localctx, predIndex);
            }
            return true;
        }
        private bool expr_sempred(ExprContext _localctx, int predIndex)
        {
            switch (predIndex)
            {
                case 0: return Precpred(_ctx, 12);

                case 1: return Precpred(_ctx, 11);

                case 2: return Precpred(_ctx, 10);

                case 3: return Precpred(_ctx, 9);

                case 4: return Precpred(_ctx, 8);

                case 5: return Precpred(_ctx, 7);
            }
            return true;
        }

        public static readonly string _serializedATN =
            "\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x3\x1DO\x4\x2\t\x2" +
            "\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4\t\t" +
            "\t\x3\x2\x3\x2\x5\x2\x15\n\x2\x3\x3\x3\x3\x5\x3\x19\n\x3\x3\x4\x3\x4\x3" +
            "\x4\x3\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3\t\x3" +
            "\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x5\t\x36\n\t" +
            "\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t" +
            "\x3\t\x3\t\x3\t\x3\t\a\tJ\n\t\f\t\xE\tM\v\t\x3\t\x2\x2\x3\x10\n\x2\x2" +
            "\x4\x2\x6\x2\b\x2\n\x2\f\x2\xE\x2\x10\x2\x2\t\x3\x2\x15\x16\x3\x2\b\t" +
            "\x3\x2\n\v\x3\x2\f\r\x3\x2\xE\xF\x3\x2\x10\x11\x3\x2\x12\x13T\x2\x14\x3" +
            "\x2\x2\x2\x4\x18\x3\x2\x2\x2\x6\x1A\x3\x2\x2\x2\b\x1D\x3\x2\x2\x2\n \x3" +
            "\x2\x2\x2\f\"\x3\x2\x2\x2\xE%\x3\x2\x2\x2\x10\x35\x3\x2\x2\x2\x12\x15" +
            "\x5\x6\x4\x2\x13\x15\x5\x4\x3\x2\x14\x12\x3\x2\x2\x2\x14\x13\x3\x2\x2" +
            "\x2\x15\x3\x3\x2\x2\x2\x16\x19\x5\f\a\x2\x17\x19\x5\xE\b\x2\x18\x16\x3" +
            "\x2\x2\x2\x18\x17\x3\x2\x2\x2\x19\x5\x3\x2\x2\x2\x1A\x1B\a\x5\x2\x2\x1B" +
            "\x1C\x5\b\x5\x2\x1C\a\x3\x2\x2\x2\x1D\x1E\a\x1A\x2\x2\x1E\x1F\x5\n\x6" +
            "\x2\x1F\t\x3\x2\x2\x2 !\t\x2\x2\x2!\v\x3\x2\x2\x2\"#\a\a\x2\x2#$\a\x17" +
            "\x2\x2$\r\x3\x2\x2\x2%&\a\x6\x2\x2&\'\x5\x10\t\x2\'\xF\x3\x2\x2\x2()\b" +
            "\t\x1\x2)*\a\v\x2\x2*\x36\x5\x10\t\xF+,\a\x14\x2\x2,\x36\x5\x10\t\b-\x36" +
            "\a\x1B\x2\x2.\x36\a\x1A\x2\x2/\x36\a\x19\x2\x2\x30\x36\a\x18\x2\x2\x31" +
            "\x32\a\x3\x2\x2\x32\x33\x5\x10\t\x2\x33\x34\a\x4\x2\x2\x34\x36\x3\x2\x2" +
            "\x2\x35(\x3\x2\x2\x2\x35+\x3\x2\x2\x2\x35-\x3\x2\x2\x2\x35.\x3\x2\x2\x2" +
            "\x35/\x3\x2\x2\x2\x35\x30\x3\x2\x2\x2\x35\x31\x3\x2\x2\x2\x36K\x3\x2\x2" +
            "\x2\x37\x38\f\xE\x2\x2\x38\x39\t\x3\x2\x2\x39J\x5\x10\t\xF:;\f\r\x2\x2" +
            ";<\t\x4\x2\x2<J\x5\x10\t\xE=>\f\f\x2\x2>?\t\x5\x2\x2?J\x5\x10\t\r@\x41" +
            "\f\v\x2\x2\x41\x42\t\x6\x2\x2\x42J\x5\x10\t\f\x43\x44\f\n\x2\x2\x44\x45" +
            "\t\a\x2\x2\x45J\x5\x10\t\v\x46G\f\t\x2\x2GH\t\b\x2\x2HJ\x5\x10\t\nI\x37" +
            "\x3\x2\x2\x2I:\x3\x2\x2\x2I=\x3\x2\x2\x2I@\x3\x2\x2\x2I\x43\x3\x2\x2\x2" +
            "I\x46\x3\x2\x2\x2JM\x3\x2\x2\x2KI\x3\x2\x2\x2KL\x3\x2\x2\x2L\x11\x3\x2" +
            "\x2\x2MK\x3\x2\x2\x2\a\x14\x18\x35IK";
        public static readonly ATN _ATN =
            new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
    }
} // namespace WrightsAtHome.Server.Domain.Services.Trigger.Parser.Generated
