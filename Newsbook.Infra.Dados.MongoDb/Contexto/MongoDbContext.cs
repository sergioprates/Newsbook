using MongoDB.Driver;
using Newsbook.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Infra.Dados.MongoDb.Contexto
{
    public class MongoDbContext<T>
    {
        public const string CONNECTION_STRING_NAME = "Newsbook.Core";
        private readonly string _collectionName;
        public const string DATABASE_NAME = "newsbook";

        // This is ok... Normally, they would be put into
        // an IoC container.
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoDbContext(string collection)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(DATABASE_NAME);
            this._collectionName = collection;
        }

        public IMongoClient Client
        {
            get { return _client; }
        }

        public IMongoCollection<T> Collection
        {
            get { return _database.GetCollection<T>(_collectionName); }
        }
    }
}
