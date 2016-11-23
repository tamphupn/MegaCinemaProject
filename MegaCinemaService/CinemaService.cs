using MegaCinemaData.Infrastructures;
using MegaCinemaData.Repositories;
using MegaCinemaModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCinemaService
{

    public interface ICinemaService
    {
        //defined method
        IEnumerable<Cinema> GetAll();
        Cinema Add(Cinema cinema);
        IEnumerable<Cinema> GetFoodListPaging(int page, int pageSize, out int totalRow);
        void SaveChanges();
    }

    public class CinemaService : ICinemaService
    {
        private IFoodListRepository _foodListRepository;
        private IUnitOfWork _unitOfWork;

        public Cinema Add(Cinema cinema)
        {
            return null;
        }

        public IEnumerable<Cinema> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cinema> GetFoodListPaging(int page, int pageSize, out int totalRow)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
