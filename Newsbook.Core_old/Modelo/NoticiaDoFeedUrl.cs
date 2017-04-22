using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Modelo
{
    public class NoticiaDoFeedUrl : ModeloBase
    
    {
        public NoticiaDoFeedUrl()
        {
            Noticia = new Noticia();
            FeedUrl = new FeedUrl();
            Ativo = true;
        }
        public NoticiaDoFeedUrl(FeedUrl feed, Noticia noticia)
            :base()
        {
            FeedUrlId = feed.Id;
            FeedUrl = feed;
            Noticia = noticia;
            NoticiaId = noticia.Id;

        }

        public long NoticiaId { get; set; }
        public Noticia Noticia { get; set; }

        public FeedUrl FeedUrl { get; set; }
        public long FeedUrlId { get; set; }


        public static string NomeTabela
        {
            get { return "tb_noticia_feedurl"; }
        }
    }
}
