using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCinemaData.Infrastructures
{
    public class DbFactory : Disposeable, IDbFactory
    {
        MegaCinemaDBContext dbContext;
        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }

        public MegaCinemaDBContext Init()
        {
            return dbContext ?? (dbContext = new MegaCinemaDBContext());
        }

    }
}
