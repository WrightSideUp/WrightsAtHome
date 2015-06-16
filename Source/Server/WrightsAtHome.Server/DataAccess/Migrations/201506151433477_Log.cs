namespace WrightsAtHome.Server.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Log : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Message = c.String(maxLength: 1024),
                        Exception = c.String(maxLength: 512),
                        Level = c.String(maxLength: 10),
                        Logger = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Log");
        }
    }
}
