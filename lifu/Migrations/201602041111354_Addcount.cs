namespace lifu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addcount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WebCounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 100),
                        TotalCount = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WebCounts");
        }
    }
}
