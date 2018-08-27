using System;
using System.Collections.Generic;

namespace AgendaNUINF.EntidadesDTO {
    public class PessoaDTO {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public IList<TelefoneDTO> Telefones { get; set; }
    }
}