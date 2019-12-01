using System;
using ToDoApp.Application.Notifications.Interfaces;
using ToDoApp.Application.ToDoItems.Commands.CreateToDoItem;

namespace ToDoApp.Application.Notifications.Models
{
    public class TodoCreatedNotificationDto:INotification
    {
        public Guid ToDoId { get; set; }

        public Guid UserId { get; set; }
        public string Type => "Created";
    }
}
