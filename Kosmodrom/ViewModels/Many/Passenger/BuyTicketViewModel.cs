using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Models.Add;
using Kosmodrom.Models.Contexts;
using Kosmodrom.Views.WindowPopUps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kosmodrom.ViewModels.Many.Passenger
{
    public class BuyTicketViewModel : BaseManyViewModel<PassengerDeparture>
    {
        public int UserId;
        private Reservation _Model { set; get; }
        public Reservation Model
        {
            get => _Model;
            set
            {
                if (Model != value)
                {
                    _Model = value;
                    OnPropertyChanged(() => Model);
                }
            }
        }
        public ICommand BuyCommand { set; get; }
        public BuyTicketViewModel() 
        {
            BuyCommand = new BaseCommand(() => BuyTicket());
            WeakReferenceMessenger.Default.Register<LoggerMessenger<PassengerWireframeViewModel>>(this, (recipient, message) => SetID(message));
            WeakReferenceMessenger.Default.Register<ReservationMessenger<HangarReservation>>(this, (recipient, message) => SeatPick(message));       
        }
        public void SetID(LoggerMessenger<PassengerWireframeViewModel> message)
        {
            UserId = message.Id;
            Refresh();
        }
        public void BuyTicket()
        {
            SeatPickView pop = new SeatPickView();
            pop.Show();
            WeakReferenceMessenger.Default.Send(new ReservationMessenger<PassengerDeparture>(UserId, SelectedItem.Id, SelectedItem));
            
        }
        public override void DeleteFromDatabase()
        {
            //throw new NotImplementedException();
        }
        public void SeatPick(ReservationMessenger<HangarReservation> message)
        {
            Database.Reservations.Add(new Reservation()
            {
                Id = 0,
                SeatNumber = message.Id,
                PassengerDeparturesId = SelectedItem.Id,
                PassengerId = UserId,
                IsActive = true,
                CreatedAt = DateTime.Now,
                LastEditedAt = DateTime.Now,
            });
            Database.SaveChanges();
        }
        public override void Edit()
        {
            //throw new NotImplementedException();
        }

        protected override List<ComboBoxVM> GetSearchColumns()
        {
            return new()
            {
                new(1,nameof(PassengerDeparture.TimeOfDeparture)),
                new(2,nameof(PassengerDeparture.TimeOfArrival)),
                new(3,nameof(PassengerDeparture.Destination)),
                new(4,nameof(PassengerDeparture.Price)),
                new(5,nameof(PassengerDeparture.Vehicle)),
                new(6,nameof(PassengerDeparture.Vehicle.SeatCount)),
                new(7,nameof(PassengerDeparture.Pilot1)),
                new(8,nameof(PassengerDeparture.Pilot2)),
            };
        }

        protected override IQueryable<PassengerDeparture> Search(IQueryable<PassengerDeparture> models)
        {
            switch (SearchColumn)
            {
                case nameof(PassengerDeparture.TimeOfDeparture):
                    return models.Where(item => item.TimeOfDeparture.ToString() == SearchInput);
                case nameof(PassengerDeparture.Destination):
                    return models.Where(item => item.Destination.Name.ToString() == SearchInput);
                case nameof(PassengerDeparture.TimeOfArrival):
                    return models.Where(item => item.TimeOfArrival.ToString() == SearchInput);
                case nameof(PassengerDeparture.Vehicle.SeatCount):
                    return models.Where(item => item.Vehicle.SeatCount.ToString() == SearchInput);
                case nameof(PassengerDeparture.Vehicle):
                    return models.Where(item => item.Vehicle.Name.ToString() == SearchInput);
                case nameof(PassengerDeparture.Pilot1):
                    return models.Where(item => item.Pilot1.FirstName.ToString() == SearchInput);
                case nameof(PassengerDeparture.Pilot2):
                    return models.Where(item => item.Pilot2.FirstName.ToString() == SearchInput);
                case nameof(PassengerDeparture.Price):
                    return models.Where(item => item.Price.ToString() == SearchInput);
                default:
                    return models;
            }
        }

        protected override IQueryable<PassengerDeparture> Sort(IQueryable<PassengerDeparture> models)
        {
            switch (SortColumn)
            {
                case nameof(PassengerDeparture.TimeOfDeparture):
                    return SortDescending ? models.OrderByDescending(item => item.TimeOfDeparture) : models.OrderBy(item => item.TimeOfDeparture);
                case nameof(PassengerDeparture.Destination):
                    return SortDescending ? models.OrderByDescending(item => item.Destination.Name) : models.OrderBy(item => item.Destination.Name);
                case nameof(PassengerDeparture.TimeOfArrival):
                    return SortDescending ? models.OrderByDescending(item => item.TimeOfArrival) : models.OrderBy(item => item.TimeOfArrival);
                case nameof(PassengerDeparture.Vehicle.SeatCount):
                    return SortDescending ? models.OrderByDescending(item => item.Vehicle.SeatCount) : models.OrderBy(item => item.Vehicle.SeatCount);
                case nameof(PassengerDeparture.Vehicle):
                    return SortDescending ? models.OrderByDescending(item => item.Vehicle.Name) : models.OrderBy(item => item.Vehicle.Name);
                case nameof(PassengerDeparture.Pilot1):
                    return SortDescending ? models.OrderByDescending(item => item.Pilot1.FirstName) : models.OrderBy(item => item.Pilot1.FirstName);
                case nameof(PassengerDeparture.Pilot2):
                    return SortDescending ? models.OrderByDescending(item => item.Pilot2.FirstName) : models.OrderBy(item => item.Pilot2.FirstName);
                case nameof(PassengerDeparture.Price):
                    return SortDescending ? models.OrderByDescending(item => item.Price) : models.OrderBy(item => item.Price);
                default:
                    return models;
            }
        }
        public override void Refresh()
        {
            Models = new ObservableCollection<PassengerDeparture>(GetModels());
        }

        public override IQueryable<PassengerDeparture> GetModels()
        {
            return new DatabaseContext().PassengerDepartures.Include(item => item.Destination)
                                        .Include(item => item.LandingPadReservation)
                                        .Include(item => item.HangarReservation)
                                        .Include(item => item.Pilot1)
                                        .Include(item => item.Pilot2)
                                        .Include(item => item.Vehicle)
                                        .Where(item => item.IsActive == true);
        }
    }
}
