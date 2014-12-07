namespace AccionLaboral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Languages", "Level");
            DropColumn("dbo.Languages", "Percentage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Languages", "Percentage", c => c.Int(nullable: false));
            AddColumn("dbo.Languages", "Level", c => c.String(unicode: false));
        }
    }
}
