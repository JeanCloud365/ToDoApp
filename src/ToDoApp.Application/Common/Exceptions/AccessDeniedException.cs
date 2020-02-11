using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Application.Common.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string message)
            : base(message)
        {
        }
    }
}
