
namespace LogicGatesAvalonia.Models.DataBaseModel
{
    // Класс DataBaseRecord представляет запись в базе данных.
    // Поля отмеченные # должны сохраняться после выхода.

    public class DataBaseRecord 
    {
        // Тип логического элемента (например, AND, OR, NOT и т.д.)
        public string Type { get; set; }

        // Уникальный хэш-код записи
        public int HashCode { get; set; }

        // Хэш-коды входных значений
        public int HashCodeInput_1 { get; set; }
        public int HashCodeInput_2 { get; set; }

        // Хэш-код выхода
        public int HashCodeOutputLink { get; set; }

        // Значения входов, по умолчанию 0
        public int Input_1Value { get; set; } = 0; // # Должен сохраняться после выхода
        public int Input_2Value { get; set; } = 0; // # Должен сохраняться после выхода

        // Координаты на холсте, по умолчанию 0.0
        public double CanvasX { get; set; } = 0.0; // # Должен сохраняться после выхода
        public double CanvasY { get; set; } = 0.0; // # Должен сохраняться после выхода

        // Пустой конструктор по умолчанию
        public DataBaseRecord()
        {
        }

        // Конструктор для инициализации записи с типом и хэш-кодом
        public DataBaseRecord(string type, int hashCode)
        {
            Type = type;
            HashCode = hashCode;
        }
    }
}
