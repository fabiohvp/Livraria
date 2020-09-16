namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoUsuarioCadastradorParaAuditoria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Livro", "IdUsuarioCadastrador", c => c.String(nullable: false, maxLength: 36));
            AddColumn("dbo.Usuario", "IdUsuarioCadastrador", c => c.String(nullable: false, maxLength: 36));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "IdUsuarioCadastrador");
            DropColumn("dbo.Livro", "IdUsuarioCadastrador");
        }
    }
}
