namespace System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipments", "UnitId", "dbo.Units");
            DropForeignKey("dbo.DataEquipments", "Equipment_Id", "dbo.Equipments");
            DropIndex("dbo.DataEquipments", new[] { "Equipment_Id" });
            DropIndex("dbo.Equipments", new[] { "UnitId" });
            DropTable("dbo.DataEquipments");
            DropTable("dbo.Equipments");
            DropTable("dbo.Units");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UnitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DataEquipments",
                c => new
                    {
                        EquipmentId = c.Int(nullable: false, identity: true),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        PeriodType = c.Int(nullable: false),
                        Equipment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipmentId);
            
            CreateIndex("dbo.Equipments", "UnitId");
            CreateIndex("dbo.DataEquipments", "Equipment_Id");
            AddForeignKey("dbo.DataEquipments", "Equipment_Id", "dbo.Equipments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Equipments", "UnitId", "dbo.Units", "Id", cascadeDelete: true);
        }
    }
}
