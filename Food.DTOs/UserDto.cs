using Microsoft.AspNetCore.Http;

namespace Food.DTOs
{
    public class UserDto
    {
        public  int  USERID { get; set; }
        public string NAME { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string ADDRESS { get; set; }
        public string POSTCODE { get; set; }
        public string EMAIL { get; set; }
        public string MOBILE { get; set; }
        public string IMAGEURL { get; set; }
        public IFormFile IMAGE { get; set; }
    }
}
