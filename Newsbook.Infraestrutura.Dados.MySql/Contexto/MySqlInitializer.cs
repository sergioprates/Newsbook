using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Newsbook.Infraestrutura.Dados.MYSQL.Contexto
{
    internal class MySqlInitializer : IDatabaseInitializer<ContextoDb>
    {
        public void InitializeDatabase(ContextoDb context)
        {
            if (!context.Database.Exists())
            {
                context.Database.Create();
            }
            else
            {                
                var migrationHistoryTableExists = ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreQuery<int>(
                string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE table_name = '__MigrationHistory'"));
                if (migrationHistoryTableExists.FirstOrDefault() == 0)
                {
                    context.Database.Delete();
                    context.Database.Create();
                }
            }
        }
    }
}
