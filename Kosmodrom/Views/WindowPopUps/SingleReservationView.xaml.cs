using Kosmodrom.Models;
using Kosmodrom.ViewModels.WindowPopUps.Reservations;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kosmodrom.Views.WindowPopUps
{
    /// <summary>
    /// Interaction logic for SingleReservationView.xaml
    /// </summary>
    public partial class SingleReservationView : Window
    {
        public SingleReservationView()
        {
            InitializeComponent();
        }
        public SingleReservationView(int i)
        {
            if(i%2 == 0) 
            {
                DataContext = new HangarReservationViewModel(i);
            }
            else if(i%2 == 1 || i == 1) 
            {
                DataContext = new LandingPadReservationViewModel(i);
            }
            InitializeComponent();
        }
    }
}
