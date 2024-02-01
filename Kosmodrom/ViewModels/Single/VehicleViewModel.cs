using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Models.Add;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace Kosmodrom.ViewModels.Single
{
    public class VehicleViewModel : BaseSingleViewModel<Vehicle>
    {
        public ICommand AddVehicle { set; get; }
        public string Name
        {
            get => Model.Name;
            set
            {
                if (Name != value)
                {
                    Model.Name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }
        public double Speed
        {
            get => Model.Speed;
            set
            {
                if (Speed != value)
                {
                    Model.Speed = value;
                    OnPropertyChanged(() => Speed);
                }
            }
        }
        public string Type
        {
            get => Model.Type;
            set
            {
                if (Type != value)
                {
                    Model.Type = value;
                    OnPropertyChanged(() => Type);
                }
            }
        }
        public int SeatCount
        {
            get => Model.SeatCount;
            set
            {
                if (SeatCount != value)
                {
                    Model.SeatCount = value;
                    OnPropertyChanged(() => SeatCount);
                }
            }
        }

        protected override DbSet<Vehicle> GetDBTable()
        {
            return Database.Vehicles;
        }
        protected override Vehicle InitializeModel()
        {
            return new Vehicle()
            {
                Name = string.Empty,
                Type = string.Empty,
                Speed = 0,
                SeatCount = 0,
                IsActive = true,
                CreatedAt = DateTime.Now,
            };
        }
        protected override void Edit(EdditorMessenger<Vehicle> message)
        {
            Model = Database.Vehicles.FirstOrDefault(item => item.Id == message.Item.Id && item.IsActive == true);
            Refresh();
            //Name = message.Item.Name;
            //Type =  message.Item.Type;
            //Speed = message.Item.Speed;
            //SeatCount = message.Item.SeatCount;
            //Model.CreatedAt = message.Item.CreatedAt;
            //Model.LastEditedAt = message.Item.LastEditedAt;
        }
        protected override void Clear()
        {
            Model.Id = 0;
            Name = string.Empty;
            Type = string.Empty;
            SeatCount = 0;
            Speed = 0;
        }

        protected override void AddRadio()
        {
            //if there are no radio buttons, this stays empty
        }

        public VehicleViewModel()
        {
            AddVehicle = new BaseCommand(() => SaveAndRefresh());
        }
    }
}
