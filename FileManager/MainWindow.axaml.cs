#pragma warning disable
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
    public class FileSystemItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string IconPath { get; set; }

        public FileSystemItem(string name, string path)
        {
            Name = name;
            Path = path;

            if (Directory.Exists(path))
            {
                IconPath = GetIconPath(path);
            }
            else
            {
                IconPath = GetIconPathByFileType(path);
            }
        }

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
        private const uint SHGFI_ICON = 0x000000100;
        private const uint SHGFI_LARGEICON = 0x000000000;

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

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);
    }

    public partial class MainWindow : Window
    {
        private ListBox _listBox;
        private Image _imageView;
        private string _currentDirectory;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _listBox = this.Find<ListBox>("listBox");
            _imageView = this.Find<Image>("imageView");

            _listBox.DoubleTapped += ListBox_DoubleTapped;
            _listBox.PointerEntered += ListBox_PointerEntered;

            _currentDirectory = Directory.GetCurrentDirectory();
            RefreshList();
        }

        private void RefreshList()
        {
            _listBox.Items.Clear();

            _listBox.Items.Add(new FileSystemItem("..", Path.GetDirectoryName(_currentDirectory)));

            foreach (var item in GetDirectoriesAndFiles(_currentDirectory))
            {
                _listBox.Items.Add(item);
            }
        }

        private IEnumerable<FileSystemItem> GetDirectoriesAndFiles(string path)
        {
            var items = new List<FileSystemItem>();

            if (Path.GetPathRoot(path).Equals(path, StringComparison.OrdinalIgnoreCase))
            {
                items.AddRange(Directory.GetLogicalDrives().Select(drive => new FileSystemItem(drive, drive)));
            }

            items.AddRange(Directory.GetDirectories(path).Select(dir => new FileSystemItem(Path.GetFileName(dir), dir)));

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

        private void ListBox_DoubleTapped(object sender, EventArgs e)
        {
            var item = _listBox.SelectedItem as FileSystemItem;
            if (item != null)
            {
                string selectedItemPath = item.Path;

                if (Directory.Exists(selectedItemPath))
                {
                    _currentDirectory = selectedItemPath;
                    RefreshList();
                }
                else if (File.Exists(selectedItemPath))
                {
                    DisplayImage(selectedItemPath);
                }
            }
        }
        private void ListBox_PointerEntered(object sender, Avalonia.Input.PointerEventArgs e)
        {
            var item = (e.Source as ListBoxItem)?.Content as FileSystemItem;
            if (item != null)
            {
                ToolTip.SetTip((Control)e.Source ?? throw new ArgumentNullException(nameof(sender)), item.Path ?? "");
            }
        }

        private void DisplayImage(string imagePath)
        {
            Bitmap bmp = new Bitmap(imagePath);
            _imageView.Source = bmp;
        }
    }

    public class PathConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string path)
            {
                return new Bitmap(path);
            }
            return AvaloniaProperty.UnsetValue;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}