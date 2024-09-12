using Food.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Repository
{
    public class Admin : IAdmin
    {
        private readonly IConfiguration _configuration;

        public Admin(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string SqlConnection()
        {
            return _configuration.GetConnectionString("FoodConnection").ToString();
        }
        public List<ProductDTO> GetProducts()
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                // Initialize the SqlCommand with your stored procedure
                SqlCommand cmd = new SqlCommand("GetProduct", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                con.Open();
                DataTable dt = new DataTable();
                ad.Fill(dt);
                List<ProductDTO> productDTOs = new List<ProductDTO>();
                foreach (DataRow dr in dt.Rows)
                {
                    productDTOs.Add(new ProductDTO()
                    {
                        PRODUCTID = Convert.ToInt32(dr["PRODUCTID"]),
                        NAME = Convert.ToString(dr["NAME"]),
                        PRICE = Convert.ToDecimal(dr["PRICE"]),
                        DESCRIPTION = Convert.ToString(dr["description"]),
                        IMAGEURL = Convert.ToString(dr["IMAGEURL"]),
                        CATEGORYID = Convert.ToInt32(dr["CATEGORYID"]),
                        QUANTITY = Convert.ToString(dr["QUANTITY"]),
                        ISACTIVE = Convert.ToBoolean(dr["ISACTIVE"]),
                        CATEGORYNAME = Convert.ToString(dr["CATEGORYNAME"])
                    });
                }
                con.Close();
                return productDTOs;
            }
        }
        public ProductDTO GetProductById(int id)
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                // Initialize the SqlCommand with your stored procedure
                SqlCommand cmd = new SqlCommand("GetProductById", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add the ID parameter to the command
                cmd.Parameters.AddWithValue("@PRODUCTID", id);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                ProductDTO productDTO = null;

                // Check if a record was found
                if (reader.Read())
                {
                    productDTO = new ProductDTO
                    {
                        PRODUCTID = Convert.ToInt32(reader["PRODUCTID"]),
                        NAME = Convert.ToString(reader["NAME"]),
                        PRICE = Convert.ToDecimal(reader["PRICE"]),
                        DESCRIPTION = Convert.ToString(reader["DESCRIPTION"]),
                        IMAGEURL = Convert.ToString(reader["IMAGEURL"]),
                        CATEGORYID = Convert.ToInt32(reader["CATEGORYID"]),
                        QUANTITY = Convert.ToString(reader["QUANTITY"]),
                        ISACTIVE = Convert.ToBoolean(reader["ISACTIVE"]),

                    };
                }

                con.Close();
                return productDTO;
            }
        }


        public void AddProduct(ProductDTO productDTO)
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("AddProduct", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NAME", productDTO.NAME);
                cmd.Parameters.AddWithValue("@DESCRIPTION", productDTO.DESCRIPTION);
                cmd.Parameters.AddWithValue("@PRICE", productDTO.PRICE);
                cmd.Parameters.AddWithValue("@QUANTITY", productDTO.QUANTITY);
                cmd.Parameters.AddWithValue("@IMAGEURL", productDTO.IMAGEURL);
                cmd.Parameters.AddWithValue("@CATEGORYID", productDTO.CATEGORYID);
                cmd.Parameters.AddWithValue("@ISACTIVE", productDTO.ISACTIVE);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }


        }
        public bool UpdatDate(ProductDTO productDTO)
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("UpdateProduct", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PRODUCTID", productDTO.PRODUCTID);
                cmd.Parameters.AddWithValue("@NAME", productDTO.NAME);
                cmd.Parameters.AddWithValue("@DESCRIPTION", productDTO.DESCRIPTION);
                cmd.Parameters.AddWithValue("@PRICE", productDTO.PRICE);
                cmd.Parameters.AddWithValue("@QUANTITY", productDTO.QUANTITY);
                cmd.Parameters.AddWithValue("@IMAGEURL", productDTO.IMAGEURL);
                cmd.Parameters.AddWithValue("@CATEGORYID", productDTO.CATEGORYID);
                cmd.Parameters.AddWithValue("@ISACTIVE", productDTO.ISACTIVE);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public List<ProductDTO> EditGetData()
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("GetProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                List<ProductDTO> EmpList = new List<ProductDTO>();

                foreach (DataRow dr in dt.Rows)
                {
                    EmpList.Add
                        (new ProductDTO
                        {
                            PRODUCTID = Convert.ToInt32(dr["PRODUCTID"]),
                            NAME = Convert.ToString(dr["NAME"]),
                            PRICE = Convert.ToDecimal(dr["PRICE"]),
                            DESCRIPTION = Convert.ToString(dr["description"]),
                            IMAGEURL = Convert.ToString(dr["IMAGEURL"]),
                            CATEGORYID = Convert.ToInt32(dr["CATEGORYID"]),
                            QUANTITY = Convert.ToString(dr["QUANTITY"]),
                            ISACTIVE = Convert.ToBoolean(dr["ISACTIVE"]),

                        }
                        );
                }
                return EmpList;
            }
        }
        public void DeleteData(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.SqlConnection()))
                {
                    SqlCommand cmd = new SqlCommand("DELETEPRODUCTS", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PRODUCTID", id);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    // You can check if i > 0 to confirm that a row was deleted
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error while deleting the product: " + ex.Message);
            }


        }

    }
}
