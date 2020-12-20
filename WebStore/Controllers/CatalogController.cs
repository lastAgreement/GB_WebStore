using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        public readonly IProductData _ProductData;
        public CatalogController(IProductData productData)
        {
            _ProductData = productData;
        }
        public IActionResult Shop(int? BrandId, int? SectionId)
        {
            var filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId,
            };

            var products = _ProductData.GetProducts(filter);

            return View(new CatalogViewModel
            { 
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products
                    .OrderBy(p => p.Order)
                    .ToView()
            });
        }
        public IActionResult Details(int id)
        {
            var product = _ProductData.GetProductById(id);
            if (product is null)
                return NotFound();

            return View(product.ToView());
        }
    }
}
