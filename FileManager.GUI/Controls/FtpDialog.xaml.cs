using System.Windows;
using System.Windows.Input;

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

            AddressTextBox.Focus();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FtpDialog_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            Close();
        }
    }
}
