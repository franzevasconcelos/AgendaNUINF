using AgendaNUINF.API.Entidades;
using AgendaNUINF.EntidadesDTO;
using AutoMapper;

namespace AgendaNUINF.API.Mapeamentos {
    public class PessoaMapper : Profile {
        public PessoaMapper() {
            CreateMap<Pessoa, PessoaDTO>()
                .ReverseMap();
        }
    }
}