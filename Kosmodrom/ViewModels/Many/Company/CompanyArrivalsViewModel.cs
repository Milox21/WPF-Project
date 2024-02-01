using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Models.Add;
using Kosmodrom.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmodrom.ViewModels.Many.Company
{
    class CompanyArrivalsViewModel : BaseManyViewModel<PrivateArrival>
    {
        public int UserId { get; set; }
        public CompanyArrivalsViewModel()
        {
            WeakReferenceMessenger.Default.Register<RefreshMessage<PrivateArrival>>(this, (recipient, message) => Refresh());
            WeakReferenceMessenger.Default.Register<LoggerMessenger<CompanyWireframeViewModel>>(this, (recipient, message) => SetId(message));
        }
        public void SetId(LoggerMessenger<CompanyWireframeViewModel> message)
        {
            UserId = message.Id;
            Refresh();
        }
        public override void DeleteFromDatabase()
        {
            if(SelectedItem != null) 
            {
                PrivateArrival? model = Database.PrivateArrivals.FirstOrDefault(item => item.IsActive == true && item.Id == SelectedItem.Id);
                if(model != null)
                {
                    model.IsActive = false;
                    Database.SaveChanges();
                }
            }
        }
        public override void Edit()
        {
            WeakReferenceMessenger.Default.Send(new EdditorMessenger<PrivateArrival>(1, SelectedItem));

        }
        public override IQueryable<PrivateArrival> GetModels()
        {
            return new DatabaseContext().PrivateArrivals.Include(item => item.Destination)
                                        .Include(item => item.LandingPadReservation)
                                        .Include(item => item.HangarReservation)
                                        .Where(item => item.IsActive == true && UserId == item.CompanyId);
        }

        public override void Refresh()
        {
            Models = new ObservableCollection<PrivateArrival>(GetModels());
        }

        protected override List<ComboBoxVM> GetSearchColumns()
        {
            return new()
            {
                new(1,nameof(PrivateArrival.TimeOfArrival)),
                new(2,nameof(PrivateArrival.Destination.Name)),
            };
        }

        protected override IQueryable<PrivateArrival> Search(IQueryable<PrivateArrival> models)
        {
            switch (SearchColumn)
            {
                case nameof(PrivateArrival.TimeOfArrival):
                    return models.Where(item => item.TimeOfArrival.ToString() == SearchInput);
                case nameof(PrivateArrival.Destination.Name):
                    return models.Where(item => item.Destination.Name.ToString() == SearchInput);
                default:
                    return models;
            }
        }

        protected override IQueryable<PrivateArrival> Sort(IQueryable<PrivateArrival> models)
        {
            switch (SortColumn)
            {
                case nameof(PrivateArrival.TimeOfArrival):
                    return SortDescending ? models.OrderByDescending(item => item.TimeOfArrival) : models.OrderBy(item => item.TimeOfArrival);
                case nameof(PrivateArrival.Destination.Name):
                    return SortDescending ? models.OrderByDescending(item => item.Destination.Name) : models.OrderBy(item => item.Destination.Name);
                default:
                    return models;
            }
        }
    }
}
