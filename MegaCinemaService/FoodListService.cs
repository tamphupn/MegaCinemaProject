using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaModel.Models;
using MegaCinemaData.Repositories;
using MegaCinemaData.Infrastructures;

namespace MegaCinemaService
{
    public interface IFoodListService
    {
        //defined method
        IEnumerable<FoodList> GetAll();
        FoodList Add(FoodList foodList);
        void SaveChanges();
    }

    public class FoodListService : IFoodListService
    {
        //implement method
        private IFoodListRepository _foodListRepository;
        private IUnitOfWork _unitOfWork;

        public FoodListService(IFoodListRepository foodListRepository, IUnitOfWork unitOfWork)
        {
            _foodListRepository = foodListRepository;
            _unitOfWork = unitOfWork;
        }

        public FoodList Add(FoodList foodList)
        {
            return _foodListRepository.Add(foodList);
        }

        public IEnumerable<FoodList> GetAll()
        {
            return _foodListRepository.GetAll();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
