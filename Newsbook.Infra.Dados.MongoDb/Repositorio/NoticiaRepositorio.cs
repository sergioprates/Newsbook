using Newsbook.Core.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newsbook.Core.Modelo;
using Newsbook.Infra.Dados.MongoDb.Contexto;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Newsbook.Infra.Dados.MongoDb.Repositorio
{
    public class NoticiaRepositorio : RepositorioBase<Noticia, string>, INoticiaRepositorio
    {
        public NoticiaRepositorio()
            : base("noticia")
        {

        }

        public Noticia Buscar(string url)
        {
            var query = FilterBuilder.Eq(x => x.Link, url);
            return base.Buscar(query);
        }

        public List<Noticia> Listar(DateTime data)
        {
            var query = FilterBuilder.Eq(x => x.DataPublicacao, data);
            return base.Listar(query).OrderByDescending(x=> x.DataPublicacao).ToList();
        }

        public List<Noticia> Listar(FeedUrl feedUrl)
        {
            var query = FilterBuilder.Eq(x => x.FeedUrl._id, feedUrl._id);
            return base.Listar(query).OrderByDescending(x => x.DataPublicacao).ToList();
        }

        public List<Noticia> Listar(int limit)
        {
            var query = FilterBuilder.Empty;
            var sort = SortBuilder.Descending(x => x.DataPublicacao);
            return base.Listar(query, sort, limit);
        }

        public List<Noticia> Listar(int limit, int skip)
        {
            var query = FilterBuilder.Empty;
            var sort = SortBuilder.Descending(x => x.DataPublicacao);
            return base.Listar(query, sort, limit, skip);
        }

        public List<Noticia> Listar(FeedUrl feedUrl, int limit)
        {
            var query = FilterBuilder.Eq(x=> x.FeedUrl._id, feedUrl._id);
            var sort = SortBuilder.Descending(x => x.DataPublicacao);
            return base.Listar(query, sort, limit);
        }

        public List<Noticia> Listar(FeedUrl feedUrl, int limit, int skip)
        {
            var query = FilterBuilder.Eq(x => x.FeedUrl._id, feedUrl._id);
            var sort = SortBuilder.Descending(x => x.DataPublicacao);
            return base.Listar(query, sort, limit, skip);
        }
    }
}
