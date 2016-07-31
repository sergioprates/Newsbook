using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Migrations.History;
using System.Data.Entity;
using System.Data.Common;

namespace Newsbook.Infraestrutura.Dados.MYSQL.Contexto
{
    public class MySqlHistoryContext : HistoryContext
    {
        public MySqlHistoryContext(
          DbConnection existingConnection,
          string defaultSchema)
            : base(existingConnection, defaultSchema)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HistoryRow>().Property(h => h.MigrationId).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<HistoryRow>().Property(h => h.ContextKey).HasMaxLength(200).IsRequired();
        }
    }
}
