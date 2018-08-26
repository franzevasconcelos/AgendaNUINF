using System.Collections.Generic;
using AgendaNUINF.API.Entidades;
using AgendaNUINF.API.Models.Exceptions;
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

        public void Adicionar(int idPessoa, TelefoneDTO telefoneDto) {
            var pessoa = _pessoaRepository.PorId(idPessoa);

            if(pessoa == null)
                throw new PessoaNaoEncontradaException();

            var telefone = _mapper.Map<Telefone>(telefoneDto);
            pessoa.Telefones.Add(telefone);
            telefone.Pessoa = pessoa;

            _pessoaRepository.Atualizar(pessoa);
        }
    }
}