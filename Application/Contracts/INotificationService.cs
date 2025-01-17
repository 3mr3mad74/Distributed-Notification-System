using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface INotificationService
    {
        public  Task SendRealTimeNotification(RealTimeNotification notification);

    }
}
