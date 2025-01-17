using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
     public interface INotificationDatabaseRepository
    {
        public Task InsertNotification(Notification notification);
    }
}
