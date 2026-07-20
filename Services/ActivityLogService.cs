using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PayAdmin.Repositories;
using Tap2PayAdmin.Models;

namespace Tap2PayAdmin.Services
{
    public class ActivityLogService
    {
        private readonly ActivityLogRepository repository =
            new ActivityLogRepository();

        public void AddLog(
            int userId,
            string fullName,
            string role,
            string action,
            string details)
        {
            ActivityLog log = new ActivityLog
            {
                UserId = userId,
                FullName = fullName,
                Role = role,
                Action = action,
                Details = details,
                LogDate = DateTime.Now
            };

            repository.AddLog(log);
        }

        public List<ActivityLog> GetAllLogs()
        {
            return repository.GetAllLogs();
        }

        public int GetTotalLogs()
        {
            return repository.GetTotalLogs();
        }
    }
}
