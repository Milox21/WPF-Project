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

    class DestinationsViewModel : BaseManyViewModel<Destination>
    {
        public DestinationsViewModel()
        {
            WeakReferenceMessenger.Default.Register<RefreshMessage<Destination>>(this, (recipient, message) => Refresh());
        }

        public override void DeleteFromDatabase()
        {
            if (SelectedItem != null)
            {
                Destination? model = Database.Destinations.FirstOrDefault(item => item.IsActive == true && item.Id == SelectedItem.Id);
                if (model != null)
                {
                    model.IsActive = false;
                    Database.SaveChanges();
                }
            }
        }
        public override void Edit()
        {
            WeakReferenceMessenger.Default.Send(new EdditorMessenger<Destination>(1, SelectedItem));
        }
        public override IQueryable<Destination> GetModels()
        {
            return new DatabaseContext().Destinations.Where(item => item.IsActive == true);
        }

        public override void Refresh()
        {
            Models = new ObservableCollection<Destination>(GetModels());
        }

        protected override List<ComboBoxVM> GetSearchColumns()
        {
            return new()
            {
                new(1,nameof(Destination.Name)),
                new(2,nameof(Destination.Distance)),
                new(3,nameof(Destination.IsAvailable)),
                new(4,nameof(Destination.Delay))
            };
        }

        protected override IQueryable<Destination> Search(IQueryable<Destination> models)
        {
            switch (SearchColumn)
            {
                case nameof(Destination.Name):
                    return models.Where(item => item.Name.Contains(SearchInput));
                case nameof(Destination.Distance):
                    return models.Where(item => item.Distance.ToString() == SearchInput);
                case nameof(Destination.Delay):
                    return models.Where(item => item.Delay.ToString() == SearchInput);
                default:
                    return models;
            }
        }

        protected override IQueryable<Destination> Sort(IQueryable<Destination> models)
        {
            switch (SortColumn)
            {
                case nameof(Destination.Name):
                    return SortDescending ? models.OrderByDescending(item => item.Name) : models.OrderBy(item => item.Name);
                case nameof(Destination.Distance):
                    return SortDescending ? models.OrderByDescending(item => item.Distance) : models.OrderBy(item => item.Distance);
                case nameof(Destination.IsAvailable):
                    return SortDescending ? models.OrderByDescending(item => item.IsAvailable) : models.OrderBy(item => item.IsAvailable);
                default: return models;
            }
        }
    }
}
