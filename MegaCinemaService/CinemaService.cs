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
        void SaveChanges();
        Cinema Find(int id);
        Cinema Delete(Cinema cinema);
        void Update(Cinema cinema);
        IEnumerable<Cinema> GetCinemaPaging(int page, int pageSize, out int totalRow);

        Cinema FindLast();
    }

    public class CinemaService : ICinemaService
    {
        private ICinemaRepository _cinemaRepository;
        private IUnitOfWork _unitOfWork;

        public CinemaService(ICinemaRepository cinemaRepository, IUnitOfWork unitOfWork)
        {
            _cinemaRepository = cinemaRepository;
            _unitOfWork = unitOfWork;
        }

        public Cinema Add(Cinema cinema)
        {
            return _cinemaRepository.Add(cinema);
        }

        public Cinema Delete(Cinema cinema)
        {
            return _cinemaRepository.Delete(cinema);
        }

        public Cinema Find(int id)
        {
            return _cinemaRepository.GetSingleById(id);
        }

        public Cinema FindLast()
        {
            return _cinemaRepository.GetAll().Last();
        }

        public IEnumerable<Cinema> GetAll()
        {
            return _cinemaRepository.GetAll();
        }

        public IEnumerable<Cinema> GetCinemaPaging(int page, int pageSize, out int totalRow)
        {
            //var query = _cinemaRepository.GetMulti(x => x.CinemaStatus == "ACT");
            var query = _cinemaRepository.GetAll();
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Cinema cinema)
        {
            _cinemaRepository.Update(cinema);
        }
    }
}
