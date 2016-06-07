using System.Windows;
using System.Windows.Media.Imaging;

namespace FileManager.GUI.Controls
{
    public partial class FileView
    {
        public FileView(BitmapImage image, string name)
        {
            InitializeComponent();

            Image.Source = image;
            Filename.Text = name;
        }

        public BitmapImage MyImageSource
        {
            get { return (BitmapImage)GetValue(MyImageSourceProperty); }
            set { SetValue(MyImageSourceProperty, value); }
        }

        public static readonly DependencyProperty MyImageSourceProperty =
            DependencyProperty.Register("MyImageSource", typeof(BitmapImage), typeof(FileView));
    }
}
