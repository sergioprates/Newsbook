using Newsbook.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Interface.Repositorio
{
    public interface INoticiaRepositorio : IRepositorioBase<Noticia>
    {
        Noticia Buscar(string url);

        List<Noticia> Listar(DateTime data);
    }
}
