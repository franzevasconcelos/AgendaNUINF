using System;
using AgendaNUINF.EntidadesDTO;
using AutoMapper;
using WebView.ViewModels;

namespace WebView.Models {
    public class ListagemViewModelMapper : Profile {
        public ListagemViewModelMapper() {
            CreateMap<PessoaDTO, ListagemViewModel>()
                .ForMember(dest => dest.Idade,
                           opt => opt.MapFrom(src => CalculaIdade(src)))
                .ForMember(dest => dest.QuantidadeTelefones, opt => opt.MapFrom(src => src.Telefones.Count));
        }

        private static string CalculaIdade(PessoaDTO src) {
            if (src.DataNascimento == null) return null;

            var idade = Convert.ToInt32((DateTime.Now - src.DataNascimento).Value.TotalDays / 365).ToString();
            return $"{idade} anos";
        }
    }
}