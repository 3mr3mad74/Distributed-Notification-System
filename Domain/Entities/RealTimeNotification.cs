﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RealTimeNotification : Notification
    {
        public bool IsRead { get; set; }

    }
}
