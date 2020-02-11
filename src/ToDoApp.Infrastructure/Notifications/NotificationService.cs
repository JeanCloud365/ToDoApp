using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Application.Notifications.Interfaces;
using ToDoApp.Application.Notifications.Models;

namespace ToDoApp.Infrastructure
{
    public class NotificationService : INotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly IToDoDbContext _toDoDbContext;
        private readonly ILogger _logger;

        public NotificationService(HttpClient httpClient, IToDoDbContext toDoDbContext, ILogger<NotificationService> logger)
        {
            _toDoDbContext = toDoDbContext;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task Notify(INotification notification)
        {
            var user = await _toDoDbContext.ToDoUsers.Include(o => o.Webhooks).FirstOrDefaultAsync(o => o.Id.Equals(notification.UserId));
            if (user == null || user.Webhooks.Count == 0)
            {
                return;
            }

            foreach (var webhook in user.Webhooks)
            {

                try
                {


                    await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, webhook.Url)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(notification), Encoding.UTF8,
                            "application/json")
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed sending webhook '{3}' for user '{0}': {1}", user.Id, ex.ToString(), webhook.Url, ex);
                }

            }


        }


    }
}
