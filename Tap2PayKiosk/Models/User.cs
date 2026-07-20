using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap2PayKiosk.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string RFIDUID { get; set; }

        public decimal Balance { get; set; }
    }
}