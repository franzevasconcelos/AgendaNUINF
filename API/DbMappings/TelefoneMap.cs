using AgendaNUINF.API.Entidades;
using FluentNHibernate.Mapping;

namespace AgendaNUINF.API.DbMappings
{
    public class TelefoneMap : ClassMap<Telefone>
    {
        public TelefoneMap() {
            Id(t => t.Id);

            Map(t => t.DDD);
            Map(t => t.Numero);
        }
    }
}