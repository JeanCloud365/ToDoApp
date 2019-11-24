using System;
using ToDoApp.Application.ToDoItems.Commands.CreateToDoItem;

namespace ToDoApp.Application.Notifications.Models
{
    public class TodoUpdatedNotificationDto:BaseNotification
    {
        public Guid ToDoId { get; set; }

        public override string Type => "Updated";
    }
}
