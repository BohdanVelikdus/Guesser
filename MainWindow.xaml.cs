using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
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

namespace Guesser
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

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!validate(sender, e.Key))
            {
                e.Handled = true;
            }
            
        }

        private bool validate(object sender,Key key)
        {
            if ((Key.D0 <= key && key <= Key.D9) || (Key.NumPad0 <= key && key <= Key.NumPad9))
            {
                return true;
            }
            else if (Key.OemMinus == key || Key.Subtract == key)
            {
                if ((sender as TextBox).Text.Length == 0)
                {
                    return true;
                }
                return false;
            }
            if (Key.Space == key)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb =  sender as TextBox;
            tb.Text = tb.Text.Trim();
            tb.SelectionStart = tb.Text.Length;

            // here we will parse the data, and determine the answer
            ReadAndProcessData();
        }

        private void ReadAndProcessData() 
        {
            BigInteger biMin;
            BigInteger biMax;

            String tempNum = MinValueTextBox.Text;
            if (!BigInteger.TryParse(tempNum, out biMin))
            {
                LabelResult.Content = "Unsifficient data for deduction type!";
                return;
            }
            tempNum = MaxValueTextBox.Text;
            if (!BigInteger.TryParse(tempNum, out biMax))
            {
                LabelResult.Content = "Unsifficient data for deduction type!";
                return;
            }
            if (biMax - biMin < 0)
            {
                LabelResult.Content = "The min is greater than max!";
                MaxValueTextBox.Background = new SolidColorBrush(Colors.Red);
                return;
            }
            else
            {
                MaxValueTextBox.Background = new SolidColorBrush(Colors.White);
            }
            string reply = "";

            if (IsIntegralCheckbox.IsChecked ?? false)
            {
                // means here if checked 
                reply = DetermineTypeFloat(biMin, biMax);
            }
            else
            {
                reply = DetermineTypeInt(biMin, biMax);
            }
            LabelResult.Content = reply;
        }

        private string DetermineTypeFloat(BigInteger minNum, BigInteger maxNum)
        {
            if (IsPreciseCheckBox.IsChecked ?? false)
            {
                //mmeans must revise a decimal type
                if (minNum >= (BigInteger)decimal.MinValue && maxNum <= (BigInteger)decimal.MaxValue)
                {
                    return "decimal";
                }
                else
                {
                    return "out-of-bounds type is needed";
                }
            }
            else 
            {
                if (minNum >= (BigInteger)float.MinValue && maxNum <= (BigInteger)float.MaxValue)
                {
                    return "float";
                }
                else if (minNum >= (BigInteger)double.MinValue && maxNum <= (BigInteger)double.MaxValue)
                {
                    return "double";
                }
                else
                {
                    return "out-of-bounds type is needed";
                }
            }
        }

        private string DetermineTypeInt(BigInteger minNum, BigInteger maxNum)
        {
            if (minNum >= sbyte.MinValue && maxNum <= sbyte.MaxValue)
            {
                return "sbyte";
            }
            else if (minNum >= byte.MinValue && maxNum <= byte.MaxValue)
            {
                return "byte";
            }
            else if (minNum >= ushort.MinValue && maxNum <= ushort.MaxValue)
            {
                return "ushort";
            }
            else if (minNum >= short.MinValue && maxNum <= short.MaxValue)
            {
                return "short";
            }
            else if (minNum >= int.MinValue && maxNum <= int.MaxValue)
            {
                return "int";
            }
            else if (minNum >= uint.MinValue && maxNum <= uint.MaxValue)
            {
                return "uint";
            }
            else if (minNum >= long.MinValue && maxNum <= long.MaxValue)
            {
                return "long";
            }
            else if (minNum >= ulong.MinValue && maxNum <= ulong.MaxValue)
            {
                return "ulong";
            }
            else
            {
                return "Maybe BigInteger will help you";
            }
        }

        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Name.Equals("IsIntegralCheckbox"))
            {
                if (checkBox.IsChecked ?? false)
                {
                    IsPreciseCheckBox.Visibility = Visibility.Visible;
                }
                else 
                {
                    IsPreciseCheckBox.Visibility = Visibility.Hidden;
                }
            }
            ReadAndProcessData();
        }
    }
}
