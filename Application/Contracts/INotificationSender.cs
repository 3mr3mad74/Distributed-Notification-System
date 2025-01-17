using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
     public interface INotificationSender
    {
       public Task Send(string message);
    }
}
