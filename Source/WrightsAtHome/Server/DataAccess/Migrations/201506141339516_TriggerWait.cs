namespace WrightsAtHome.Server.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TriggerWait : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceTriggerWait",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedUserId = c.Int(nullable: false),
                        DeviceTriggerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceTrigger", t => t.DeviceTriggerId, cascadeDelete: true)
                .Index(t => t.DeviceTriggerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceTriggerWait", "DeviceTriggerId", "dbo.DeviceTrigger");
            DropIndex("dbo.DeviceTriggerWait", new[] { "DeviceTriggerId" });
            DropTable("dbo.DeviceTriggerWait");
        }
    }
}
