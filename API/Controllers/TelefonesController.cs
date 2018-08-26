using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AgendaNUINF.API.Models;

namespace AgendaNUINF.API.Controllers {
    public class TelefonesController : ApiController {
        private readonly TelefoneNegocio _telefoneNegocio;

        public TelefonesController(TelefoneNegocio telefoneNegocio) {
            _telefoneNegocio = telefoneNegocio;
        }

        // GET: api/Telefones
        public IEnumerable<string> Get() {
            return new string[] {"value1", "value2"};
        }

        // GET: api/Telefones/5
        public string Get(int id) {
            return "value";
        }

        // POST: api/Telefones
        public void Post([FromBody] string value) { }

        // PUT: api/Telefones/5
        public void Put(int id, [FromBody] string value) { }

        // DELETE: api/Telefones/5
        [Route("api/pessoas/{idPessoa}/telefones/{id}")]
        public void Delete(int id, int idPessoa) {
            _telefoneNegocio.Remover(id);
        }
        
        [Route("api/pessoas/{idPessoa}/telefones")]
        public IHttpActionResult GetPorPessoa(int idPessoa) {
            return Ok(_telefoneNegocio.Listar(idPessoa));
        }
    }
}