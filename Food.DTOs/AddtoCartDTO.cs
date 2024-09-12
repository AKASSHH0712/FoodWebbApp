using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DTOs
{
   public class AddtoCartDTO
    {
       
            public int CARTID { get; set; }
            public int USERID { get; set; }
            public int PRODUCTID { get; set; }
            public int QUANTITY { get; set; }
        public int CATEGORYID { get; set; }
        public decimal PRICE { get; set; }

    }

    }

