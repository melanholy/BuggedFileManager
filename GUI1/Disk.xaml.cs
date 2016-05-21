using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using filemanager.Application;
using filemanager.Domain;
using filemanager.Infrastructure;
using GUI;

namespace GUI1
{
    /// <summary>
    /// Логика взаимодействия для Disk.xaml
    /// </summary>
    public partial class Disk
    {
        public readonly HistoryKeeper History;
        private readonly BitmapImage FolderIcon = new BitmapImage(new Uri(@"folder.bmp"));
        private readonly BitmapImage FileIcon = new BitmapImage(new Uri(@"file.bmp"));
        public event Action<MyPath> PathChanged;
        
        public Disk(MyPath path, IFileManager fm)
        {
            InitializeComponent();

            History = new HistoryKeeper(path);
            PathChanged?.Invoke(path);
        }

        public void GoBackward()
        {
            History.GoBack();
        }

        public void GoForward()
        {
            History.GoForward();
        }

        private void WrapPanelOnMouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (mouseButtonEventArgs.ChangedButton != MouseButton.Right || mouseButtonEventArgs.Handled)
                return;
            WrapPanelContextMenu.IsOpen = true;
            WrapPanelContextMenu.PlacementTarget = WrapPanel;
        }

        private ContextMenu CreateFolderContextMenu(IFolder file)
        {
            var folderContextMenu = new ContextMenu();
            var deleteItem = new MenuItem { Header = "Delete Folder" };
            folderContextMenu.Items.Add(deleteItem);
            return folderContextMenu;
        }

        private ContextMenu CreateFileContextMenu(ITextFile file)
        {
            var fileContextMenu = new ContextMenu();
            var deleteItem = new MenuItem { Header = "Delete File" };
            fileContextMenu.Items.Add(deleteItem);
            return fileContextMenu;
        }

        private void PutFilesOnPanel(IEnumerable<IFile> files)
        {
            WrapPanel.Children.Clear();
            foreach (var file in files)
            {
                BitmapImage icon = null;
                ContextMenu contextMenu = null;
                if (file is WinFile)
                {
                    icon = FileIcon;
                    contextMenu = CreateFileContextMenu(file);
                }
                if (file is WinFolder)
                {
                    icon = FolderIcon;
                    contextMenu = CreateFolderContextMenu(file);
                }
                var folderView = new FileView(icon, file.Name);
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
        
        private void FolderViewOnMouseDoubleClick(IFile file)
        {
            if (file is IFolder)
            {
                var folder = (IFolder)file;
                History.GoToFolder(folder);
            }
        }
    }
}
