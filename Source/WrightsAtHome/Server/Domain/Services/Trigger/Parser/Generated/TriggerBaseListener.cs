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
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace WrightsAtHome.Server.Domain.Services.Trigger.Parser.Generated
{

    using Antlr4.Runtime.Misc;
    using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
    using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
    using IToken = Antlr4.Runtime.IToken;
    using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

    /// <summary>
    /// This class provides an empty implementation of <see cref="ITriggerListener"/>,
    /// which can be extended to create a listener which only needs to handle a subset
    /// of the available methods.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.3")]
    public partial class TriggerBaseListener : ITriggerListener
    {
        /// <summary>
        /// Enter a parse tree produced by <see cref="TriggerParser.afterExp"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterAfterExp([NotNull] TriggerParser.AfterExpContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="TriggerParser.afterExp"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitAfterExp([NotNull] TriggerParser.AfterExpContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="TriggerParser.Negation"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterNegation([NotNull] TriggerParser.NegationContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="TriggerParser.Negation"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitNegation([NotNull] TriggerParser.NegationContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="TriggerParser.atExp"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterAtExp([NotNull] TriggerParser.AtExpContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="TriggerParser.atExp"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitAtExp([NotNull] TriggerParser.AtExpContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="TriggerParser.trigger"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterTrigger([NotNull] TriggerParser.TriggerContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="TriggerParser.trigger"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitTrigger([NotNull] TriggerParser.TriggerContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="TriggerParser.Atom"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterAtom([NotNull] TriggerParser.AtomContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="TriggerParser.Atom"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitAtom([NotNull] TriggerParser.AtomContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="TriggerParser.duration"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterDuration([NotNull] TriggerParser.DurationContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="TriggerParser.duration"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitDuration([NotNull] TriggerParser.DurationContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="TriggerParser.Not"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterNot([NotNull] TriggerParser.NotContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="TriggerParser.Not"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitNot([NotNull] TriggerParser.NotContext context) { }

        /// <summary>
        /// Enter a parse tree produced by TriggerParser.Identifier.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterIdentifier([NotNull] TriggerParser.IdentifierContext context) { }
        /// <summary>
        /// Exit a parse tree produced by TriggerParser.Identifier.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitIdentifier([NotNull] TriggerParser.IdentifierContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="TriggerParser.timePeriod"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterTimePeriod([NotNull] TriggerParser.TimePeriodContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="TriggerParser.timePeriod"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitTimePeriod([NotNull] TriggerParser.TimePeriodContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="TriggerParser.Binary"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterBinary([NotNull] TriggerParser.BinaryContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="TriggerParser.Binary"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitBinary([NotNull] TriggerParser.BinaryContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="TriggerParser.whenExp"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterWhenExp([NotNull] TriggerParser.WhenExpContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="TriggerParser.whenExp"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitWhenExp([NotNull] TriggerParser.WhenExpContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="TriggerParser.endTrigger"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterEndTrigger([NotNull] TriggerParser.EndTriggerContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="TriggerParser.endTrigger"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitEndTrigger([NotNull] TriggerParser.EndTriggerContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="TriggerParser.Paren"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterParen([NotNull] TriggerParser.ParenContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="TriggerParser.Paren"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitParen([NotNull] TriggerParser.ParenContext context) { }

        /// <inheritdoc/>
        /// <remarks>The default implementation does nothing.</remarks>
        public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
        /// <inheritdoc/>
        /// <remarks>The default implementation does nothing.</remarks>
        public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
        /// <inheritdoc/>
        /// <remarks>The default implementation does nothing.</remarks>
        public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
        /// <inheritdoc/>
        /// <remarks>The default implementation does nothing.</remarks>
        public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
    }
} // namespace WrightsAtHome.Server.Domain.Services.Trigger.Parser.Generated
