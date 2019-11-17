namespace debatesWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class images : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Debates", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Debates", "Image");
        }
    }
}
