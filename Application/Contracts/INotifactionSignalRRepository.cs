using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface INotifactionSignalRRepository
    {
        public Task SendNotificationToTenant(string tenantId, string message);
        public Task SendNotificationToUser(string userId, string message);
    }
}
