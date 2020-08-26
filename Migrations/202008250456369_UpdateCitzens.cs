namespace Ex3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCitzens : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Citzens", "Flat_FlatId", c => c.Int());
            CreateIndex("dbo.Citzens", "Flat_FlatId");
            AddForeignKey("dbo.Citzens", "Flat_FlatId", "dbo.Flats", "FlatId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Citzens", "Flat_FlatId", "dbo.Flats");
            DropIndex("dbo.Citzens", new[] { "Flat_FlatId" });
            DropColumn("dbo.Citzens", "Flat_FlatId");
        }
    }
}
