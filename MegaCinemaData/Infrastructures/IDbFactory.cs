using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCinemaData.Infrastructures
{
    public interface IDbFactory:IDisposable
    {
        MegaCinemaDBContext Init();
    }
}
