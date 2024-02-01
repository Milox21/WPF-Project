using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.ViewModels.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Kosmodrom.ViewModels.WindowPopUps
{
    public class BannedPopUpViewModel : PropertyChangeGiver
    { 
        public string _Reason { set; get; }
        public string Reason
        {
            get => _Reason;
            set
            {
                if (Reason != value)
                {
                    _Reason = value;
                    OnPropertyChanged(() => Reason);
                }
            }
        }
        public ICommand OKCommand { get; set; }
        public BannedPopUpViewModel() 
        {
            WeakReferenceMessenger.Default.Register<BannedPassenger>(this, (recipient, message) =>
            {
                if (message != null) 
                { 
                    Reason = message.Reason; 
                }
            });
            WeakReferenceMessenger.Default.Register<BannedCompany>(this, (recipient, message) =>
            {
                if(message != null) 
                { 
                    Reason = message.Reason; 
                }
                
            });
            OKCommand = new BaseCommand(() => Close());
        }
        public void Close() 
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }

    }
}
