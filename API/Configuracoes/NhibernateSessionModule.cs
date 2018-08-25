using System;
using System.Web;
using NHibernate;
using NHibernate.Context;

namespace AgendaNUINF.API.Configuracoes {
    public class NhibernateSessionModule : IHttpModule {
        public void Init(HttpApplication context) {
            context.BeginRequest += delegate(object sender, EventArgs args) {
                CurrentSessionContext.Bind(NhibernateSetup.SessionFactory.OpenSession());
            };

            context.EndRequest += delegate(object sender, EventArgs args) {
                ISession session = CurrentSessionContext.Unbind(NhibernateSetup.SessionFactory);
                session.Flush();
                session.Close();
                session.Dispose();
            };
        }

        public void Dispose() { }
    }
}