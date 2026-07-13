using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PaySystem.Models;
using Tap2PaySystem.Repositories;

namespace Tap2PaySystem.Services
{
    public class TopUpService
    {
        private readonly UserRepository repository;

        public TopUpService()
        {
            repository = new UserRepository();
        }

        public void AddTopUp(TopUp topUp)
        {
          
        }
        public List<User> GetAllUsers()
        {
            return repository.GetAllUsers();
        }

        public void UpdateBalance(int userId, decimal newBalance)
        {
            repository.UpdateBalance(userId, newBalance);
        }
    }
}