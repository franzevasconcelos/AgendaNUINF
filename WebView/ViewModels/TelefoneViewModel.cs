using System.ComponentModel.DataAnnotations;

namespace WebView.ViewModels {
    public class TelefoneViewModel {
        public int Id { get; set; }
        public string DDD { get; set; }
        [Display(Name = "Número")]
        public string Numero { get; set; }
    }
}