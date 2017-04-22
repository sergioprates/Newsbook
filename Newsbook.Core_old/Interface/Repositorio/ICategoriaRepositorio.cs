﻿using Newsbook.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Interface.Repositorio
{
    public interface ICategoriaRepositorio : IRepositorioBase<Categoria>
    {
        Categoria BuscarPorNome(string nome);
    }
}