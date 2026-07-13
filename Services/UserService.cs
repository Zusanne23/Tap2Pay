using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PaySystem.Models;
using Tap2PaySystem.Repositories;

namespace Tap2PaySystem.Services
{
    public class UserService
    {
        private readonly IUserRepository repository;

        public UserService()
        {
            repository = new UserRepository();
        }

        public List<User> GetAllUsers()
        {
            return repository.GetAllUsers();
        }

        public void AddUser(User user)
        {
            repository.AddUser(user);
        }

        public void UpdateUser(User user)
        {
            repository.UpdateUser(user);
        }

        public void DeleteUser(int userId)
        {
            repository.DeleteUser(userId);
        }
    }
}