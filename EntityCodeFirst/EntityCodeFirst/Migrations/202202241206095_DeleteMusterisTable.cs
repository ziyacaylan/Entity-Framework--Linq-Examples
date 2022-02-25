namespace EntityCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteMusterisTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Musteris");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Musteris",
                c => new
                    {
                        MusteriID = c.Int(nullable: false, identity: true),
                        MusteriAd = c.String(),
                        MusteriUnvan = c.String(),
                        MusteriSehir = c.String(),
                    })
                .PrimaryKey(t => t.MusteriID);
            
        }
    }
}
