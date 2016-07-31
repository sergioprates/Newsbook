using Newsbook.Core.Interface.Repositorio;
using Newsbook.Core.Interface.Servico;
using Newsbook.Core.Servico;
using Newsbook.Infraestrutura.Dados.MYSQL.Repositorio;
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
            container.Register<IFeedUrlRepositorio, FeedUrlRepositorio>(Lifestyle.Scoped);
            container.Register<ICategoriaRepositorio, CategoriaRepositorio>(Lifestyle.Scoped);
            container.Register<ICategoriaDaNoticiaRepositorio, CategoriaDaNoticiaRepositorio>(Lifestyle.Scoped);
            container.Register<INoticiaRepositorio, NoticiaRepositorio>(Lifestyle.Scoped);
            container.Register<INoticiaDoFeedUrlRepositorio, NoticiaDoFeedUrlRepositorio>(Lifestyle.Scoped);
           


            //Serviços
            container.Register<IFeedUrlServico, FeedUrlServico>(Lifestyle.Scoped);
            container.Register<ICategoriaServico, CategoriaServico>(Lifestyle.Scoped);
            container.Register<ICategoriaDaNoticiaServico, CategoriaDaNoticiaServico>(Lifestyle.Scoped);
            container.Register<INoticiaServico, NoticiaServico>(Lifestyle.Scoped);
            container.Register<INoticiaDoFeedUrlServico, NoticiaDoFeedUrlServico>(Lifestyle.Scoped);
            

            return container;
        }

        public static INoticiaDoFeedUrlServico InstanciarServicoNoticiaDoFeedUrl()
        {
            Container c = GetContainer(null);
            return new NoticiaDoFeedUrlServico(
                           c.GetInstance(typeof(INoticiaDoFeedUrlRepositorio)) as INoticiaDoFeedUrlRepositorio,
                           c.GetInstance(typeof(INoticiaServico)) as INoticiaServico,
                           c.GetInstance(typeof(IFeedUrlServico)) as IFeedUrlServico,
                           c.GetInstance(typeof(ICategoriaServico)) as ICategoriaServico,
                           c.GetInstance(typeof(ICategoriaDaNoticiaServico)) as ICategoriaDaNoticiaServico);
        }    

        public static object Instanciar(Type tipo)
        {
            Container c = GetContainer(null);
            return c.GetInstance(tipo);
        }
    }
}
