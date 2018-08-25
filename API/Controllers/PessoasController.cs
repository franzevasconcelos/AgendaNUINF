using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using AgendaNUINF.API.Models;
using AgendaNUINF.EntidadesDTO;

namespace AgendaNUINF.API.Controllers {
    public class PessoasController : ApiController {
        private readonly PessoaNegocio _pessoaNegocio;

        public PessoasController(PessoaNegocio pessoaNegocio) {
            _pessoaNegocio = pessoaNegocio;
        }

        // GET: api/Pessoas
        public IEnumerable<PessoaDTO> Get() {
            return _pessoaNegocio.ListarPessoas();
        }

        // GET: api/Pessoas/5
        public PessoaDTO Get(int id) {
            return _pessoaNegocio.PorId(id);
        }

        // POST: api/Pessoas
        public IHttpActionResult Post([FromBody] PessoaDTO pessoa) {
            var idInserido =_pessoaNegocio.InserirPessoa(pessoa);

            return CreatedAtRoute<PessoaDTO>("DefaultApi", new {controller = "pessoas", id = idInserido}, null);
        }

        // PUT: api/Pessoas/5
        public void Put(int id, [FromBody] PessoaDTO pessoa) {
            _pessoaNegocio.Atualizar(pessoa);
        }

        // DELETE: api/Pessoas/5
        public void Delete(int id) { }
    }
}