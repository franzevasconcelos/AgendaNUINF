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
    class TestControllerTelefones {
        [Test]
        public void DeveTrazerTodasPessoas() {
            var pessoaNegocio = Substitute.For<PessoaNegocio>(null, null);
            pessoaNegocio.ListarPessoas().Returns(new List<PessoaDTO>());

            var pessoasController = new PessoasController(pessoaNegocio);
            var retorno = pessoasController.Get();

            Assert.IsInstanceOf<IList<PessoaDTO>>(retorno);
        }

        [Test]
        public void DeveRetornarBadRequestAoBuscarTelefonesPorPessoaComIdInvalido() {
            var telefoneNegocio = Substitute.For<TelefoneNegocio>(null, null);

            var controller = new TelefonesController(telefoneNegocio);
            var retorno = controller.GetPorPessoa(-1);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(retorno);
        }

        [Test]
        public void DeveRetornarBadRequestAoDeletarTelefoneComIdInvalido() {
            var telefoneNegocio = Substitute.For<TelefoneNegocio>(null, null);

            var controller = new TelefonesController(telefoneNegocio);
            var retorno = controller.Delete(-1, 1);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(retorno);
        }

        [Test]
        public void DeveRetornarBadRequestAoAdicionarTelefoneComIdInvalido() {
            var telefoneNegocio = Substitute.For<TelefoneNegocio>(null, null);

            var controller = new TelefonesController(telefoneNegocio);
            var retorno = controller.Post(-1, null);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(retorno);
        }


        [Test]
        public void DeverRetornarNotFoundQuandoAdicionarTelefoneAPessoaQueNaoExiste() {
            var telefoneNegocio = Substitute.For<TelefoneNegocio>(null, null);
            telefoneNegocio.When(t => t.Adicionar(1, Arg.Any<TelefoneDTO>()))
                           .Do(t => throw new PessoaNaoEncontradaException());

            var controller = new TelefonesController(telefoneNegocio);
            var retorno = controller.Post(1, null);

            Assert.IsInstanceOf<NotFoundResult>(retorno);
        }

        [Test]
        public void DeveAdicionarTelefoneComSucesso() {
            var telefoneNegocio = Substitute.For<TelefoneNegocio>(null, null);

            var controller = new TelefonesController(telefoneNegocio);
            var retorno = controller.Post(1, null);

            Assert.IsInstanceOf<CreatedNegotiatedContentResult<PessoaDTO>>(retorno);

        }
    }
}