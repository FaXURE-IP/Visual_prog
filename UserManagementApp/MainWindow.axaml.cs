using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace UserApp
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class UserFactory
    {
        public static ObservableCollection<User> GetUsers()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync("https://jsonplaceholder.typicode.com/users").Result;
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    users = JsonSerializer.Deserialize<ObservableCollection<User>>(json);
                }
                else
                {
                    Console.WriteLine("Failed to fetch user data: " + response.ReasonPhrase);
                }
            }

            return users;
        }

        public static IObservable<CollectionChangeEventArgs> CreateObservable(ObservableCollection<User> users)
        {
            return Observable.FromEvent<NotifyCollectionChangedEventHandler, CollectionChangeEventArgs>(
                h => (sender, e) => h(e),
                h => users.CollectionChanged += h,
                h => users.CollectionChanged -= h
            );
        }
    }

    public class MainWindow : Window
    {
        private ObservableCollection<User> _users;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            _users = UserFactory.GetUsers();

            var dataGrid = this.FindControl<DataGrid>("UserDataGrid");
            dataGrid.ItemsSource = _users;

            var observable = UserFactory.CreateObservable(_users);
            observable.Subscribe(change =>
            {
                LogChange(change);
            });
        }

        private static void LogChange(CollectionChangeEventArgs change)
        {
            var logMessage = $"{change.Action}: {change.NewItems?[0]}";
            File.AppendAllText("log.txt", logMessage + Environment.NewLine);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
