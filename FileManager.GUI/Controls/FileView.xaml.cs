using System.Windows.Media;
using FileManager.Domain;
using FileManager.Domain.Infrastructure;
using FileManager.Domain.Models;
using FileManager.Domain.Models.Files;

namespace FileManager.GUI.Controls
{
    public partial class FileView
    {
        public FileView(ImageSource image, string name, FileInfo info)
        {
            InitializeComponent();

            Image.Source = image;
            Filename.Text = name;
            var sizestr = info.Size.Value == FileSize.DirSize ? "<DIR>" : info.Size.Value.ToString("N0") + " Bytes";
            Description.Text = string.Format(
                "{3}\r\nSize: {0}\r\nCreated: {1:yyyy MMMM dd HH:mm:ss}\r\nModified: {2: yyyy MMMM dd HH:mm:ss}",
                sizestr, info.Created, info.Modified, name
            );
        }
    }
}
