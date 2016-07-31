﻿using Newsbook.Core.Interface.Repositorio;
using Newsbook.Core.Interface.Servico;
using Newsbook.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Servico
{
    public class NoticiaServico : ServicoBase<Noticia>, INoticiaServico
    {
         private readonly INoticiaRepositorio _repositorioContexto;

         public NoticiaServico(INoticiaRepositorio repositorio)
            : base(repositorio)
        {
            _repositorioContexto = repositorio;
        }
    }
}
