using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PayKiosk.Repositories;
using Tap2PayKiosk.Models;

namespace Tap2PayKiosk.Services
{
    public class UserService
    {
        private readonly UserRepository repository = new UserRepository();
        public User GetByRFID(string rfid)
        {
            return repository.GetByRFID(rfid);
        }

        public void UpdateBalance(int userId, decimal balance)
        {
            repository.UpdateBalance(userId, balance);
        }
    }
}
