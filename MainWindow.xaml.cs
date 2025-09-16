using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows;

namespace LEA_Nermin.Alajlani_Stanislav_Kharchenko
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Event-Handler für den "Senden"-Button
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateLogin())
            {
                string message = MessageInput.Text;
                if (!string.IsNullOrWhiteSpace(message))
                {
                    txtOutput.Text += $"[Normal]: {message}\n";
                    MessageInput.Clear();
                }
                else
                {
                    MessageBox.Show("Bitte geben Sie eine Nachricht ein.", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        // Event-Handler für den "Geheim Senden"-Button
        private void BtnSecretSend_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateLogin())
            {
                string message = MessageInput.Text;
                if (!string.IsNullOrWhiteSpace(message))
                {
                    string encryptedMessage = EncryptMessage(message);
                    txtOutput.Text += $"[Geheim]: {encryptedMessage}\n";
                    MessageInput.Clear();
                }
                else
                {
                    MessageBox.Show("Bitte geben Sie eine Nachricht ein.", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        // Validierung der Login-Daten
        private bool ValidateLogin()
        {
            string username = txtUser.Text;
            string password = txtPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Bitte geben Sie Benutzername und Passwort ein.", "Login erforderlich", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        // Beispiel für eine einfache Verschlüsselungsmethode
        private string EncryptMessage(string message)
        {
            char[] chars = message.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)(chars[i] + 1); // Verschiebt jeden Buchstaben um 1
            }
            return new string(chars);
        }
    }
}
