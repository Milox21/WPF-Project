using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Helpers.Kosmodrom.Helpers;
using Kosmodrom.Helpers.Kosmodrom.Helpers.Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Models.Add;
using Kosmodrom.Models.Contexts;
using Kosmodrom.ViewModels.Single;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Kosmodrom.ViewModels.WindowPopUps.Reservations
{
    public abstract class SingleReservationViewModel<T> : PropertyChangeGiver where T : class
    {
        public DatabaseContext Database { get; set; }
        public int ThingsAmount { set; get; }
        public List<int> Buttons { set; get; }
        private DateTime _Date { get; set; }
        public DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                if (value != Date)
                {
                    _Date = value;

                    OnPropertyChanged(() => _Date);
                }
            }
        }
        private int _selectedNumber;
        public int SelectedNumber
        {
            get { return _selectedNumber; }
            set
            {
                if (_selectedNumber != value)
                {
                    _selectedNumber = value;
                    OnPropertyChanged(() => SelectedNumber);
                }
            }
        }
        private int _SelectedThing;
        public int SelectedThing
        {
            get { return _SelectedThing; }
            set
            {
                if (SelectedThing != value)
                {
                    _SelectedThing = value;
                    OnPropertyChanged(() => SelectedThing);
                }
            }
        }
        public List<int> ThingOptions { set; get; }
        public List<List<int>> Reservations { get; set; }
        public List<string> ReservationMesseges { get; set; }

        private List<SolidColorBrush> _buttonColors;
        public List<SolidColorBrush> ButtonColors
        {
            get { return _buttonColors; }
            set
            {
                if (_buttonColors != value)
                {
                    _buttonColors = value;
                    OnPropertyChanged(() => ButtonColors);
                }
            }
        }
        public ICommand DateChangeCommand { get; set; }
        public ICommand ButtonClickCommand { get; set; }
        public ICommand SaveAndSendCommand { get; set; }

        public SingleReservationViewModel()
        {
            Database = new DatabaseContext();
            ButtonColors = new List<SolidColorBrush>();
            for (int i = 0; i < 24; i++)
            {
                ButtonColors.Add(new SolidColorBrush(Colors.Blue));
            }
            DateChangeCommand = new BaseCommand(() => InitReservations());
            ButtonClickCommand = new ParameterBaseCommand(DateHangling);
            SaveAndSendCommand = new BaseCommand(() => SaveAndSend());
            Date = DateTime.Now;
            Buttons = new(){
                -1,
                -1
            };
            Reservations = new();
            ReservationMesseges = new();
            for (int i = 0; i < 24; i++)
            {
                ReservationMesseges.Add(" ");
            }
            ThingsAmount = ThingAmount();
            ThingOptions = Enumerable.Range(1, ThingsAmount).ToList();

        }
        public abstract int ThingAmount();
        public abstract void SaveAndSend();
        public abstract void InitReservations();

        public void DateHangling(int buttonIndex)
        {
            if (Buttons[1] == buttonIndex)
            {
                ButtonColors[buttonIndex].Color = Colors.Blue;
                Buttons[1] = -1;
            }
            else if (Buttons[0] == buttonIndex)
            {
                ButtonColors[buttonIndex].Color = Colors.Blue;
                Buttons[0] = -1;
            }
            else if (Buttons[0] == -1)
            {
                ButtonColors[buttonIndex].Color = Colors.Red;
                Buttons[0] = buttonIndex;
            }
            else if (Buttons[1] == -1)
            {
                ButtonColors[buttonIndex].Color = Colors.Red;
                Buttons[1] = buttonIndex;
            }
            if (Buttons[0] != -1 && Buttons[1] != -1)
            {
                if (Buttons[0] > Buttons[1])
                {
                    for (int i = Buttons[1]; i < Buttons[0]; i++)
                    {
                        ButtonColors[i].Color = Colors.Red;
                    }
                }
                else if (Buttons[0] < Buttons[1])
                {
                    for (int i = Buttons[0]; i < Buttons[1]; i++)
                    {
                        ButtonColors[i].Color = Colors.Red;
                    }
                }
            }
            else if (Buttons[0] != -1)
            {
                foreach (SolidColorBrush item in ButtonColors)
                {
                    item.Color = Colors.Blue;
                }
                ButtonColors[Buttons[0]].Color = Colors.Red;
            }
            else if (Buttons[1] != -1)
            {
                foreach (SolidColorBrush item in ButtonColors)
                {
                    item.Color = Colors.Blue;
                }
                ButtonColors[Buttons[1]].Color = Colors.Red;
            }
        }
        public void ClearButtons()
        {
            for (int i = 0; i < 24; i++)
            {
                ReservationMesseges[i] = " ";
            }
        }
        public void RefreshButtons()
        {
            OnPropertyChanged(() => ReservationMesseges);
        }
        protected void Clear()
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }
    }
}
