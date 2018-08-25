using FluentMigrator;

namespace AgendaNUINF.Migrador {
    [Migration(01, "Migração inicial")]
    public class Migracao01 : Migration {
        public override void Up() {
            Create.Table("Pessoa")
                  .WithColumn("id")
                  .AsInt32()
                  .PrimaryKey("pk_pessoa")
                  .Identity()
                  .WithColumn("nome")
                  .AsString(100)
                  .NotNullable()
                  .WithColumn("cpf")
                  .AsString(11)
                  .Nullable()
                  .WithColumn("email")
                  .AsString(100)
                  .Nullable()
                  .WithColumn("datanascimento")
                  .AsDate()
                  .Nullable();

            Create.Table("Telefone")
                  .WithColumn("id")
                  .AsInt32()
                  .PrimaryKey("pk_telefone")
                  .WithColumn("ddd")
                  .AsString(3)
                  .NotNullable()
                  .WithColumn("numero")
                  .AsString(9)
                  .NotNullable()
                  .WithColumn("pessoa_id")
                  .AsInt32()
                  .ForeignKey("pessoa", "id");
        }

        public override void Down() { }
    }
}