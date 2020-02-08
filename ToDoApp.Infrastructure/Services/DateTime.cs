using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.Application.Common.Interfaces;

namespace ToDoApp.Infrastructure.Services
{
    public class DateTimeService:IDateTimeService
    {
        public System.DateTime Now => System.DateTime.Now;
    }
}
