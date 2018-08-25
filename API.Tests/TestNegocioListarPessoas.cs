using System.Collections.Generic;
using AgendaNUINF.API.Entidades;
using AgendaNUINF.API.Models;
using AgendaNUINF.API.Models.Exceptions;
using AgendaNUINF.API.Models.Persistencia;
using AutoMapper;
using NHibernate;
using NSubstitute;
using NUnit.Framework;

namespace API.Tests {
    [TestFixture]
    public class TestNegocioListarPessoas : BaseTest {
        [Test]
        public void DeveListarTodasPessoas() {
            var pessoaRepository = Substitute.For<PessoaRepository>((ISession) null);
            pessoaRepository.Todas()
                            .Returns(new List<Pessoa>
                                     {
                                         new Pessoa
                                         {
                                             Id = 1,
                                             Nome = "Fulano"
                                         },
                                         new Pessoa
                                         {
                                             Id = 2,
                                             Nome = "Beltrano"
                                         }
                                     });

            var pessoaNegocio = new PessoaNegocio(pessoaRepository, Mapper.Instance);

            var retorno = pessoaNegocio.ListarPessoas();

            Assert.That(retorno.Count, Is.EqualTo(2));
            Assert.That(retorno[0].Id, Is.EqualTo(1));
            Assert.That(retorno[1].Id, Is.EqualTo(2));
        }

        [Test]
        public void DeveTrazerPessoaPorId() {
            var pessoaRepository = Substitute.For<PessoaRepository>((ISession) null);
            pessoaRepository.PorId(3)
                            .Returns(new Pessoa
                                     {
                                         Id = 3,
                                         Nome = "Fulano"
                                     });
            var pessoaNegocio = new PessoaNegocio(pessoaRepository, Mapper.Instance);
            var retorno = pessoaNegocio.PorId(3);

            Assert.That(retorno, Is.Not.Null);
            Assert.That(retorno.Id, Is.EqualTo(3));
        }

        [Test]
        public void DeveLancarExcecaoAoBuscarPorPessoaInexistente() {
            var pessoaRepository = Substitute.For<PessoaRepository>((ISession) null);
            pessoaRepository.PorId(4).Returns((Pessoa) null);
            var pessoaNegocio = new PessoaNegocio(pessoaRepository, Mapper.Instance);


            Assert.Throws<PessoaNaoEncontradaException>(() => { pessoaNegocio.PorId(4); });
        }
    }
}