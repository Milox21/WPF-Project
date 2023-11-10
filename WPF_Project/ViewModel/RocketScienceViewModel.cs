using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_Project.View;

namespace WPF_Project.ViewModel
{
    public class RocketScienceViewModel : INotifyPropertyChanged
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

        public ICommand ShipHeadCommand { get; set; }
        public ICommand ShipSegmentCommand { get; set; }
        public ICommand EngineCommand { get; set; }

        public RocketScienceViewModel()
        {
            // Inicjalizacja komend
            ShipHeadCommand = new RelayCommand(ShowShipHeadView);
            ShipSegmentCommand = new RelayCommand(ShowShipSegmentView);
            EngineCommand = new RelayCommand(ShowEngineView);


            // Domyślny widok
            CurrentView = new ShipHeadView();
        }

        private void ShowShipHeadView()
        {
            CurrentView = new ShipHeadView();
        }

        private void ShowShipSegmentView()
        {
            CurrentView = new ShipSegmentView();
        }

        private void ShowEngineView()
        {
            CurrentView = new ShipEngineView();
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
