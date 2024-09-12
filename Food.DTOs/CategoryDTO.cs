using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DTOs
{
    public class CategoryDTO
    {
        public int CATEGORYID { get; set; }
        public string CATEGORYNAME { get; set; }
        public IEnumerable<ProductDTO> PRODUCTS { get; set; }
    }
}
