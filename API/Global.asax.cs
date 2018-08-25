using System.Configuration;
using System.Web.Http;
using AgendaNUINF.API.Configuracoes;

namespace AgendaNUINF.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            NhibernateSetup.Init();
           // Mapper.Initialize(cfg => cfg.AddProfiles(typeof(QuestaoMapper).Assembly));

            var caminhoBanco = new AppSettingsReader().GetValue("Banco.Arquivo", typeof(string)).ToString();
            Migrador.Configuracao.RealizarUpgrade(caminhoBanco);
        }
    }
}
