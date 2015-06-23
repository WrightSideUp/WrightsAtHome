namespace WrightsAtHome.Server.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Indexes : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Device", "Name", unique: true, name: "IX_DeviceName");
            CreateIndex("dbo.Device", "Sequence", unique: true, name: "IX_DeviceSequence");
            CreateIndex("dbo.DeviceState", "Name", unique: true, name: "IX_DeviceStateName");
            CreateIndex("dbo.DeviceState", "Sequence", unique: true, name: "IX_DeviceStateSequence");
            CreateIndex("dbo.Sensor", "Name", unique: true, name: "IX_SensorName");
            CreateIndex("dbo.Sensor", "Sequence", unique: true, name: "IX_SensorSequence");
            CreateIndex("dbo.SensorType", "Name", unique: true, name: "IX_SensorTypeName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SensorType", "IX_SensorTypeName");
            DropIndex("dbo.Sensor", "IX_SensorSequence");
            DropIndex("dbo.Sensor", "IX_SensorName");
            DropIndex("dbo.DeviceState", "IX_DeviceStateSequence");
            DropIndex("dbo.DeviceState", "IX_DeviceStateName");
            DropIndex("dbo.Device", "IX_DeviceSequence");
            DropIndex("dbo.Device", "IX_DeviceName");
        }
    }
}
