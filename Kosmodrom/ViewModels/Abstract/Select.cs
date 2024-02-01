using Kosmodrom.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kosmodrom.ViewModels.Abstract
{
    public abstract class Select : INotifyPropertyChanged
    {

        public ICommand OptionOneCommand { get; set; }
        public ICommand OptionTwoCommand { get; set; }

        public Select()
        {
            OptionOneCommand = new BaseCommand(() => OptionOne());
            OptionTwoCommand = new BaseCommand(() => OptionTwo());
        }

        abstract public void OptionOne();
        abstract public void OptionTwo();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
