using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tap2PaySystem.Models;
using Tap2PaySystem.Repositories;

namespace Tap2PaySystem.Services
{
    public class TopUpHistoryService
    {
        private readonly TopUpHistoryRepository repository =
            new TopUpHistoryRepository();

        public List<TopUpHistory> GetTopUpHistory()
        {
            return repository.GetTopUpHistory();
        }
    }
}
