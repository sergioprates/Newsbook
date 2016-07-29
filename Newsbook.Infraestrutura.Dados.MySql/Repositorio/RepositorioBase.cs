using Newsbook.Core.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Infraestrutura.Dados.MySql.Repositorio
{
    public class RepositorioBase<TEntity> : IRepositorioBase<TEntity> where TEntity :class
    {
        public long Inserir(TEntity obj)
        {
            throw new NotImplementedException();
        }

        public TEntity BuscarPorId(long id)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> Listar()
        {
            throw new NotImplementedException();
        }

        public void Alterar(TEntity obj)
        {
            throw new NotImplementedException();
        }

        public void Deletar(TEntity obj)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
