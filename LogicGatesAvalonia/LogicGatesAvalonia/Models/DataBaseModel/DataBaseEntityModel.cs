using Avalonia.Controls;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Media;

using LogicGatesAvalonia.Controls;
using LogicGatesAvinternal.Controls;

namespace LogicGatesAvalonia.Models.DataBaseModel
{
    // Статический класс DataBaseEntityModel для работы с набором данных и базой данных.
    public static class DataBaseEntityModel
    {
        // Статическое свойство DataSet, содержащее список записей базы данных.
        public static List<DataBaseRecord> DataSet { get; set; } = new List<DataBaseRecord>();

        // Метод для поиска записи в DataSet по хэш-коду.
        public static DataBaseRecord FindRecord(int hashKey)
        {
            try
            {
                return DataSet.First(record => record.HashCode == hashKey);
            }
            catch(InvalidOperationException)
            {
                throw new InvalidOperationException("no record in data base");
            }
        }

        // Метод для создания таблицы в базе данных, если она не существует.
        public static void CreateTableBaseIfNotExists(string connectionString,
            string tableName = "MainShema")
        {
            using(var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = $"CREATE TABLE IF NOT EXISTS {tableName}" +
                    " (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                    "HashCode INTEGER," +
                    "HashCodeInput_1 INTEGER DEFAULT NULL," +
                    "HashCodeInput_2 INTEGER DEFAULT NULL," +
                    "Input_1Value INTEGER DEFAULT NULL," +
                    "Input_2Value INTEGER DEFAULT NULL," +
                    "CanvasX REAL," +
                    "CanvasY REAL," +
                    "Type TEXT NOT NULL," +
                    "HashCodeOutputLink INTEGER DEFAULT NULL)";

                command.ExecuteNonQuery();
            }
        }

        // Метод для очистки таблицы в базе данных.
        public static void ClearTable(string connectionString,
            string tableName = "MainShema") 
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM {tableName}";
                command.ExecuteNonQuery();
            }
        }

        // Метод для записи данных из DataSet в базу данных.
        public static void WriteDataToDataBase(string connectionString,
            string tableName = "MainShema")
        {
            // Создание таблицы, если она не существует.
            CreateTableBaseIfNotExists(connectionString, tableName);

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                // Очистка таблицы перед записью данных.
                ClearTable(connectionString, tableName);

                // Запись каждой записи из DataSet в базу данных.
                foreach(var record in DataSet)
                {
                    string sql = $"INSERT INTO {tableName} (" +
                        "HashCode," +
                        "HashCodeInput_1," +
                        "HashCodeInput_2," +
                        "Input_1Value," +
                        "Input_2Value," +
                        "CanvasX," +
                        "CanvasY," +
                        "Type," +
                        "HashCodeOutputLink) " +
                        "VALUES (" +
                        "@HashCode," +
                        "@HashCodeInput_1," +
                        "@HashCodeInput_2," +
                        "@Input_1Value," +
                        "@Input_2Value," +
                        "@CanvasX," +
                        "@CanvasY," +
                        "@Type," +
                        "@HashCodeOutputLink)";

                    using var insertCommand = new SqliteCommand(sql, connection);

                    insertCommand.Parameters.AddWithValue("@HashCode", record.HashCode);
                    insertCommand.Parameters.AddWithValue("@HashCodeInput_1", record.HashCodeInput_1);
                    insertCommand.Parameters.AddWithValue("@HashCodeInput_2", record.HashCodeInput_2);
                    insertCommand.Parameters.AddWithValue("@Input_1Value", record.Input_1Value);
                    insertCommand.Parameters.AddWithValue("@Input_2Value", record.Input_2Value);
                    insertCommand.Parameters.AddWithValue("@CanvasX", record.CanvasX);
                    insertCommand.Parameters.AddWithValue("@CanvasY", record.CanvasY);
                    insertCommand.Parameters.AddWithValue("@Type", record.Type);
                    insertCommand.Parameters.AddWithValue("@HashCodeOutputLink", record.HashCodeOutputLink);

                    insertCommand.ExecuteNonQuery();
                }   
            }
        }

        // Метод для обновления актуальных данных управления на холсте.
        public static void UpdateActualControlsData(List<Control> canvasControls)
        {
            foreach (var control in canvasControls)
            {
                if(control is BaseLogicalGateControl logicalGateControl)
                {
                    try
                    {
                        var controlRecord = FindRecord(logicalGateControl.Hash);

                        if (control.RenderTransform is TranslateTransform translateTransform)
                        {
                            double controlX = translateTransform.X;
                            double controlY = translateTransform.Y;
                            controlRecord.CanvasX = controlX;
                            controlRecord.CanvasY = controlY;
                        }

                        if (logicalGateControl is BaseOneChannelLogicalContorl oneChannelControl)
                        {
                            controlRecord.Input_1Value = Convert.ToInt32(oneChannelControl.Input);
                        }

                        if (logicalGateControl is BaseTwoChannelLogicalControl twoChannelControl)
                        {
                            controlRecord.Input_1Value = Convert.ToInt32(twoChannelControl.Input_1);
                            controlRecord.Input_2Value = Convert.ToInt32(twoChannelControl.Input_2);
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Игнорирование исключения, если запись не найдена.
                    }
                }
            }
        }

        // Метод для удаления записи из DataSet и обновления связанных записей.
        public static void Remove(int recordHashcode)
        {
            try
            {
                var deletionRecord = FindRecord(recordHashcode);
                if (deletionRecord != null)
                {
                    if (deletionRecord.HashCodeOutputLink != 0)
                    {
                        var outLinkRecord = FindRecord(deletionRecord.HashCodeOutputLink);
                        if (outLinkRecord.HashCodeInput_1 == deletionRecord.HashCode)
                        {
                            outLinkRecord.HashCodeInput_1 = 0;
                        }
                        if (outLinkRecord.HashCodeInput_2 == deletionRecord.HashCode)
                        {
                            outLinkRecord.HashCodeInput_2 = 0;
                        }
                    }

                    if (deletionRecord.HashCodeInput_1 != 0)
                    {
                        var in1LinkRecord = FindRecord(deletionRecord.HashCodeInput_1);
                        in1LinkRecord.HashCodeOutputLink = 0;
                    }

                    if (deletionRecord.HashCodeInput_2 != 0)
                    {
                        var in2LinkRecord = FindRecord(deletionRecord.HashCodeInput_2);
                        in2LinkRecord.HashCodeOutputLink = 0;
                    }

                    DataSet.Remove(deletionRecord);
                }
            }
            catch (InvalidOperationException)
            {
                // Игнорирование исключения, если запись не найдена.
            }
        }

        // Метод для записи информации о соединении в записи.
        public static void WriteConnectorInfoToRecord(
            BaseLogicalGateControl output,
            BaseLogicalGateControl input,
            InputSockets socketId)
        {
            foreach (var record in DataSet)
            {
                if (record.HashCode == output.Hash)
                {
                    record.HashCodeOutputLink = input.Hash;

                    if (socketId == InputSockets.firstSocket)
                    {
                        foreach (var linkedRecord in DataSet)
                        {
                            if (record.HashCodeOutputLink == linkedRecord.HashCode)
                            {
                                linkedRecord.HashCodeInput_1 = record.HashCode;
                                break;
                            }
                        }
                    }

                    if (socketId == InputSockets.secondSocket)
                    {
                        foreach (var linkedRecord in DataSet)
                        {
                            if (record.HashCodeOutputLink == linkedRecord.HashCode)
                            {
                                linkedRecord.HashCodeInput_2 = record.HashCode;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
