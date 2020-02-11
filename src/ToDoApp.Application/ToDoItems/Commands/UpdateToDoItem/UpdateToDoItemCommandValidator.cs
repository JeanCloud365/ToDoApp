using System.Data;
using FluentValidation;

namespace ToDoApp.Application.ToDoItems.Commands.UpdateToDoItem
{
    public class UpdateToDoItemCommandValidator:AbstractValidator<UpdateToDoItemCommand>
    {
        public UpdateToDoItemCommandValidator()
        {
            RuleFor(o => o.Title).NotEmpty().NotNull();
            RuleFor(o => o.Description).NotEmpty().NotNull();
        }
        
    }
}