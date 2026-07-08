using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PaySystem.Models;
using Tap2PaySystem.Repositories;

namespace Tap2PaySystem.Services
{
    public class LoginService
    {
        private readonly IUserRepository repository;

        public LoginService()
        {
            repository = new UserRepository();
        }

        public User Login(string username, string password)
        {
            return repository.Login(username, password);
        }
    }
}