using AgendaNUINF.API.Entidades;
using FluentNHibernate.Mapping;

namespace AgendaNUINF.API.Mapeamentos.Banco {
    public class PessoaDbMap : ClassMap<Pessoa> {
        public PessoaDbMap() {
            Id(p => p.Id);

            Map(p => p.Nome);
            Map(p => p.CPF).Nullable();
            Map(p => p.DataNascimento).Nullable();
            Map(p => p.Email).Nullable();

            HasMany(p => p.Telefones).Cascade.All();
        }
    }
}