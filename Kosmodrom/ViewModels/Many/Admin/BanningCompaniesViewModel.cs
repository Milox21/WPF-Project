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
using System.Windows.Input;

namespace Kosmodrom.ViewModels.Many.Admin
{
    public class BanningCompaniesViewModel : BaseManyViewModel<CompanyLogIn>
    {
        public ICommand BanCommand { set; get; }
        public BanningCompaniesViewModel()
        {
            BanCommand = new BaseCommand(() => BanCompany());
        }
        public void BanCompany()
        {
            Database.BannedCompanies.Add(new BannedCompany()
            {
                CompanyId = SelectedItem.Id,
                Reason = "za wyzyskiwanie proletariatu",
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

        public override IQueryable<CompanyLogIn> GetModels()
        {

            var dbContext = new DatabaseContext();

            var result = from companyLogIn in dbContext.CompanyLogIns
                         join bannedCompany in dbContext.BannedCompanies
                         on companyLogIn.Id equals bannedCompany.CompanyId into bannedCompaniesGroup
                         from bannedCompany in bannedCompaniesGroup.DefaultIfEmpty()
                         where bannedCompany == null || !(bannedCompany.IsActive == true)
                         select companyLogIn;

            var resultList = result.ToList();
            return result;
        }

        public override void Refresh()
        {
            Models = new ObservableCollection<CompanyLogIn>(GetModels());
        }

        protected override List<ComboBoxVM> GetSearchColumns()
        {
            return new()
            {
                new(1,nameof(CompanyLogIn.CompanyName)),
                new(2,nameof(CompanyLogIn.Login)),
            };
        }

        protected override IQueryable<CompanyLogIn> Search(IQueryable<CompanyLogIn> models)
        {
            switch (SearchColumn)
            {
                case nameof(CompanyLogIn.CompanyName):
                    return models.Where(item => item.CompanyName.Contains(SearchInput));
                case nameof(CompanyLogIn.Login):
                    return models.Where(item => item.Login.Contains(SearchInput));
                default:
                    return models;
            }
        }

        protected override IQueryable<CompanyLogIn> Sort(IQueryable<CompanyLogIn> models)
        {
            switch (SortColumn)
            {
                case nameof(CompanyLogIn.CompanyName):
                    return SortDescending ? models.OrderByDescending(item => item.CompanyName) : models.OrderBy(item => item.CompanyName);
                case nameof(CompanyLogIn.Login):
                    return SortDescending ? models.OrderByDescending(item => item.Login) : models.OrderBy(item => item.Login);
                default: return models;
            }
        }

        }
}
