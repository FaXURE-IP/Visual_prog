using Dapper;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Text;

namespace LogicGatesAvalonia.Models.DataBaseModel
{
    // Класс ShemasReader используется для чтения данных из таблицы базы данных.
    public class ShemasReader
    {
        // Поле для хранения SQL-запроса на выборку данных.
        private string SelectQuery = $@"SELECT HashCode as {nameof(DataBaseRecord.HashCode)}," +
            $"HashCodeInput_1 as {nameof(DataBaseRecord.HashCodeInput_1)}," +
            $"HashCodeInput_2 as {nameof(DataBaseRecord.HashCodeInput_2)}," +
            $"Input_1Value as {nameof(DataBaseRecord.Input_1Value)}," +
            $"Input_2Value as {nameof(DataBaseRecord.Input_2Value)}," +
            $"CanvasX as {nameof(DataBaseRecord.CanvasX)}," +
            $"CanvasY as {nameof(DataBaseRecord.CanvasY)}," +
            $"Type as {nameof(DataBaseRecord.Type)}," +
            $"HashCodeOutputLink as {nameof(DataBaseRecord.HashCodeOutputLink)}";

        // Метод для построения SQL-запроса на выборку данных из указанной таблицы.
        private void BuildSelectQuery(string tableName)
        {
            var sb = new StringBuilder();
            sb.Append(SelectQuery).Append($" FROM {tableName}");
            SelectQuery = sb.ToString();
        }

        // Конструктор класса, принимает имя таблицы и строит SQL-запрос.
        public ShemasReader(string tableName)
        {
            BuildSelectQuery(tableName);
        }

        // Метод для обновления SQL-запроса с новым именем таблицы.
        public void SetShema(string tableName)
        {
            BuildSelectQuery(tableName);
        }

        // Метод для чтения данных из базы данных.
        public void ReadShema()
        {
            // Получение конфигурации базы данных.
            var dbConfig = new DataBaseConfig();

            // Создание и открытие подключения к базе данных.
            using var connection = new SqliteConnection(dbConfig.ConnectionString);
            connection.Open();

            // Выполнение SQL-запроса и сохранение результата в DataSet.
            DataBaseEntityModel.DataSet = connection.Query<DataBaseRecord>(SelectQuery).ToList();
        }
    }
}
