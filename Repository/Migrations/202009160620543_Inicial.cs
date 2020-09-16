namespace Repository.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aluguel",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "Id",
                                    new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { Name: Id, IsUnique: True }")
                                },
                            }),
                        IdLivro = c.String(nullable: false, maxLength: 36),
                        IdUsuario = c.String(nullable: false, maxLength: 36),
                        ValorPago = c.Decimal(precision: 18, scale: 2),
                        DataLocacao = c.DateTime(nullable: false),
                        DataDevolucao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Livro", t => t.IdLivro)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario)
                .Index(t => t.IdLivro)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.Livro",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "Id",
                                    new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { Name: Id, IsUnique: True }")
                                },
                            }),
                        Autor = c.String(nullable: false, maxLength: 100),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Ano = c.Short(nullable: false),
                        Volume = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "Id",
                                    new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { Name: Id, IsUnique: True }")
                                },
                            }),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 50),
                        Senha = c.String(nullable: false, maxLength: 36),
                        Permissao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Aluguel", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Aluguel", "IdLivro", "dbo.Livro");
            DropIndex("dbo.Aluguel", new[] { "IdUsuario" });
            DropIndex("dbo.Aluguel", new[] { "IdLivro" });
            DropTable("dbo.Usuario",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "Id",
                        new Dictionary<string, object>
                        {
                            { "Id", "IndexAnnotation: { Name: Id, IsUnique: True }" },
                        }
                    },
                });
            DropTable("dbo.Livro",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "Id",
                        new Dictionary<string, object>
                        {
                            { "Id", "IndexAnnotation: { Name: Id, IsUnique: True }" },
                        }
                    },
                });
            DropTable("dbo.Aluguel",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "Id",
                        new Dictionary<string, object>
                        {
                            { "Id", "IndexAnnotation: { Name: Id, IsUnique: True }" },
                        }
                    },
                });
        }
    }
}
