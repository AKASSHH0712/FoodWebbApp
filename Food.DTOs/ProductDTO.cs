using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DTOs
{
    public class ProductDTO()
    {
        public int PRODUCTID { get; set; }
        public decimal PRICE { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string IMAGEURL { get; set; }
        public IFormFile file { get; set; }
        public int CATEGORYID { get; set; }
        public string QUANTITY { get; set; }
        public bool ISACTIVE { get; set; }
        public string CATEGORYNAME { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
        public IEnumerable<ProductDTO> FastFood { get; set; }
        public IEnumerable<ProductDTO> SouthIndian { get; set; }
        public IEnumerable<ProductDTO> Chinese { get; set; }

        public IEnumerable<ProductDTO> Italian { get; set; }

        public IEnumerable<ProductDTO> Mexican { get; set; }

        public IEnumerable<ProductDTO> Desserts { get; set; }

        public IEnumerable<ProductDTO> Beverages { get; set; }

        public IEnumerable<ProductDTO> NorthIndian { get; set; }

        public IEnumerable<ProductDTO> Continental { get; set; }

        public IEnumerable<ProductDTO> Seafood { get; set; }



    }
}
