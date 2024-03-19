using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Reactive.Subjects;

namespace CollectionObserverFactory
{
    // Фабрика для создания объекта IObservable<NotifyCollectionChangedEventArgs> из коллекции ObservableCollection
    public static class CollectionObserverFactory
    {
        public static IObservable<NotifyCollectionChangedEventArgs> CreateObserver(ObservableCollection<object> collection)
        {
            var subject = new Subject<NotifyCollectionChangedEventArgs>();
            collection.CollectionChanged += (sender, args) => subject.OnNext(args);
            return subject;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создание коллекции
            var collection = new ObservableCollection<object>();

            // Создание объекта наблюдателя
            var observer = CollectionObserverFactory.CreateObserver(collection);

            // Подписка на уведомления об изменениях и логирование в файл
            observer.Subscribe(args =>
            {
                LogToFile($"Change Type: {args.Action}, New Items Count: {args.NewItems?.Count}, Old Items Count: {args.OldItems?.Count}");
            });

            // Пример изменений в коллекции
            collection.Add("Item 1");
            collection.Add("Item 2");
            collection.RemoveAt(0);

            // Задержка перед закрытием консоли
            Console.ReadLine();
        }

        // Метод для логирования данных в файл
        static void LogToFile(string message)
        {
            string filePath = "log.txt";
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }
}
