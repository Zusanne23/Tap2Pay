using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PayAdmin.Models;
using Tap2PayAdmin.Repositories;

namespace Tap2PayAdmin.Services
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
            repository.AddTopUp(topUp);
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