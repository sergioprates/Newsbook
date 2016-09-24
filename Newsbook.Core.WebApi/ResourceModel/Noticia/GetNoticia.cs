using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newsbook.Core.WebApi.ResourceModel.Noticia
{
    public class GetNoticia : ResourceModelBase
    {
        private string _descricao;
        private string _titulo;
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string UrlFoto { get; set; }

        public string Link { get; set; }

        public DateTime DataPublicacao { get; set; }

        public string strDataPublicacao { get { return DataPublicacao.ToString("dd/MM/yyyy HH:mm:ss"); } }

        public List<CategoriaItem> CategoriasDaNoticia { get; set; }

        public GetNoticia()
        {
            CategoriasDaNoticia = new List<CategoriaItem>();
        }
    }

    public class CategoriaItem : ResourceModelBase
    {
        public string Nome { get; set; }
    }
}