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
    public class NoticiaDoFeedUrlServico : ServicoBase<NoticiaDoFeedUrl>, INoticiaDoFeedUrlServico
    {
        private readonly INoticiaDoFeedUrlRepositorio _repositorioContexto;
        private readonly INoticiaServico _servicoNoticia;
        private readonly IFeedUrlServico _servicoFeedUrl;
        private readonly ICategoriaServico _servicoCategoria;
        private readonly ICategoriaDaNoticiaServico _servicoCategoriaDaNoticia;

        public NoticiaDoFeedUrlServico(INoticiaDoFeedUrlRepositorio repositorio, 
            INoticiaServico servicoNoticia, 
            IFeedUrlServico servicoFeedUrl, 
            ICategoriaServico servicoCategoria,
            ICategoriaDaNoticiaServico servicoCategoriaDaNoticia)
            : base(repositorio)
        {
            _repositorioContexto = repositorio;
            _servicoNoticia = servicoNoticia;
            _servicoFeedUrl = servicoFeedUrl;
            _servicoCategoria = servicoCategoria;
            _servicoCategoriaDaNoticia = servicoCategoriaDaNoticia;
        }

        public void Armazenar(NoticiaDoFeedUrl noticiaDoFeed)
        {
            if (noticiaDoFeed != null )
            {
                noticiaDoFeed.FeedUrlId = noticiaDoFeed.FeedUrl.Id;
                //Verificar se a noticia já existe para o feed atual através do link.
                noticiaDoFeed.Ativo = true;

                Noticia noticiaAux = _servicoNoticia.Buscar(noticiaDoFeed.Noticia.Link);

                if (noticiaAux == null)
                {
                    noticiaDoFeed.Noticia.Id = _servicoNoticia.Salvar(noticiaDoFeed.Noticia);

                    for (int i = 0; i < noticiaDoFeed.Noticia.Categorias.Count; i++)
                    {
                        var c = _servicoCategoria.BuscarPorNome(noticiaDoFeed.Noticia.Categorias[i].Categoria.Nome);
                        if (c != null)
                        {
                            noticiaDoFeed.Noticia.Categorias[i].CategoriaId = c.Id;
                        }
                        else
                        {
                            noticiaDoFeed.Noticia.Categorias[i].CategoriaId = _servicoCategoria.Salvar(noticiaDoFeed.Noticia.Categorias[i].Categoria);
                        }

                        noticiaDoFeed.Noticia.Categorias[i].NoticiaId = noticiaDoFeed.NoticiaId;
                        noticiaDoFeed.Noticia.Categorias[i].Ativo = true;
                        noticiaDoFeed.Noticia.Categorias[i].Id = _servicoCategoriaDaNoticia.Salvar(noticiaDoFeed.Noticia.Categorias[i]);
                    
                    }

                    noticiaDoFeed.NoticiaId = noticiaDoFeed.Noticia.Id;

                    noticiaDoFeed.Id = Salvar(noticiaDoFeed);
                }
            }
        }


        public List<NoticiaDoFeedUrl> Listar(FeedUrl feedUrl)
        {
            return _repositorioContexto.Listar(feedUrl);
        }


        public List<NoticiaDoFeedUrl> Listar(DateTime data)
        {
            return _repositorioContexto.Listar(data);
        }
    }
}
