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

namespace Kosmodrom.ViewModels.Many.Admin
{
    internal class AcceptingMalfuncionsViewModel : BaseManyViewModel<Malfunction>
    {
        public override void DeleteFromDatabase()
        {
            //throw new NotImplementedException();
        }

        public override void Edit()
        {
            //throw new NotImplementedException();
        }

        public override IQueryable<Malfunction> GetModels()
        {
            return new DatabaseContext().Malfunctions.Where(item => item.IsActive == true).Include(item => item.Destination);
        }

        public override void Refresh()
        {
            Models = new ObservableCollection<Malfunction>(GetModels());
        }

        protected override List<ComboBoxVM> GetSearchColumns()
        {
            return new()
            {
                new(1,nameof(Malfunction.Destination.Name)),
                new(2,nameof(Malfunction.Reason)),
                new(3,nameof(Malfunction.DelayTime)),
            };
        }

        protected override IQueryable<Malfunction> Search(IQueryable<Malfunction> models)
        {
            switch (SearchColumn)
            {
                case nameof(Malfunction.Destination.Name):
                    return models.Where(item => item.Destination.Name.Contains(SearchInput));
                case nameof(Malfunction.Reason):
                    return models.Where(item => item.Reason.Contains(SearchInput));
                case nameof(Malfunction.DelayTime):
                    return models.Where(item => item.DelayTime.ToString() == SearchInput);
                default:
                    return models;
            }
        }

        protected override IQueryable<Malfunction> Sort(IQueryable<Malfunction> models)
        {
            switch (SortColumn)
            {
                case nameof(Malfunction.Destination.Name):
                    return SortDescending ? models.OrderByDescending(item => item.Destination.Name) : models.OrderBy(item => item.Destination.Name);
                case nameof(Malfunction.Reason):
                    return SortDescending ? models.OrderByDescending(item => item.Reason) : models.OrderBy(item => item.Reason);
                case nameof(Malfunction.DelayTime):
                    return SortDescending ? models.OrderByDescending(item => item.DelayTime) : models.OrderBy(item => item.DelayTime);
                default: return models;
            }
        }
    }
}
