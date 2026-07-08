using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PaySystem.Models;

namespace Tap2PaySystem.Repositories
{
    public interface IUserRepository
    {
        User Login(string username, string password);
        List<User> GetAllUsers();
        void AddUser(User user);
    }

}
