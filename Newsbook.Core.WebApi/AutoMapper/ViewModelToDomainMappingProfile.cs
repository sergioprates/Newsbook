using AutoMapper;
using Newsbook.Core.Modelo;
using Newsbook.Core.WebApi.ResourceModel.FeedUrl;
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
            Mapper.CreateMap<GetFeedUrl, FeedUrl>();
          
        }
    }
}