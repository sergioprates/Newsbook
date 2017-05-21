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
            Mapper.CreateMap<FeedUrl, GetFeedUrl>().AfterMap((src, dest) => dest.Id = src._id);
            Mapper.CreateMap<Noticia, GetNoticia>().AfterMap((src, dest) =>
            {
                dest.Id = src._id;
                dest.DataPublicacao = TimeZoneInfo.ConvertTime(src.DataPublicacao, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            });
        }

            // Mapper.CreateMap<FormaDePagamentoViewModel, FormaDePagamento>().ForMember(c => c.BandeiraCartao, o => o.UseDestinationValue());
            //Mapper.CreateMap<RegistrarFormaDePagamento, FormaDePagamento>().ForMember(c => c.BandeiraCartao, o => o.UseDestinationValue());

        
    }
}