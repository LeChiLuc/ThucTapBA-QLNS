namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Status", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Status");
        }
    }
}
