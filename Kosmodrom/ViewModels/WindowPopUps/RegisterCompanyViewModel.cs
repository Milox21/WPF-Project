using Kosmodrom.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmodrom.ViewModels.WindowPopUps
{
    public class RegisterCompanyViewModel : RegisterViewModel<CompanyLogIn>
    {
        public string Login
        {
            get => Model.Login;
            set
            {
                if (Login != value)
                {
                    Model.Login = value;
                    OnPropertyChanged(() => Login);
                }
            }
        }
        public string CompanyName
        {
            get => Model.CompanyName;
            set
            {
                if (CompanyName != value)
                {
                    Model.CompanyName = value;
                    OnPropertyChanged(() => CompanyName);
                }
            }
        }
        public string Password
        {
            get => Model.Password;
            set
            {
                if (Password != value)
                {
                    Model.Password = value;
                    OnPropertyChanged(() => Password);
                }
            }
        }
        private string _PasswordRepeated { get; set; }
        public string PasswordRepeated
        {
            get => _PasswordRepeated;
            set
            {
                if (PasswordRepeated != value)
                {
                    _PasswordRepeated = value;
                    OnPropertyChanged(() => PasswordRepeated);
                }
            }
        }
        public RegisterCompanyViewModel()
        {
        }
        protected override CompanyLogIn InitializeModel()
        {
            return new CompanyLogIn()
            {
                IsActive = true,
                CreatedAt = DateTime.Now,
                LastEditedAt = DateTime.Now,
            };
        }

        protected override DbSet<CompanyLogIn> GetDBTable()
        {
            return Database.CompanyLogIns;
        }

        public override bool Check()
        {
            char[] specialSymbols = { '!', '@', '#', '$', '%', '^', '&', '*' };
            if (Password == PasswordRepeated)
            {
                if(Password.Any(c => specialSymbols.Contains(c)))
                {
                    if(Password.Any(char.IsUpper))
                    {
                        return true;
                    }
                    else
                    {
                        Error = "Passwords must have at leas one uppercase letter";
                        PasswordRepeated = "";
                        Password = "";
                        return false;
                    }
                }
                else
                {
                    Error = "Passwords must contain special symbols";
                    PasswordRepeated = "";
                    Password = "";
                    return false;
                }
                

            }
            else
            {
                Error = "Passwords are not identical";
                PasswordRepeated = "";
                Password = "";
                return false;
            }
        }
    }
}
