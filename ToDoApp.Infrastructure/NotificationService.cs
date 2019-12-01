using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Application.Notifications.Interfaces;
using ToDoApp.Application.Notifications.Models;

namespace ToDoApp.Infrastructure
{
    public class NotificationService:INotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly IToDoDbContext _toDoDbContext;
        private readonly ILogger _logger;

        public NotificationService(HttpClient httpClient, IToDoDbContext toDoDbContext,ILogger<NotificationService> logger)
        {
            _toDoDbContext = toDoDbContext;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task Notify(INotification notification)
        {
            var user = await _toDoDbContext.ToDoUsers.FindAsync(notification.UserId);
            if (user == null || user?.WebhookUrl == null)
            {
                return;
            }

            try
            {

                await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, user.WebhookUrl)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(notification),Encoding.UTF8,"application/json")
                });
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed sending webhook for user '{0}': {1}",user.Id,ex.ToString(),ex);
            }


        }

      
    }
}
