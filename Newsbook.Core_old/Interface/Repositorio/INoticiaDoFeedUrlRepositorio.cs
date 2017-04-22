using Newsbook.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Interface.Repositorio
{
    public interface INoticiaDoFeedUrlRepositorio : IRepositorioBase<NoticiaDoFeedUrl>
    {
        List<NoticiaDoFeedUrl> Listar(FeedUrl feedUrl);

        List<NoticiaDoFeedUrl> Listar(DateTime data);
    }
}
