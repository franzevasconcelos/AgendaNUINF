using System.Collections.Generic;
using System.Linq;
using AgendaNUINF.API.Entidades;
using AgendaNUINF.API.Models.Exceptions;
using AgendaNUINF.API.Models.Persistencia;
using AgendaNUINF.EntidadesDTO;
using AutoMapper;

namespace AgendaNUINF.API.Models {
    public class PessoaNegocio {
        private readonly PessoaRepository _pessoaRepository;
        private readonly IMapper _mapper;

        public PessoaNegocio(PessoaRepository pessoaRepository, IMapper mapper) {
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
        }

        public virtual int InserirPessoa(PessoaDTO pessoa) {
            var pessoaASalvar = _mapper.Map<Pessoa>(pessoa);
            foreach (var telefone in pessoaASalvar.Telefones) {
                telefone.Pessoa = pessoaASalvar;
            }

            var idPessoa = _pessoaRepository.Inserir(pessoaASalvar);

            return idPessoa;
        }

        public virtual void Atualizar(PessoaDTO pessoaDto) {
            var pessoaEncontrada = _pessoaRepository.PorId(pessoaDto.Id);

            if (pessoaEncontrada == null)
                throw new PessoaNaoEncontradaException();

            pessoaEncontrada.Nome = pessoaDto.Nome;
            pessoaEncontrada.DataNascimento = pessoaDto.DataNascimento;
            pessoaEncontrada.Email = pessoaDto.Email;
            pessoaEncontrada.CPF = pessoaDto.CPF;

            foreach (var telefoneDto in pessoaDto.Telefones) {
                if (telefoneDto.Id == 0) {
                    var novoTelefone = _mapper.Map<Telefone>(telefoneDto);
                    novoTelefone.Pessoa = pessoaEncontrada;
                    pessoaEncontrada.Telefones.Add(novoTelefone);
                    continue;
                }

                var telefone = pessoaEncontrada.Telefones.SingleOrDefault(t => t.Id == telefoneDto.Id);
                telefone.DDD = telefoneDto.DDD;
                telefone.Numero = telefoneDto.Numero;
            }

            _pessoaRepository.Atualizar(pessoaEncontrada);
        }


        public virtual IList<PessoaDTO> ListarPessoas() {
            return _mapper.Map<IList<PessoaDTO>>(_pessoaRepository.Todas());
        }

        public virtual IList<PessoaDTO> ListarPessoas(string nome, string cpf) {
            return _mapper.Map<IList<PessoaDTO>>(_pessoaRepository.Pequisa(nome, cpf));
        }

        public virtual PessoaDTO PorId(int id) {
            var pessoa = _pessoaRepository.PorId(id);

            if(pessoa == null)
                throw new PessoaNaoEncontradaException();

            return _mapper.Map<PessoaDTO>(pessoa);
        }

        public virtual void Remover(int id) {
            _pessoaRepository.Remover(id);
        }
    }
}