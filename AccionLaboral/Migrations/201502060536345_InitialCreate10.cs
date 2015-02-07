namespace AccionLaboral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Clients", "RejectionDescription", c => c.String(unicode: false));
            DropColumn("Clients", "RejectionDesciption");
        }
        
        public override void Down()
        {
            AddColumn("Clients", "RejectionDesciption", c => c.String(unicode: false));
            DropColumn("Clients", "RejectionDescription");
        }
    }
}
