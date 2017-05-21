using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Interface.Servico
{
    public interface IServicoBase<TEntity, TKey> where TEntity : class
    {
        TEntity Inserir(TEntity obj);
        TEntity BuscarPorId(TKey id);
        List<TEntity> Listar();
        
        void Alterar(TKey id, TEntity obj);
        void Deletar(TKey id);
    }
}
