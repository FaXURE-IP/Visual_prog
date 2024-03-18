using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherViewer
{
    public partial class MainWindow : Window
    {
        private const string OpenWeatherApiKey = "1020ea0fb7e3d1d7433b1abcd7347850";
        private const string OpenWeatherApiUrl = "https://api.openweathermap.org/data/2.5/forecast";
        private const string OpenWeatherImageBaseUrl = "https://openweathermap.org/img/wn/";

        private TextBlock _weatherInfoTextBlock;
        private StackPanel _weatherForecastPanel;
        private TextBox _cityTextBox;

        public MainWindow()
        {
            InitializeComponent();
            _weatherInfoTextBlock = this.FindControl<TextBlock>("WeatherInfoTextBlock");
            _weatherForecastPanel = this.FindControl<StackPanel>("WeatherForecastPanel");
            _cityTextBox = this.FindControl<TextBox>("CityTextBox");
            _cityTextBox.Text = "Novosibirsk"; // Default city
            UpdateWeatherInfo();
        }

        private async void UpdateWeatherInfo()
        {
            try
            {
                // Очищаем старую информацию перед обновлением
                _weatherForecastPanel.Children.Clear();

                var cityCoordinates = await GetCityCoordinatesAsync(_cityTextBox.Text);
                var weatherData = await GetWeatherForecastAsync(cityCoordinates);
                _weatherInfoTextBlock.Text = $"Weather forecast for {_cityTextBox.Text}:\n{weatherData}";
            }
            catch (Exception ex)
            {
                _weatherInfoTextBlock.Text = $"Error: {ex.Message}";
            }
        }

        private async Task<string> GetCityCoordinatesAsync(string cityName)
        {
            using (var httpClient = new HttpClient())
            {
                var apiUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit=5&appid={OpenWeatherApiKey}";
                var response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }

        private async Task<string> GetWeatherForecastAsync(string cityCoordinates)
        {
            var weatherInfo = System.Text.Json.JsonDocument.Parse(cityCoordinates);
            var latitude = weatherInfo.RootElement[0].GetProperty("lat").GetDouble();
            var longitude = weatherInfo.RootElement[0].GetProperty("lon").GetDouble();

            string apiUrl = $"{OpenWeatherApiUrl}?lat={latitude}&lon={longitude}&appid={OpenWeatherApiKey}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var weatherData = System.Text.Json.JsonDocument.Parse(responseBody);
                for (int i = 0; i <= 4; i++)
                {
                    DateTime forecastDate = DateTime.Now.AddDays(i);
                    var weatherDescription = weatherData.RootElement.GetProperty("list")[i * 8].GetProperty("weather")[0].GetProperty("description").GetString();
                    var temperatureKelvin = weatherData.RootElement.GetProperty("list")[i * 8].GetProperty("main").GetProperty("temp").GetDouble();
                    var temperatureCelsius = temperatureKelvin - 273.15;
                    var windSpeed = weatherData.RootElement.GetProperty("list")[i * 8].GetProperty("wind").GetProperty("speed").GetDouble();
                    var weatherIcon = weatherData.RootElement.GetProperty("list")[i * 8].GetProperty("weather")[0].GetProperty("icon").GetString();
                    var imageUrl = $"{OpenWeatherImageBaseUrl}{weatherIcon}@2x.png";
                    var image = await LoadImageAsync(imageUrl);
                    if (image != null)
                    {
                        var forecastPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Avalonia.Thickness(0, 5, 0, 0) };
                        forecastPanel.Children.Add(new TextBlock { Text = $"{forecastDate.ToString("dd MMMM")}:", Width = 100 });
                        forecastPanel.Children.Add(new TextBlock { Text = $"{weatherDescription}\nTemperature: {temperatureCelsius:F1}°C\nWind Speed: {windSpeed} m/s", Width = 200 });
                        forecastPanel.Children.Add(new Image { Source = image, Width = 50, Height = 50, Margin = new Avalonia.Thickness(5) });
                        _weatherForecastPanel.Children.Add(forecastPanel);
                    }
                }

                return "";
            }
        }





        private async Task<Bitmap?> LoadImageAsync(string imageUrl)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(imageUrl);
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    return new Bitmap(stream);
                }
            }
            catch (Exception)
            {
                // Если не удается загрузить изображение, вернуть null
                return null;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void UpdateButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            UpdateWeatherInfo();
        }
    }
}
