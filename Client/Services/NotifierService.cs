using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Services
{
    public class NotifierService 
    {
        private readonly List<string> values = new List<string>();


        public async Task AddTolist(string value)
        {
            values.Add(value);
            if (Notify != null)
            {
                await Notify?.Invoke();
            }

        }

        public event Func<Task> Notify;
    }
}
