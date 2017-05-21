using Newsbook.Core.Interface.Repositorio;
using Newsbook.Core.Interface.Servico;
using Newsbook.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Servico
{
    public class FeedUrlServico : ServicoBase<FeedUrl, string>, IFeedUrlServico
    {
        private readonly IFeedUrlRepositorio _repositorioContexto;

        public FeedUrlServico(IFeedUrlRepositorio repositorio)
            : base(repositorio)
        {
            _repositorioContexto = repositorio;
        }
        
    }
}
