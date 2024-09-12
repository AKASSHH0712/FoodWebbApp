using Food.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Repository
{
    public interface IAdmin
    {
        public List<ProductDTO> GetProducts();
        public ProductDTO GetProductById(int id);
        public void AddProduct(ProductDTO productDTO);
        public bool UpdatDate(ProductDTO productDTO);
        public List<ProductDTO> EditGetData();
        public void DeleteData(int id);
    }
}
