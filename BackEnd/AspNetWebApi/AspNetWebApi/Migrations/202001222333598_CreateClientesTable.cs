namespace AspNetWebApi.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateClientesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 200),
                        Cpf = c.String(maxLength: 10),
                        Uf = c.String(maxLength: 2),
                        Cidade = c.String(maxLength: 50),
                        Bairro = c.String(maxLength: 50),
                        Rua = c.String(maxLength: 50),
                        Numero = c.Int(),
                        Cep = c.String(maxLength: 10),
                        PontoReferencia = c.String(maxLength: 30),
                        Complemento = c.String(maxLength: 30),
                        Ativo = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cliente");
        }
    }
}
