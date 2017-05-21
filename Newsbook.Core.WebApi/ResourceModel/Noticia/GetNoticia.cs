using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newsbook.Core.WebApi.ResourceModel.Noticia
{
    public class GetNoticia : ResourceModelBase
    {
        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public string UrlFoto { get; set; }

        public string Link { get; set; }

        public DateTime DataPublicacao { get; set; }

        public string strDataPublicacao { get { return DataPublicacao.ToString("dd/MM/yyyy HH:mm:ss"); } }

        public List<string> CategoriasDaNoticia { get; set; }
        
    }
    
}