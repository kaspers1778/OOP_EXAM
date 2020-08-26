namespace Ex3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contracts", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contracts", "Date", c => c.String());
        }
    }
}
