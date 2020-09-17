namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoValorPrevistoEQuantidadeDias : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Aluguel", "ValorPrevisto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Aluguel", "QuantidadeDias", c => c.Int(nullable: false));
            AddColumn("dbo.Livro", "ValorAluguel", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Livro", "ValorAluguel");
            DropColumn("dbo.Aluguel", "QuantidadeDias");
            DropColumn("dbo.Aluguel", "ValorPrevisto");
        }
    }
}
