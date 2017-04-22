using Newsbook.Core.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newsbook.Core.Modelo;

namespace Newsbook.Infra.Dados.MongoDb.Repositorio
{
    public class NoticiaRepositorio : INoticiaRepositorio
    {
        public void Alterar(Noticia obj)
        {
            throw new NotImplementedException();
        }

        public Noticia Buscar(string url)
        {
            throw new NotImplementedException();
        }

        public Noticia BuscarPorId(long id)
        {
            throw new NotImplementedException();
        }

        public void Deletar(Noticia obj)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Noticia Inserir(Noticia obj)
        {
            throw new NotImplementedException();
        }

        public List<Noticia> Listar(DateTime data)
        {
            throw new NotImplementedException();
        }

        public List<Noticia> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
