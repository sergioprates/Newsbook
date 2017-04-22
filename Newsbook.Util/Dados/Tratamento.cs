using Newsbook.Core.Modelo;
using Newsbook.FeedParserUrl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Util.Dados
{
    public class Tratamento
    {

        public static Noticia TratarNoticiaDoFeedUrl(Item item, FeedUrl feed)
        {
            Noticia noticia = new Noticia();
            noticia.Ativo = true;
            noticia.Titulo = item.Title;
            noticia.Conteudo = item.Content;
            noticia.Link = item.Link;
            noticia.DataPublicacao = item.PublishDate;
            noticia.FeedUrl = feed;
            if (item.Categories != null)
            {
                foreach (var c in item.Categories)
                {
                    noticia.Categorias.Add(c);
                }
            }
            
            return noticia;

        }
    }
}
