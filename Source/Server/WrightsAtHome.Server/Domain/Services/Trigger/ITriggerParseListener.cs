using WrightsAtHome.Server.Domain.Services.Trigger.Parser.Generated;

namespace WrightsAtHome.Server.Domain.Services.Trigger
{
    public interface ITriggerParseListener : ITriggerListener
    {
        void Initialize();
    }
}