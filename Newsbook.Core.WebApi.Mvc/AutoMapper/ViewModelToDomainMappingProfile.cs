using AutoMapper;
using Newsbook.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newsbook.Core.WebApi.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            //Mapper.CreateMap<ProprietarioViewModel, Proprietario>();
          
        }
    }
}