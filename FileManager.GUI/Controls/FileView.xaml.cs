using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FileManager.Domain;
using FileManager.Domain.Infrastructure;

namespace FileManager.GUI.Controls
{
    public partial class FileView
    {
        public FileView(ImageSource image, string name, FileInfo info)
        {
            InitializeComponent();

            Image.Source = image;
            Filename.Text = name;
            var sizestr = info.Size.Value == FileSize.DirSize ? info.Size.ToString() : info.Size.ToString("N0") + " Bytes";
            Description.Text = string.Format(
                "{3}\r\nSize: {0}\r\nCreated: {1:yyyy MMMM dd HH:mm:ss}\r\nModified: {2: yyyy MMMM dd HH:mm:ss}",
                sizestr, info.Created, info.Modified, name
            );
        }
    }
}
