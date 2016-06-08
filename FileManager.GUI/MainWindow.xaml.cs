using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FileManager.Domain.FTP;
using FileManager.Domain.Infrastructure;
using FileManager.Domain.Windows;
using FileManager.GUI.Application;
using FileManager.GUI.Controls;
using Limilabs.FTP.Client;

namespace FileManager.GUI
{
    public partial class MainWindow
    {
        private Disk Active;
        private PluginRepo Repo;

        public MainWindow(PluginRepo repo)
        {
            InitializeComponent();

            Repo = repo;

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

        private async void FtpConnect_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FtpDialog();
            dialog.ShowDialog();
            var client = new Ftp();
            IPHostEntry host;
            try
            {
                host = await Dns.GetHostEntryAsync(dialog.Address);
            }
            catch (SocketException)
            {
                MessageBox.Show("Некорректный адрес.");
                return;
            }

            client.Connect(host.AddressList[0], 21, false);
            try
            {
                if (dialog.Password != "" && dialog.Login != "")
                    await Task.Run(() => client.Login(dialog.Login, dialog.Password));
                else
                    await Task.Run(() => client.LoginAnonymous());
            }
            catch (FtpResponseException)
            {
                MessageBox.Show("Некорректный логин или пароль.");
                return;
            }

            var root = new FtpFolder(new MyPath("/"), client);
            var disk = new Disk(root, Repo, new FtpFileManager(root, client))
            {
                Header = dialog.Address
            };
            DiskTabs.Items.Add(disk);
        }
    }
}
