using System;
using System.Windows;
using System.Windows.Controls;

namespace WPF_Project.View
{
    public partial class MenuBar : UserControl
    {
        public event EventHandler<string> ViewButtonClicked;

        public MenuBar()
        {
            InitializeComponent();
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag is string viewName)
            {
                ViewButtonClicked?.Invoke(this, viewName);
            }
        }
    }
}