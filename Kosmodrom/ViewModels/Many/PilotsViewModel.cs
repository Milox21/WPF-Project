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
    class PilotsViewModel : BaseManyViewModel<Pilot>
    {

        public PilotsViewModel() 
        {
            WeakReferenceMessenger.Default.Register<RefreshMessage<Pilot>>(this, (recipient, message) => Refresh());
        }
        public override IQueryable<Pilot> GetModels()
        {
            return new DatabaseContext().Pilots.Where(item => item.IsActive == true);
        }

        protected override List<ComboBoxVM> GetSearchColumns()
        {
            return new()
            {
                new(1,nameof(Pilot.FirstName)),
                new(2,nameof(Pilot.LastName)),
                new(3,nameof(Pilot.Nationality)),
                new(4,nameof(Pilot.Age)),
                new(5,nameof(Pilot.Experience))
            };
        }
        public override void Edit()
        {
            WeakReferenceMessenger.Default.Send(new EdditorMessenger<Pilot>(1, SelectedItem));
        }
        protected override IQueryable<Pilot> Search(IQueryable<Pilot> models)
        {
            switch (SearchColumn)
            {
                case nameof(Pilot.FirstName):
                    return models.Where(item => item.FirstName.Contains(SearchInput));
                case nameof(Pilot.LastName):
                    return models.Where(item => item.LastName.Contains(SearchInput));
                case nameof(Pilot.Nationality):
                    return models.Where(item => item.Nationality.Contains(SearchInput));
                case nameof(Pilot.Age):
                    return models.Where(item => item.Experience.ToString() == SearchInput);
                case nameof(Pilot.Experience):
                    return models.Where(item => item.Experience.ToString() == SearchInput);
                default:
                    return models;
            }
        }

        protected override IQueryable<Pilot> Sort(IQueryable<Pilot> models)
        {
            switch(SortColumn)
            {
                case nameof(Pilot.FirstName):
                    return SortDescending ? models.OrderByDescending(item => item.FirstName) : models.OrderBy(item => item.FirstName);
                case nameof(Pilot.LastName):
                    return SortDescending ? models.OrderByDescending(item => item.LastName) : models.OrderBy(item => item.LastName);
                case nameof(Pilot.Nationality):
                    return SortDescending ? models.OrderByDescending(item => item.Nationality) : models.OrderBy(item => item.LastName);
                case nameof(Pilot.Age):
                    return SortDescending ? models.OrderByDescending(item => item.Age) : models.OrderBy(item => item.LastName);
                case nameof(Pilot.Experience):
                    return SortDescending ? models.OrderByDescending(item => item.Experience) : models.OrderBy(item => item.LastName);
                default: return models;
            }
        }

        public override void DeleteFromDatabase()
        {
            if (SelectedItem != null)
            {
                Pilot? model = Database.Pilots.FirstOrDefault(item => item.IsActive == true && item.Id == SelectedItem.Id);
                if (model != null)
                {
                    model.IsActive = false;
                    Database.SaveChanges();
                }
            }
        }

        public override void Refresh()
        {
            Models = new ObservableCollection<Pilot>(GetModels());
        }
    }
}
