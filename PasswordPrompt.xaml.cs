using System.Windows;

namespace LEA_Nermin.Alajlani_Stanislav_Kharchenko
{
    public partial class PasswordPrompt : Window
    {
        public string Password => pwdBox.Password;

        public PasswordPrompt()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
