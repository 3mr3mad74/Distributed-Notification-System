﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EmailNotification:Notification
    { 
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
    }
}
