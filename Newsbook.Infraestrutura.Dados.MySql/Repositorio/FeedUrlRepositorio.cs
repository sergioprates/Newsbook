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
    public class FeedUrlRepositorio : RepositorioBase<FeedUrl>, IFeedUrlRepositorio
    {
        public List<FeedUrl> ListarAtivos()
        {
            using (var cn = ConexaoFactory.Instanciar(strConexao))
            {
                return cn.Query<FeedUrl>(string.Format("SELECT * FROM {0} WHERE Ativo = 1", FeedUrl.NomeTabela)).ToList();
            }
        }
    }
}
