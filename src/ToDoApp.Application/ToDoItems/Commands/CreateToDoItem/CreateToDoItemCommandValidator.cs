using System.Data;
using FluentValidation;

namespace ToDoApp.Application.ToDoItems.Commands.CreateToDoItem
{
    public class CreateToDoItemCommandValidator:AbstractValidator<CreateToDoItemCommand>
    {
        public CreateToDoItemCommandValidator()
        {
            RuleFor(o => o.Title).NotEmpty().NotNull();
            RuleFor(o => o.Description).NotEmpty().NotNull();
        }
        
    }
}