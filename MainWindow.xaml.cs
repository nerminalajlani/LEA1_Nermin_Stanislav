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
using System.IO.Ports;



using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LEA_Nermin.Alajlani_Stanislav_Kharchenko
{
    public partial class MainWindow : Window
    {
        private bool isDecryptMode = false;  // true = Nachricht entschlüsseln, false = Nachricht verschlüsseln
        private bool isLoggedIn = false;   // merkt sich, ob Login erfolgt ist
        private string savedUser = "";     // Benutzername speichern
        private string savedPassword = ""; // Passwort speichern

        SerialPort sp = new SerialPort();
        string[] ports = SerialPort.GetPortNames();

        // Nachrichten-Sammlung (automatisch mit ListBox verbunden)
        public ObservableCollection<ChatMessage> Messages { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ToggleMessageControls(false); // Nachrichteneingabe & Buttons sperren
            COM.ItemsSource = ports;
            sp.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
        }

        private void COM_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (sp.IsOpen)
                    sp.Close();
                sp.PortName = COM.SelectedItem as string;
                sp.BaudRate = 9600;
                sp.Encoding = Encoding.UTF8;
                sp.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Dispatcher.Invoke(() => txtOutput.Text += sp.ReadExisting());
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
                sp.Write(txtUser.Text + ": " + MessageInput.Text + "\n");
                txtOutput.Text += "Sie: " + MessageInput.Text + "\n";
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

            string message = MessageInput.Text.Trim();

            // Wenn etwas eingegeben wurde, dann verschlüsseln und senden
            if (!string.IsNullOrEmpty(message))
            {
                string geheim = Atbash(message);

                if (sp != null && sp.IsOpen)
                {
                    sp.Write($"{txtUser.Text} (geheim): {geheim}\n");
                }

                txtOutput.AppendText($"Sie (geheim): {geheim}\n");
                MessageInput.Clear();
            }
            else
            {
                // Kein neuer Text eingegeben – versuchen, die letzte geheime Nachricht zu entschlüsseln
                var letzteGeheime = txtOutput.Text.Split('\n')
                                        .LastOrDefault(line => line.Contains("(geheim):"));

                if (letzteGeheime == null)
                {
                    MessageBox.Show("Keine geheime Nachricht zum Entschlüsseln gefunden.");
                    return;
                }

                // Extrahiere den verschlüsselten Text
                string verschluesselt = letzteGeheime.Split(new[] { "(geheim):" }, StringSplitOptions.None)[1].Trim();

                // Passwortfenster öffnen
                PasswordPrompt prompt = new PasswordPrompt();
                prompt.Owner = this;

                if (prompt.ShowDialog() == true)
                {
                    string eingegebenesPasswort = prompt.Password;

                    if (eingegebenesPasswort == savedPassword)
                    {
                        string entschluesselt = Atbash(verschluesselt);
                        MessageBox.Show($"🔓 Entschlüsselte Nachricht:\n{entschluesselt}", "Entschlüsselt");
                    }
                    else
                    {
                        MessageBox.Show("❌ Falsches Passwort!", "Zugriff verweigert");
                    }
                }
            }
        }

        // Diese Methode implementiert die Atbash-Verschlüsselung.
        private string Atbash(string input)
        {
            char[] array = input.ToCharArray();

            for (int i = 0; i < array.Length; i++)
            {
                char c = array[i];

                // Buchstaben (Großbuchstaben)
                if (c >= 'A' && c <= 'Z')
                {
                    array[i] = (char)('Z' - (c - 'A'));
                }
                // Buchstaben (Kleinbuchstaben)
                else if (c >= 'a' && c <= 'z')
                {
                    array[i] = (char)('z' - (c - 'a'));
                }
                // Ziffern
                else if (c >= '0' && c <= '9')
                {
                    array[i] = (char)('9' - (c - '0'));
                }
                // Sonstige Zeichen bleiben gleich
            }

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

        // Nachrichten-Klasse Test

        public class ChatMessage
        {
            public string User { get; set; }
            public string Text { get; set; }
        }
    }
}