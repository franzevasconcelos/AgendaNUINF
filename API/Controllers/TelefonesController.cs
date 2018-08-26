using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AgendaNUINF.API.Models;
using AgendaNUINF.EntidadesDTO;

namespace AgendaNUINF.API.Controllers {
    [RoutePrefix("api/pessoas/{idPessoa}")]
    public class TelefonesController : ApiController {
        private readonly TelefoneNegocio _telefoneNegocio;

        public TelefonesController(TelefoneNegocio telefoneNegocio) {
            _telefoneNegocio = telefoneNegocio;
        }

        // POST: api/Telefones
        [Route("telefones")]
        public void Post(int idPessoa, [FromBody] TelefoneDTO telefoneDto) {
            _telefoneNegocio.Adicionar(idPessoa, telefoneDto);
        }

        // DELETE: api/Telefones/5
        [Route("telefones/{id}")]
        public void Delete(int id, int idPessoa) {
            _telefoneNegocio.Remover(id);
        }
        
        [Route("telefones")]
        public IHttpActionResult GetPorPessoa(int idPessoa) {
            return Ok(_telefoneNegocio.Listar(idPessoa));
        }
    }
}