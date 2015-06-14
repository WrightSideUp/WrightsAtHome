using System.Data.Entity.ModelConfiguration;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.DataAccess.Configuration
{
    public class DeviceTriggerWaitConfiguration : EntityTypeConfiguration<DeviceTriggerWait>
    {
        public DeviceTriggerWaitConfiguration()
        {
            HasRequired(w => w.AfterTrigger)
                .WithMany()
                .Map(m => m.MapKey("DeviceTriggerId"));

            Property(w => w.StartTime).IsRequired();
            Property(w => w.LastModified).IsRequired();
            Property(w => w.LastModifiedUserId).IsRequired();
        }
    }
}