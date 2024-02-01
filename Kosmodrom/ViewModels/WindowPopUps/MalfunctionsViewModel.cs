using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Models.Add;
using Kosmodrom.ViewModels.Single;
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
    public class MalfunctionsViewModel: BaseSingleViewModel<Malfunction>
    {
        public int DelayTime
        {
            get => Model.DelayTime;
            set
            {
                if (DelayTime != value)
                {
                    Model.DelayTime = value;
                    OnPropertyChanged(() => DelayTime);
                }
            }
        }

        public string Reason
        {
            get => Model.Reason;
            set
            {
                if (Reason != value)
                {
                    Model.Reason = value;
                    OnPropertyChanged(() => Reason);
                }
            }
        }
        public int? SelectedDestination
        {
            get => Model.DestinationId;
            set
            {
                if (SelectedDestination != value)
                {
                    Model.DestinationId = value;
                    OnPropertyChanged(() => SelectedDestination);
                }
            }
        }
        public List<GenericComboBoxVM<Destination>> Destinations { get; set; }
        public ICommand OKCommand { get; set; }
        public MalfunctionsViewModel() 
        {
            Destinations = SetDestinations();
            OKCommand = new BaseCommand(() => Close());
        }
        public List<GenericComboBoxVM<Destination>> SetDestinations()
        {
            return Database.Destinations.Where(item => item.IsActive == true).Select(item => new GenericComboBoxVM<Destination>()
            {
                Id = item.Id,
                Item = item
            }).ToList();
        }
        public void Close()
        {
            Database.Malfunctions.Add(Model);
            Database.SaveChanges();
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }
        protected override void AddRadio()
        {
            //throw new NotImplementedException();
        }

        protected override void Clear()
        {
            //throw new NotImplementedException();
        }

        protected override DbSet<Malfunction> GetDBTable()
        {
            return Database.Malfunctions;
        }

        protected override Malfunction InitializeModel()
        {
            return new Malfunction()
            {
                IsActive = true,
                CreatedAt = DateTime.Now,
                LastEditedAt = DateTime.Now,
            };
        }
    }
}
