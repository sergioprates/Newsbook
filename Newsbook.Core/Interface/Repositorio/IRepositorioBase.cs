using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Interface.Repositorio
{
    public interface IRepositorioBase<T, TKey> : IDisposable
    {
        void Alterar(TKey id, T obj);
        T BuscarPorId(TKey id);
        void Deletar(TKey id);
        T Inserir(T obj);
        List<T> Listar();
        

    }
}
