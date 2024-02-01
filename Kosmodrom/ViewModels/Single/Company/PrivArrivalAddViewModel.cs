using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Models.Add;
using Kosmodrom.Views.WindowPopUps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Kosmodrom.ViewModels.Single.Company
{
    
    class PrivArrivalAddViewModel : BaseSingleViewModel<PrivateArrival>
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
            get => Model.TimeOfArrival;
            set
            {
                if (Date != value)
                {
                    Model.TimeOfArrival = value;
                    OnPropertyChanged(() => Date);
                }
            }
        }
        private Color _PadColor;
        public Color PadColor
        {
            get => _PadColor;
            set
            {
                if (PadColor != value)
                {
                    _PadColor = value;
                    OnPropertyChanged(() => PadColor);
                }
            }
        }
        public ICommand ReserveLandingCommand { get; set; }
        public ICommand ReserveHangarCommand { get; set; }
        public List<GenericComboBoxVM<Destination>> Destinations { get; set; }
        public PrivArrivalAddViewModel() 
        {
            PadColor = Colors.Black;
            WeakReferenceMessenger.Default.Register<LoggerMessenger<CompanyWireframeViewModel>>(this, (recipient, message) => SetId(message));
            WeakReferenceMessenger.Default.Register<ReservationMessenger<LandingPadsReservation>>(this, (recipient, message) => SetPadReservation(message));
            WeakReferenceMessenger.Default.Register<ReservationMessenger<HangarReservation>>(this, (recipient, message) => SetHangarReservation(message));
            Date = DateTime.Now;
            ReserveLandingCommand = new BaseCommand(() => ReserveLadning());
            ReserveHangarCommand = new BaseCommand(() => ReserveHangar());
            Destinations = SetDestinations();
        }
        public void SetId(LoggerMessenger<CompanyWireframeViewModel> message)
        {
            UserId = message.Id;
        }
        public void SetPadReservation(ReservationMessenger<LandingPadsReservation> message)
        {
            if(message.Id == 1)
            {
                message.Reservation.CompanyId = UserId;
                Model.LandingPadReservation = message.Reservation;
            }
        }
        public void SetHangarReservation(ReservationMessenger<HangarReservation> message)
        {
            if (message.Id == 2)
            {
                message.Reservation.CompanyId = UserId;
                Model.HangarReservation = message.Reservation;
            }
        }
        public void ReserveLadning()
        {
            SingleReservationView pop = new(1);
            pop.Show();
        }
        public void ReserveHangar()
        {
            SingleReservationView pop = new(2);
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
        protected override void AddRadio()
        {
            Model.CompanyId = UserId;
        }

        protected override void Clear()
        {
            Model.Id = 0;
            Model.LandingPadReservation = null;
            Model.HangarReservation = null;
        }

        protected override DbSet<PrivateArrival> GetDBTable()
        {
            return Database.PrivateArrivals;
        }

        protected override PrivateArrival InitializeModel()
        {
            return new PrivateArrival()
            {
                CreatedAt = DateTime.Now,
                LastEditedAt = DateTime.Now,
                IsActive = true,
            };
        }


    }
}
