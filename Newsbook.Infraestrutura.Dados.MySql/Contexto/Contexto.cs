using Newsbook.Core.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Infraestrutura.Dados.MYSQL.Contexto
{
   public class ContextoDb : DbContext, IUnitOfWork
    {
       public ContextoDb()
            : base("Newsbook.Core")
        {
            Database.SetInitializer(new MySqlInitializer());
        }

       public ContextoDb(string connString)
            : base(connString)
        {
            Database.SetInitializer(new MySqlInitializer());
        }

       protected override void OnModelCreating(DbModelBuilder modelBuilder)
       {
           // ORGANIZADO EM ORDEM ALFABETICA
           // MIGRAR DEPOIS PARA CLASSE ESPECIFICA DE MAPEAMENTO

           // Categoria
           modelBuilder.Entity<Categoria>().ToTable("tb_categoria");
          // modelBuilder.Entity<Categoria>().Ignore(x=> NomeTabela);
           // FIM Categoria

           // CategoriaDaNoticia
           modelBuilder.Entity<CategoriaDaNoticia>().ToTable("tb_categoria_noticia");
           modelBuilder.Entity<CategoriaDaNoticia>().Ignore(x => x.Categoria);
           modelBuilder.Entity<CategoriaDaNoticia>().Ignore(x => x.Noticia);
          // modelBuilder.Entity<CategoriaDaNoticia>().Ignore(x => x.NomeTabela);
           // FIM CategoriaDaNoticia

           // FeedUrl
           modelBuilder.Entity<FeedUrl>().ToTable("tb_feedurl");
           modelBuilder.Entity<FeedUrl>().Ignore(x => x.Noticias);
           // FIM FeedUrl

           // Noticia
           modelBuilder.Entity<Noticia>().ToTable("tb_noticia");
           modelBuilder.Entity<Noticia>().Ignore(x => x.Categorias);
          // modelBuilder.Entity<Noticia>().Ignore(x => x.NomeTabela);
           // FIM Noticia 

           // NoticiaDoFeedUrl
           modelBuilder.Entity<NoticiaDoFeedUrl>().ToTable("tb_noticia_feedurl");
           modelBuilder.Entity<NoticiaDoFeedUrl>().Ignore(x => x.FeedUrl);
           modelBuilder.Entity<NoticiaDoFeedUrl>().Ignore(x => x.Noticia);
           //modelBuilder.Entity<NoticiaDoFeedUrl>().Ignore(x => x.NomeTabela);
           // FIM NoticiaDoFeedUrl 
       }

       public void commit()
       {
           foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
           {
               if (entry.State == EntityState.Added)
               {
                   entry.Property("DataCadastro").CurrentValue = DateTime.Now;
               }

               if (entry.State == EntityState.Modified)
               {
                   entry.Property("DataCadastro").IsModified = false;
               }
           }

           foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataAlteracao") != null))
           {
               if (entry.State == EntityState.Added)
               {
                   entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
               }

               if (entry.State == EntityState.Modified)
               {
                   entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                   entry.Property("DataAlteracao").IsModified = true;
               }
           }

           base.SaveChanges();
       }
    }
}
