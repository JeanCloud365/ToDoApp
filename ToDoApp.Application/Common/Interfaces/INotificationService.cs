using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Notifications.Models;

namespace ToDoApp.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task Notify(BaseNotification notification);
    }
}
