namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AcertoValorPagoNaoPermiteNulo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Aluguel", "ValorPago", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Aluguel", "ValorPago", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
