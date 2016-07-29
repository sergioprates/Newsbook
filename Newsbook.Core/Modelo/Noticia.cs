using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Modelo
{
    public class Noticia : ModeloBase
    {
        public Noticia()
        {
            Categorias = new List<CategoriaDaNoticia>();
        }


        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string UrlFoto { get; set; }

        public string Link { get; set; }

        public List<CategoriaDaNoticia> Categorias { get; set; }
    }
}
