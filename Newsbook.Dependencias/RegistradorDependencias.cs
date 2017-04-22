using Newsbook.Core.Interface.Repositorio;
using Newsbook.Core.Interface.Servico;
using Newsbook.Core.Servico;
using Newsbook.Infra.Dados.MongoDb.Repositorio;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Dependencias
{
    public class RegistradorDependencias
    {
        public static Container GetContainer(ScopedLifestyle scope)
        {
            var container = new Container();

            if (scope != null)
            {
                container.Options.DefaultScopedLifestyle = scope;                
            }
            //Repositorios
            container.Register<IFeedUrlRepositorio, FeedUrlRepositorio>();
            container.Register<INoticiaRepositorio, NoticiaRepositorio>();
           


            //Serviços
            container.Register<IFeedUrlServico, FeedUrlServico>();
            container.Register<INoticiaServico, NoticiaServico>();
            

            return container;
        }

          

        public static object Instanciar(Type tipo)
        {
            Container c = GetContainer(null);
            return c.GetInstance(tipo);
        }
    }
}
