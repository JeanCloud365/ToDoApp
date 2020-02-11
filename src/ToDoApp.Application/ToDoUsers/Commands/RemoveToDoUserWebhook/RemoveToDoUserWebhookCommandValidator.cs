using FluentValidation;

namespace ToDoApp.Application.ToDoUsers.Commands.RemoveToDoUserWebhook
{
    public class RemoveToDoUserWebhookCommandValidator : AbstractValidator<RemoveToDoUserWebhookCommand>
    {
        public RemoveToDoUserWebhookCommandValidator()
        {
            RuleFor(o => o.Id).NotEmpty().NotNull();
        }
    }
}
