
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Modelo
{
    public class FeedUrl : ModeloBase<string>
    {
        public FeedUrl()
        {
            this._id = Guid.NewGuid().ToString();
        }
        public string Titulo { get; set; }

        public string Url { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }
    }
}
