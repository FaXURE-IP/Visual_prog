using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileExplorer
{
    public class FileSystemItem
    {
        public string Name { get; set; }
    }


    public partial class MainWindow : Window
    {
        private ListBox _listBox;
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
            _listBox.DoubleTapped += ListBox_DoubleTapped;

            _currentDirectory = Directory.GetCurrentDirectory();
            RefreshList();
        }

        private void RefreshList()
        {
            _listBox.Items.Clear();

            _listBox.Items.Add(new FileSystemItem { Name = _currentDirectory });

            foreach (var item in GetDirectoriesAndFiles(_currentDirectory))
            {
                _listBox.Items.Add(item);
            }
        }


        private IEnumerable<FileSystemItem> GetDirectoriesAndFiles(string path)
        {
            var items = new List<FileSystemItem>();
            if (!Path.GetPathRoot(path).Equals(path, StringComparison.OrdinalIgnoreCase))
            {
                items.Add(new FileSystemItem { Name = ".." });
            }
            if (Path.GetPathRoot(path).Equals(path, StringComparison.OrdinalIgnoreCase))
            {
                items.AddRange(Directory.GetLogicalDrives().Select(drive => new FileSystemItem { Name = drive }));
            }
            items.AddRange(Directory.GetDirectories(path).Select(dir => new FileSystemItem { Name = Path.GetFileName(dir) }));
            items.AddRange(Directory.GetFiles(path).Select(file => new FileSystemItem { Name = Path.GetFileName(file) }));

            return items;
        }


        private void ListBox_DoubleTapped(object sender, EventArgs e)
        {
            var item = _listBox.SelectedItem;
            if (item != null && item is FileSystemItem fileSystemItem)
            {
                string selectedItem = fileSystemItem.Name;

                // Handle ".." (up one level)
                if (selectedItem.Equals("..") && !Path.GetPathRoot(_currentDirectory).Equals(_currentDirectory, StringComparison.OrdinalIgnoreCase))
                {
                    _currentDirectory = Directory.GetParent(_currentDirectory).FullName;
                    RefreshList();
                    return;
                }

                string selectedPath = Path.Combine(_currentDirectory, selectedItem);
                if (Directory.Exists(selectedPath))
                {
                    _currentDirectory = selectedPath;
                    RefreshList();
                    return;
                }
            }
        }
    }
}
