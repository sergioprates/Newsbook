using Newsbook.Core.Interface.Repositorio;
using Newsbook.Core.Interface.Servico;
using Newsbook.Core.Modelo;
using Newsbook.Core.Servico;
using Newsbook.Dependencias;
using Newsbook.Infraestrutura.Dados.MYSQL.Repositorio;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Newsbook.Console.Teste
{
    class Program
    {
        static void Main(string[] args)
        {

            Container c = RegistradorDependencias.GetContainer(null);

            var noticiaServico = new NoticiaDoFeedUrlServico(
            c.GetInstance(typeof(INoticiaDoFeedUrlRepositorio)) as INoticiaDoFeedUrlRepositorio,
            c.GetInstance(typeof(INoticiaServico)) as INoticiaServico,
            c.GetInstance(typeof(IFeedUrlServico)) as IFeedUrlServico,
            c.GetInstance(typeof(ICategoriaServico)) as ICategoriaServico,
            c.GetInstance(typeof(ICategoriaDaNoticiaServico)) as ICategoriaDaNoticiaServico);

            //FeedUrl f = new FeedUrl();
            //f.Titulo = "Teste";
            //f.Descricao = "teste";
            //f.Url = "url";
            //f.Ativo = true;
            //repositorio.Salvar(f);
            using(StreamReader reader = new StreamReader("feed.xml"))
            {
                var xmlReader = XmlReader.Create(reader);
                SyndicationFeed feed = SyndicationFeed.Load(xmlReader);

                foreach (var item in feed.Items)
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

                    string u = item.Categories[0].Name;
                    

                    System.Console.WriteLine("Titulo: " + item.Title.Text);
                    System.Console.WriteLine("Descrição: " + descricao);
                    System.Console.WriteLine("Imagem: " + imagem);
                    System.Console.WriteLine("Link: " + link);
                    System.Console.WriteLine("Data de publicação: " + item.PublishDate.ToString("dd/MM/yyyy HH:mm:ss"));
                }

                System.Console.ReadKey();
            }

        }
    }
}
