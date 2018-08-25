using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AgendaNUINF.API.Entidades;

namespace AgendaNUINF.API.Controllers {
    public class PessoasController : ApiController {
        // GET: api/Pessoas
        public IEnumerable<Pessoa> Get() {
            return new List<Pessoa>
                   {
                       new Pessoa
                       {
                           Id = 1,
                           Nome = "Fulano da Silva",
                           Telefones = new List<Telefone>
                                       {
                                           new Telefone
                                           {
                                               Id = 1,
                                               DDD = "85",
                                               Numero =
                                                   "999998888"
                                           }
                                       }
                       }
                   };
        }

        // GET: api/Pessoas/5
        public string Get(int id) {
            return "value";
        }

        // POST: api/Pessoas
        public void Post([FromBody] string value) { }

        // PUT: api/Pessoas/5
        public void Put(int id, [FromBody] string value) { }

        // DELETE: api/Pessoas/5
        public void Delete(int id) { }
    }
}