using Microsoft.AspNetCore.Mvc;
using System;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.ViewModels;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), /*Authorize(Roles = Role.Administrator)*/]
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;

        public CatalogController(IProductData ProductData) => _ProductData = ProductData;

        public IActionResult Index() => View(_ProductData.GetProducts());

        public IActionResult Edit(int? id)
        {
            if (id is null) return View(new Product());

            var product = _ProductData.GetProductById((int)id);
            if (product is null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid) return View(product);

            if (product is null)
                throw new ArgumentNullException(nameof(product));

            if (product.Id == 0)
                _ProductData.Add(product);
            else
                _ProductData.Update(product);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _ProductData.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
