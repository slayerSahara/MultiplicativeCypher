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

namespace MultiplicativeCypher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int encryptKey;
        public bool cipherType = true;
        public MainWindow()
        {
            InitializeComponent();
            letters();
        }

        public static Dictionary<char, int> alphabet;

        public void letters()
        {
            alphabet = new Dictionary<char, int>();
            char c = 'a';
            alphabet.Add(c, 0);

            for (int i = 1; i < 26; i++)
            {
                alphabet.Add(++c, i);
            }
        }

        internal static int GetAlphabetPosition(int textPosition, int keyPosition, string mode)
        {
            int result = 0;

            switch (mode)
            {
                case "encrypt":
                    result = textPosition * keyPosition % 26;
                    break;
                case "decrypt":
                    result = textPosition % 26 * keyPosition;

                    if (result < 0)
                    {
                        result = (result * (0 - 1)) % 26;
                    }

                    break;
            }

            return result;
        }

        static int inverseMod(int encryptKey)
        {
            int inverse = 0;

            switch (encryptKey)
            {
                case 1:
                    inverse = 25;
                    break;
                case 25:
                    inverse = 1;
                    break;
                case 3:
                    inverse = 9;
                    break;
                case 9:
                    inverse = 3;
                    break;
                case 5:
                    inverse = 21;
                    break;
                case 21:
                    inverse = 5;
                    break;
                case 7:
                    inverse = 15;
                    break;
                case 15:
                    inverse = 7;
                    break;
                case 11:
                    inverse = 19;
                    break;
                case 19:
                    inverse = 11;
                    break;
                case 17:
                    inverse = 23;
                    break;
                case 23:
                    inverse = 17;
                    break;
            }
            return inverse;
        }

        public static string encrypt(string input, int encryptKey, string mode)
        {
            string translation = string.Empty;

            foreach (char letter in input)
            {
                var charposition = alphabet[letter];
                var res = GetAlphabetPosition(charposition, encryptKey, mode);
                translation += alphabet.Keys.ElementAt(res % 26);
            }
            return translation;
        }

        public static string decrypt(string input, int encryptKey, string mode)
        {
            string translation = string.Empty;
            int decryptKey = inverseMod(encryptKey);

            foreach (char letter in input)
            {
                var charposition = alphabet[letter];
                var res = GetAlphabetPosition(charposition, decryptKey, mode);
                translation += alphabet.Keys.ElementAt(res % 26);
            }
            return translation;
        }

        private void num_click(object sender, RoutedEventArgs e)
        {
            string test = "";
            MenuItem key = e.OriginalSource as MenuItem;

            test = (string)key.Header;
            encryptKey = Int32.Parse(test);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button tempButton = e.OriginalSource as Button;

            if(tempButton.Name == "encrypt_click")
            {
                StartUp.Visibility = Visibility.Hidden;
                UserEntry.Visibility = Visibility.Visible;

                cipherType = true;
            }
            else if(tempButton.Name == "decrypt_click")
            {
                StartUp.Visibility = Visibility.Hidden;
                UserEntry.Visibility = Visibility.Visible;

                cipherType = false;
            }
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            Button tempButton = e.OriginalSource as Button;

            if(tempButton.Name == "submit")
            {
                UserEntry.Visibility = Visibility.Hidden;
                Cipher.Visibility = Visibility.Visible;

                if (cipherType)
                {
                    message.Content += encrypt(input.Text.ToString(), encryptKey, "encrypt");
                }
                else
                {
                    message.Content += decrypt(input.Text.ToString(), encryptKey, "decrypt");
                }
            }
        }
    }
}
