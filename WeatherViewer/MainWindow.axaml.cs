using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherViewer
{
    public partial class MainWindow : Window
    {
        private const string OpenWeatherApiKey = "1020ea0fb7e3d1d7433b1abcd7347850";
        private const string OpenWeatherGeoApiUrl = "http://api.openweathermap.org/geo/1.0/direct";
        private const string OpenWeatherApiUrl = "https://api.openweathermap.org/data/2.5/forecast";

        private TextBlock _weatherInfoTextBlock;
        private TextBox _cityTextBox;

        public MainWindow()
        {
            InitializeComponent();
            _weatherInfoTextBlock = this.FindControl<TextBlock>("WeatherInfoTextBlock");
            _cityTextBox = this.FindControl<TextBox>("CityTextBox");
            _cityTextBox.Text = "Novosibirsk"; // Default city
            UpdateWeatherInfo();
        }

        private async void UpdateWeatherInfo()
        {
            try
            {
                var cityCoordinates = await GetCityCoordinatesAsync(_cityTextBox.Text);
                var weatherData = await GetWeatherDataAsync(cityCoordinates);
                // Update UI with weather information
                _weatherInfoTextBlock.Text = $"Weather in {_cityTextBox.Text}: {weatherData}";
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., network issues, API errors)
                _weatherInfoTextBlock.Text = $"Error: {ex.Message}";
            }
        }

        private async Task<string> GetCityCoordinatesAsync(string cityName)
        {
            using (var httpClient = new HttpClient())
            {
                var apiUrl = $"{OpenWeatherGeoApiUrl}?q={cityName}&limit=5&appid={OpenWeatherApiKey}";
                var response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                // Process responseBody to extract city coordinates
                // For simplicity, just returning the response for now
                return responseBody;
            }
        }

        private async Task<string> GetWeatherDataAsync(string cityCoordinates)
        {
            // Распарсим JSON-ответ от API OpenWeather и извлечем нужные данные о погоде
            var weatherInfo = System.Text.Json.JsonDocument.Parse(cityCoordinates);

            // Извлекаем координаты города
            var latitude = weatherInfo.RootElement[0].GetProperty("lat").GetDouble();
            var longitude = weatherInfo.RootElement[0].GetProperty("lon").GetDouble();

            // Формируем URL для запроса погодных данных
            string apiUrl = $"{OpenWeatherApiUrl}?lat={latitude}&lon={longitude}&appid={OpenWeatherApiKey}";

            using (var httpClient = new HttpClient())
            {
                // Отправляем GET-запрос к API OpenWeather
                var response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                // Читаем ответ в виде строки
                string responseBody = await response.Content.ReadAsStringAsync();

                // Распарсим JSON-ответ от API OpenWeather и извлечем нужные данные о погоде
                var weatherData = System.Text.Json.JsonDocument.Parse(responseBody);

                // Извлекаем описание погоды, температуру и скорость ветра
                var weatherDescription = weatherData.RootElement.GetProperty("list")[0].GetProperty("weather")[0].GetProperty("description").GetString();

                // Температура в Кельвинах
                var temperatureKelvin = weatherData.RootElement.GetProperty("list")[0].GetProperty("main").GetProperty("temp").GetDouble();

                // Преобразуем температуру в градусы Цельсия
                var temperatureCelsius = temperatureKelvin - 273.15;

                // Скорость ветра
                var windSpeed = weatherData.RootElement.GetProperty("list")[0].GetProperty("wind").GetProperty("speed").GetDouble();

                // Формируем строку с информацией о погоде
                string weatherDataString = $"Weather: {weatherDescription}, Temperature: {temperatureCelsius:F1}°C, Wind Speed: {windSpeed} m/s";

                return weatherDataString;
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
