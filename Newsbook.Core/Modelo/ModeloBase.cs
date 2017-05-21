using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Core.Modelo
{
    public class ModeloBase<TKey>
    {
        [BsonId]
        public TKey _id { get; set; }
    }
}
