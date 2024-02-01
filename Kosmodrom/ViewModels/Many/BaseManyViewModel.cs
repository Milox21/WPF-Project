using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Models.Add;
using Kosmodrom.Models.Contexts;
using Kosmodrom.Views.Many;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kosmodrom.ViewModels.Many
{
    public abstract class BaseManyViewModel<ModelType> : INotifyPropertyChanged where ModelType : class 
    {
        public DatabaseContext Database { get; set; }
        private ObservableCollection<ModelType> _Models;
        public ObservableCollection<ModelType> Models
        {
            get => _Models;
            set
            {
                if (_Models != value)
                {
                    _Models = value;
                    OnPropertyChanged(() => Models);
                }
            }
        }

        private ModelType? _SelectedItem;
        public ModelType? SelectedItem
        {
            get => _SelectedItem;
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    OnPropertyChanged(() => SelectedItem);
                }
            }
        }

        private string? _SearchInput;
        public string? SearchInput
        {
            get => _SearchInput;
            set
            {
                if (_SearchInput != value)
                {
                    _SearchInput = value;
                    OnPropertyChanged(() => SearchInput);
                }
            }
        }
        public List<ComboBoxVM> SearchandOrderColumns { get; set; }
        public string SearchColumn { get; set; }
        public string SortColumn { get; set; }
        public bool SortDescending { get; set; }
        public ICommand GetModelsCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public BaseManyViewModel()
        {
            Database = new();
            GetSearchModels();
            GetModelsCommand = new BaseCommand(() => GetSearchModels());
            DeleteCommand = new BaseCommand(() => Delete());
            EditCommand = new BaseCommand(() => Edit());
            SearchandOrderColumns = GetSearchColumns();
        }
        public abstract IQueryable<ModelType> GetModels();
        protected abstract IQueryable<ModelType> Search(IQueryable<ModelType> models);
        protected abstract List<ComboBoxVM> GetSearchColumns();
        protected abstract IQueryable<ModelType> Sort(IQueryable<ModelType> models);
        public abstract void Refresh();
        public abstract void DeleteFromDatabase();
        public abstract void Edit();
        public void Delete()
        {
            if (SelectedItem != null)
            {
                DeleteFromDatabase();
                Models.Remove(SelectedItem);
            }
            
        }
        private void GetSearchModels()
        {
            IQueryable<ModelType> modelTypes = GetModels();
            if (!(string.IsNullOrEmpty(SearchInput)))
            {
                modelTypes = Search(modelTypes);
            }
            modelTypes = Sort(modelTypes);
            Models = new ObservableCollection<ModelType>(modelTypes);
        }
        #region PropertyChanged
        protected void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            OnPropertyChanged(propertyName);
        }

        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)

            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
