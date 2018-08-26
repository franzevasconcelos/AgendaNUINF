using System.Collections.Generic;
using AgendaNUINF.API.Models.Persistencia;
using AgendaNUINF.EntidadesDTO;
using AutoMapper;

namespace AgendaNUINF.API.Models {
    public class TelefoneNegocio {
        private readonly PessoaRepository _pessoaRepository;
        private readonly IMapper _mapper;

        public TelefoneNegocio(PessoaRepository pessoaRepository, IMapper mapper) {
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
        }

        public virtual void Remover(int id) {
            _pessoaRepository.RemoverTelefone(id);
        }

        public virtual IList<TelefoneDTO> Listar(int idPessoa) {
            var telefones = _pessoaRepository.TelefonesPorPessoa(idPessoa);
            return _mapper.Map<List<TelefoneDTO>>(telefones);
        }
    }
}