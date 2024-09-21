namespace MiniSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mini : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        DueDate = c.DateTime(nullable: false),
                        ClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .Index(t => t.ClassId);
            
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ClassId = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ClassId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 200),
                        Age = c.Int(nullable: false),
                        Email = c.String(),
                        ClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .Index(t => t.ClassId);
            
            CreateTable(
                "dbo.Subject",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignment", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.Student", "ClassId", "dbo.Classes");
            DropIndex("dbo.Student", new[] { "ClassId" });
            DropIndex("dbo.Assignment", new[] { "ClassId" });
            DropTable("dbo.Subject");
            DropTable("dbo.Student");
            DropTable("dbo.Classes");
            DropTable("dbo.Assignment");
        }
    }
}
