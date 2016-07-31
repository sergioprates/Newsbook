using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Modelo
{
    public class FeedUrl : ModeloBase
    {
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string Url { get; set; }

        public List<NoticiaDoFeedUrl> Noticias { get; set; }

        public static string NomeTabela
        {
            get { return "tb_feedurl"; }
        }
    }
}
