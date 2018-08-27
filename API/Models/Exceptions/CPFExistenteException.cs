using System;

namespace AgendaNUINF.API.Models.Exceptions {
    public class CPFExistenteException : Exception {
        public CPFExistenteException() { }

        public CPFExistenteException(string mensagem) : base(mensagem) { }
    }
}