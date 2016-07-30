using Newsbook.Core.Modelo;
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
        public static NoticiaDoFeedUrl TratarNoticiaDoFeedUrl(SyndicationItem item, FeedUrl feed)
        {
            string textoSemParagrafo = item.Summary.Text.Replace("<p>", "").Replace("</p>", "");
            string descricao = textoSemParagrafo.Substring(0, textoSemParagrafo.ToLower().IndexOf("<img"));
            string imagem = textoSemParagrafo.Substring(textoSemParagrafo.ToLower().IndexOf("<img"));

            imagem = imagem.Substring(imagem.IndexOf("http://"));
            imagem = imagem.Substring(0, imagem.IndexOf("'"));

            string link = "#";
            if (item.Links != null && item.Links.Count > 0)
            {
                link = item.Links[0].Uri.ToString();
            }

            //string u = item.Categories[0].Name;

            Noticia noticia = new Noticia();
            noticia.Ativo = true;
            noticia.Titulo = item.Title.Text;
            noticia.Descricao = descricao;
            noticia.Link = link;
            noticia.UrlFoto = imagem;
            noticia.DataPublicacao = Convert.ToDateTime(item.PublishDate);

            if (item.Categories != null)
            {
                foreach (var c in item.Categories)
                {
                    Categoria categoria = new Categoria();
                    categoria.Nome = c.Name;
                    categoria.Ativo = true;
                    noticia.Categorias.Add(new CategoriaDaNoticia(categoria, noticia));
                }
            }           

            NoticiaDoFeedUrl noticiaDoFeed = new NoticiaDoFeedUrl(feed, noticia);

            return noticiaDoFeed;

        }
    }
}
