namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCreatedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CreatedDate");
        }
    }
}
