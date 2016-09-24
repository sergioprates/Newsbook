using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.App.Model
{
    public class Noticia : ModeloBase
    {
        public string _titulo;
        public string _descricao;
        public string _urlFoto;
        public DateTime _dataPublicacao;
        public string _link;

        public string Titulo { get { return _titulo; } set { SetPropertyValue<string>(ref _titulo, value); } }

        public string Descricao { get { return _descricao; } set { SetPropertyValue<string>(ref _descricao, value); } }

        public string UrlFoto { get { return _urlFoto; } set { SetPropertyValue<string>(ref _urlFoto, value); } }

        public string Link { get { return _link; } set { SetPropertyValue<string>(ref _link, value); } }

        public DateTime DataPublicacao { get { return _dataPublicacao; } set { SetPropertyValue<DateTime>(ref _dataPublicacao, value); } }

        public List<Categoria> Categorias { get; set; }

        
    }
}
