using Newsbook.Core.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newsbook.Core.Modelo;

namespace Newsbook.Infra.Dados.MongoDb.Repositorio
{
    public class FeedUrlRepositorio : RepositorioBase<FeedUrl, string>, IFeedUrlRepositorio
    {
        public FeedUrlRepositorio()
            : base("feedurl")
        {

        }
        
    }
}
