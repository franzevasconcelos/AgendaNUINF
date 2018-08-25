using System.Collections.Generic;
using AgendaNUINF.API.Entidades;
using NHibernate;

namespace AgendaNUINF.API.Models.Persistencia {
    public class PessoaRepository {
        private readonly ISession _sessao;

        public PessoaRepository(ISession sessao) {
            _sessao = sessao;
        }

        public virtual int Inserir(Pessoa pessoa) {
            return (int) _sessao.Save(pessoa);
        }

        public virtual void Atualizar(Pessoa pessoa) {
            _sessao.SaveOrUpdate(pessoa);
        }

        public virtual Pessoa PorId(int id) {
            return _sessao.QueryOver<Pessoa>()
                          .Where(p => p.Id == id)
                          .SingleOrDefault();
        }

        public virtual IList<Pessoa> Todas() {
            return _sessao.QueryOver<Pessoa>().List();
        }

        public virtual void Remover(int id) {
            _sessao.CreateQuery("DELETE PESSOA WHERE Id = :id")
                   .SetInt32("id", id)
                   .ExecuteUpdate();
        }
    }
}