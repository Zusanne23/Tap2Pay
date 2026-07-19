using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PayAdmin.Models;
using Tap2PayAdmin.Repositories;

namespace Tap2PayAdmin.Services
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