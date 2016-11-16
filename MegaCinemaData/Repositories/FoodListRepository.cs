using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;

namespace MegaCinemaData.Repositories
{
    public interface IFoodListRepository:IRepository<FoodList>
    {

    }
    public class FoodListRepository:RepositoryBase<FoodList>, IFoodListRepository
    {
        public FoodListRepository(IDbFactory dbfactory):base(dbfactory)
        {

        }
    }
}
