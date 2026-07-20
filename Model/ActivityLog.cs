using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap2PayAdmin.Models
{
    public class ActivityLog
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
        public DateTime LogDate { get; set; }
    }
}