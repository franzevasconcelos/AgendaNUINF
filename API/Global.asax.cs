using System.Configuration;
using System.Web.Http;
using AgendaNUINF.API.Configuracoes;
using AgendaNUINF.API.Mapeamentos;
using AutoMapper;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace AgendaNUINF.API {
    public class WebApiApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            NhibernateSetup.Init();

            //Migração inicial
            var caminhoBanco = new AppSettingsReader().GetValue("Banco.Arquivo", typeof(string)).ToString();
            Migrador.Configuracao.RealizarUpgrade(caminhoBanco);

            //Configurando injeção de dependência
            var container = Configuracoes.SimpleInjector.ObterContainer();
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}