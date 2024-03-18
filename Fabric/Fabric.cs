using System;
using System.Collections.ObjectModel;
using System.IO; 
using System.Reactive.Linq;
using System.Collections.Specialized;

class Program
{
    static void Main(string[] args)
    {
        var collection = new ObservableCollection<int>();
        var factory = new CollectionEventFactory();

        // Подписываемся на события изменения коллекции
        factory.GetEventStream(collection)
               .Subscribe(args => LogChangesToFile(args, "log.txt"));

        // Добавляем и удаляем элементы из коллекции для демонстрации работы
        collection.Add(1);
        collection.Add(2);
        collection.Remove(1);

        Console.WriteLine("Changes logged to log.txt");
    }

    static void LogChangesToFile(NotifyCollectionChangedEventArgs args, string filePath)
    {
        using (StreamWriter writer = File.AppendText(filePath))
        {
            writer.WriteLine($"Change Type: {args.Action}, New Items: {string.Join(",", args.NewItems)}, Old Items: {string.Join(",", args.OldItems)}");
        }
    }
}

public class CollectionEventFactory
{
    public IObservable<NotifyCollectionChangedEventArgs> GetEventStream(ObservableCollection<int> collection)
    {
        return Observable.FromEvent<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
            handler =>
            {
                NotifyCollectionChangedEventHandler wrappedHandler = (sender, args) => handler(args);
                return wrappedHandler;
            },
            handler => collection.CollectionChanged += handler,
            handler => collection.CollectionChanged -= handler
        );
    }
}
