using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PaySystem.Models;

namespace Tap2PaySystem.Repositories
{
    public interface ITopUpRepository
    {
        void AddTopUp(TopUp topUp);
        List<User> GetAllUsers();
        void UpdateBalance(int userId, decimal newBalance);
    }
}