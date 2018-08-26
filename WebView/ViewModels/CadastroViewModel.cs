using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebView.ViewModels {
    public class CadastroViewModel {
        public int Id { get; set; } 
        [Required]
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        [Display(Name = "Data de nascimento")]
        public DateTime? DataNascimento { get; set; }

        public IList<TelefoneViewModel> Telefones { get; set; }
    }
}