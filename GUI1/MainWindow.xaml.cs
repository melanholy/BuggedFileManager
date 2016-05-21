using System.Linq;
using System.Windows;
using filemanager.Domain;
using filemanager.Infrastructure;
using GUI1;

namespace GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Disk Active;
        public MainWindow()
        {
            InitializeComponent();
            
            var folders = WinFolder.GetRootFolders();
            foreach (var disk in folders.Select(folder => new Disk(folder.Path) { Header = folder.Name }))
                DiskTabs.Items.Add(disk);
            DiskTabs.SelectedIndex = 0;
            ChangeActiveManager();
            Active.PathChanged += path => textBox.Text = path.Path;
            DiskTabs.SelectionChanged += (sender, args) => ChangeActiveManager();
        }
        
        private void ChangeActiveManager()
        {
            Active = (Disk)DiskTabs.SelectedItem;
            BackButton.Click += (sender, args) => Active.GoBackward();
            ForwardButton.Click += (sender, args) => Active.GoForward();
        }
    }
}
