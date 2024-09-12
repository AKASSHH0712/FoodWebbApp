using Food.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Repository
{
   public interface IAddtocart
    {
        public List<AddtoCartDTO> GetCartData();
        public void AddToCart(AddtoCartDTO addtocartDto);

        public void EditQuantity(int Quantity,int ProductId);
        public void EditPrice(decimal Price, int ProductId);
        public bool UpdateCartQuantity(int userId, int cartId, int quantity);
        //public int GetTotalPrice();

    }
}
