using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.FeedParserUrl
{
    public class Feed
    {
        public string Description { get; set; }

        public string Title { get; set; }

        public FeedType Type { get; set; }

        public IList<Item> Items { get; set; }

        public Feed()
        {
            this.Items = new List<Item>();
        }
    }
}
