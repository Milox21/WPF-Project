using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kosmodrom.ViewModels.Single
{
    public abstract class BaseSingleViewModel<T> : INotifyPropertyChanged where T : class, new()
    {
        public DatabaseContext Database { get; set; }
        public ICommand SaveAndRefreshCommand { get; set; }
        private T _Model { set; get; }
        public T Model
        {
            get => _Model;
            set
            {
                if (Model != value)
                {
                    _Model = value;
                    OnPropertyChanged(() => Model);
                }
            }
        }
        public BaseSingleViewModel() 
        {
            Database = new();
            SaveAndRefreshCommand = new BaseCommand(() => SaveAndRefresh());
            WeakReferenceMessenger.Default.Register<EdditorMessenger<T>>(this, (recipient, message) => Edit(message));
            Model = InitializeModel();
        }
        protected void SaveAndRefresh()
        {
            AddRadio();
            if(!(GetModelId()>0))
            {
                GetDBTable().Add(Model);
            }
            Database.SaveChanges();
            WeakReferenceMessenger.Default.Send<RefreshMessage<T>>();
            
            Clear();
        }
        protected virtual int GetModelId()
        {
            return (int)(Model.GetType().GetProperty("Id")?.GetValue(Model) ?? 0);
        }
        protected virtual void Edit(EdditorMessenger<T> message)
        {
        }
        public void Refresh()
        {
            foreach(var item in this.GetType().GetProperties())
            {
                OnPropertyChanged(item.Name);
            }
        }
        protected abstract DbSet<T> GetDBTable();
        protected abstract void Clear();
        protected abstract void AddRadio();
        protected abstract T InitializeModel();
        #region Propertychanged
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
    }
        #endregion
}

