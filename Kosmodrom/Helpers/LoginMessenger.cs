using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmodrom.Helpers
{
    public class LoginMessenger
    {
        public string Message;
        public int Id;

        public LoginMessenger(string message, int id)
        {
            Message = message;
            Id = id;
        }
    }
}
