using System;
using ToDoApp.Application.ToDoItems.Commands.CreateToDoItem;

namespace ToDoApp.Application.Notifications.Models
{
    public class TodoCreatedNotificationDto:BaseNotification
    {
        public Guid ToDoId { get; set; }

        public override string Type => "Created";
    }
}
