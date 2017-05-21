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
    public class NoticiaServico : ServicoBase<Noticia, string>, INoticiaServico
    {
         private readonly INoticiaRepositorio _repositorioContexto;

         public NoticiaServico(INoticiaRepositorio repositorio)
            : base(repositorio)
        {
            _repositorioContexto = repositorio;
        }

         public Noticia Buscar(string url)
         {
             return _repositorioContexto.Buscar(url);
         }

         public List<Noticia> Listar(DateTime data)
         {
             return _repositorioContexto.Listar(data);
         }

        public List<Noticia> Listar(FeedUrl feedUrl)
        {
            return _repositorioContexto.Listar(feedUrl);
        }

        public List<Noticia> Listar(int limit)
        {
            return _repositorioContexto.Listar(limit);
        }

        public List<Noticia> Listar(int limit, int skip)
        {
            return _repositorioContexto.Listar(limit, skip);
        }
    }
}
