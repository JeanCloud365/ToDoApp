using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ToDoApp.Application.Common.Interfaces
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
    }
}
