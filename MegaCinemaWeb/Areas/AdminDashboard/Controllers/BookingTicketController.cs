using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MegaCinemaCommon.StatusCommon;
using MegaCinemaModel.Models;
using MegaCinemaService;
using MegaCinemaWeb.Infrastructure.Core;
using MegaCinemaWeb.Models;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class BookingTicketController : BaseController
    {
        public IBookingTicketService _bookingTicketService;

        public BookingTicketController(IBookingTicketService bookingTicketService)
        {
            _bookingTicketService = bookingTicketService;
        }

        // GET: AdminDashboard/BookingTicket
        public ActionResult Index(int page = 0)
        {
            int pageSize = CommonConstrants.PAGE_SIZE;
            int totalRow = 0;
            var result = _bookingTicketService.GetTicketListPaging(page, pageSize, out totalRow);
            var resultVm = Mapper.Map<IEnumerable<BookingTicket>, IEnumerable<BookingTicketViewModel>>(result);

            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<BookingTicketViewModel>()
            {
                Items = resultVm,
                MaxPage = CommonConstrants.PAGE_SIZE,
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage,
                Count = resultVm.Count(),
            };

            return View(paginationSet);
        }
    }
}