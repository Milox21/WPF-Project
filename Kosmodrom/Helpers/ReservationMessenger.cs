using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmodrom.Helpers
{
    public class ReservationMessenger<T> where T : class, new()
    {
        public int Id { set; get; } 
        public int Id2{ set; get; } 
        public T Reservation { get; set; }
        public ReservationMessenger(int id,T obj)
        {
            Id = id;
            Reservation = obj;
        }

        public ReservationMessenger(int id, int id2,T obj)
        {
            Id = id;
            Id2 = id2;
            Reservation = obj;
        }
    }
}
