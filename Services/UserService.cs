using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PayAdmin.Models;
using Tap2PayAdmin.Repositories;

namespace Tap2PayAdmin.Services
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

        public User GetUserByRFID(string rfid)
        {
            return repository.GetUserByRFID(rfid);
        }

        public int GetTotalUsers()
        {
            return repository.GetTotalUsers();
        }

        public int GetActiveUsers()
        {
            return repository.GetActiveUsers();
        }

        public int GetCashiers()
        {
            return repository.GetCashiers();
        }
    }
}