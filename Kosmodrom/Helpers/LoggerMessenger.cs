using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmodrom.Helpers
{
    public class LoggerMessenger<T> where T : class, new()
    {
        public LoggerMessenger(int iD)
        {
            Id = iD;
            LoggerType = new();
        }

        public int Id { get; set; }
        public T LoggerType { get; set; }
        
    }
}
