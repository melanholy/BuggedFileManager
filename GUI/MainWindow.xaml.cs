using System.Linq;
using System.Windows;
using filemanager.Domain;
using filemanager.Infrastructure;
using GUI.Application;

namespace GUI
{
    public partial class MainWindow
    {
        private Disk Active;

        public MainWindow(PluginRepo repo)
        {
            InitializeComponent();
            
            var folders = WinFolder.GetRootFolders().ToArray();
            foreach (var disk in folders.Select(folder => new Disk(folder) { Header = folder.Path.GetFileName() }))
                DiskTabs.Items.Add(disk);

            Active = (Disk)DiskTabs.Items[0];
            Active.PathChanged += SetPathText;
            SetPathText(Active.Current);
            DiskTabs.SelectionChanged += (sender, args) => ChangeActiveManager();
            DiskTabs.SelectedIndex = 0;
        }
        
        private void ChangeActiveManager()
        {
            BackButton.Click -= BackButtonOnClick;
            ForwardButton.Click -= ForwardButtonOnClick;
            Active.PathChanged -= SetPathText;

            Active = (Disk)DiskTabs.SelectedItem;

            SetPathText(Active.Current);
            Active.PathChanged += SetPathText;
            BackButton.Click += BackButtonOnClick;
            ForwardButton.Click += ForwardButtonOnClick;
        }

        private void SetPathText(MyPath path)
        {
            textBox.Text = path.PathStr;
        }

        private void ForwardButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            Active.GoForward();
        }

        private void BackButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            Active.GoBackward();
        }
    }
}
