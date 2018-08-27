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
    class TestNegocioTelefone : BaseTest {
        [Test]
        public void DeveInserirTelefone() {
            var pessoaRepository = Substitute.For<PessoaRepository>((ISession) null);
            pessoaRepository.PorId(1)
                            .Returns(new Pessoa
                                     {
                                         Id = 1,
                                         Nome = "Fulano",
                                         Telefones = new List<Telefone>()
                                     });

            pessoaRepository.Atualizar(Arg.Any<Pessoa>());

            var telefoneNegocio = new TelefoneNegocio(pessoaRepository, Mapper.Instance);

            var telefoneDto = new TelefoneDTO
                              {
                                  DDD = "85",
                                  Numero = "333334444"
                              };

            telefoneNegocio.Adicionar(1, telefoneDto);

            pessoaRepository.Received(1).PorId(1);
            pessoaRepository.Received(1).Atualizar(Arg.Any<Pessoa>());
        }

        [Test]
        public void DeveLancarExcecaoAoInserirTelefoneDePessoaInexistente() {
            var pessoaRepository = Substitute.For<PessoaRepository>((ISession) null);
            pessoaRepository.PorId(1).Returns((Pessoa) null);


            var telefoneNegocio = new TelefoneNegocio(pessoaRepository, Mapper.Instance);


            Assert.Throws<PessoaNaoEncontradaException>(() => telefoneNegocio.Adicionar(1, null));
        }

        [Test]
        public void DeveListarTelefonesDePessoa() {
            var pessoaRepository = Substitute.For<PessoaRepository>((ISession) null);
            pessoaRepository.TelefonesPorPessoa(1)
                            .Returns(new List<Telefone>
                                     {
                                         new Telefone
                                         {
                                             Id = 1
                                         }
                                     });


            var telefoneNegocio = new TelefoneNegocio(pessoaRepository, Mapper.Instance);
            var retorno = telefoneNegocio.Listar(1);

            Assert.That(retorno.Count, Is.EqualTo(1));
        }
    }
}