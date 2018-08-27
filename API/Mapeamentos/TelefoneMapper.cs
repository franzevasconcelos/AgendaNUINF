using AgendaNUINF.API.Entidades;
using AgendaNUINF.EntidadesDTO;
using AutoMapper;

namespace AgendaNUINF.API.Mapeamentos {
    public class TelefoneMapper : Profile {
        public TelefoneMapper() {
            CreateMap<Telefone, TelefoneDTO>()
                .ReverseMap();
        }
    }
}