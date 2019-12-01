using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Application.Notifications.Interfaces
{
    public interface INotification
    {
        Guid UserId { get; set; }
        string Type { get; }
    }
}
