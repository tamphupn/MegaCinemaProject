using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCinemaData.Infrastructures
{
    public interface IUnitOfWork
    {
        void Commit();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTran();
    }
}
