using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using Kosmodrom.Models.Contexts;
using Kosmodrom.ViewModels.Abstract;
using Kosmodrom.Views.Single.Company;
using Kosmodrom.Views.WindowPopUps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kosmodrom.ViewModels
{
    public class CompanyWireframeViewModel : WireframeViewModel
    {
        public int UserId { set; get; }
        private UserControl _CurrentCompanyView {set;get;}
        public UserControl CurrentCompanyView
        {
            get { return _CurrentCompanyView; }
            set
            {
                if (CurrentCompanyView != value)
                {
                    _CurrentCompanyView = value;
                    OnPropertyChanged(() => CurrentCompanyView);
                }

            }
        }
        private UserControl _AddingView { get; set; }
        public UserControl AddingView
        {
            get { return _AddingView; }
            set
            {
                if (AddingView != value)
                {
                    _AddingView = value;
                    OnPropertyChanged(() => AddingView);
                }
                
            }
        }
        private string _CompanyName { get; set; }
        public string CompanyName
        {
            get { return _CompanyName; }
            set
            {
                if (CompanyName != value)
                {
                    _CompanyName = value;
                    OnPropertyChanged(() => CompanyName);
                }
            }
        }
        public ICommand FirstButtonCommand { set; get; }
        public ICommand SecondButtonCommand { set; get; }
        public ICommand ThirdButtonCommand { set; get; }
        public CompanyWireframeViewModel() 
        {
            CurrentCompanyView = new PrivArrivalAddView();
            WeakReferenceMessenger.Default.Register<LoggerMessenger<CompanyLogIn>>(this, (recipient, message) => LogMe(message));
            FirstButtonCommand = new BaseCommand(() => FirstButton());
            ThirdButtonCommand = new BaseCommand(() => ThirdButton());
        }

        public void SecondButton()
        {
            CurrentCompanyView = new PrivDepartureAddView();
            WeakReferenceMessenger.Default.Send(new LoggerMessenger<CompanyWireframeViewModel>(UserId));
        }
        public void FirstButton()
        {
            CurrentCompanyView = new PrivArrivalAddView();
            WeakReferenceMessenger.Default.Send(new LoggerMessenger<CompanyWireframeViewModel>(UserId));
        }
        public void ThirdButton()
        {
            MalfunctionsView pop = new();
            pop.Show();
        }
        public void LogMe(LoggerMessenger<CompanyLogIn> message)
        {
            UserId = message.Id;
            WeakReferenceMessenger.Default.Send(new LoggerMessenger<CompanyWireframeViewModel>(message.Id));
            string? name = Database.CompanyLogIns.FirstOrDefault(item => item.Id == message.Id && item.IsActive == true).CompanyName;
            if (name != null) 
            {
                CompanyName = name;
            }
        }
    }
}
