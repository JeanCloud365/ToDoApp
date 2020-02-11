using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace ToDoApp.Domain.Entities
{
    public class WebHook
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Uri Url { get; set; }
        public ToDoUser Owner { get; set; }
        public Guid OwnerId { get; set; }


        public override bool Equals(object obj)
        {
            var compare = obj as WebHook;

            if (compare == null)
            {
                return false;
            }
            return this.Id.Equals(compare.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
