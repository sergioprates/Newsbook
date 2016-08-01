using Newsbook.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Interface.Servico
{
    public interface INoticiaDoFeedUrlServico : IServicoBase<NoticiaDoFeedUrl>
    {
        void Armazenar(NoticiaDoFeedUrl noticiaDoFeed);

        List<NoticiaDoFeedUrl> Listar(FeedUrl feedUrl);
    }
}
