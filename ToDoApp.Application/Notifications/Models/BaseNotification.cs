using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Application.Notifications.Models
{
    public abstract class BaseNotification
    {
        public Guid UserId { get; set; }
        public abstract string Type { get;  }
    }
}
