using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaData.Repositories;

namespace MegaCinemaService
{
    public interface ICustomerService
    {
        int FindCustomerID(string id);
    }
    public class CustomerService:ICustomerService
    {
        private ICustomerRepository _customerRepository;
        private IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public int FindCustomerID(string id)
        {
            return _customerRepository.GetCustomerId(id);
        }
    }
}
