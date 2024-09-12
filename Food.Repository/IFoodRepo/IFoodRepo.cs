using Food.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Repository
{
    public interface IFoodRepo
    {
        public void AddUser(UserDto userDto);
        public bool UserExists(UserDto userDto);

        public List<UserDto> GetUsers();
        public bool UpdatDate(UserDto userDto);

    }
}
