using Newsbook.App.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.App.ViewModel
{
    public class PaginaPrincipalViewModel
    {
        public ObservableCollection<Noticia> Noticias { get; set; }

        public PaginaPrincipalViewModel()
        {
            Noticias = new ObservableCollection<Noticia>();

            Noticias.Add(new Noticia() { Titulo = "Noticia titulo", Descricao = "Descrição" });
        }
    }
}
