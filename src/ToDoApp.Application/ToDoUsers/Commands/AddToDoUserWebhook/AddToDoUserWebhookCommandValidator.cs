using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace ToDoApp.Application.ToDoUsers.Commands.AddToDoUserWebhook
{
    public class AddToDoUserWebhookCommandValidator : AbstractValidator<AddToDoUserWebhookCommand>
    {
        public AddToDoUserWebhookCommandValidator()
        {
           
            RuleFor(o => o.WebhookUrl).NotEmpty().NotNull();
        }
    }
}
