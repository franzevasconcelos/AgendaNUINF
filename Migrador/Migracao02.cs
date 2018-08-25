using System;
using FluentMigrator;

namespace AgendaNUINF.Migrador {
    [Migration(02, "Carga")]
    public class Migracao02 : Migration {
        public override void Up() {
            Insert.IntoTable("Pessoa")
                  .InSchema("dbo")
                  .Row(new
                       {
                           nome = "Fulano da Silva",
                           cpf = "111111111111",
                           email = "fulano@email.com",
                           datanascimento = new DateTime(1980, 10, 05)
                       });

            Insert.IntoTable("Telefone")
                  .InSchema("dbo")
                  .Row(new
                       {
                           pessoa_id = 1,
                           ddd = "85",
                           numero = "999999999"
                       });

            Insert.IntoTable("Telefone")
                  .InSchema("dbo")
                  .Row(new
                       {
                           pessoa_id = 1,
                           ddd = "85",
                           numero = "999995555"
                       });

            Insert.IntoTable("Pessoa")
                  .InSchema("dbo")
                  .Row(new
                       {
                           nome = "Beltrano Souza",
                           cpf = "22222222222",
                           email = "beltrano@email.com",
                           datanascimento = new DateTime(1991, 1, 25)
                       });

            Insert.IntoTable("Telefone")
                  .InSchema("dbo")
                  .Row(new
                       {
                           pessoa_id = 2,
                           ddd = "84",
                           numero = "988883333"
                       });
        }

        public override void Down() { }
    }
}