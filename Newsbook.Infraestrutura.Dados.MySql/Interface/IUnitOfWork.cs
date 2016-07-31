using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.Infraestrutura.Dados.MYSQL
{
    public interface IUnitOfWork
    {
        void commit();
    }
}
