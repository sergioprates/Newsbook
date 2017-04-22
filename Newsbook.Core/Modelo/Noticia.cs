using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Modelo
{
    public class Noticia
    {
        public ObjectId _id { get; set; }

        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public string Link { get; set; }

        public DateTime DataPublicacao { get; set; }

        public List<string> Categorias { get; set; }

        public FeedUrl FeedUrl { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
