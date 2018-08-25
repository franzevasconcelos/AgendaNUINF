using AgendaNUINF.API.Entidades;
using FluentNHibernate.Mapping;

namespace AgendaNUINF.API.Mapeamentos.Banco {
    public class TelefoneDbMap : ClassMap<Telefone> {
        public TelefoneDbMap() {
            Id(t => t.Id);

            Map(t => t.DDD);
            Map(t => t.Numero);
            References(t => t.Pessoa);
        }
    }
}