using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Kosmodrom.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private UserControl currentView;

        public UserControl CurrentView
        {
            get { return currentView; }
            set
            {
                currentView = value;
                OnPropertyChanged();
            }
        }
        public MainWindowViewModel()
        {
            CurrentView = new LogInView();
            WeakReferenceMessenger.Default.Register<LoginMessenger>(this, (recipient, message) => Changer(message));
        }
        public void Changer(LoginMessenger message)
        {
            if(message.Message.Equals("passenger"))
            {
                CurrentView = new PassengerWireframeView();
                WeakReferenceMessenger.Default.Send(new LoggerMessenger<PassengerLogIn>(message.Id));
            }
            else if(message.Message.Equals("company"))
            {
                CurrentView = new CompanyWireframeView();
                WeakReferenceMessenger.Default.Send(new LoggerMessenger<CompanyLogIn>(message.Id));
            }
            else if(message.Id == 0)
            {
                CurrentView = new OwnerWireframe();
            }
            else if (message.Id == -1)
            {
                CurrentView = new LogInView();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
