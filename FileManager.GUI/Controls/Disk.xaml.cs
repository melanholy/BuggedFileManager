using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using FileManager.Domain.Infrastructure;
using FileManager.Domain.Models;
using FileManager.Domain.Models.Files;
using FileManager.Domain.Models.Windows;
using FileManager.GUI.Application;

namespace FileManager.GUI.Controls
{
    public partial class Disk
    {
        public MyPath Current => Manager.CurrentPath;
        private readonly FileManagerWithHistory Manager;
        private static readonly BitmapImage FolderIcon  = new BitmapImage(
            new Uri(@"pack://application:,,,/gfilemanager;component/Resources/folder.bmp"));
        private static readonly BitmapImage FileIcon = new BitmapImage(
            new Uri(@"pack://application:,,,/gfilemanager;component/Resources/file.bmp"));
        private readonly FileTree Tree;
        private readonly PluginRepo Repo;

        public event Action<MyPath> PathChanged;
        
        public Disk(Folder root, PluginRepo repo, FileManagerWithHistory manager)
        {
            InitializeComponent();
            
            Repo = repo;
            Manager = manager;
            Tree = new FileTree(root);

            FileTree.ItemsSource = new [] { Tree.Root };
            PutFilesOnPanel(root.EnumerateFiles());
        }

        public void GoBackward()
        {
            try
            {
                var folder = Manager.GoBack();
                PathChanged?.Invoke(Current);
                PutFilesOnPanel(folder.EnumerateFiles());
            }
            catch (EmptyHistoryException)
            {
                
            }
        }

        public void GoForward()
        {
            try
            {
                var folder = Manager.GoForward();
                PathChanged?.Invoke(Current);
                PutFilesOnPanel(folder.EnumerateFiles());
            }
            catch (EmptyHistoryException)
            {
                
            }
        }

        public void CopyExample(MyFile source, MyFile destination)
        {
            var process = Manager.Move(source, true);

            /* some stuff */

            process.To(destination);

            Manager.Move(source, false).To(destination);
        }

        public void GoUp()
        {
            var folder = Manager.GoUp();
            PathChanged?.Invoke(Current);
            PutFilesOnPanel(folder.EnumerateFiles());
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
            var deleteItem = new MenuItem { Header = "Delete File" };
            fileContextMenu.Items.Add(deleteItem);
            return fileContextMenu;
        }

        public void GoToFolder(MyPath path)
        {
            var folder = Manager.Go(path);
            PutFilesOnPanel(folder.EnumerateFiles());
            PathChanged?.Invoke(Current);
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
                var folderView = new FileView(icon, file.Path.GetFileName(), file.Info);
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
                PutFilesOnPanel(folder.EnumerateFiles());
                Manager.Go(folder);
                PathChanged?.Invoke(Current);
            }
            if (file is TextFile)
            {
                file = (TextFile)file;
                object result;
                if (Repo.TryGet(file.Path.GetExt(), out result))
                {
                }
            }
        }

        private void FileTree_OnExpanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)e.OriginalSource;
            var node = item.Header as TreeNode;
            Tree.Expand(node); 
        }

        private void FileTree_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var node = (TreeNode)e.NewValue;
            if (!(node.File is Folder))
                return;

            var dir = (Folder)node.File;
            Manager.Go(dir);
            PutFilesOnPanel(dir.EnumerateFiles());
            PathChanged?.Invoke(Current);
        }
    }
}
