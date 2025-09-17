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
using System.IO;



using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LEA_Nermin.Alajlani_Stanislav_Kharchenko
{
    public partial class MainWindow : Window
    {
        private bool isLoggedIn = false;
        private string savedUser = "";
        private string savedPassword = "";

        // Nachrichten-Sammlung (automatisch mit ListBox verbunden)
        public ObservableCollection<ChatMessage> Messages { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Messages = new ObservableCollection<ChatMessage>();
            ChatList.ItemsSource = Messages;

            ToggleMessageControls(false);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!isLoggedIn)
            {
                string user = txtUser.Text;
                string password = txtPassword.Password;

                if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Bitte Benutzername und Passwort eingeben!");
                    return;
                }

                savedUser = user;
                savedPassword = password;
                isLoggedIn = true;

                MessageBox.Show($"Willkommen {savedUser}!");
                BtnLogin.Content = "Abmelden";
                ToggleMessageControls(true);
            }
            else
            {
                isLoggedIn = false;
                BtnLogin.Content = "Anmelden";
                MessageBox.Show("Du wurdest abgemeldet.");
                ToggleMessageControls(false);
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (!isLoggedIn)
            {
                MessageBox.Show("Bitte zuerst einloggen!");
                return;
            }

            string message = MessageInput.Text;
            if (!string.IsNullOrWhiteSpace(message) && message != "Nachricht Schreiben ...")
            {
                Messages.Add(new ChatMessage { User = savedUser, Text = message });
                MessageInput.Clear();
            }
        }

        private void BtnSecretSend_Click(object sender, RoutedEventArgs e)
        {
            if (!isLoggedIn)
            {
                MessageBox.Show("Bitte zuerst einloggen!");
                return;
            }

            string message = MessageInput.Text;
            if (!string.IsNullOrWhiteSpace(message) && message != "Nachricht Schreiben ...")
            {
                string geheim = ConvertToSecret(message);
                Messages.Add(new ChatMessage { User = savedUser + " (geheim)", Text = geheim });
                MessageInput.Clear();
            }
        }

        private string ConvertToSecret(string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
                array[i] = (char)(array[i] + 1);
            return new string(array);
        }

        private void chkShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            txtPasswordVisible.Text = txtPassword.Password;
            txtPasswordVisible.Visibility = Visibility.Visible;
            txtPassword.Visibility = Visibility.Collapsed;
        }

        private void chkShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            txtPassword.Password = txtPasswordVisible.Text;
            txtPassword.Visibility = Visibility.Visible;
            txtPasswordVisible.Visibility = Visibility.Collapsed;
        }

        private void ToggleMessageControls(bool enabled)
        {
            MessageInput.IsEnabled = enabled;
            foreach (var child in ((Grid)MessageInput.Parent).Children)
            {
                if (child is Button btn)
                    btn.IsEnabled = enabled;
            }
        }

        private void MessageInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MessageInput.Text == "Nachricht Schreiben ...")
                MessageInput.Text = "";
        }

        private void MessageInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MessageInput.Text))
                MessageInput.Text = "Nachricht Schreiben ...";
        }

        private void MessageInput_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }

    // Nachrichten-Klasse Test
    public class ChatMessage
    {
        public string User { get; set; }
        public string Text { get; set; }
    }
}
