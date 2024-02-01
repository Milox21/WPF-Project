using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Models.Add;
using Kosmodrom.Models.Contexts;
using Kosmodrom.Views.WindowPopUps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace Kosmodrom.ViewModels.Single.Admin
{
    public class DepartureViewModel : BaseSingleViewModel<PassengerDeparture>
    {
        public DateTime TimeOfDeparture
        {
            get => Model.TimeOfDeparture;
            set
            {
                if (TimeOfDeparture != value)
                {
                    Model.TimeOfDeparture = value;
                    OnPropertyChanged(() => TimeOfDeparture);
                }
            }
        }
        public DateTime TimeOfArrival
        {
            get => Model.TimeOfArrival;
            set
            {
                if (TimeOfArrival != value)
                {
                    Model.TimeOfArrival = value;
                    OnPropertyChanged(() => TimeOfArrival);
                }
            }
        }
        public int? PilotOne
        {
            get => Model.Pilot1Id;
            set
            {
                if (PilotOne != value)
                {
                    Model.Pilot1Id = value;
                    OnPropertyChanged(() => PilotOne);
                }
            }
        }
        public int? PilotTwo
        {
            get => Model.Pilot2Id;
            set
            {
                if (PilotTwo != value)
                {
                    Model.Pilot2Id = value;
                    OnPropertyChanged(() => PilotTwo);
                }
            }
        }
        public int? SelectedDestination
        {
            get => Model.DestinationId;
            set
            {
                if (SelectedDestination != value)
                {
                    Model.DestinationId = value;
                    OnPropertyChanged(() => SelectedDestination);
                }
            }
        }
        public int? SelectedVehicle
        {
            get => Model.VehicleId;
            set
            {
                if (SelectedVehicle != value)
                {
                    Model.VehicleId = value;
                    OnPropertyChanged(() => SelectedVehicle);
                }
            }
        }
        public double? Price
        {

            get => Model.Price;
            set
            {
                if (Price != value)
                {
                    Model.Price = value;
                    OnPropertyChanged(() => Price);
                }
            }
        }
        public ICommand ReserveLandingCommand { set; get; }
        public ICommand ReserveHangarCommand { set; get; }
        public List<GenericComboBoxVM<Pilot>> Pilots { get; set; }
        public List<GenericComboBoxVM<Destination>> Destinations { get; set; }
        public List<GenericComboBoxVM<Vehicle>> Vehicles { get; set; }
        public DepartureViewModel() 
        {
            WeakReferenceMessenger.Default.Register<ReservationMessenger<LandingPadsReservation>>(this, (recipient, message) => SetPadReservation(message));
            WeakReferenceMessenger.Default.Register<ReservationMessenger<HangarReservation>>(this, (recipient, message) => SetHangarReservation(message));

            ReserveLandingCommand = new BaseCommand(() => ReserveLadning());
            ReserveHangarCommand = new BaseCommand(() => ReserveHangar());

            TimeOfDeparture = DateTime.Now;
            TimeOfArrival = DateTime.Now;
            Pilots = SetPilots();
            Destinations = SetDestinations();
            Vehicles = SetVehicles();
            

        }

        public void SetPadReservation(ReservationMessenger<LandingPadsReservation> message)
        {
            if (message.Id == 5)
            {
                message.Reservation.CompanyId = null;
                Model.LandingPadReservation = message.Reservation;
            }
        }
        public void SetHangarReservation(ReservationMessenger<HangarReservation> message)
        {
            if (message.Id == 6)
            {
                message.Reservation.CompanyId = null;
                Model.HangarReservation = message.Reservation;
            }
        }
        public void ReserveLadning()
        {
            SingleReservationView pop = new(5);
            pop.Show();
        }
        public void ReserveHangar()
        {
            SingleReservationView pop = new(6);
            pop.Show();
        }
        protected override void Edit(EdditorMessenger<PassengerDeparture> message)
        {
            Model = Database.PassengerDepartures.FirstOrDefault(item => item.Id == message.Item.Id && item.IsActive == true);
            Refresh();
            //Price = message.Item.Price;
            //TimeOfDeparture = message.Item.TimeOfDeparture;
            //TimeOfArrival = message.Item.TimeOfArrival;
            //PilotOne = message.Item.Pilot1Id;
            //PilotTwo = message.Item.Pilot2Id;
            //SelectedDestination = message.Item.DestinationId; 
            //SelectedVehicle = message.Item.VehicleId;
            //Model.CreatedAt = message.Item.CreatedAt;
            //Model.LastEditedAt = message.Item.LastEditedAt;
        }
        public List<GenericComboBoxVM<Pilot>> SetPilots()
        {

            return Database.Pilots.Where(item => item.IsActive == true).Select(item => new GenericComboBoxVM<Pilot>()
            {
                Id = item.Id,
                Item = item
            }).ToList();

        }

        public List<GenericComboBoxVM<Destination>> SetDestinations()
        {
            return Database.Destinations.Where(item => item.IsActive == true).Select(item => new GenericComboBoxVM<Destination>()
            {
                Id = item.Id,
                Item = item
            }).ToList();
        }

        public List<GenericComboBoxVM<Vehicle>> SetVehicles()
        {
            return Database.Vehicles.Where(item => item.IsActive == true).Select(item => new GenericComboBoxVM<Vehicle>()
            {
                Id = item.Id,
                Item = item
            }).ToList();
        }
        protected override void AddRadio()
        {
            
        }

        protected override void Clear()
        {
            Model.Id = 0;
            TimeOfDeparture = DateTime.Now;
            TimeOfArrival = DateTime.Now;
            Price = 0;
            SelectedVehicle = null;
            SelectedDestination = null;
            PilotOne = null;
            PilotTwo = null;


        }

        protected override DbSet<PassengerDeparture> GetDBTable()
        {
            return Database.PassengerDepartures;
        }

        protected override PassengerDeparture InitializeModel()
        {
            return new PassengerDeparture()
            {
                CreatedAt = DateTime.Now,
                LastEditedAt = DateTime.Now,

            };
        }


    }
}
