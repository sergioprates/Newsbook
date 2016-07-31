using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newsbook.Core.WebApi.ResourceModel.FeedUrl
{
    public class GetFeedUrl : ResourceModelBase
    {
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string Url { get; set; }
    }
}