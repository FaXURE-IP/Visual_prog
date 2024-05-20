using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Users
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Tree.IsVisible = false;
            TableButton.Checked += (sender, args) => Switch(true);
            TreeButton.Checked += (sender, args) => Switch(false);
        }

        public void Switch(bool isDataGrid)
        {
            if (isDataGrid)
            {
                Tree.IsVisible = false;
                Table.IsVisible = true;
                AddButton.IsVisible = true;
            }
            else
            {
                Tree.IsVisible = true;
                Table.IsVisible = false;
                AddButton.IsVisible = false;
            }
        }

        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                ((UsersTable)DataContext).RemoveRow((User)Table.SelectedItem);
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            ((UsersTable)DataContext).AddRow();
        }
    }
}