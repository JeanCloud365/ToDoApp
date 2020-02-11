using ToDoApp.Domain.Infrastructure;

namespace ToDoApp.Domain.Enumerations
{
    public class ToDoStatus:Enumeration
    {
        public static ToDoStatus NotDone = new ToDoStatus(1,"NotDone");
        public static ToDoStatus Done = new ToDoStatus(2,"Done");
        public ToDoStatus(int id, string name) : base(id, name)
        {
            
        }
    }
}