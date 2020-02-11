using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace ToDoApp.Application.ToDoItems.Queries.GetToDoItem
{
    public class GetToDoItemValidator : AbstractValidator<GetToDoItemQuery>
    {
        public GetToDoItemValidator()
        {
            RuleFor(o => o.ItemId).NotNull().NotEmpty();
        }
    }

}
