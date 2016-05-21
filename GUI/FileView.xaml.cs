using System.Windows;
using System.Windows.Media.Imaging;

namespace GUI
{
    /// <summary>
    /// Логика взаимодействия для FileView.xaml
    /// </summary>
    public partial class FileView
    {
        public FileView(BitmapImage image, string name)
        {
            InitializeComponent();
            view.Source = image;
            filename.Text = name;
        }

        public BitmapImage MyImageSource
        {
            get { return (BitmapImage)GetValue(MyImageSourceProperty); }
            set { SetValue(MyImageSourceProperty, value); }
        }

        public static readonly DependencyProperty MyImageSourceProperty =
            DependencyProperty.Register("MyImageSource",
                typeof(BitmapImage), typeof(FileView));
    }
}
