namespace Ex3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Citzens",
                c => new
                    {
                        CitzenId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        SecondName = c.String(),
                        IsAdult = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CitzenId);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        ContractId = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        isDebt = c.Boolean(nullable: false),
                        Debt = c.Int(),
                        Flat_FlatId = c.Int(),
                        Rentor_CitzenId = c.Int(),
                    })
                .PrimaryKey(t => t.ContractId)
                .ForeignKey("dbo.Flats", t => t.Flat_FlatId)
                .ForeignKey("dbo.Citzens", t => t.Rentor_CitzenId)
                .Index(t => t.Flat_FlatId)
                .Index(t => t.Rentor_CitzenId);
            
            CreateTable(
                "dbo.Flats",
                c => new
                    {
                        FlatId = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Rooms = c.Int(nullable: false),
                        Square = c.Int(nullable: false),
                        UsefulSquare = c.Int(nullable: false),
                        Entrance_EntranceId = c.Int(),
                        House_HouseId = c.Int(),
                    })
                .PrimaryKey(t => t.FlatId)
                .ForeignKey("dbo.Entrances", t => t.Entrance_EntranceId)
                .ForeignKey("dbo.Houses", t => t.House_HouseId)
                .Index(t => t.Entrance_EntranceId)
                .Index(t => t.House_HouseId);
            
            CreateTable(
                "dbo.Entrances",
                c => new
                    {
                        EntranceId = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        House_HouseId = c.Int(),
                    })
                .PrimaryKey(t => t.EntranceId)
                .ForeignKey("dbo.Houses", t => t.House_HouseId)
                .Index(t => t.House_HouseId);
            
            CreateTable(
                "dbo.Houses",
                c => new
                    {
                        HouseId = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Street = c.String(),
                        Floors = c.Int(nullable: false),
                        HouseComplex_HouseComplexId = c.Int(),
                    })
                .PrimaryKey(t => t.HouseId)
                .ForeignKey("dbo.HouseComplexes", t => t.HouseComplex_HouseComplexId)
                .Index(t => t.HouseComplex_HouseComplexId);
            
            CreateTable(
                "dbo.HouseComplexes",
                c => new
                    {
                        HouseComplexId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.HouseComplexId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contracts", "Rentor_CitzenId", "dbo.Citzens");
            DropForeignKey("dbo.Contracts", "Flat_FlatId", "dbo.Flats");
            DropForeignKey("dbo.Flats", "House_HouseId", "dbo.Houses");
            DropForeignKey("dbo.Flats", "Entrance_EntranceId", "dbo.Entrances");
            DropForeignKey("dbo.Entrances", "House_HouseId", "dbo.Houses");
            DropForeignKey("dbo.Houses", "HouseComplex_HouseComplexId", "dbo.HouseComplexes");
            DropIndex("dbo.Houses", new[] { "HouseComplex_HouseComplexId" });
            DropIndex("dbo.Entrances", new[] { "House_HouseId" });
            DropIndex("dbo.Flats", new[] { "House_HouseId" });
            DropIndex("dbo.Flats", new[] { "Entrance_EntranceId" });
            DropIndex("dbo.Contracts", new[] { "Rentor_CitzenId" });
            DropIndex("dbo.Contracts", new[] { "Flat_FlatId" });
            DropTable("dbo.HouseComplexes");
            DropTable("dbo.Houses");
            DropTable("dbo.Entrances");
            DropTable("dbo.Flats");
            DropTable("dbo.Contracts");
            DropTable("dbo.Citzens");
        }
    }
}
