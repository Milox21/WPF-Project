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
using System.Windows.Data;
using System.Windows.Input;

namespace Kosmodrom.ViewModels.Many.Admin
{
    public class BanningUsersViewModel : BaseManyViewModel<PassengerLogIn>
    {
        public ICommand BanCommand { set; get; }
        public BanningUsersViewModel()
        {
            BanCommand = new BaseCommand(() => BanUser());
        }
        
        public void BanUser()
        {
            Database.BannedPassengers.Add(new BannedPassenger()
            {
                PassengerId = SelectedItem.Id,
                Reason = "za żywota",
                IsActive = true,
                CreatedAt = DateTime.Now,
                LastEditedAt = DateTime.Now,
            });
            Database.SaveChanges();
            Refresh();
        }
        

        public override void DeleteFromDatabase()
        {
            //throw new NotImplementedException();
        }

        public override void Edit()
        {
            //throw new NotImplementedException();
        }

        public override IQueryable<PassengerLogIn> GetModels()
        {
            var dbContext = new DatabaseContext();

            var result = from passengerLogIn in dbContext.PassengerLogIns
                         join bannedPassenger in dbContext.BannedPassengers
                         on passengerLogIn.Id equals bannedPassenger.PassengerId into bannedPassengersGroup
                         from bannedPassenger in bannedPassengersGroup.DefaultIfEmpty()
                         where bannedPassenger == null || !(bannedPassenger.IsActive == true)
                         select passengerLogIn;

            var resultList = result.ToList();
            return result;
        }

        public override void Refresh()
        {
            Models = new ObservableCollection<PassengerLogIn>(GetModels());
        }

        protected override List<ComboBoxVM> GetSearchColumns()
        {
            return new()
            {
                new(1,nameof(PassengerLogIn.Name)),
                new(2,nameof(PassengerLogIn.Surname)),
                new(3,nameof(PassengerLogIn.Login)),
            };
        }

        protected override IQueryable<PassengerLogIn> Search(IQueryable<PassengerLogIn> models)
        {
            switch (SearchColumn)
            {
                case nameof(PassengerLogIn.Name):
                    return models.Where(item => item.Name.Contains(SearchInput));
                case nameof(PassengerLogIn.Surname):
                    return models.Where(item => item.Surname.Contains(SearchInput));
                case nameof(PassengerLogIn.Login):
                    return models.Where(item => item.Login.Contains(SearchInput));
                default:
                    return models;
            }
        }

        protected override IQueryable<PassengerLogIn> Sort(IQueryable<PassengerLogIn> models)
        {
            switch (SortColumn)
            {
                case nameof(PassengerLogIn.Name):
                    return SortDescending ? models.OrderByDescending(item => item.Name) : models.OrderBy(item => item.Name);
                case nameof(PassengerLogIn.Surname):
                    return SortDescending ? models.OrderByDescending(item => item.Surname) : models.OrderBy(item => item.Surname);
                case nameof(PassengerLogIn.Login):
                    return SortDescending ? models.OrderByDescending(item => item.Login) : models.OrderBy(item => item.Login);
                default: return models;
            }
        }
    }
}
