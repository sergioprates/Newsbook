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
    public class NoticiaRepositorio : RepositorioBase<Noticia>, INoticiaRepositorio
    {
        private readonly ICategoriaDaNoticiaRepositorio _categoriaNoticiaRepositorio;

        public NoticiaRepositorio(ICategoriaDaNoticiaRepositorio categoriaNoticiaRepositorio)
        {
            _categoriaNoticiaRepositorio = categoriaNoticiaRepositorio;
        }

        public Noticia Buscar(string url)
        {
            using (var cn = ConexaoFactory.Instanciar(strConexao))
            {
                return cn.Query<Noticia>(string.Format("SELECT * FROM {0} WHERE Link = @Url", Noticia.NomeTabela), new { Url = url }).FirstOrDefault();
            }
        }

        public List<Noticia> Listar(DateTime data)
        {
            using (var cn = ConexaoFactory.Instanciar(strConexao))
            {
                var lista = cn.Query<Noticia>(string.Format("SELECT * FROM {0} WHERE concat(year(DataPublicacao), month(DataPublicacao), day(DataPublicacao)) = @Data ORDER BY DataPublicacao desc;", Noticia.NomeTabela), new { Data = data.Year.ToString() + data.Month.ToString() + data.Day.ToString() }).ToList();
//                var lista = cn.Query<Noticia>(string.Format("SELECT * FROM {0}", Noticia.NomeTabela)).ToList();

                for (int i = 0; i < lista.Count; i++)
                {
                    lista[i].Categorias = _categoriaNoticiaRepositorio.Listar(lista[i]);
                }

                return lista;
            }
        }
    }
}
