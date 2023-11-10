using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_Project.View;

namespace WPF_Project.ViewModel
{
    class PlanetScienceViewModel : INotifyPropertyChanged
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

        public ICommand PlanetCommand { get; set; }
        public ICommand MoonCommand { get; set; }
        public ICommand SpaceStationCommand { get; set; }
        public ICommand StructureCommand { get; set; }
        public ICommand AnomalyCommand { get; set; }

        public PlanetScienceViewModel()
        {
            // Inicjalizacja komend
            PlanetCommand = new RelayCommand(ShowPlanetCommand);
            MoonCommand = new RelayCommand(ShowMoonCommand);
            SpaceStationCommand = new RelayCommand(ShowSpaceStationCommand);
            StructureCommand = new RelayCommand(ShowStructureCommand);
            AnomalyCommand = new RelayCommand(ShowAnomalyCommandd);


            // Domyślny widok
            CurrentView = new PlanetView();
        }

        private void ShowPlanetCommand()
        {
            CurrentView = new PlanetView();
        }

        private void ShowMoonCommand()
        {
            CurrentView = new MoonView();
        }

        private void ShowSpaceStationCommand()
        {
            CurrentView = new SpaceStationView();
        }
        private void ShowStructureCommand()
        {
            CurrentView = new StructureView();
        }
        private void ShowAnomalyCommandd()
        {
            CurrentView = new AnomalyView();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}


