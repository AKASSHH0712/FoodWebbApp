using Food.DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Repository
{
    public class FoodRepo : IFoodRepo
    {
        private readonly IConfiguration _configuration;

        public FoodRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string SqlConnection()
        {
            return _configuration.GetConnectionString("FoodConnection").ToString();
        }
        public void AddUser(UserDto userDTO)
        {
            if (UserExists(userDTO))
            {
                throw new Exception("User already exists with the provided username or email.");
            }

            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("AddUser", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NAME", userDTO.NAME);
                cmd.Parameters.AddWithValue("@USERNAME", userDTO.USERNAME);
                cmd.Parameters.AddWithValue("@PASSWORD", userDTO.PASSWORD);
                cmd.Parameters.AddWithValue("@EMAIL", userDTO.EMAIL);
                cmd.Parameters.AddWithValue("@MOBILE", userDTO.MOBILE);
                cmd.Parameters.AddWithValue("@ADDRESS", userDTO.ADDRESS);
                cmd.Parameters.AddWithValue("@POSTCODE", userDTO.POSTCODE);
                cmd.Parameters.AddWithValue("@IMAGEURL", userDTO.IMAGEURL);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
        public bool UserExists(UserDto userDto)
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("CheckUserExists", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERNAME", userDto.USERNAME);
                cmd.Parameters.AddWithValue("@EMAIL", userDto.EMAIL);



                con.Open();
                int userCount = (int)cmd.ExecuteScalar();
                con.Close();

                return userCount > 0;
            }
        }



        public List<UserDto> GetUsers()
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetUser", con))
                {
                    List<UserDto> List = new List<UserDto>();

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    con.Open();
                    sda.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        List.Add(new UserDto
                        {
                           
                            NAME = Convert.ToString(dr["NAME"]),
                            USERNAME = Convert.ToString(dr["USERNAME"]),
                            PASSWORD = Convert.ToString(dr["PASSWORD"]),
                            USERID = Convert.ToInt32(dr["USERID"]),
                            IMAGEURL = Convert.ToString(dr["IMAGEURL"]),
                            ADDRESS =  Convert.ToString(dr["ADDRESS"]),
                            POSTCODE = Convert.ToString(dr["POSTCODE"]),
                            EMAIL = Convert.ToString(dr["EMAIL"]),
                            MOBILE = Convert.ToString(dr["MOBILE"]),




                        });
                    }

                    return List;
                }
            }
        }
       public bool UpdatDate(UserDto userDto)
{
    using (SqlConnection con = new SqlConnection(this.SqlConnection()))
    {
        SqlCommand cmd = new SqlCommand("UpdateUser", con); // Correct stored procedure name
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@NAME", userDto.NAME);
        cmd.Parameters.AddWithValue("@USERNAME", userDto.USERNAME);
        cmd.Parameters.AddWithValue("@PASSWORD", userDto.PASSWORD);
        cmd.Parameters.AddWithValue("@EMAIL", userDto.EMAIL);
        cmd.Parameters.AddWithValue("@MOBILE", userDto.MOBILE);
        cmd.Parameters.AddWithValue("@ADDRESS", userDto.ADDRESS);
        cmd.Parameters.AddWithValue("@POSTCODE", userDto.POSTCODE);
        cmd.Parameters.AddWithValue("@IMAGEURL", userDto.IMAGEURL);
        cmd.Parameters.AddWithValue("@USERID", userDto.USERID); // Make sure USERID is updated

        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        return i > 0;
    }
}


    }
}
