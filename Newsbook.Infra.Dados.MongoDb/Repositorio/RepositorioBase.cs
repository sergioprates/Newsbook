using MongoDB.Bson;
using MongoDB.Driver;
using Newsbook.Core.Interface.Repositorio;
using Newsbook.Core.Modelo;
using Newsbook.Infra.Dados.MongoDb.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Infra.Dados.MongoDb.Repositorio
{
    public class RepositorioBase<T,TKey> : IRepositorioBase<T, TKey>
    {
        protected readonly MongoDbContext<T> Repository;        

        public RepositorioBase(string collection)
        {
            Repository = new MongoDbContext<T>(collection);
        }         
                

        public void Deletar(FilterDefinition<T> query)
        {
            Repository.Collection.DeleteOne(query);
        }

        public void Dispose()
        {
        }

        public T Inserir(T obj)
        {
            Repository.Collection.InsertOne(obj);

            return obj;
        }

        public List<T> Listar()
        {
            return Repository.Collection.Find(this.FilterBuilder.Empty).ToList();
        }

        public void Alterar(TKey id, T obj)
        {
            var query = FilterBuilder.Eq("_id", new ObjectId(id as string));
            Repository.Collection.ReplaceOne(query, obj);
        }

        public T BuscarPorId(TKey id)
        {
            var query = FilterBuilder.Eq("_id", new ObjectId(id as string));
            return Repository.Collection.Find(query).FirstOrDefault();
        }

        public void Deletar(TKey id)
        {
            var query = FilterBuilder.Eq("_id", new ObjectId(id as string));
            Repository.Collection.DeleteOne(query);
        }

        public T Buscar(FilterDefinition<T> query)
        {
            return Repository.Collection.Find(query).FirstOrDefault();
        }

        public List<T> Listar(FilterDefinition<T> query)
        {
            return Repository.Collection.Find(query).ToList();
        }

        public List<T> Listar(FilterDefinition<T> query, SortDefinition<T> sort, int limit)
        {
            return Repository.Collection.Find(query).Sort(sort).Limit(limit).ToList();
        }

        public List<T> Listar(FilterDefinition<T> query, SortDefinition<T> sort, int limit, int skip)
        {
            return Repository.Collection.Find(query).Sort(sort).Skip(skip).Limit(limit).ToList();
        }

        public FilterDefinitionBuilder<T> FilterBuilder
        {
            get
            {
                return Builders<T>.Filter;
            }
        }

        public UpdateDefinitionBuilder<T> UpdateBuilder
        {
            get
            {
                return Builders<T>.Update;
            }
        }

        public IndexKeysDefinitionBuilder<T> IndexBuilder
        {
            get
            {
                return Builders<T>.IndexKeys;
            }
        }

        public ProjectionDefinitionBuilder<T> ProjectionBuilder
        {
            get
            {
                return Builders<T>.Projection;
            }
        }


        public SortDefinitionBuilder<T> SortBuilder
        {
            get
            {
                return Builders<T>.Sort;
            }
        }
    }
}
