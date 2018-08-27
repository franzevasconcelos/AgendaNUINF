using AgendaNUINF.API.Mapeamentos;
using AgendaNUINF.API.Models;
using AgendaNUINF.API.Models.Persistencia;
using AutoMapper;
using AutoMapper.Configuration;
using NHibernate;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace AgendaNUINF.API.Configuracoes {
    public class SimpleInjector {
        public static Container ObterContainer() {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<PessoaRepository>(Lifestyle.Transient);
            container.Register<PessoaNegocio>(Lifestyle.Transient);
            container.Register<TelefoneNegocio>(Lifestyle.Transient);

            container.Register<ISession>(() => NhibernateSetup.SessionFactory.OpenSession(),
                                         new AsyncScopedLifestyle());

            container.RegisterSingleton<IMapper>(() => GetMapper(container));

            return container;
        }

        public static IMapper GetMapper(Container container) {
            var mce = new MapperConfigurationExpression();
            mce.ConstructServicesUsing(container.GetInstance);
            mce.AddProfiles(typeof(PessoaMapper).Assembly);

            var mc = new MapperConfiguration(mce);
            mc.AssertConfigurationIsValid();

            IMapper mapper = new Mapper(mc, t => container.GetInstance(t));

            return mapper;
        }
    }
}