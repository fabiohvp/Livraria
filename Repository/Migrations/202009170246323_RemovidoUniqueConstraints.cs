namespace Repository.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class RemovidoUniqueConstraints : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Aluguel", "IdLivro", "dbo.Livro");
            DropForeignKey("dbo.Aluguel", "IdUsuario", "dbo.Usuario");
            DropPrimaryKey("dbo.Aluguel");
            DropPrimaryKey("dbo.Livro");
            DropPrimaryKey("dbo.Usuario");
            AlterColumn("dbo.Aluguel", "Id", c => c.String(nullable: false, maxLength: 36,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "Id",
                        new AnnotationValues(oldValue: "IndexAnnotation: { Name: Id, IsUnique: True }", newValue: null)
                    },
                }));
            AlterColumn("dbo.Livro", "Id", c => c.String(nullable: false, maxLength: 36,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "Id",
                        new AnnotationValues(oldValue: "IndexAnnotation: { Name: Id, IsUnique: True }", newValue: null)
                    },
                }));
            AlterColumn("dbo.Usuario", "Id", c => c.String(nullable: false, maxLength: 36,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "Id",
                        new AnnotationValues(oldValue: "IndexAnnotation: { Name: Id, IsUnique: True }", newValue: null)
                    },
                }));
            AddPrimaryKey("dbo.Aluguel", "Id");
            AddPrimaryKey("dbo.Livro", "Id");
            AddPrimaryKey("dbo.Usuario", "Id");
            AddForeignKey("dbo.Aluguel", "IdLivro", "dbo.Livro", "Id");
            AddForeignKey("dbo.Aluguel", "IdUsuario", "dbo.Usuario", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Aluguel", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Aluguel", "IdLivro", "dbo.Livro");
            DropPrimaryKey("dbo.Usuario");
            DropPrimaryKey("dbo.Livro");
            DropPrimaryKey("dbo.Aluguel");
            AlterColumn("dbo.Usuario", "Id", c => c.String(nullable: false, maxLength: 36,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "Id",
                        new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { Name: Id, IsUnique: True }")
                    },
                }));
            AlterColumn("dbo.Livro", "Id", c => c.String(nullable: false, maxLength: 36,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "Id",
                        new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { Name: Id, IsUnique: True }")
                    },
                }));
            AlterColumn("dbo.Aluguel", "Id", c => c.String(nullable: false, maxLength: 36,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "Id",
                        new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { Name: Id, IsUnique: True }")
                    },
                }));
            AddPrimaryKey("dbo.Usuario", "Id");
            AddPrimaryKey("dbo.Livro", "Id");
            AddPrimaryKey("dbo.Aluguel", "Id");
            AddForeignKey("dbo.Aluguel", "IdUsuario", "dbo.Usuario", "Id");
            AddForeignKey("dbo.Aluguel", "IdLivro", "dbo.Livro", "Id");
        }
    }
}
