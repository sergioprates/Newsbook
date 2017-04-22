using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Interface.Repositorio
{
    public interface IRepositorioBase<TEntity> : IDisposable
        where TEntity : class
    {
        TEntity Inserir(TEntity obj);
        TEntity BuscarPorId(long id);
        List<TEntity> Listar();
        void Alterar(TEntity obj);
        void Deletar(TEntity obj);
    }
}
