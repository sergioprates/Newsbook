using log4net;
using log4net.Config;
using Newsbook.Core.Interface.Repositorio;
using Newsbook.Core.Interface.Servico;
using Newsbook.Core.Modelo;
using Newsbook.Core.Servico;
using Newsbook.Dependencias;
using Newsbook.Infraestrutura.Dados.MYSQL.Repositorio;
using Newsbook.Util.Dados;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Newsbook.Download
{
    class Program
    {
        static ILog log = null;
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger("FileAppender");
            log.Info("Processo de download das noticias iniciado com sucesso. Listando FeedUrl para consultar noticias.");

            IFeedUrlRepositorio repositorio = RegistradorDependencias.Instanciar(typeof(IFeedUrlRepositorio)) as IFeedUrlRepositorio;

            List<FeedUrl> feeds = repositorio.Listar();
            log.Info(string.Format("Feed url listados: {0}. Iniciando download das noticias atualizadas.", feeds.Count));
            INoticiaDoFeedUrlServico noticiaDoFeedUrlServico = RegistradorDependencias.InstanciarServicoNoticiaDoFeedUrl();
            for (int i = 0; i < feeds.Count; i++)
            {
                log.Info(string.Format("{0} - Criando Xml Reader para a url {1}.", feeds[i].Titulo, feeds[i].Url));

               // StreamReader reader = new StreamReader("feed.xml");
                //var xmlReader = XmlReader.Create(reader);
                XmlReader xmlReader = XmlReader.Create(feeds[i].Url);
                log.Info(string.Format("{0} - Xml reader criado. Criando sindication feed.", feeds[i].Titulo));
                SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
                log.Info(string.Format("{0} - Sindication feed criado. Tratando noticias para armazenar. Total: {1}", feeds[i].Titulo, feed.Items.Count()));
               

                foreach (SyndicationItem item in feed.Items)
                {
                    try
                    {
                        var noticia = Tratamento.TratarNoticiaDoFeedUrl(item, feeds[i]);
                        log.Info(string.Format("{0} - Noticia {1} tratada. Armazenando no BD.", feeds[i].Titulo, noticia.Noticia.Titulo));
                        noticiaDoFeedUrlServico.Armazenar(noticia);
                        log.Info(string.Format("{0} - Noticia {1} armazenada no BD com sucesso.", feeds[i].Titulo, noticia.Noticia.Titulo));
                    }
                    catch (Exception erro)
                    {
                        log.Error(string.Format("{0} - Ocorreu um problema no armazenamento da noticia.", feeds[i].Titulo), erro);
                    }
                }
            }
        }
    }
}
