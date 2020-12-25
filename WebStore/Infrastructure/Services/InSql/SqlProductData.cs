using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB _db;

        public SqlProductData(WebStoreDB db) =>_db = db;

        public Brand GetBrandById(int id) => _db.Brands.Include(b => b.Products).FirstOrDefault(b => b.Id == id);

        public IEnumerable<Brand> GetBrands() => _db.Brands.Include(b => b.Products);


        public Section GetSectionById(int id) => _db.Sections.Include(s => s.Products).FirstOrDefault(s => s.Id == id);

        public IEnumerable<Section> GetSections() => _db.Sections.Include(s => s.Products);


        public Product GetProductById(int id) => _db.Products
            .Include(p => p.Section)
            .Include(p => p.Brand)
            .FirstOrDefault(p => p.Id == id);

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products;

            if (Filter?.Ids?.Length > 0)
            {
                query = query.Where(product =>Filter.Ids.Contains(product.Id));
            }
            else
            {
                if (Filter?.BrandId != null)
                    query = query.Where(product => product.BrandId == Filter.BrandId);

                if (Filter?.SectionId != null)
                    query = query.Where(product => product.SectionId == Filter.SectionId);
            }
            return query;
        }

        void IProductData.Add(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
        }

        void IProductData.Update(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
        }

        void IProductData.Delete(int id)
        {
            _db.Products.Remove(GetProductById(id));
            _db.SaveChanges();
        }
    }
}
