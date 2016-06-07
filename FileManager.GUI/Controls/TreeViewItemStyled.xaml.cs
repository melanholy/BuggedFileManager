using System.Windows.Media.Imaging;

namespace FileManager.GUI.Controls
{
    public partial class TreeViewItemStyled
    {
        public BitmapImage Icon;

        public TreeViewItemStyled(BitmapImage icon)
        {
            Extensions.SetImage(this, icon);
            InitializeComponent();
            
        }
    }
}
