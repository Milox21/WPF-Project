using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Models.Add;
using Kosmodrom.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmodrom.ViewModels.Many
{
    class VehiclesViewModel : BaseManyViewModel<Vehicle>
    {
        public VehiclesViewModel()
        {
            WeakReferenceMessenger.Default.Register<RefreshMessage<Vehicle>>(this, (recipient, message) => Refresh());
        }

        public override void DeleteFromDatabase()
        {
            if (SelectedItem != null)
            {
                Vehicle? model = Database.Vehicles.FirstOrDefault(item => item.IsActive == true && item.Id == SelectedItem.Id);
                if (model != null)
                {
                    model.IsActive = false;
                    Database.SaveChanges();
                }
            }
        }

        public override IQueryable<Vehicle> GetModels()
        {
            return new DatabaseContext().Vehicles.Where(item => item.IsActive == true);
        }

        public override void Refresh()
        {
            Models = new ObservableCollection<Vehicle>(GetModels());
        }
        public override void Edit()
        {
            WeakReferenceMessenger.Default.Send(new EdditorMessenger<Vehicle>(1, SelectedItem));
        }
        protected override List<ComboBoxVM> GetSearchColumns()
        {

            return new()
            {
                new(1,nameof(Vehicle.Name)),
                new(2,nameof(Vehicle.Speed)),
                new(3,nameof(Vehicle.Type)),
                new(4,nameof(Vehicle.SeatCount)),
            };
     
        }

        protected override IQueryable<Vehicle> Search(IQueryable<Vehicle> models)
        {
            switch (SearchColumn)
            {
                case nameof(Vehicle.Name):
                    return models.Where(item => item.Name.Contains(SearchInput));
                case nameof(Vehicle.Type):
                    return models.Where(item => item.Type.Contains(SearchInput));
                case nameof(Vehicle.Speed):
                    return models.Where(item => item.Speed.ToString() == SearchInput);
                case nameof(Vehicle.SeatCount):
                    return models.Where(item => item.SeatCount.ToString() == SearchInput);
                default:
                    return models;
            }
        }

        protected override IQueryable<Vehicle> Sort(IQueryable<Vehicle> models)
        {
            switch (SortColumn)
            {
                case nameof(Vehicle.Name):
                    return SortDescending ? models.OrderByDescending(item => item.Name) : models.OrderBy(item => item.Name);
                case nameof(Vehicle.Type):
                    return SortDescending ? models.OrderByDescending(item => item.Type) : models.OrderBy(item => item.Type);
                case nameof(Vehicle.Speed):
                    return SortDescending ? models.OrderByDescending(item => item.Speed) : models.OrderBy(item => item.Speed);
                case nameof(Vehicle.SeatCount):
                    return SortDescending ? models.OrderByDescending(item => item.SeatCount) : models.OrderBy(item => item.SeatCount);
                default: return models;
            }
        }
    }
}
