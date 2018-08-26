using System.Collections.Generic;
using System.Web.Http;
using AgendaNUINF.API.Models;
using AgendaNUINF.API.Models.Exceptions;
using AgendaNUINF.EntidadesDTO;

namespace AgendaNUINF.API.Controllers {
    public class PessoasController : ApiController {
        private readonly PessoaNegocio _pessoaNegocio;

        public PessoasController(PessoaNegocio pessoaNegocio) {
            _pessoaNegocio = pessoaNegocio;
        }

        public IEnumerable<PessoaDTO> Get(string nome = null, string cpf = null) {
            return _pessoaNegocio.ListarPessoas(nome, cpf);
        }

        // GET: api/Pessoas/5
        public IHttpActionResult Get(int id) {
            if (id <= 0)
                return BadRequest("Informe um id válido");

            try {
                return Ok(_pessoaNegocio.PorId(id));
            } catch (PessoaNaoEncontradaException) {
                return NotFound();
            }
        }

        // POST: api/Pessoas
        public IHttpActionResult Post([FromBody] PessoaDTO pessoa) {
            var idInserido = _pessoaNegocio.InserirPessoa(pessoa);

            return CreatedAtRoute<PessoaDTO>("DefaultApi", new {controller = "pessoas", id = idInserido}, null);
        }

        // PUT: api/Pessoas/5
        public IHttpActionResult Put(int id, [FromBody] PessoaDTO pessoa) {
            if (id <= 0)
                return BadRequest("Informe um id válido");

            try {
                _pessoaNegocio.Atualizar(pessoa);
                return Ok();
            } catch (PessoaNaoEncontradaException) {
                return NotFound();
            }
        }

        // DELETE: api/Pessoas/5
        public void Delete(int id) {
            _pessoaNegocio.Remover(id);
        }
    }
}