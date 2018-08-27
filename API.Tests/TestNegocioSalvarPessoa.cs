using System.Collections.Generic;
using AgendaNUINF.API.Entidades;
using AgendaNUINF.API.Models;
using AgendaNUINF.API.Models.Exceptions;
using AgendaNUINF.API.Models.Persistencia;
using AgendaNUINF.EntidadesDTO;
using AutoMapper;
using NHibernate;
using NSubstitute;
using NUnit.Framework;

namespace API.Tests {
    [TestFixture]
    class TestNegocioSalvarPessoa : BaseTest {
        [Test]
        public void DeveInserirPessoa() {
            var pessoaRepository = Substitute.For<PessoaRepository>((ISession) null);
            pessoaRepository.Inserir(Arg.Any<Pessoa>());

            var pessoaNegocio = new PessoaNegocio(pessoaRepository, Mapper.Instance);

            var pessoaDto = new PessoaDTO
                            {
                                Nome = "Fulano da Silva"
                            };

            pessoaNegocio.InserirPessoa(pessoaDto);

            pessoaRepository.Received(1).Inserir(Arg.Any<Pessoa>());
        }

        [Test]
        public void DeveAtualizarPessoa() {
            var pessoaRepository = Substitute.For<PessoaRepository>((ISession) null);
            pessoaRepository.Atualizar(Arg.Any<Pessoa>());

            var pessoaEncontrada = PessoaEncontrada();
            pessoaRepository.PorId(1).Returns(pessoaEncontrada);
            
            var pessoaNegocio = new PessoaNegocio(pessoaRepository, null);

            var pessoaDto = PessoaDto();
            pessoaNegocio.Atualizar(pessoaDto);

            pessoaRepository.Received(1).Atualizar(pessoaEncontrada);
        }

        [Test]
        public void DeveAtualizarPessoaEInserirNovoTelefone()
        {
            var pessoaRepository = Substitute.For<PessoaRepository>((ISession)null);
            pessoaRepository.Atualizar(Arg.Any<Pessoa>());

            var pessoaEncontrada = PessoaEncontrada();
            pessoaRepository.PorId(1).Returns(pessoaEncontrada);

            var pessoaDto = PessoaDto();
            pessoaDto.Telefones.Add(new TelefoneDTO
                                    {
                                        Numero = "444"
                                    });

            var mapper = Substitute.For<IMapper>();
            mapper.Map<Telefone>(pessoaDto.Telefones[0])
                  .Returns(new Telefone
                           {
                               Numero = "444"
                           });

            var pessoaNegocio = new PessoaNegocio(pessoaRepository, mapper);

            pessoaNegocio.Atualizar(pessoaDto);

            pessoaRepository.Received(1).Atualizar(pessoaEncontrada);
        }

        [Test]
        public void DeveLancarExcecaoAoTentarAtualizarPessoaInexistente() {
            var pessoaRepository = Substitute.For<PessoaRepository>((ISession)null);
            pessoaRepository.PorId(1).Returns((Pessoa) null);

            var pessoaNegocio = new PessoaNegocio(pessoaRepository, null);

            Assert.Throws<PessoaNaoEncontradaException>(() => pessoaNegocio.Atualizar(PessoaDto()));
        }

        [Test]
        public void DeveLancarExcecaoAoInserirNovaPessoaComCpfJaExistente() {
            var pessoaRepository = Substitute.For<PessoaRepository>((ISession)null);
            pessoaRepository.Pequisa(null, "1").Returns(new List<Pessoa> {new Pessoa()});

            var pessoaNegocio = new PessoaNegocio(pessoaRepository, null);

            var pessoaDto = PessoaDto();
            pessoaDto.CPF = "1";
            Assert.Throws<CPFExistenteException>(() => pessoaNegocio.InserirPessoa(pessoaDto));

        }

        private static PessoaDTO PessoaDto() {
            PessoaDTO dto = new PessoaDTO
                            {
                                Id = 1,
                                Nome = "Fulano da Silva",
                                Telefones = new List<TelefoneDTO>
                                            {
                                                new TelefoneDTO
                                                {
                                                    Id = 1,
                                                    Numero = "999"
                                                }
                                            }
                            };
            return dto;
        }

        private static Pessoa PessoaEncontrada() {
            return new Pessoa
                   {
                       Id = 1,
                       Nome = "Fulano",
                       Telefones = new List<Telefone>
                                   {
                                       new Telefone
                                       {
                                           Id = 1,
                                           Numero = "888"
                                       }
                                   }
                   };
        }
    }
}