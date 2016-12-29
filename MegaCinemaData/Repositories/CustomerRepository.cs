using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;

namespace MegaCinemaData.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        int GetCustomerId(string id);
    }
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(IDbFactory dbfactory):base(dbfactory)
        {

        }

        public int GetCustomerId(string id)
        {
            var result = this.DbContext.Users.Include(n => n.Customer).SingleOrDefault(n => n.Id == id);
            return result.Customer == null ? 0 : result.Customer.CustomerID;
        }
    }
}
