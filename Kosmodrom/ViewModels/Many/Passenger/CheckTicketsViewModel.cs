using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Models.Add;
using Kosmodrom.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmodrom.ViewModels.Many.Passenger
{
    public class CheckTicketsViewModel : BaseManyViewModel<Reservation>
    {
        public int UserId { get; set; }
        public CheckTicketsViewModel() 
        {
            WeakReferenceMessenger.Default.Register<LoggerMessenger<PassengerWireframeViewModel>>(this, (recipient, message) => SetID(message));
        }

        public void SetID(LoggerMessenger<PassengerWireframeViewModel> message)
        {
            UserId = message.Id;
            Refresh();
        }

        public override void DeleteFromDatabase()
        {
            //throw new NotImplementedException();
        }

        public override void Edit()
        {
            //throw new NotImplementedException();
        }

        protected override List<ComboBoxVM> GetSearchColumns()
        {
            return new()
            {
                new(1,nameof(Reservation.SeatNumber)),
                new(2,nameof(Reservation.PassengerDepartures.Destination.Name)),
                new(3,nameof(Reservation.PassengerDepartures.Price)),
                new(4,nameof(Reservation.PassengerDepartures.TimeOfDeparture)),
            };
        }

        protected override IQueryable<Reservation> Search(IQueryable<Reservation> models)
        {
            switch (SearchColumn)
            {
                case nameof(Reservation.SeatNumber):
                    return models.Where(item => item.SeatNumber.ToString() == SearchInput);
                case nameof(Reservation.PassengerDepartures.Destination.Name):
                    return models.Where(item => item.PassengerDepartures.Destination.Name.ToString() == SearchInput);
                case nameof(Reservation.PassengerDepartures.Price):
                    return models.Where(item => item.PassengerDepartures.Price.ToString() == SearchInput);
                case nameof(Reservation.PassengerDepartures.TimeOfDeparture):
                    return models.Where(item => item.PassengerDepartures.TimeOfDeparture.ToString() == SearchInput);
                default:
                    return models;
            }
        }

        protected override IQueryable<Reservation> Sort(IQueryable<Reservation> models)
        {
            switch (SortColumn)
            {
                case nameof(Reservation.SeatNumber):
                    return SortDescending ? models.OrderByDescending(item => item.SeatNumber) : models.OrderBy(item => item.SeatNumber);
                case nameof(Reservation.PassengerDepartures.Destination):
                    return SortDescending ? models.OrderByDescending(item => item.PassengerDepartures.Destination.Name) : models.OrderBy(item => item.PassengerDepartures.Destination.Name);
                case nameof(Reservation.PassengerDepartures.Price):
                    return SortDescending ? models.OrderByDescending(item => item.PassengerDepartures.Price) : models.OrderBy(item => item.PassengerDepartures.Price);
                case nameof(Reservation.PassengerDepartures.TimeOfDeparture):
                    return SortDescending ? models.OrderByDescending(item => item.PassengerDepartures.TimeOfDeparture) : models.OrderBy(item => item.PassengerDepartures.TimeOfDeparture);
                default:
                    return models;
            }
        }
        public override void Refresh()
        {
            Models = new ObservableCollection<Reservation>(GetModels());
        }

        public override IQueryable<Reservation> GetModels()
        {
            return new DatabaseContext().Reservations.Include(item => item.PassengerDepartures).ThenInclude(item => item.Destination).Where(item => item.PassengerId == UserId && item.IsActive == true);
        }
    }

}
