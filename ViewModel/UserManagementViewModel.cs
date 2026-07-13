using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Tap2PaySystem.Models;
using Tap2PaySystem.Services;

namespace Tap2PaySystem.ViewModel
{
        public class UserManagementViewModel : BaseViewModel
        {
            private readonly UserService userService;

            public ObservableCollection<User> Users { get; set; }

            public UserManagementViewModel()
            {
                userService = new UserService();

                LoadUsers();
            }

            public void LoadUsers()
            {
                Users = new ObservableCollection<User>(userService.GetAllUsers());

                OnPropertyChanged(nameof(Users));
            }
        }
}
