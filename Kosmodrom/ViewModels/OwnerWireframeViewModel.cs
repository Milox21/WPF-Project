using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Views.Many;
using Kosmodrom.Views.Single;
using Kosmodrom.Views.Single.Admin;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace Kosmodrom.ViewModels
{
    internal class OwnerWireframeViewModel : PropertyChangeGiver
    {
        private UserControl currentOwnerView;
        public UserControl CurrentOwnerView
        {
            get { return currentOwnerView; }
            set
            {
                if (CurrentOwnerView != value)
                {
                    currentOwnerView = value;
                    OnPropertyChanged(() => CurrentOwnerView);
                }
            }
        }
    
        public ICommand MenuButtonCommand { get; set; }
        public ICommand FirstButtonCommand { get; set; }
        public ICommand SecondButtonCommand { get; set; }
        public ICommand ThirdButtonCommand { get; set; }
        public ICommand FourthButtonCommand { get; set; }
        public ICommand FifthButtonCommand { get; set; }
        public OwnerWireframeViewModel() 
        {
            CurrentOwnerView = new PilotVIew();
            MenuButtonCommand = new BaseCommand(() => SetMenuView());
            FirstButtonCommand = new BaseCommand(() => SetPilotView());
            SecondButtonCommand = new BaseCommand(() => SetDestinationView());
            ThirdButtonCommand = new BaseCommand(() => SetVehicleView());
            FourthButtonCommand = new BaseCommand(() => SetDeparturesView());
            FifthButtonCommand = new BaseCommand(() => SetControlPanelView());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetDeparturesView()
        {
            CurrentOwnerView = new DeparturesView();
        }
        public void SetPilotView()
        {
            CurrentOwnerView = new PilotVIew();
        }
        public void SetControlPanelView()
        {
            CurrentOwnerView = new AdminControlPanelView();
        }

        public void SetDestinationView()
        {
            CurrentOwnerView = new DestinationView();
        }

        public void SetMenuView()
        {
            WeakReferenceMessenger.Default.Send(new LoginMessenger("menu", -1));
        }
        public void SetVehicleView()
        {
            CurrentOwnerView = new VehicleView();
        }
    }
}
