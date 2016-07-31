using AutoMapper;
using Newsbook.Core.Modelo;
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
           // Mapper.CreateMap<ProprietarioViewModel, Proprietario>().ForMember(c => c.Padarias, o => o.UseDestinationValue());

           // Mapper.CreateMap<FormaDePagamentoViewModel, FormaDePagamento>().ForMember(c => c.BandeiraCartao, o => o.UseDestinationValue());
            //Mapper.CreateMap<RegistrarFormaDePagamento, FormaDePagamento>().ForMember(c => c.BandeiraCartao, o => o.UseDestinationValue());
            
        }
    }
}