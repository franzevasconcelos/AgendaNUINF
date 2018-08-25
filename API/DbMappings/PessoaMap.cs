using AgendaNUINF.API.Entidades;
using FluentNHibernate.Mapping;

namespace AgendaNUINF.API.DbMappings
{
    public class PessoaMap: ClassMap<Pessoa>
    {
        public PessoaMap() {
            Id(p => p.Id);

            Map(p => p.Nome);
            Map(p => p.CPF).Nullable();
            Map(p => p.DataNascimento).Nullable();
            Map(p => p.Email).Nullable();

            HasMany(p => p.Telefones).Cascade.All();
        }
    }
}