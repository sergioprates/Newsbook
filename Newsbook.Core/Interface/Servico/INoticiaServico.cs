using Newsbook.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Interface.Servico
{
    public interface INoticiaServico : IServicoBase<Noticia, string>
    {
        Noticia Buscar(string url);

        List<Noticia> Listar(DateTime data);

        List<Noticia> Listar(FeedUrl feedUrl);

        List<Noticia> Listar(int limit);

        List<Noticia> Listar(int limit, int skip);
    }
}
