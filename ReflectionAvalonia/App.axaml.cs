using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ReflectionAvalonia.Models.User;
using ReflectionAvalonia.Models;
using ReflectionAvalonia.Services;
using ReflectionAvalonia.ViewModels;
using ReflectionAvalonia.Views;
using System.Collections.Generic;

namespace ReflectionAvalonia
{
    public partial class App : Application
    {
        private List<AnytypePresentationModel> presentationModels = 
            new List<AnytypePresentationModel>();
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            var jsonAnswer = HttpRequest.Request("https://jsonplaceholder.typicode.com/users");
            JsonDeserializer<List<User>> jsonDeserializer = new JsonDeserializer<List<User>>();
            var users = jsonDeserializer.Deserialize(jsonAnswer);

            foreach (var user in users)
            {
                presentationModels.Add(new AnytypePresentationModel(user));
            }
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(presentationModels),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}