using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PayAdmin.Models;

namespace Tap2PayAdmin
{
    public static class Session
    {
        public static User CurrentUser { get; set; }
    }
}
