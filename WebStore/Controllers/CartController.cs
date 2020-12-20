using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            return View(_cartService.TransformFromCart());
        }

        public IActionResult AddToCart(int Id)
        {
            _cartService.AddToCart(Id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DecrementFromCart(int Id)
        {
            _cartService.DecrementFromCart(Id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveFromCart(int Id)
        {
            _cartService.RemoveFromCart(Id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Clear()
        {
            _cartService.Clear();
            return RedirectToAction(nameof(Index));
        }
    }
}
