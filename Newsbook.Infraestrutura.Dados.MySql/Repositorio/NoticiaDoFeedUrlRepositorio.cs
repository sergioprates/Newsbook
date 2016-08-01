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
        public List<NoticiaDoFeedUrl> Listar(FeedUrl feedUrl)
        {
            using (var cn = ConexaoFactory.Instanciar(strConexao))
            {
                return cn.Query<NoticiaDoFeedUrl>(string.Format("SELECT * FROM {0} WHERE FeedUrlId = @Id", NoticiaDoFeedUrl.NomeTabela), feedUrl).ToList();
            }
        }
    }
}
