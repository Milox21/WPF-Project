using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Models.Contexts;
using Kosmodrom.ViewModels.Abstract;
using Kosmodrom.ViewModels.Single;
using Kosmodrom.Views.WindowPopUps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace Kosmodrom.ViewModels
{
    public class LogInViewModel : PropertyChangeGiver
    {
        public DatabaseContext Database { get; set; }

        private PassengerLogIn? PassengerLogin;
        private AdminLogIn? AdminLogin;
        private CompanyLogIn? CompanyLogin;


        public ICommand LogInCommand { get; set; }
        public ICommand RegisterPassengerCommand { get; set; }
        public ICommand RegisterCompanyCommand { get; set; }

        public string _Login { get; set; }
        public string Login 
        {
            get { return _Login; }
            set
            {
                if (Login != value)
                {
                    _Login = value;
                    OnPropertyChanged(() => Login);
                }
            }
        }
        private string _Password { get; set; }
        public string Password 
        {
            get { return _Password; }
            set
            {
                if (Password != value)
                {
                    _Password = value;
                    OnPropertyChanged(() => Password);
                }
            }
        }

        public LogInViewModel()
        {
            Database = new DatabaseContext();
            LogInCommand = new BaseCommand(() => LogIn());
            RegisterCompanyCommand = new BaseCommand(() => RegisterCompany());
            RegisterPassengerCommand = new BaseCommand(() => RegisterPassenger());
        }
        public void TakeLogIns()
        {

            PassengerLogin = Database.PassengerLogIns.FirstOrDefault(item => item.IsActive == true && item.Login == Login && item.Password == Password);
            AdminLogin = Database.AdminLogIns.FirstOrDefault(item => item.IsActive == true && item.Login == Login && item.Password == Password);
            CompanyLogin = Database.CompanyLogIns.FirstOrDefault(item => item.IsActive == true && item.Login == Login && item.Password == Password);
            if(Database.SpaceportSupports.FirstOrDefault(item => item.IsActive == true && item.Thing == "Hangars") == null)
            {
                Database.SpaceportSupports.Add(new SpaceportSupport()
                {
                    Thing = "Hangars",
                    Amount = 4,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    LastEditedAt = DateTime.Now,
                });
            }
            if (Database.SpaceportSupports.FirstOrDefault(item => item.IsActive == true && item.Thing == "LandingPads") == null)
            {
                Database.SpaceportSupports.Add(new SpaceportSupport()
                {
                    Thing = "LandingPads",
                    Amount = 4,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    LastEditedAt = DateTime.Now,
                });
            }
            //Properties.Settings.Default.UserId = PassengerLogin.Id;
        }
        public void LogIn()
        {
            TakeLogIns();

            //WeakReferenceMessenger.Default.Send<string>("admin");
            if (PassengerLogin != null)
            {
                BannedPassenger? check = Database.BannedPassengers.FirstOrDefault(item => item.PassengerId == PassengerLogin.Id && item.IsActive == true);
                if (check == null)
                {
                    Database.PassengerLogIns.FirstOrDefault(item => item.Id == PassengerLogin.Id && item.IsActive == true).LastLogin = DateTime.Now;
                    Database.SaveChanges();
                    WeakReferenceMessenger.Default.Send(new LoginMessenger("passenger",PassengerLogin.Id));
                }
                else
                {
                    BannedPopUpView popup = new();
                    popup.Show();
                    WeakReferenceMessenger.Default.Send<BannedPassenger>(check);
                    
                    Login = "";
                    Password = "";
                }
                return; 
            }

            if (CompanyLogin != null)
            {
                BannedCompany? check = Database.BannedCompanies.FirstOrDefault(item => item.CompanyId == CompanyLogin.Id && item.IsActive == true);
                if (check == null)
                {
                    Database.CompanyLogIns.FirstOrDefault(item => item.Id == CompanyLogin.Id && item.IsActive == true).LastLogin = DateTime.Now;
                    Database.SaveChanges();
                    WeakReferenceMessenger.Default.Send(new LoginMessenger("company", CompanyLogin.Id));
                }
                else
                {
                    BannedPopUpView popup = new();
                    popup.Show();
                    WeakReferenceMessenger.Default.Send<BannedCompany>(check);
                    
                    Login = "";
                    Password = "";
                }
                return;
            }

            if (AdminLogin != null)
            {
                Database.AdminLogIns.FirstOrDefault(item => item.Id == AdminLogin.Id && item.IsActive == true).LastLogin = DateTime.Now;
                Database.SaveChanges();
                WeakReferenceMessenger.Default.Send(new LoginMessenger("admin", 0));
                return;
            }

            if(Database.AdminLogIns.FirstOrDefault(item => item.IsActive == true) == null )
            {
                AdminLogIn FirstAdmin = new()
                {
                    IsActive = true,
                    Login = Login,
                    Password = Password,
                    CreatedAt = DateTime.Now,
                    LastEditedAt = DateTime.Now
                };
                Login = "";
                Password = "";
                Database.Add(FirstAdmin);
                Database.SaveChanges();
            } 
        }

        public void RegisterCompany()
        {
            RegisterCompanyView popup = new();
            popup.Show();
        }

        public void RegisterPassenger()
        {
            RegisterPassengerView popup = new();
            popup.Show();
        }
    }
}
