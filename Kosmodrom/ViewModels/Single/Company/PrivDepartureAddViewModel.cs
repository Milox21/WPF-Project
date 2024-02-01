using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Models.Add;
using Kosmodrom.Models.Contexts;
using Kosmodrom.ViewModels.Many;
using Kosmodrom.Views.WindowPopUps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kosmodrom.ViewModels.Single.Company
{
    class PrivDepartureAddViewModel : BaseSingleViewModel<PrivateDeparture>
    {
        public int UserId { set; get; }
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
        public DateTime Date
        {
            get => Model.TimeOfDeparture;
            set
            {
                if (Date != value)
                {
                    Model.TimeOfDeparture = value;
                    OnPropertyChanged(() => Date);
                }
            }
        }
        public ICommand ReserveLandingCommand { get; set; }
        public ICommand ReserveHangarCommand { get; set; }
        public List<GenericComboBoxVM<Destination>> Destinations { get; set; }
        public PrivDepartureAddViewModel()
        {
            WeakReferenceMessenger.Default.Register<LoggerMessenger<CompanyWireframeViewModel>>(this, (recipient, message) => SetId(message));
            WeakReferenceMessenger.Default.Register<ReservationMessenger<LandingPadsReservation>>(this, (recipient, message) => SetPadReservation(message));
            WeakReferenceMessenger.Default.Register<ReservationMessenger<HangarReservation>>(this, (recipient, message) => SetHangarReservation(message));
            Date = DateTime.Now;
            Destinations = SetDestinations();
            ReserveLandingCommand = new BaseCommand(() => ReserveLadning());
            ReserveHangarCommand = new BaseCommand(() => ReserveHangar());
        }
        protected override void AddRadio()
        {
            Model.CompanyId = UserId;
        }
        public void SetId(LoggerMessenger<CompanyWireframeViewModel> message)
        {
            UserId = message.Id;
        }
        public void SetPadReservation(ReservationMessenger<LandingPadsReservation> message)
        {
            if (message.Id == 3)
            {
                message.Reservation.CompanyId = UserId;
                Model.LandingPadReservation = message.Reservation;
            }
        }
        public void SetHangarReservation(ReservationMessenger<HangarReservation> message)
        {
            if (message.Id == 4)
            {
                message.Reservation.CompanyId = UserId;
                Model.HangarReservation = message.Reservation;
            }
        }
        protected override void Clear()
        {
            Model.Id = 0;
            Model.LandingPadReservation = null;
            Model.HangarReservation = null;
        }
        public void ReserveLadning()
        {
            SingleReservationView pop = new(3);
            pop.Show();
        }
        public void ReserveHangar()
        {
            SingleReservationView pop = new(4);
            pop.Show();
        }
        public List<GenericComboBoxVM<Destination>> SetDestinations()
        {
            return Database.Destinations.Where(item => item.IsActive == true).Select(item => new GenericComboBoxVM<Destination>()
            {
                Id = item.Id,
                Item = item
            }).ToList();
        }
        protected override DbSet<PrivateDeparture> GetDBTable()
        {
            return Database.PrivateDepartures;
        }

        protected override PrivateDeparture InitializeModel()
        {
            return new PrivateDeparture()
            {
                CreatedAt = DateTime.Now,
                LastEditedAt = DateTime.Now,
                IsActive = true,
                HangarReservationId = 1,
                LandingPadReservationId = 1,
                
            };
        }
        public void ShowLadning()
        {
            SingleReservationView pop = new();
            pop.Show();
        }

    }
}
