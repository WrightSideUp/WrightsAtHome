using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Trigger.Parser.Generated;

namespace WrightsAtHome.Server.Domain.Services.Trigger.Validator
{
    public class TriggerValidatorListener : TriggerBaseListener, ITriggerParseListener
    {
        private readonly ITriggerHelpers helpers;
        private readonly TriggerValidatorErrors errorListener;

        public TriggerType TriggerType { get; private set; }

        public TriggerValidatorListener(ITriggerHelpers helpers, TriggerValidatorErrors errorListener)
        {
            this.helpers = helpers;
            this.errorListener = errorListener;
        }

        public void Initialize()
        {
        }

        public override void ExitAtExp(TriggerParser.AtExpContext context)
        {
            TriggerType = TriggerType.At;
        }

        public override void ExitAfterExp(TriggerParser.AfterExpContext context)
        {
            TriggerType = TriggerType.After;
        }

        public override void ExitWhenExp(TriggerParser.WhenExpContext context)
        {
            TriggerType = TriggerType.When;
        }

        public override void ExitIdentifier(TriggerParser.IdentifierContext context)
        {
            string identifier = context.ID().GetText();

            if (!helpers.IsValidSensorName(identifier))
            {
                var token = context.ID().Symbol;

                errorListener.SyntaxError(null, token, token.Line, token.Column,
                    string.Format("'{0}' is not a valid sensor name", identifier), null);
            }
        }
    }
}