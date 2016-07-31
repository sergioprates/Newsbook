using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Infraestrutura.Dados.MYSQL.Factory
{
    public class ConexaoFactory
    {
        public static DbConnection Instanciar(string strCn)
        {
            return new MySqlConnection(strCn);
        }
    }
}
