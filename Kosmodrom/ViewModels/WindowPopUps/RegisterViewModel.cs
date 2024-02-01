using Kosmodrom.Helpers;
using Kosmodrom.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Kosmodrom.ViewModels.WindowPopUps
{
    public abstract class RegisterViewModel<T> : PropertyChangeGiver where T : class 
    {
        private string _Error { set; get; }
        public string Error
        {
            get => _Error;
            set
            {
                if (Error != value)
                {
                    _Error = value;
                    OnPropertyChanged(() => Error);
                }
            }
        }
        public DatabaseContext Database;
        public T Model { get; set; }

        public ICommand OKCommand { get; set; }
        public RegisterViewModel() 
        {
            Database = new DatabaseContext();
            Model = InitializeModel();
            OKCommand = new BaseCommand(() => Close());
        }

        protected abstract T InitializeModel();
        protected abstract DbSet<T> GetDBTable();
        public abstract bool Check();
        protected void Close()
        {
            if(Check())
            {
                GetDBTable().Add(Model);
                Database.SaveChanges();
                foreach (Window item in Application.Current.Windows)
                {
                    if (item.DataContext == this) item.Close();
                }
            }
        }
    }
}
