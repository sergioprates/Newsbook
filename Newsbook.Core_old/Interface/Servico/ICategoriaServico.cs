using Newsbook.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Interface.Servico
{
    public interface ICategoriaServico : IServicoBase<Categoria>
    {
        Categoria BuscarPorNome(string nome);
    }
}
