using Newsbook.Core.Interface.Repositorio;
using Newsbook.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Infraestrutura.Dados.MYSQL.Repositorio
{
    public class CategoriaRepositorio : RepositorioBase<Categoria>, ICategoriaRepositorio
    {
        public Categoria BuscarPorNome(string nome)
        {
            return _repositorio.Set<Categoria>().FirstOrDefault(x => x.Nome.ToUpper() == nome.ToUpper());
        }
    }
}
