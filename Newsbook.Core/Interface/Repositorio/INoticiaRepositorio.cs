using Newsbook.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Interface.Repositorio
{
    public interface INoticiaRepositorio : IRepositorioBase<Noticia, string>
    {
        Noticia Buscar(string url);

        List<Noticia> Listar(DateTime data);

        List<Noticia> Listar(FeedUrl feedUrl);

        List<Noticia> Listar(FeedUrl feedUrl, int limit);

        List<Noticia> Listar(FeedUrl feedUrl, int limit, int skip);


        List<Noticia> Listar(int limit);
        List<Noticia> Listar(int limit, int skip);
    }
}
