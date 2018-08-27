using AgendaNUINF.EntidadesDTO;
using AutoMapper;
using WebView.ViewModels;

namespace WebView.Models {
    public class CadastroViewModelMapper : Profile {
        public CadastroViewModelMapper() {
            CreateMap<CadastroViewModel, PessoaDTO>();

            CreateMap<TelefoneViewModel, TelefoneDTO>();
        }
    }
}