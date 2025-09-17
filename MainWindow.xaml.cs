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



namespace LEA_Nermin.Alajlani_Stanislav_Kharchenko
{
    public partial class MainWindow : Window
    {
        private bool isLoggedIn = false;   // merkt sich, ob Login erfolgt ist
        private string savedUser = "";     // Benutzername speichern
        private string savedPassword = ""; // Passwort speichern

        public MainWindow()
        {
            InitializeComponent();
            ToggleMessageControls(false); // Nachrichteneingabe & Buttons sperren
        }

        // Login Button
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

                // Hier könntest du echte Prüfung einbauen (DB, Datei, etc.)
                // Wir nehmen erstmal: Login akzeptieren & speichern
                savedUser = user;
                savedPassword = password;
                isLoggedIn = true;

                MessageBox.Show($"Willkommen {savedUser}!");
                BtnLogin.Content = "Abmelden";

                ToggleMessageControls(true); // Eingaben freigeben
            }
            else
            {
                // Logout
                isLoggedIn = false;
                BtnLogin.Content = "Anmelden";
                MessageBox.Show("Du wurdest abgemeldet.");

                ToggleMessageControls(false); // Eingaben sperren
            }
        }

        // Nachrichten senden
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (!isLoggedIn)
            {
                MessageBox.Show("Bitte zuerst einloggen!");
                return;
            }

            string message = MessageInput.Text;
            if (!string.IsNullOrWhiteSpace(message))
            {
                txtOutput.AppendText($"{savedUser}: {message}\n");
                MessageInput.Clear();
            }
        }

        // Geheim senden (z.B. einfach Text verschlüsseln -> hier Dummy)
        private void BtnSecretSend_Click(object sender, RoutedEventArgs e)
        {
            if (!isLoggedIn)
            {
                MessageBox.Show("Bitte zuerst einloggen!");
                return;
            }

            string message = MessageInput.Text;
            if (!string.IsNullOrWhiteSpace(message))
            {
                string geheim = ConvertToSecret(message);
                txtOutput.AppendText($"{savedUser} (geheim): {geheim}\n");
                MessageInput.Clear();
            }
        }

        // Dummy Verschlüsselung (kannst du anpassen)
        private string ConvertToSecret(string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
                array[i] = (char)(array[i] + 1); // einfacher Caesar-Shift
            return new string(array);
        }

        // Passwort anzeigen/ausblenden
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

        // Eingabefelder sperren/freigeben
        private void ToggleMessageControls(bool enabled)
        {
            MessageInput.IsEnabled = enabled;
            foreach (var child in ((Grid)MessageInput.Parent).Children)
            {
                if (child is Button btn)
                    btn.IsEnabled = enabled;
            }
        }

        // Placeholder-Logik für MessageInput
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
            // Hier könntest du Live-Validierung machen, aktuell nicht nötig
        }
    }
}
