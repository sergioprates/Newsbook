using AutoMapper;
using Newsbook.Core.Modelo;
using Newsbook.Core.WebApi.ResourceModel.FeedUrl;
using Newsbook.Core.WebApi.ResourceModel.Noticia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newsbook.Core.WebApi.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<FeedUrl, GetFeedUrl>();
            Mapper.CreateMap<Noticia, GetNoticia>().AfterMap(
            (src, dest) =>
            {
                if (src.Categorias != null)
                {
                    var categorias = src.Categorias.Select(x => x.Categoria).ToList();
                    List<CategoriaItem> novaLista = new List<CategoriaItem>();
                    for (int i = 0; i < categorias.Count; i++)
                    {
                        CategoriaItem c = new CategoriaItem();
                        c.Id = categorias[i].Id;
                        c.Nome = categorias[i].Nome;
                        novaLista.Add(c);
                    }

                    dest.CategoriasDaNoticia = novaLista;
                }
            });

           // Mapper.CreateMap<FormaDePagamentoViewModel, FormaDePagamento>().ForMember(c => c.BandeiraCartao, o => o.UseDestinationValue());
            //Mapper.CreateMap<RegistrarFormaDePagamento, FormaDePagamento>().ForMember(c => c.BandeiraCartao, o => o.UseDestinationValue());
            
        }
    }
}