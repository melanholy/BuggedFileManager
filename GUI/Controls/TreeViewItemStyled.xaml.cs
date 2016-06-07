using System.Windows.Media.Imaging;

namespace GUI
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
