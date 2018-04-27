using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CurrentlyInked
{
    /// <summary>
    /// Interaction logic for InputPrompt.xaml
    /// </summary>
    public partial class InputPrompt : Window
    {

        //add a delegate
        public delegate void InputUpdateHandler(object sender, InputUpdateEventArgs e);

        //add an event of the delegate type
        public event InputUpdateHandler InputUpdated;

        string textDefault = "Please enter value.";
        public InputPrompt()
        {
            InitializeComponent();
        }

        private void ButtonClick_Done(object sender, RoutedEventArgs e)
        {
            string inptValue;
            //read the textbox and set the member variable
            if (Value.Text == textDefault || Value.Text.Length == 0)
            {
                MessageBox.Show("Please fill in the information", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                inptValue = Value.Text;
                // instance the event args and pass it the value
                InputUpdateEventArgs args = new InputUpdateEventArgs(inptValue);

                //raise the even with the updated argument

                InputUpdated(this, args);
                this.Close();
            }
        }
        private void ButtonClick_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = tb.Text == textDefault ? string.Empty : tb.Text;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = tb.Text == string.Empty ? textDefault : tb.Text;
        }
    }
}
