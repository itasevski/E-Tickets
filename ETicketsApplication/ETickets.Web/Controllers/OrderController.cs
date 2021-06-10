
using ETickets.Domain.DomainModels;
using ETickets.Domain.DTO;
using ETickets.Domain.Identity;
using ETickets.Repository;
using ETickets.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ETickets.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<ETicketsApplicationUser> _userManager;
        private readonly IOrderService _orderService;

        public OrderController(UserManager<ETicketsApplicationUser> userManager, IOrderService orderService)
        {
            _userManager = userManager;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            OrderDto orderDto = _orderService.GetAllOrdersAsync(userId).Result;

            return View(orderDto);
        }
    }
}
