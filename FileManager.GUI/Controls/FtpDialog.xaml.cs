using System.Windows;

namespace FileManager.GUI.Controls
{
    public partial class FtpDialog
    {
        public string Address => AddressTextBox.Text;
        public string Password => PasswordTextBox.Text;
        public string Login => LoginTextBox.Text;

        public FtpDialog()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
