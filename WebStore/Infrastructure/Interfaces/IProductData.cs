using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Domain;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();

        Section GetSectionById(int id);

        IEnumerable<Brand> GetBrands();

        Brand GetBrandById(int id);

        IEnumerable<Product> GetProducts(ProductFilter Filter = null);

        Product GetProductById(int id);
    }
}
