using Newtonsoft.Json;
using ReactiveUI;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reactive.Linq;

namespace Users
{
    public class User : ReactiveObject
    {
        private string _id;
        private string _name;
        private string _username;
        private string _email;
        private Address _address;
        private string _phone;
        private string _website;
        private Company _company;
        [JsonProperty("id")]
        public string Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }
        [JsonProperty("name")]
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        [JsonProperty("username")]
        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }
        [JsonProperty("email")]
        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }
        [JsonProperty("address")]
        public Address Address
        {
            get => _address;
            set => this.RaiseAndSetIfChanged(ref _address, value);
        }
        [JsonProperty("phone")]
        public string Phone
        {
            get => _phone;
            set => this.RaiseAndSetIfChanged(ref _phone, value);
        }
        [JsonProperty("website")]
        public string Website
        {
            get => _website;
            set => this.RaiseAndSetIfChanged(ref _website, value);
        }
        [JsonProperty("company")]
        public Company Company
        {
            get => _company;
            set => this.RaiseAndSetIfChanged(ref _company, value);
        }

        public User(string id)
        {
            Id = id;
            Name = "";
            Username = "";
            Email = "";
            Address = new();
            Phone = "";
            Website = "";
            Company = new();
        }
    }

    public class Address : ReactiveObject
    {
        private string _street;
        private string _suite;
        private string _city;
        private string _zipcode;
        private Geo _geo;
        [JsonProperty("street")]
        public string Street
        {
            get => _street;
            set => this.RaiseAndSetIfChanged(ref _street, value);
        }
        [JsonProperty("suite")]
        public string Suite
        {
            get => _suite;
            set => this.RaiseAndSetIfChanged(ref _suite, value);
        }
        [JsonProperty("city")]
        public string City
        {
            get => _city;
            set => this.RaiseAndSetIfChanged(ref _city, value);
        }
        [JsonProperty("zipcode")]
        public string Zipcode
        {
            get => _zipcode;
            set => this.RaiseAndSetIfChanged(ref _zipcode, value);
        }
        [JsonProperty("geo")]
        public Geo Geo
        {
            get => _geo;
            set => this.RaiseAndSetIfChanged(ref _geo, value);
        }

        public Address()
        {
            Street = "";
            Suite = "";
            City = "";
            Zipcode = "";
            Geo = new();
        }
    }

    public class Geo : ReactiveObject
    {
        private string _lat;
        private string _lng;
        [JsonProperty("lat")]
        public string Lat
        {
            get => _lat;
            set => this.RaiseAndSetIfChanged(ref _lat, value);
        }
        [JsonProperty("lng")]
        public string Lng
        {
            get => _lng;
            set => this.RaiseAndSetIfChanged(ref _lng, value);
        }

        public Geo()
        {
            Lat = "";
            Lng = "";
        }
    }

    public class Company : ReactiveObject
    {
        private string _name;
        private string _catchPhrase;
        private string _bs;
        [JsonProperty("name")]
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        [JsonProperty("catchPhrase")]
        public string CatchPhrase
        {
            get => _catchPhrase;
            set => this.RaiseAndSetIfChanged(ref _catchPhrase, value);
        }
        [JsonProperty("bs")]
        public string Bs
        {
            get => _bs;
            set => this.RaiseAndSetIfChanged(ref _bs, value);
        }

        public Company()
        {
            Name = "";
            CatchPhrase = "";
            Bs = "";
        }
    }

    public static class ObservableCollectionFactory
    {
        public static IObservable<NotifyCollectionChangedEventArgs> CreateObservable<Users>(ObservableCollection<Users> collection)
        {
            return Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                h => collection.CollectionChanged += h,
                h => collection.CollectionChanged -= h)
                .Select(ep => ep.EventArgs);
        }
    }

    internal class UsersTable
    {
        public ObservableCollection<User> Users { get; set; } = [];
        private IObservable<NotifyCollectionChangedEventArgs> observable;
        private StreamWriter writer = new(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\log.txt", true);

        public void AddRow()
        {
            Users.Add(new User((Users.Count() + 1).ToString()));
        }
        
        public void RemoveRow(User user)
        {
            Users.Remove(user);
        }

        public void Subscribe()
        {
            observable = ObservableCollectionFactory.CreateObservable(Users);
            observable.Subscribe(args =>
            {
                if (args.NewItems != null)
                {
                    foreach (User item in args.NewItems)
                    {
                        writer.WriteLine($"Added item {item.Id}");
                        writer.Flush();

                        item.PropertyChanged += (sender, args) =>
                        {
                            writer.WriteLine($"Property \"{args.PropertyName}\" changed in item {item.Id}");
                            writer.Flush();
                        };

                        item.Address.PropertyChanged += (sender, args) =>
                        {
                            writer.WriteLine($"Property \"{args.PropertyName}\" changed in item {item.Id}");
                            writer.Flush();
                        };

                        item.Address.Geo.PropertyChanged += (sender, args) =>
                        {
                            writer.WriteLine($"Property \"{args.PropertyName}\" changed in item {item.Id}");
                            writer.Flush();
                        };

                        item.Company.PropertyChanged += (sender, args) =>
                        {
                            writer.WriteLine($"Property \"{args.PropertyName}\" changed in item {item.Id}");
                            writer.Flush();
                        };
                    }
                }
                if (args.OldItems != null)
                {
                    foreach (User item in args.OldItems)
                    {
                        writer.WriteLine($"Deleted item {item.Id}");
                        writer.Flush();
                    }
                }
            });
        }

        public UsersTable()
        {
            Subscribe();
            RestClient call = new("https://jsonplaceholder.typicode.com/users");
            RestResponse response = call.Execute(new RestRequest());
            if (response.Content == "[]")
                return;
            List<User> list = JsonConvert.DeserializeObject<List<User>>(response.Content);
            foreach (User item in list)
                Users.Add(item);
        }
    }
}