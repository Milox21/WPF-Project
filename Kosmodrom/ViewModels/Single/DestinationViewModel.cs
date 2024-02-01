using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kosmodrom.ViewModels.Single
{
    
    public class DestinationViewModel : BaseSingleViewModel<Destination>
    {
        public ICommand AddDestination { get; set; }
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
        public double Distance
        {
            get => Model.Distance;
            set
            {
                if (Distance != value)
                {
                    Model.Distance = value;
                    OnPropertyChanged(() => Distance);
                }
            }
        }
        public bool IsAvailable
        {
            get => Model.IsAvailable;
            set
            {
                if (IsAvailable != value)
                {
                    Model.IsAvailable = value;
                    OnPropertyChanged(() => IsAvailable);
                }
            }
        }
        public DestinationViewModel()
        {
            AddDestination = new BaseCommand(() => SaveAndRefresh());
        }
        protected override DbSet<Destination> GetDBTable()
        {
            return Database.Destinations;
        }

        protected override Destination InitializeModel()
        {

            return new Destination
            {
                IsAvailable = false,
                IsActive = true,
                Delay =0,
                CreatedAt = DateTime.Now,
            };
        }

        protected override void Clear()
        {
            Model.Id = 0;
            Name = string.Empty;
            Distance = 0;
            IsAvailable = false;
        }

        protected override void AddRadio()
        {
            //if there are no radio buttons, this stays empty
        }
        protected override void Edit(EdditorMessenger<Destination> message)
        {
            Model = Database.Destinations.FirstOrDefault(item => item.Id == message.Item.Id && item.IsActive == true);
            Refresh();
            //Name = message.Item.Name;
            //Distance = message.Item.Distance;
            //IsAvailable = message.Item.IsAvailable;
            //Model.CreatedAt = message.Item.CreatedAt;
            //Model.LastEditedAt = message.Item.LastEditedAt;
        }
    }
    
}
