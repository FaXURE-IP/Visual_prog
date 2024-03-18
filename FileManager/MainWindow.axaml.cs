#pragma warning disable // Отключает предупреждения компилятора для этого участка кода

// Импорт необходимых библиотек Avalonia
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace FileExplorer
{
    // Представляет элемент в файловой системе
    public class FileSystemItem
    {
        public string Name { get; set; } // Имя файла или каталога
        public string Path { get; set; } // Полный путь к файлу или каталогу
        public string IconPath { get; set; } // Путь к значку, связанному с файлом или каталогом

        // Конструктор
        public FileSystemItem(string name, string path)
        {
            Name = name;
            Path = path;

            // Устанавливает путь к значку в зависимости от того, является ли элемент каталогом или файлом
            if (Directory.Exists(path))
            {
                IconPath = GetIconPath(path);
            }
            else
            {
                IconPath = GetIconPathByFileType(path);
            }
        }

        // Получает путь к значку для определенного типа файла
        private string GetIconPathByFileType(string filePath)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            IntPtr hImgLarge = SHGetFileInfo(filePath, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);
            if (hImgLarge != IntPtr.Zero)
            {
                System.Drawing.Icon icon = (System.Drawing.Icon)System.Drawing.Icon.FromHandle(shinfo.hIcon).Clone();
                DestroyIcon(shinfo.hIcon);

                string iconPath = $"{System.IO.Path.GetTempPath()}{Guid.NewGuid()}.png";
                using (FileStream iconStream = new FileStream(iconPath, FileMode.Create))
                {
                    icon.ToBitmap().Save(iconStream, System.Drawing.Imaging.ImageFormat.Png);
                }

                return iconPath;
            }

            return "";
        }

        // Получает путь к значку для каталога
        private string GetIconPath(string path)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            IntPtr hImgLarge = SHGetFileInfo(path, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);
            if (hImgLarge != IntPtr.Zero)
            {
                System.Drawing.Icon icon = (System.Drawing.Icon)System.Drawing.Icon.FromHandle(shinfo.hIcon).Clone();
                DestroyIcon(shinfo.hIcon);

                string iconPath = $"{System.IO.Path.GetTempPath()}{Guid.NewGuid()}.png";
                using (FileStream iconStream = new FileStream(iconPath, FileMode.Create))
                {
                    icon.ToBitmap().Save(iconStream, System.Drawing.Imaging.ImageFormat.Png);
                }

                return iconPath;
            }

            return "";
        }

        // Константы для получения информации о файле
        private const uint SHGFI_ICON = 0x000000100;
        private const uint SHGFI_LARGEICON = 0x000000000;

        // Структура для информации о файле
        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        // Внешний метод для получения информации о файле
        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        // Внешний метод для уничтожения дескриптора значка
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);
    }

    // Класс основного окна
    public partial class MainWindow : Window
    {
        private ListBox _listBox; // Элемент управления ListBox для отображения элементов файловой системы
        private Image _imageView; // Элемент управления Image для отображения изображений
        private string _currentDirectory; // Текущий отображаемый каталог

        // Конструктор
        public MainWindow()
        {
            InitializeComponent();
        }

        // Инициализирует компоненты окна
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this); // Загружает компоненты XAML
            _listBox = this.Find<ListBox>("listBox"); // Находит элемент управления ListBox
            _imageView = this.Find<Image>("imageView"); // Находит элемент управления Image

            // Обработчики событий для ListBox
            _listBox.DoubleTapped += ListBox_DoubleTapped;
            _listBox.PointerEntered += ListBox_PointerEntered;

            _currentDirectory = Directory.GetCurrentDirectory(); // Устанавливает текущий каталог
            RefreshList(); // Обновляет список файлов и каталогов
        }

        // Обновляет список файлов и каталогов
        private void RefreshList()
        {
            _listBox.Items.Clear(); // Очищает существующие элементы

            // Добавляет запись родительского каталога ".."
            _listBox.Items.Add(new FileSystemItem("..", Path.GetDirectoryName(_currentDirectory)));

            // Добавляет каталоги и файлы в список
            foreach (var item in GetDirectoriesAndFiles(_currentDirectory))
            {
                _listBox.Items.Add(item);
            }
        }

        // Получает каталоги и файлы для указанного пути
        private IEnumerable<FileSystemItem> GetDirectoriesAndFiles(string path)
        {
            var items = new List<FileSystemItem>();

            // Добавляет логические диски, если выбран корневой путь
            if (Path.GetPathRoot(path).Equals(path, StringComparison.OrdinalIgnoreCase))
            {
                items.AddRange(Directory.GetLogicalDrives().Select(drive => new FileSystemItem(drive, drive)));
            }

            // Добавляет каталоги
            items.AddRange(Directory.GetDirectories(path).Select(dir => new FileSystemItem(Path.GetFileName(dir), dir)));

            // Добавляет изображения
            foreach (string file in Directory.GetFiles(path))
            {
                string extension = Path.GetExtension(file);
                if (extension != null)
                {
                    string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                    if (imageExtensions.Contains(extension.ToLower()))
                    {
                        items.Add(new FileSystemItem(Path.GetFileName(file), file));
                    }
                    else
                    {
                        items.Add(new FileSystemItem(Path.GetFileName(file), file));
                    }
                }
            }

            return items;
        }

        // Обработчик события двойного щелчка по элементу в ListBox
        private void ListBox_DoubleTapped(object sender, EventArgs e)
        {
            var item = _listBox.SelectedItem as FileSystemItem;
            if (item != null)
            {
                string selectedItemPath = item.Path;

                if (Directory.Exists(selectedItemPath))
                {
                    _currentDirectory = selectedItemPath;
                    RefreshList(); // Обновляет список при входе в каталог
                }
                else if (File.Exists(selectedItemPath))
                {
                    DisplayImage(selectedItemPath); // Отображает выбранный файл изображения
                }
            }
        }

        // Обработчик события входа указателя в элемент ListBoxItem
        private void ListBox_PointerEntered(object sender, Avalonia.Input.PointerEventArgs e)
        {
            var item = (e.Source as ListBoxItem)?.Content as FileSystemItem;
            if (item != null)
            {
                ToolTip.SetTip((Control)e.Source ?? throw new ArgumentNullException(nameof(sender)), item.Path ?? ""); // Устанавливает всплывающую подсказку для элемента
            }
        }

        // Отображает изображение в элементе Image
        private void DisplayImage(string imagePath)
        {
            Bitmap bmp = new Bitmap(imagePath);
            _imageView.Source = bmp;
        }
    }

    // Конвертер для преобразования пути файла в изображение Bitmap
    public class PathConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string path)
            {
                return new Bitmap(path); // Преобразует путь файла в изображение Bitmap
            }
            return AvaloniaProperty.UnsetValue;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
