using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain
{
    public class ProductFilter
    {
        public int? SectionId { get; set; }

        public int? BrandId { get; set; }
    }
}
