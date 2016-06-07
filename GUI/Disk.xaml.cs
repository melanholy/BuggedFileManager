using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using filemanager.Domain.Interfaces;
using filemanager.Infrastructure;
using GUI.Application;

namespace GUI
{
    public partial class Disk
    {
        public MyPath Current;
        private readonly HistoryKeeper<MyPath> History;
        private readonly BitmapImage FolderIcon = new BitmapImage(new Uri(@"folder.bmp", UriKind.Relative));
        private readonly BitmapImage FileIcon = new BitmapImage(new Uri(@"file.bmp", UriKind.Relative));
        public event Action<MyPath> PathChanged;
        
        public Disk(Folder root)
        {
            InitializeComponent();
            
            History = new HistoryKeeper<MyPath>(root.Path);
            Current = root.Path;
            PutFilesOnPanel(root.EnumerateFiles());
        }

        public void GoBackward()
        {
            Current = History.GoBack();
            PathChanged?.Invoke(Current);
        }

        public void GoForward()
        {
            Current = History.GoForward();
            PathChanged?.Invoke(Current);
        }

        private void WrapPanelOnMouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (mouseButtonEventArgs.ChangedButton != MouseButton.Right || mouseButtonEventArgs.Handled)
                return;
            WrapPanelContextMenu.IsOpen = true;
            WrapPanelContextMenu.PlacementTarget = WrapPanel;
        }

        private ContextMenu CreateFolderContextMenu(Folder file)
        {
            var folderContextMenu = new ContextMenu();
            var deleteItem = new MenuItem { Header = "Delete Folder" };
            folderContextMenu.Items.Add(deleteItem);
            return folderContextMenu;
        }

        private ContextMenu CreateFileContextMenu(TextFile file)
        {
            var fileContextMenu = new ContextMenu();
            var deleteItem = new MenuItem { Header = "Delete MyFile" };
            fileContextMenu.Items.Add(deleteItem);
            return fileContextMenu;
        }

        private void PutFilesOnPanel(IEnumerable<MyFile> files)
        {
            WrapPanel.Children.Clear();
            foreach (var file in files)
            {
                BitmapImage icon = null;
                ContextMenu contextMenu = null;
                if (file is TextFile)
                {
                    icon = FileIcon;
                    contextMenu = CreateFileContextMenu((TextFile)file);
                }
                if (file is Folder)
                {
                    icon = FolderIcon;
                    contextMenu = CreateFolderContextMenu((Folder)file);
                }
                var folderView = new FileView(icon, file.Path.GetFileName());
                folderView.MouseUp += (sender, args) =>
                {
                    if (args.ChangedButton == MouseButton.Right)
                    {
                        args.Handled = true;
                        if (contextMenu != null)
                        {
                            contextMenu.IsOpen = true;
                            contextMenu.PlacementTarget = folderView;
                        }
                    }
                };
                folderView.MouseDoubleClick += (s, e) => FolderViewOnMouseDoubleClick(file);
                WrapPanel.Children.Add(folderView);
            }
        }
        
        private void FolderViewOnMouseDoubleClick(MyFile file)
        {
            if (file is Folder)
            {
                var folder = (Folder)file;
            }
        }
    }
}
