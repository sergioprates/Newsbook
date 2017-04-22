using Newsbook.Core.Interface.Repositorio;
using Newsbook.Core.Interface.Servico;
using Newsbook.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Servico
{
    public class CategoriaServico : ServicoBase<Categoria>, ICategoriaServico
    {
        private readonly ICategoriaRepositorio _repositorioContexto;

        public CategoriaServico(ICategoriaRepositorio repositorio)
            : base(repositorio)
        {
            _repositorioContexto = repositorio;
        }

        public Categoria BuscarPorNome(string nome)
        {
            return _repositorioContexto.BuscarPorNome(nome);
        }
    }
}
