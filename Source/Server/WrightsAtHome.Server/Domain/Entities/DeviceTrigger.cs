using System;
using System.ComponentModel.DataAnnotations;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class DeviceTrigger : IBaseEntity
    {
        public int Id { get; set; }

        public Device Device { get; set; }

        public int DeviceId { get; set; }

        public DeviceState ToState { get; set; }

        public int ToStateId { get; set; }

        public string TriggerText { get; set; }

        public int Sequence { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastModified { get; set; }

        public int LastModifiedUserId { get; set; }
    }
}