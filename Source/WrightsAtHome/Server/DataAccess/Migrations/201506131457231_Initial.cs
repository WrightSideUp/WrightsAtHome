namespace WrightsAtHome.Server.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Sequence = c.Int(nullable: false),
                        ImageName = c.String(nullable: false, maxLength: 20),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeviceState",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                        Sequence = c.Int(nullable: false),
                        IsTransitional = c.Boolean(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeviceStateChange",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppliedDate = c.DateTime(nullable: false),
                        WasOverride = c.Boolean(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedUserId = c.Int(nullable: false),
                        AfterStateId = c.Int(nullable: false),
                        BeforeStateId = c.Int(nullable: false),
                        DeviceId = c.Int(nullable: false),
                        DeviceTriggerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceState", t => t.AfterStateId)
                .ForeignKey("dbo.DeviceState", t => t.BeforeStateId)
                .ForeignKey("dbo.Device", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.DeviceTrigger", t => t.DeviceTriggerId)
                .Index(t => t.AfterStateId)
                .Index(t => t.BeforeStateId)
                .Index(t => t.DeviceId)
                .Index(t => t.DeviceTriggerId);
            
            CreateTable(
                "dbo.DeviceTrigger",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TriggerText = c.String(nullable: false, maxLength: 1024),
                        Sequence = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedUserId = c.Int(nullable: false),
                        DeviceId = c.Int(nullable: false),
                        DeviceStateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Device", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.DeviceState", t => t.DeviceStateId, cascadeDelete: true)
                .Index(t => t.DeviceId)
                .Index(t => t.DeviceStateId);
            
            CreateTable(
                "dbo.SensorReading",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReadingDate = c.DateTime(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedUserId = c.Int(nullable: false),
                        SensorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sensor", t => t.SensorId, cascadeDelete: true)
                .Index(t => t.SensorId);
            
            CreateTable(
                "dbo.Sensor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Sequence = c.Int(nullable: false),
                        ImageName = c.String(nullable: false, maxLength: 20),
                        ReadInterval = c.Time(nullable: false, precision: 7),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedUserId = c.Int(nullable: false),
                        SensorTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorType", t => t.SensorTypeId, cascadeDelete: true)
                .Index(t => t.SensorTypeId);
            
            CreateTable(
                "dbo.SensorType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeviceDeviceStateXref",
                c => new
                    {
                        DeviceId = c.Int(nullable: false),
                        DeviceStateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeviceId, t.DeviceStateId })
                .ForeignKey("dbo.Device", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.DeviceState", t => t.DeviceStateId, cascadeDelete: true)
                .Index(t => t.DeviceId)
                .Index(t => t.DeviceStateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensorReading", "SensorId", "dbo.Sensor");
            DropForeignKey("dbo.Sensor", "SensorTypeId", "dbo.SensorType");
            DropForeignKey("dbo.DeviceStateChange", "DeviceTriggerId", "dbo.DeviceTrigger");
            DropForeignKey("dbo.DeviceTrigger", "DeviceStateId", "dbo.DeviceState");
            DropForeignKey("dbo.DeviceTrigger", "DeviceId", "dbo.Device");
            DropForeignKey("dbo.DeviceStateChange", "DeviceId", "dbo.Device");
            DropForeignKey("dbo.DeviceStateChange", "BeforeStateId", "dbo.DeviceState");
            DropForeignKey("dbo.DeviceStateChange", "AfterStateId", "dbo.DeviceState");
            DropForeignKey("dbo.DeviceDeviceStateXref", "DeviceStateId", "dbo.DeviceState");
            DropForeignKey("dbo.DeviceDeviceStateXref", "DeviceId", "dbo.Device");
            DropIndex("dbo.DeviceDeviceStateXref", new[] { "DeviceStateId" });
            DropIndex("dbo.DeviceDeviceStateXref", new[] { "DeviceId" });
            DropIndex("dbo.Sensor", new[] { "SensorTypeId" });
            DropIndex("dbo.SensorReading", new[] { "SensorId" });
            DropIndex("dbo.DeviceTrigger", new[] { "DeviceStateId" });
            DropIndex("dbo.DeviceTrigger", new[] { "DeviceId" });
            DropIndex("dbo.DeviceStateChange", new[] { "DeviceTriggerId" });
            DropIndex("dbo.DeviceStateChange", new[] { "DeviceId" });
            DropIndex("dbo.DeviceStateChange", new[] { "BeforeStateId" });
            DropIndex("dbo.DeviceStateChange", new[] { "AfterStateId" });
            DropTable("dbo.DeviceDeviceStateXref");
            DropTable("dbo.SensorType");
            DropTable("dbo.Sensor");
            DropTable("dbo.SensorReading");
            DropTable("dbo.DeviceTrigger");
            DropTable("dbo.DeviceStateChange");
            DropTable("dbo.DeviceState");
            DropTable("dbo.Device");
        }
    }
}
