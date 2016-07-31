using Newsbook.Core.Interface.Repositorio;
using Newsbook.Infraestrutura.Dados.MYSQL.Contexto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Infraestrutura.Dados.MYSQL.Repositorio
{
    public class RepositorioBase<TEntity> : IRepositorioBase<TEntity> where TEntity :class
    {
        protected readonly ContextoDb _repositorio;
        protected readonly string strConexao = ConfigurationManager.ConnectionStrings["Newsbook.Core"].ToString();
        public RepositorioBase()
        {
            _repositorio = new ContextoDb();
        }

        public virtual TEntity Inserir(TEntity obj)
        {
            obj = _repositorio.Set<TEntity>().Add(obj);
            _repositorio.commit();
            return obj;
        }

        public virtual TEntity BuscarPorId(long id)
        {
            return _repositorio.Set<TEntity>().Find(id);
        }

        public virtual List<TEntity> Listar()
        {
            return _repositorio.Set<TEntity>().ToList();
        }

        public virtual void Alterar(TEntity obj)
        {
            _repositorio.Entry(obj).State = EntityState.Modified;
            _repositorio.commit();
        }

        public virtual void Deletar(TEntity obj)
        {
            _repositorio.Set<TEntity>().Remove(obj);
            _repositorio.commit();
        }

        public virtual void Dispose()
        {
            _repositorio.Dispose();
        }
    }
}
