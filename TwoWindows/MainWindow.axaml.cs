using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Reactive.Linq;

namespace TwoWindows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenSecondaryWindow_Click(object sender, RoutedEventArgs e)
        {
            SecondaryWindow secondaryWindow = new();
            WindowFactory.CreateObservable(secondaryWindow).Subscribe(args =>
            {
                textBlock.Text += args.RoutedEvent.Name + '\n';
            });
            secondaryWindow.Show();
        }
    }
}