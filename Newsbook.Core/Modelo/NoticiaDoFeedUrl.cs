using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Modelo
{
    public class NoticiaDoFeedUrl : ModeloBase
    {
        public long NoticiaId { get; set; }
        public Noticia Noticia { get; set; }

        public FeedUrl FeedUrl { get; set; }
        public long FeedUrlId { get; set; }
    }
}
