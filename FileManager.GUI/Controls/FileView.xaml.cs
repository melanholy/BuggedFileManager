using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FileManager.GUI.Controls
{
    public partial class FileView
    {
        private readonly Timer MouseWatcher;
        private readonly ContextMenu NameContextMenu;
        public FileView(ImageSource image, string name)
        {
            InitializeComponent();

            Image.Source = image;
            Filename.Text = name;
            MouseWatcher = new Timer
            {
                AutoReset = false,
                Interval = 1500
            };
            MouseWatcher.Elapsed += MouseWatcherOnElapsed;
            MouseEnter += (s, e) => HandleStaticMouse();
            NameContextMenu = new ContextMenu();
            var deleteItem = new MenuItem { Header = Filename.Text };
            NameContextMenu.Items.Add(deleteItem);
        }

        private void HandleStaticMouse()
        {
        }


        private void MouseWatcherOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            NameContextMenu.IsOpen = true;
            NameContextMenu.PlacementTarget = this;
            var deleteItem = new MenuItem { Header = Filename.Text };
            NameContextMenu.Items.Add(deleteItem);
        }

        private void OnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            MouseWatcher.Stop();
            MouseWatcher.Start();
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
