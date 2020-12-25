using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Services.InCookies
{
    public class InCookiesCartService: ICartService
    {
        private readonly IProductData _ProductData;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly string _CartName;
        private Cart Cart { 
            get 
            { 
                var context = _HttpContextAccessor.HttpContext;
                var cookies = context.Response.Cookies;
                var cart_cookie = context.Request.Cookies[_CartName];
                if (cart_cookie is null)
                {
                    var cart = new Cart();
                    cookies.Append(_CartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }

                ReplaceCookies(cookies, cart_cookie);
                return JsonConvert.DeserializeObject<Cart>(cart_cookie);
            }
            set => ReplaceCookies(_HttpContextAccessor.HttpContext!.Response.Cookies, JsonConvert.SerializeObject(value));
        }

        private void ReplaceCookies(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(_CartName);
            cookies.Append(_CartName, cookie);
        }

        void ICartService.AddToCart(int Id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == Id);

            if (item is null)
                cart.Items.Add(new CartItem { ProductId = Id, Quantity = 1 });

            else item.Quantity++;

            Cart = cart;
        }

        void ICartService.DecrementFromCart(int Id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == Id);

            if (item is null) return;
            if (item.Quantity > 0)
                item.Quantity--;
            if (item.Quantity == 0)
                cart.Items.Remove(item);

            Cart = cart;
        }

        void ICartService.RemoveFromCart(int Id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == Id);

            if (item is null) return;

            cart.Items.Remove(item);
            Cart = cart;
        }

        void ICartService.Clear()
        {
            var cart = Cart;
            cart.Items.Clear();
            Cart = cart;
        }

        CartViewModel ICartService.TransformFromCart()
        {
            var products = _ProductData.GetProducts(new ProductFilter { Ids = Cart.Items.Select(i=>i.ProductId).ToArray()});
            var productViewModels = products.ToView().ToDictionary(p => p.Id);
            return new CartViewModel
                {
                Items = Cart.Items.Select(i=>(productViewModels[i.ProductId],i.Quantity))
                    };
        }

        public InCookiesCartService(IProductData productData, IHttpContextAccessor httpContextAccessor)
        {
            _ProductData = productData;
            _HttpContextAccessor = httpContextAccessor;

            var user = httpContextAccessor.HttpContext!.User;
            var user_name = user.Identity!.IsAuthenticated ? $"({user.Identity.Name})" : null;

            _CartName = $"WebStore.Cart{user_name}";
        }

    }
}
