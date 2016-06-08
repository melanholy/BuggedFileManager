using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FileManager.Domain.Infrastructure;
using FileManager.Domain.Windows;
using FileManager.GUI.Application;
using FileManager.GUI.Controls;

namespace FileManager.GUI
{
    public partial class MainWindow
    {
        private Disk Active;

        public MainWindow(PluginRepo repo)
        {
            InitializeComponent();
            
            var folders = WinFolder.GetRootFolders().ToArray();
            var disks = folders
                .Select(folder => new Disk(folder, repo, new WinFileManager(folder))
                    {
                        Header = folder.Path.GetFileName()
                    });
            foreach (var disk in disks)
                DiskTabs.Items.Add(disk);

            Active = (Disk)DiskTabs.Items[0];
            Active.PathChanged += SetPathText;
            SetPathText(folders[0].Path);
            DiskTabs.SelectionChanged += ChangeActiveManager;
            DiskTabs.SelectedIndex = 0;
        }
        
        private void ChangeActiveManager(object sender, SelectionChangedEventArgs e)
        {
            Active.PathChanged -= SetPathText;

            Active = (Disk)DiskTabs.SelectedItem;

            SetPathText(Active.Current);
            Active.PathChanged += SetPathText;
        }

        private void SetPathText(MyPath path)
        {
            PathTextBox.Text = path.PathStr;
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            Active.GoBackward();
        }

        private void ForwardButton_OnClick(object sender, RoutedEventArgs e)
        {
            Active.GoForward();
        }

        private void UpButton_OnClick(object sender, RoutedEventArgs e)
        {
            Active.GoUp();
        }

        private void PathTextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            MyPath path;
            try
            {
                path = new MyPath(PathTextBox.Text);
            }
            catch (ArgumentException)
            {
                PathTextBox.Text = Active.Current.PathStr;
                return;
            }
            Active.GoToFolder(path);
        }
    }
}
