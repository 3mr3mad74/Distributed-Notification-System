using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class Notification
    {
        public string ApplicationId { get; set; }
        public string UserId { get; set; }
        public string TenantId { get; set; }
        public  string Message { get; set; }
        public CommunicationChannels CommunicationChannels { get; set; }

    }
}
