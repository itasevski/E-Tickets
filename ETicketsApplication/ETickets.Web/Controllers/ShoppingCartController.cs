
using ETickets.Domain.DomainModels;
using ETickets.Domain.DTO;
using ETickets.Domain.Identity;
using ETickets.Repository;
using ETickets.Service.Interface;
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
    public class ShoppingCartController : Controller
    {
        private readonly UserManager<ETicketsApplicationUser> _userManager;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(UserManager<ETicketsApplicationUser> userManager, IShoppingCartService shoppingCartService)
        {
            _userManager = userManager;
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ShoppingCartDto shoppingCartDto = _shoppingCartService.getShoppingCartInfo(userId);

            return View(shoppingCartDto);
        }

        public async Task<IActionResult> DeleteFromShoppingCart(Guid? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _shoppingCartService.deleteTicketFromShoppingCart(userId, id.Value);

            return RedirectToAction("Index", "ShoppingCart");
        }

        public async Task<IActionResult> Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _shoppingCartService.orderNow(userId);

            return RedirectToAction("Index", "Order");
        }


    }
}
