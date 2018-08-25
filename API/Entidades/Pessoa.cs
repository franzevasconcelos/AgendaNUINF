using System;
using System.Collections.Generic;

namespace AgendaNUINF.API.Entidades {
    public class Pessoa : EntidadeBase {
        public virtual string Nome { get; set; }
        public virtual string CPF { get; set; }
        public virtual DateTime? DataNascimento { get; set; }
        public virtual string Email { get; set; }
        public virtual IList<Telefone> Telefones { get; set; }
    }
}