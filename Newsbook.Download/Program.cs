using log4net;
using log4net.Config;
using Newsbook.Core.Interface.Repositorio;
using Newsbook.Core.Interface.Servico;
using Newsbook.Core.Modelo;
using Newsbook.Dependencias;
using Newsbook.FeedParserUrl;
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
            try
            {
                IFeedUrlRepositorio feedRepositorio = RegistradorDependencias.Instanciar(typeof(IFeedUrlRepositorio)) as IFeedUrlRepositorio;

                INoticiaRepositorio noticiaRepositorio = RegistradorDependencias.Instanciar(typeof(INoticiaRepositorio)) as INoticiaRepositorio;

                List<FeedUrl> feeds = feedRepositorio.Listar();
                log.Info(string.Format("Feed url listados: {0}. Iniciando download das noticias atualizadas.", feeds.Count));

                for (int i = 0; i < feeds.Count; i++)
                {
                    try
                    {
                        log.Info(string.Format("{0} - Baixando noticias e parseando na url {0}.", feeds[i].Url));
                        Feed feed = FeedParser.Parse(feeds[i].Url);
                        log.Info(string.Format("{0} - Noticias parseadas{0}.", feeds[i].Url));
                        for (int x = 0; x < feed.Items.Count; x++)
                        {
                            try
                            {
                                log.Info(string.Format("{0} - Tratando noticia para core {0}.", feeds[i].Url));
                                var noticia = Tratamento.TratarNoticiaDoFeedUrl(feed.Items[x], feeds[i]);
                                log.Info(string.Format("{0} - Noticia {1} tratada. Verificando se existe no BD.", feeds[i].Url, noticia.Link));
                                var existe = noticiaRepositorio.Buscar(noticia.Link);

                                if (existe == null)
                                {
                                    log.Info(string.Format("{0} - Noticia {1} não existe. Armazenando no BD", feeds[i].Url, noticia.Link));
                                    noticiaRepositorio.Inserir(noticia);
                                    log.Info(string.Format("{0} - Noticia {1} armazenada no BD com sucesso.", feeds[i].Url, noticia.Link));
                                }
                                else
                                {
                                    log.Info(string.Format("{0} - Noticia {1} já existe. Não será gravada novamente.", feeds[i].Url, noticia.Link));
                                }                                
                            }
                            catch (Exception erro)
                            {
                                log.Error(string.Format("{0} - Ocorreu um problema no armazenamento da noticia.", feeds[i].Url), erro);
                            }
                        }
                    }
                    catch (XmlException erro)
                    {
                        log.Error(string.Format("{0} - Ocorreu um problema no parseamento do feed. Verificar XML", feeds[i].Url), erro);
                    }
                    catch (Exception erro)
                    {
                        log.Error(string.Format("{0} - Ocorreu um problema não identificado no parseamento do feed.", feeds[i].Url), erro);
                    }


                }

                log.Info("Processo de download das noticias finalizado com sucesso.");
            }
            catch (Exception erro)
            {
                log.Fatal(string.Format("Ocorreu um problema no processo de download das noticias."), erro);
                log.Info("Processo de download das noticias finalizado com erro.");
            }
        }
    }
}
