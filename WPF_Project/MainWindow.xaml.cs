using System;
using System.Windows;
using System.Windows.Controls;
using WPF_Project.View;

namespace WPF_Project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            menuBar.ViewButtonClicked += MenuBar_ViewButtonClicked;
        }

        private void MenuBar_ViewButtonClicked(object sender, string v)
        {
            switch (v)
            {
                case "View1":
                    contentControl.Content = new RocketScience();
                    break;
                case "View2":
                    contentControl.Content = new PlanetScience();
                    break;
                case "View3":
                    contentControl.Content = new RocketScience();
                    break;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null && menuItem.Tag is string viewName)
            {
                MenuBar_ViewButtonClicked(this, viewName);
            }
        }
    }
}