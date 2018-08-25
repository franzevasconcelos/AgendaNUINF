using System.Collections.Generic;
using System.Web.Http.Results;
using AgendaNUINF.API.Controllers;
using AgendaNUINF.API.Models;
using AgendaNUINF.API.Models.Exceptions;
using AgendaNUINF.EntidadesDTO;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace API.Tests {
    [TestFixture]
    class TestControllerPessoas {
        [Test]
        public void DeveTrazerTodasPessoas() {
            var pessoaNegocio = Substitute.For<PessoaNegocio>(null, null);
            pessoaNegocio.ListarPessoas().Returns(new List<PessoaDTO>());

            var pessoasController = new PessoasController(pessoaNegocio);
            var retorno = pessoasController.Get();

            Assert.IsInstanceOf<IList<PessoaDTO>>(retorno);
        }

        [Test]
        public void DeveBuscarPessoaPorId() {
            var pessoaNegocio = Substitute.For<PessoaNegocio>(null, null);
            pessoaNegocio.PorId(1).Returns(new PessoaDTO());

            var pessoasController = new PessoasController(pessoaNegocio);
            var retorno = pessoasController.Get(1);

            Assert.IsInstanceOf<OkNegotiatedContentResult<PessoaDTO>>(retorno);
        }

        [Test]
        public void DeveRetornarBadRequestAoBuscarPessoaPorIdInvalido() {
            var pessoaNegocio = Substitute.For<PessoaNegocio>(null, null);
            pessoaNegocio.PorId(1).Returns(new PessoaDTO());

            var pessoasController = new PessoasController(pessoaNegocio);
            var retorno = pessoasController.Get(-1);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(retorno);
        }

        [Test]
        public void DeverRetornarNotFoundQuandoAoBuscarPessoaQueNaoExiste() {
            var pessoaNegocio = Substitute.For<PessoaNegocio>(null, null);
            pessoaNegocio.PorId(1).Throws(new PessoaNaoEncontradaException());

            var pessoasController = new PessoasController(pessoaNegocio);
            var retorno = pessoasController.Get(1);

            Assert.IsInstanceOf<NotFoundResult>(retorno);
        }

        [Test]
        public void DeveRetornarCreatedAoInserirNovaPessoa() {
            var pessoaNegocio = Substitute.For<PessoaNegocio>(null, null);

            var pessoasController = new PessoasController(pessoaNegocio);
            var retorno = pessoasController.Post(new PessoaDTO());

            Assert.IsInstanceOf<CreatedAtRouteNegotiatedContentResult<PessoaDTO>>(retorno);
        }

        [Test]
        public void DeveRetornarOkAoAtualizarUmaPessoa() {
            var pessoaNegocio = Substitute.For<PessoaNegocio>(null, null);

            var pessoasController = new PessoasController(pessoaNegocio);
            var retorno = pessoasController.Put(1, new PessoaDTO());

            Assert.IsInstanceOf<OkResult>(retorno);
        }

        [Test]
        public void DeveRetornarNotFoundCasoNaoEncontrePessoaAoAtualizar() {
            var pessoaNegocio = Substitute.For<PessoaNegocio>(null, null);
            pessoaNegocio.When(p => p.Atualizar(Arg.Any<PessoaDTO>()))
                         .Do(d => throw new PessoaNaoEncontradaException());

            var pessoasController = new PessoasController(pessoaNegocio);
            var retorno = pessoasController.Put(1, new PessoaDTO());

            Assert.IsInstanceOf<NotFoundResult>(retorno);
        }

        [Test]
        public void DeveRetornarBadRequestAoAtualizarPessoaComIdInvalido() {
            var pessoaNegocio = Substitute.For<PessoaNegocio>(null, null);
            pessoaNegocio.PorId(1).Returns(new PessoaDTO());

            var pessoasController = new PessoasController(pessoaNegocio);
            var retorno = pessoasController.Put(-1, new PessoaDTO());

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(retorno);
        }

        [Test]
        public void DeveDeletarPessoaComSucesso() {
            var pessoaNegocio = Substitute.For<PessoaNegocio>(null, null);

            var pessoasController = new PessoasController(pessoaNegocio);

            Assert.DoesNotThrow(() => pessoasController.Delete(1));
        }
    }
}