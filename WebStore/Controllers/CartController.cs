using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

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
            return View(new CartOrderViewModel { Cart = _cartService.TransformFromCart() });
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

        [Authorize]
        public async Task<IActionResult> CheckOut(OrderViewModel Model, [FromServices] IOrderService orderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Index), new CartOrderViewModel
                    { 
                        Cart = _cartService.TransformFromCart(),
                        Order = Model
                });


            var order = await orderService.CreateOrder(User.Identity.Name, _cartService.TransformFromCart(), Model);
            _cartService.Clear();

            return RedirectToAction(nameof(OrderConfirmed), new { order.Id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
