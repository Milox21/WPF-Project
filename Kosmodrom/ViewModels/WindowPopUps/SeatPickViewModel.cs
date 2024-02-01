using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Models.Add;
using Kosmodrom.Models.Contexts;
using Kosmodrom.ViewModels.Many;
using Kosmodrom.ViewModels.Single;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Kosmodrom.ViewModels.WindowPopUps
{
    public class SeatPickViewModel : BaseSingleViewModel<PassengerDeparture>
    {
        public int UserId;
        public int DepartureId;
        private int _PickedSeat {  get; set; }
        public int PickedSeat
        {
            get
            { return _PickedSeat; }
            set
            {
                if (PickedSeat != value)
                {
                    _PickedSeat = value;
                    OnPropertyChanged(() => PickedSeat);
                }
            }
        }
        public ICommand OKCommand { get; set; }
        public List<int> Seats { get; set; }
        public SeatPickViewModel()
        {
            Seats = new List<int>();
            OKCommand = new BaseCommand(() => Close());
            WeakReferenceMessenger.Default.Register<ReservationMessenger<PassengerDeparture>>(this, (recipient, message) => SetSeats(message));
           
        }

        public void SetSeats(ReservationMessenger<PassengerDeparture> message)
        {
            UserId = message.Id;
            DepartureId = message.Id2;
            //PassengerDeparture departure = Database.PassengerDepartures.FirstOrDefault(item => item.IsActive == true && item.Id == message.Id2);
            Seats = Enumerable.Range(1, message.Reservation.Vehicle.SeatCount).ToList();

            List<int> SeatsTaken = Database.Reservations.Where(item => item.PassengerDeparturesId ==  message.Id2).Select(item => item.SeatNumber).ToList();

            Seats = Seats.Except(SeatsTaken).ToList();
            OnPropertyChanged(() => Seats);
        }
        public void Close()
        {
            Database.Reservations.Add(new Reservation()
            {
                PassengerDeparturesId = DepartureId,
                PassengerId = Properties.Settings.Default.UserId,
                SeatNumber = PickedSeat,
            });;
            Database.SaveChanges();
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }
        protected override void AddRadio()
        {

        }

        protected override void Clear()
        {

        }

        protected override DbSet<PassengerDeparture> GetDBTable()
        {
            return Database.PassengerDepartures;
        }

        protected override PassengerDeparture InitializeModel()
        {
            return new PassengerDeparture();
        }
    }
}
