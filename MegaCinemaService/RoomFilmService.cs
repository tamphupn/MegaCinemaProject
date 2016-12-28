using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaData.Repositories;
using MegaCinemaModel.Models;

namespace MegaCinemaService
{
    public interface IRoomFilmService
    {
        IEnumerable<RoomFilm> GetAll();
        RoomFilm Add(RoomFilm roomFilm);
        void SaveChanges();      
    }
    public class RoomFilmService : IRoomFilmService
    {
        private IRoomFilmRepository _roomFilmRepository;
        private IUnitOfWork _unitOfWork;

        public RoomFilmService(IRoomFilmRepository roomFilmRepository, IUnitOfWork unitOfWork)
        {
            _roomFilmRepository = roomFilmRepository;
            _unitOfWork = unitOfWork;
        }

        public RoomFilm Add(RoomFilm roomFilm)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoomFilm> GetAll()
        {
            return _roomFilmRepository.GetAll();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
