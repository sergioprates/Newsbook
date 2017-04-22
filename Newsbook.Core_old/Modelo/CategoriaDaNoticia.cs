using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Modelo
{
    public class CategoriaDaNoticia : ModeloBase
    {
        public CategoriaDaNoticia()
        {
            Categoria = new Categoria();
            Noticia = new Noticia();
            Ativo = true;
        }

        public CategoriaDaNoticia(Categoria c, Noticia n)
            :base()
        {
            Categoria = c;
            CategoriaId = c.Id;
            Noticia = n;
            NoticiaId = n.Id;
        }


        public Categoria Categoria { get; set; }
        public long CategoriaId { get; set; }

        public Noticia Noticia { get; set; }
        public long NoticiaId { get; set; }

        public static string NomeTabela
        {
            get { return "tb_categoria_noticia"; }
        }
    }
}
