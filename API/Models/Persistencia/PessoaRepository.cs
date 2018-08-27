using System.Collections.Generic;
using AgendaNUINF.API.Entidades;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

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
            _sessao.Flush();
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
            _sessao.CreateQuery("DELETE Pessoa WHERE Id = :id")
                   .SetInt32("id", id)
                   .ExecuteUpdate();
        }

        public virtual void RemoverTelefone(int id) {
            _sessao.CreateQuery("DELETE Telefone WHERE Id = :id")
                   .SetInt32("id", id)
                   .ExecuteUpdate();
        }

        public virtual IList<Telefone> TelefonesPorPessoa(int id) {
            return _sessao.QueryOver<Telefone>()
                   .Where(t => t.Pessoa.Id == id)
                   .List();
        }

        public virtual IList<Pessoa> Pequisa(string nome, string cpf) {
            var query = _sessao.QueryOver<Pessoa>();

            if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(cpf))
                query.Where(p => p.Nome.IsLike(nome, MatchMode.Anywhere))
                     .And(p => p.CPF == cpf);

            if (string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(cpf))
                query.Where(p => p.CPF == cpf);

            if (!string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(cpf))
                query.Where(p => p.Nome.IsLike(nome, MatchMode.Anywhere));

            return query.List();
        }
    }
}