namespace TeachersAssistant.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGradeDecimalColumnToSubmissions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignmentSubmissions", "GradeNumeric", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssignmentSubmissions", "GradeNumeric");
        }
    }
}
