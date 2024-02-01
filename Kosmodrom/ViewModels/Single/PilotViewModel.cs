using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace Kosmodrom.ViewModels.Single
{
    public  class PilotViewModel : BaseSingleViewModel<Pilot>
    {
        public ICommand AddPilotCommand { get; set; }
        public string FirstName
        {
            get => Model.FirstName;
            set
            {
                if (FirstName != value)
                {
                    Model.FirstName = value;
                    OnPropertyChanged(() => FirstName);
                }
            }
        }
        public string LastName
        {
            get => Model.LastName;
            set
            {
                if (LastName != value)
                {
                    Model.LastName = value;
                    OnPropertyChanged(() => LastName);
                }
            }
        }
        public string Nationality
        {
            get => Model.Nationality;
            set
            {
                if (Nationality != value)
                {
                    Model.Nationality = value;
                    OnPropertyChanged(() => Nationality);
                }
            }
        }
        public string Age
        {
            get => Model.Age.ToString();
            set
            {
                if (Age != value)
                {
                    Model.Age = int.Parse(value);
                    OnPropertyChanged(() => Age);
                }
            }
        }
        public string Experience 
        {
            get => Model.Experience.ToString();
            set
            {
                if (Experience != value)
                {
                    Model.Experience = int.Parse(value);
                    OnPropertyChanged(() => Experience);
                }
            }
        }
        public bool LeadPilot
        {
            get => Model.LeadPilot;
            set
            {
                if (LeadPilot != value)
                {
                    Model.LeadPilot = value;
                    OnPropertyChanged(() => LeadPilot);
                }
            }
        }
        public PilotViewModel()
        {
           
        }
        protected override DbSet<Pilot> GetDBTable()
        {
            return Database.Pilots;
        }

        private bool[] _sexArray = new bool[] {false, false};
        public bool[] SexArray
        {
            get {
                return _sexArray; 
            }
        }
        protected override void Edit(EdditorMessenger<Pilot> message)
        {
            Model = Database.Pilots.FirstOrDefault(item => item.Id == message.Item.Id && item.IsActive == true);
            Refresh();
            //    Model.Id = message.Id;
            //    FirstName = message.Item.FirstName; 
            //    LastName = message.Item.LastName; 
            //    Nationality = message.Item.Nationality;
            //    Age = message.Item.Age.ToString();
            //    Experience = message.Item.Experience.ToString();
            //    LeadPilot = message.Item.LeadPilot;
            //    Model.CreatedAt = message.Item.CreatedAt;
            //    Model.LastEditedAt = message.Item.LastEditedAt;
            //    if (message.Item.Sex == "male")
            //    {
            //        _sexArray[0] = true;
            //    }
            //    else
            //    {
            //        _sexArray[1] = true;
            //    }

        }
        protected override Pilot InitializeModel()
        {
            return new Pilot()
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                LeadPilot = false,
                Nationality = string.Empty,
                Age = 0,
                Experience = 0,
                IsActive = true,
                CreatedAt = DateTime.Now,
                LastEditedAt = DateTime.Now,
            };
        }

        protected override void Clear()
        {
            Model.Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            LeadPilot = false;
            Experience = string.Empty;
            Age = string.Empty;
            Nationality= string.Empty;
            SexArray[0] = false;
            SexArray[1]  = false;

        }

        protected override void AddRadio() 
        {
            Model.Sex = "Female";
            if (Array.IndexOf(_sexArray, true) == 0)
            {
                Model.Sex = "Male";
            }
        }

    }
}
