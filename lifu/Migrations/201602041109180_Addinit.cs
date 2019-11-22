namespace lifu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addinit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 100),
                        Alias = c.String(maxLength: 50),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 100),
                        PasswordSalt = c.String(maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 50),
                        Gender = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 200),
                        MyPic = c.String(maxLength: 50),
                        JobTitle = c.String(maxLength: 50),
                        UnitId = c.Int(nullable: false),
                        Permission = c.String(maxLength: 500),
                        Poster = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.UnitId, cascadeDelete: true)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 100),
                        Permission = c.String(),
                        Alias = c.String(maxLength: 50),
                        Poster = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 200),
                        UpImageUrl = c.String(maxLength: 200),
                        ShortMemo = c.String(maxLength: 1000),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Counter = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CaseDiagrams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        CaseId = c.Int(nullable: false),
                        UpPicUrl = c.String(maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Counter = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cases", t => t.CaseId, cascadeDelete: true)
                .Index(t => t.CaseId);
            
            CreateTable(
                "dbo.Cases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        AreaId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        UpImageUrl = c.String(maxLength: 200),
                        ServerImageUrl = c.String(maxLength: 200),
                        AreaImageUrl = c.String(maxLength: 200),
                        Article = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Counter = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.AreaId, cascadeDelete: true)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CasePictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        CaseId = c.Int(nullable: false),
                        UpPicUrl = c.String(maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cases", t => t.CaseId, cascadeDelete: true)
                .Index(t => t.CaseId);
            
            CreateTable(
                "dbo.CaseServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.String(maxLength: 200),
                        Reserved = c.String(maxLength: 200),
                        CostId = c.Int(nullable: false),
                        AreaId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Gender = c.Int(nullable: false),
                        Birthday = c.DateTime(),
                        AreaCode = c.String(maxLength: 10),
                        Telphone = c.String(maxLength: 50),
                        Mobile = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 200),
                        City = c.String(maxLength: 10),
                        Division = c.String(maxLength: 10),
                        Zip = c.String(maxLength: 10),
                        Address = c.String(nullable: false, maxLength: 100),
                        Information = c.String(nullable: false),
                        MoerInformation = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CaseServiceCosts", t => t.CostId, cascadeDelete: true)
                .ForeignKey("dbo.CaseServiceAreas", t => t.AreaId, cascadeDelete: true)
                .ForeignKey("dbo.CaseServiceTypes", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.CostId)
                .Index(t => t.AreaId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.CaseServiceCosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CaseServiceAreas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CaseServiceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        AreaCodeAM = c.String(maxLength: 10),
                        TelphoneAM = c.String(maxLength: 50),
                        AreaCodePM = c.String(maxLength: 10),
                        TelphonePM = c.String(maxLength: 50),
                        Mobile = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 200),
                        User = c.String(),
                        ShortMemo = c.String(),
                        Replay = c.String(),
                        Poster = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleMembers",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        Member_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.Member_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.Member_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.Member_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.RoleMembers", new[] { "Member_Id" });
            DropIndex("dbo.RoleMembers", new[] { "Role_Id" });
            DropIndex("dbo.CaseServices", new[] { "TypeId" });
            DropIndex("dbo.CaseServices", new[] { "AreaId" });
            DropIndex("dbo.CaseServices", new[] { "CostId" });
            DropIndex("dbo.CasePictures", new[] { "CaseId" });
            DropIndex("dbo.Cases", new[] { "AreaId" });
            DropIndex("dbo.CaseDiagrams", new[] { "CaseId" });
            DropIndex("dbo.Members", new[] { "UnitId" });
            DropForeignKey("dbo.RoleMembers", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.RoleMembers", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.CaseServices", "TypeId", "dbo.CaseServiceTypes");
            DropForeignKey("dbo.CaseServices", "AreaId", "dbo.CaseServiceAreas");
            DropForeignKey("dbo.CaseServices", "CostId", "dbo.CaseServiceCosts");
            DropForeignKey("dbo.CasePictures", "CaseId", "dbo.Cases");
            DropForeignKey("dbo.Cases", "AreaId", "dbo.Areas");
            DropForeignKey("dbo.CaseDiagrams", "CaseId", "dbo.Cases");
            DropForeignKey("dbo.Members", "UnitId", "dbo.Units");
            DropTable("dbo.RoleMembers");
            DropTable("dbo.Services");
            DropTable("dbo.CaseServiceTypes");
            DropTable("dbo.CaseServiceAreas");
            DropTable("dbo.CaseServiceCosts");
            DropTable("dbo.CaseServices");
            DropTable("dbo.CasePictures");
            DropTable("dbo.Areas");
            DropTable("dbo.Cases");
            DropTable("dbo.CaseDiagrams");
            DropTable("dbo.News");
            DropTable("dbo.Roles");
            DropTable("dbo.Members");
            DropTable("dbo.Units");
        }
    }
}
