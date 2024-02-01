using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kosmodrom.ViewModels.Abstract
{
    public abstract class WireframeViewModel : PropertyChangeGiver
    {
        public DatabaseContext Database { get; set; }
        public ICommand MenuButtonCommand { get; set; }
        public WireframeViewModel() 
        {
            Database = new DatabaseContext();
            MenuButtonCommand = new BaseCommand(() => SetMenuView());
        }
        public void SetMenuView()
        {
            WeakReferenceMessenger.Default.Send(new LoginMessenger("menu", -1));
        }
        #region PropertyChange
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
