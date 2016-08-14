using Newsbook.Core.Interface.Repositorio;
using Newsbook.Core.Modelo;
using Newsbook.Infraestrutura.Dados.MYSQL.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Newsbook.Infraestrutura.Dados.MYSQL.Repositorio
{
    public class NoticiaDoFeedUrlRepositorio : RepositorioBase<NoticiaDoFeedUrl>, INoticiaDoFeedUrlRepositorio
    {
        private readonly INoticiaRepositorio _noticiaRepositorio;
        public NoticiaDoFeedUrlRepositorio(INoticiaRepositorio noticiaRepositorio)
        {
            _noticiaRepositorio = noticiaRepositorio;
        }

        public List<NoticiaDoFeedUrl> Listar(FeedUrl feedUrl)
        {
            using (var cn = ConexaoFactory.Instanciar(strConexao))
            {
                var lista = cn.Query<NoticiaDoFeedUrl, Noticia, NoticiaDoFeedUrl>(string.Format("SELECT A.*, B.* FROM {0} A INNER JOIN {1} B ON A.NoticiaId = B.Id WHERE FeedUrlId = @Id", NoticiaDoFeedUrl.NomeTabela, FeedUrl.NomeTabela),
                    (noticiaDoFeed, noticia) =>
                    {
                        noticiaDoFeed.Noticia = noticia;
                        return noticiaDoFeed;
                    }, feedUrl).ToList();


                return lista;
            }
        }

        public List<NoticiaDoFeedUrl> Listar(DateTime data)
        {
            using (var cn = ConexaoFactory.Instanciar(strConexao))
            {
                return cn.Query<NoticiaDoFeedUrl>(string.Format("SELECT * FROM {0} WHERE concat(year(DataPublicacao), month(DataPublicacao), day(DataPublicacao)) = @Data", Noticia.NomeTabela), new { Data = data.Year.ToString() + data.Month.ToString() + data.Day.ToString() }).ToList();
            }
        }
    }
}
