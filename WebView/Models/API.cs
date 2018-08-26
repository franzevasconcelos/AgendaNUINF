using System.Configuration;

namespace WebView.Models {
    public class API {
        private static readonly string UrlBase = new AppSettingsReader().GetValue("URL.API", typeof(string)).ToString();

        public static string Pessoas() {
            return $"{UrlBase}/api/pessoas";
        }

        public static string Pessoa(int id) {
            return $"{UrlBase}/api/pessoas/{id}";
        }

        public static string Telefones(int idPessoa) {
            return $"{UrlBase}/api/pessoas/{idPessoa}/telefones";
        }

        public static string Telefone(int idTelefone, int idPessoa) {
            return $"{UrlBase}/api/pessoas/{idPessoa}/telefones{idTelefone}";
        }
    }
}