using Newtonsoft.Json;
using System.IO;

namespace LogicGatesAvalonia.Models.DataBaseModel
{
    // Класс DataBaseConfig предназначен для конфигурации базы данных.
    public class DataBaseConfig
    {
        // Статическое поле для хранения пути к файлу базы данных.
        public static readonly string DbPath = "Shemas.db";

        // Свойство для хранения строки подключения к базе данных.
        public string ConnectionString { get; private set; }

        // Конструктор класса DataBaseConfig.
        public DataBaseConfig()
        {
            // Чтение конфигурации базы данных из JSON-файла.
            var jsonText = File.ReadAllText("DataBaseConfig.json");

            // Десериализация JSON в динамический объект.
            dynamic dbConfig = JsonConvert.DeserializeObject(jsonText);

            // Получение строки подключения из конфигурации.
            ConnectionString = dbConfig.connectionString;

            // Создание таблицы в базе данных, если она не существует.
            DataBaseEntityModel.CreateTableBaseIfNotExists(ConnectionString);
        }
    }
}

