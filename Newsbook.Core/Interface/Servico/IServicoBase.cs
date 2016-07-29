using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Interface.Servico
{
    public interface IServicoBase<TEntity> where TEntity : class
    {
        long Salvar(TEntity obj);

        long Inserir(TEntity obj);
        TEntity BuscarPorId(long id);
        List<TEntity> Listar();
        
        void Alterar(TEntity obj);
        void Deletar(TEntity obj);
    }
}
