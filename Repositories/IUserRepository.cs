using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PayAdmin.Models;

namespace Tap2PayAdmin.Repositories
{
    public interface IUserRepository
    {
        User Login(string username, string password);
        List<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);

        User GetUserByRFID(string rfid);
    }

}
