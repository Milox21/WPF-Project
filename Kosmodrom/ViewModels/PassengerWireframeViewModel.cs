using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.ViewModels.Abstract;
using Kosmodrom.Views.Many.Passenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kosmodrom.ViewModels
{
    public class PassengerWireframeViewModel : WireframeViewModel
    {
        public int userID;
        public ICommand FirstButtonCommand { get; set; }
        public ICommand SecondButtonCommand { get; set; }
        private UserControl _AddingView { get; set; }
        public UserControl AddingView
        {
            get { return _AddingView; }
            set
            {
                _AddingView = value;
                OnPropertyChanged(() => AddingView);
            }
        }
        private string _NameSurname { get; set; }
        public string NameSurname
        {
            get { return _NameSurname; }
            set
            {
                if (NameSurname != value)
                {
                    _NameSurname = value;
                    OnPropertyChanged(() => NameSurname);
                }
            }
        }
        
        public PassengerWireframeViewModel()
        {
            
            AddingView = new BuyTicketView();
            WeakReferenceMessenger.Default.Register<LoggerMessenger<PassengerLogIn>>(this, (recipient, message) => LogMe(message));
            FirstButtonCommand = new BaseCommand(() => SetBuyTicket());
            SecondButtonCommand = new BaseCommand(() => SetShowTicket());
        }
        public void SetBuyTicket()
        {
            
            AddingView = new BuyTicketView();
            WeakReferenceMessenger.Default.Send(new LoggerMessenger<PassengerWireframeViewModel>(userID));

        }
        public void SetShowTicket()
        {
           
            AddingView = new CheckTicketsView();
            WeakReferenceMessenger.Default.Send(new LoggerMessenger<PassengerWireframeViewModel>(userID));
        }
        public void LogMe(LoggerMessenger<PassengerLogIn> message)
        {
            userID = message.Id;
            PassengerLogIn? passenger = Database.PassengerLogIns.FirstOrDefault(item => item.Id == message.Id && item.IsActive == true); 
            if (passenger != null) 
            {
                NameSurname = passenger.Name + " " + passenger.Surname;
            }
        }
    }
}
