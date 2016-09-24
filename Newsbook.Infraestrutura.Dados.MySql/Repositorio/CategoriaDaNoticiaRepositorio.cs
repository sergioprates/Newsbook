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
    public class CategoriaDaNoticiaRepositorio : RepositorioBase<CategoriaDaNoticia>, ICategoriaDaNoticiaRepositorio
    {
        public List<CategoriaDaNoticia> Listar(Noticia noticia)
        {
            using (var cn = ConexaoFactory.Instanciar(strConexao))
            {
                var lista = cn.Query<CategoriaDaNoticia, Categoria, CategoriaDaNoticia>(string.Format("SELECT A.*, B.* FROM {0} A INNER JOIN {1} B ON A.CategoriaId = B.Id WHERE NoticiaId = @Id", CategoriaDaNoticia.NomeTabela, Categoria.NomeTabela),
                    (categoriaDaNoticia, categoria) =>
                    {
                        categoriaDaNoticia.Categoria = categoria;
                        return categoriaDaNoticia;
                    }, noticia).ToList();


                return lista;
            }
        }
    }
}
