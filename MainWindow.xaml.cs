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
        public MainWindow()
        {
            InitializeComponent();
        }

        static int inverseMod(int encryptKey, int symbolLength)
        {
            int length = symbolLength, v = 0, d = 1;
            while (encryptKey > 0)
            {
                int t = length / encryptKey, x = encryptKey;
                encryptKey = length % x;
                length = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= symbolLength;
            if (v < 0) v = (v + symbolLength) % symbolLength;
            return v;
        }

        public static string encrypt(string input, int encryptKey)
        {
            string translation = string.Empty;

            char[] chars = input.ToUpper().ToCharArray();

            foreach (char letter in input)
            {
                int index = Convert.ToInt32(letter - 65);
                translation += Convert.ToChar(((index * encryptKey) % 26) + 65);
            }
            return translation;
        }

        public static string decrypt(string input, int encryptKey)
        {
            string translation = string.Empty;
            int decryptKey = inverseMod(encryptKey, 26);

            char[] chars = input.ToUpper().ToCharArray();

            foreach (char letter in input)
            {
                int index = Convert.ToInt32(letter - 65);
                if (index < 0) index = Convert.ToInt32(index) + 26;
                translation += Convert.ToChar(((decryptKey * index) % 26) + 65);
            }
            return translation;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button tempButton = e.OriginalSource as Button;

            if(tempButton.Name == "encrypt_click")
            {
                StartUp.Visibility = Visibility.Hidden;
                UserEntry.Visibility = Visibility.Visible;
            }
            else if(tempButton.Name == "decrypt_click")
            {
                StartUp.Visibility = Visibility.Hidden;
                UserEntry.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            Button tempButton = e.OriginalSource as Button;

            if(tempButton.Name == "submit")
            {
                UserEntry.Visibility = Visibility.Hidden;
                Cipher.Visibility = Visibility.Visible;
            }
        }
    }
}
