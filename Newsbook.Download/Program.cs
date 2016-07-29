using log4net;
using log4net.Config;
using Newsbook.Core.Interface.Repositorio;
using Newsbook.Core.Modelo;
using Newsbook.Infraestrutura.Dados.MySql.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            IFeedUrlRepositorio repositorio = new FeedUrlRepositorio();

            List<FeedUrl> feeds = repositorio.Listar();

            for (int i = 0; i < feeds.Count; i++)
            {
                
            }
        }
    }
}
