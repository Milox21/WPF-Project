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
    class CompanyDestinationsViewModel : BaseManyViewModel<PrivateDeparture>
    {
        public int UserId { get; set; }
        public CompanyDestinationsViewModel()
        {
            WeakReferenceMessenger.Default.Register<RefreshMessage<PrivateDeparture>>(this, (recipient, message) => Refresh());
            WeakReferenceMessenger.Default.Register<LoggerMessenger<CompanyWireframeViewModel>>(this, (recipient, message) => SetId(message));
        }
        public void SetId(LoggerMessenger<CompanyWireframeViewModel> message)
        {
            UserId = message.Id;
            Refresh();
        }
        public override void DeleteFromDatabase()
        {
            if (SelectedItem != null)
            {
                PrivateDeparture? model = Database.PrivateDepartures.FirstOrDefault(item => item.IsActive == true && item.Id == SelectedItem.Id);
                if (model != null)
                {
                    model.IsActive = false;
                    Database.SaveChanges();
                }
            }
        }

        public override void Edit()
        {
            WeakReferenceMessenger.Default.Send(new EdditorMessenger<PrivateDeparture>(1, SelectedItem));
        }
        public override IQueryable<PrivateDeparture> GetModels()
        {
            return new DatabaseContext().PrivateDepartures.Include(item => item.Destination)
                                        .Include(item => item.LandingPadReservation)
                                        .Include(item => item.HangarReservation)
                                        .Where(item => item.IsActive == true && UserId == item.CompanyId);
        }

        public override void Refresh()
        {
            Models = new ObservableCollection<PrivateDeparture>(GetModels());
        }

        protected override List<ComboBoxVM> GetSearchColumns()
        {
            return new()
            {
                new(1,nameof(PrivateDeparture.TimeOfDeparture)),
                new(2,nameof(PrivateDeparture.Destination.Name)),
            };
        }

        protected override IQueryable<PrivateDeparture> Search(IQueryable<PrivateDeparture> models)
        {
            switch (SearchColumn)
            {
                case nameof(PrivateDeparture.TimeOfDeparture):
                    return models.Where(item => item.TimeOfDeparture.ToString() == SearchInput);
                case nameof(PrivateDeparture.Destination.Name):
                    return models.Where(item => item.Destination.Name.ToString() == SearchInput);
                default:
                    return models;
            }
        }

        protected override IQueryable<PrivateDeparture> Sort(IQueryable<PrivateDeparture> models)
        {
            switch (SortColumn)
            {
                case nameof(PrivateDeparture.TimeOfDeparture):
                    return SortDescending ? models.OrderByDescending(item => item.TimeOfDeparture) : models.OrderBy(item => item.TimeOfDeparture);
                case nameof(PrivateDeparture.Destination.Name):
                    return SortDescending ? models.OrderByDescending(item => item.Destination.Name) : models.OrderBy(item => item.Destination.Name);
                default:
                    return models;
            }
        }
    }
}
