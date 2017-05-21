using Newsbook.Core.Interface.Repositorio;
using Newsbook.Core.Interface.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Servico
{
    public class ServicoBase<TEntity, TKey> : IServicoBase<TEntity, TKey> where TEntity : class
    {
        private readonly IRepositorioBase<TEntity, TKey> _repositorio;

        public ServicoBase(IRepositorioBase<TEntity, TKey> repositorio)
        {
            _repositorio = repositorio;
        }

        public virtual TEntity Inserir(TEntity obj)
        {
           return _repositorio.Inserir(obj);
        }
        public List<TEntity> Listar()
        {
            return _repositorio.Listar();
        }

        public virtual void Alterar(TKey id, TEntity obj)
        {
            _repositorio.Alterar(id, obj);
        }

        public virtual void Deletar(TKey id)
        {
            _repositorio.Deletar(id);
        }

        public virtual TEntity BuscarPorId(TKey id)
        {
            return _repositorio.BuscarPorId(id);
        }
    }
}
