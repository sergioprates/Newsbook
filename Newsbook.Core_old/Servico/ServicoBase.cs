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
    public class ServicoBase<TEntity> : IServicoBase<TEntity> where TEntity : class
    {
        private readonly IRepositorioBase<TEntity> _repositorio;

        public ServicoBase(IRepositorioBase<TEntity> repositorio)
        {
            _repositorio = repositorio;
        }

        public virtual long Salvar(TEntity obj)
        {
            var id = obj.GetType().GetRuntimeProperties().ToList().FirstOrDefault(x=> x.Name == "Id");
            if (id != null && Convert.ToInt64(id.GetValue(obj)) != 0)
            {
                _repositorio.Alterar(obj);
            }
            else
            {
                obj = _repositorio.Inserir(obj);
                id = obj.GetType().GetRuntimeProperties().ToList().FirstOrDefault(x => x.Name == "Id");
                return Convert.ToInt64(id.GetValue(obj));
            }
            
            return Convert.ToInt64(id.GetValue(obj));
        }

        public virtual TEntity Inserir(TEntity obj)
        {
           return _repositorio.Inserir(obj);
        }

        public TEntity BuscarPorId(long id)
        {
            return _repositorio.BuscarPorId(id);
        }

        public List<TEntity> Listar()
        {
            return _repositorio.Listar();
        }

        public virtual void Alterar(TEntity obj)
        {
            _repositorio.Alterar(obj);
        }

        public void Deletar(TEntity obj)
        {
            _repositorio.Deletar(obj);
        }
    }
}
