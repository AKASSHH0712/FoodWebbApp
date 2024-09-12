using Food.DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Food.Repository
{
    public class Addtocart : IAddtocart
    {
        private readonly IConfiguration _configuration;

        public Addtocart(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       
        private string SqlConnection()
        {
            return _configuration.GetConnectionString("FoodConnection").ToString();
        }

        public void AddToCart(AddtoCartDTO add)
        {
            using (SqlConnection conn = new SqlConnection(this.SqlConnection()))
            {

                using (SqlCommand cmd = new SqlCommand("Addtocartitems", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USERID", add.USERID);
                    cmd.Parameters.AddWithValue("@PRODUCTID", add.PRODUCTID);
                    cmd.Parameters.AddWithValue("@QUANTITY", add.QUANTITY);  // Corrected Parameter Name
                    cmd.Parameters.AddWithValue("@CATEGORYID", add.CATEGORYID);  // Corrected Parameter Name
                    cmd.Parameters.AddWithValue("@PRICE", add.PRICE);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<AddtoCartDTO> GetCartData()
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                // Initialize the SqlCommand with your stored procedure
                SqlCommand cmd = new SqlCommand("GetCart", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                con.Open();
                DataTable dt = new DataTable();
                ad.Fill(dt);
                List<AddtoCartDTO> productDTOs = new List<AddtoCartDTO>();
                foreach (DataRow dr in dt.Rows)
                {
                    productDTOs.Add(new AddtoCartDTO()
                    {
                        PRODUCTID = Convert.ToInt32(dr["PRODUCTID"]),
                        CARTID = Convert.ToInt32(dr["CARTID"]),
                        USERID = Convert.ToInt32(dr["USERID"]),
                        QUANTITY = Convert.ToInt32(dr["QUANTITY"]),
                        PRICE = Convert.ToDecimal(dr["PRICE"]),

                    });
                }
                con.Close();
                return productDTOs;
            }
        }

        public void EditQuantity(int Quantity, int ProductId)
        {
            using (SqlConnection conn = new SqlConnection(this.SqlConnection()))
            {

                using (SqlCommand cmd = new SqlCommand("UpdateQuantity", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QUANTITY", Quantity);  // Corrected Parameter Name
                    cmd.Parameters.AddWithValue("@ProductId", ProductId);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditPrice(decimal Price, int ProductId)
        {
            using (SqlConnection conn = new SqlConnection(this.SqlConnection()))
            {

                using (SqlCommand cmd = new SqlCommand("UpdatePrice", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@price", Price);  // Corrected Parameter Name
                    cmd.Parameters.AddWithValue("@ProductId", ProductId);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public bool UpdateCartQuantity(int userId, int cartId, int quantity)
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("UpdateCartQuantity", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERID", userId);
                cmd.Parameters.AddWithValue("@CARTID", cartId);
                cmd.Parameters.AddWithValue("@QUANTITY", quantity);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }


    }
}
