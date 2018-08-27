using System.Web.Http;
using AgendaNUINF.API.Models;
using AgendaNUINF.API.Models.Exceptions;
using AgendaNUINF.EntidadesDTO;

namespace AgendaNUINF.API.Controllers {
    [RoutePrefix("api/pessoas/{idPessoa}")]
    public class TelefonesController : ApiController {
        private readonly TelefoneNegocio _telefoneNegocio;

        public TelefonesController(TelefoneNegocio telefoneNegocio) {
            _telefoneNegocio = telefoneNegocio;
        }

        [Route("telefones")]
        public IHttpActionResult Post(int idPessoa, [FromBody] TelefoneDTO telefoneDto) {
            if (idPessoa <= 0)
                return BadRequest("Informe um id válido");

            try {
                _telefoneNegocio.Adicionar(idPessoa, telefoneDto);
            } catch (PessoaNaoEncontradaException) {
                return NotFound();
            }

            return Created<PessoaDTO>($"api/pessoas/{idPessoa}/telefones", null);
        }

        [Route("telefones/{id}")]
        public IHttpActionResult Delete(int id, int idPessoa) {
            if (idPessoa <= 0 || id <= 0)
                return BadRequest("Informe um id válido");

            _telefoneNegocio.Remover(id);
            return Ok();
        }
        
        [Route("telefones")]
        public IHttpActionResult GetPorPessoa(int idPessoa) {
            if (idPessoa <= 0)
                return BadRequest("Informe um id válido");

            return Ok(_telefoneNegocio.Listar(idPessoa));
        }
    }
}