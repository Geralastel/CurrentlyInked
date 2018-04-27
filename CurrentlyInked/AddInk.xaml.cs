using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddInk : Window
    {
        string inkNameTextDefault = "Please enter the ink name";
        string brandFileName = "inkbrands.txt";
        string inkColourFileName = "inkcolours.txt";

        //add a delegate
        public delegate void InputUpdateHandler(object sender, InputUpdateEventArgs e);

        //add an event of the delegate type
        public event InputUpdateHandler InkListUpdated;

        public AddInk()
        {
            InitializeComponent();
            PopulateBrandList();
            PopulateColourList();
        }

        private void ButtonClick_Done(object sender, RoutedEventArgs e)
        {
            if(BrandNames.Text.Length == 0 || InkName.Text.Length == 0 || InkName.Text == inkNameTextDefault || InkColours.Text.Length == 0)
            {
                MessageBox.Show("Please fill out all the ink details", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                string brandVal, nameVal, colourVal;

                //read the forum and set the member variables
                brandVal = BrandNames.Text;
                nameVal = InkName.Text;
                colourVal = InkColours.Text;

                //instance the event args and apass it the variables
                InputUpdateEventArgs args = new InputUpdateEventArgs(brandVal, nameVal, colourVal);

                //raise the event flag with the updated arguments
                InkListUpdated(this, args);
                this.Close();
            }
        }

        private void ButtonClick_AddNewBrand(object sender, RoutedEventArgs e)
        {
            InputPrompt f = new InputPrompt();

            // Add an event handler to update this form
            // When the brand window is updated (when InputUpdated fires)
            f.InputUpdated += new InputPrompt.InputUpdateHandler(InputPrompt_ButtonClicked_InkBrand);
            f.ShowDialog();
        }

        //handles the event from InputPrompt
        private void InputPrompt_ButtonClicked_InkBrand(object sender, InputUpdateEventArgs e)
        {
            // update the forms values from the event args
            if (e.Input != null)
            {
                string[] lines = System.IO.File.ReadAllLines(brandFileName);
                bool copy = false;

                //read penbrands.txt to see if added brand already exists
                foreach (string line in lines)
                {
                    if (line == e.Input)
                    {
                        copy = true;
                    }
                }

                //if it doesn't exists add to txt file
                if (!copy)
                {
                    BrandNames.Items.Add(e.Input);
                    //Write to file to save new brand

                    using (StreamWriter w = File.AppendText(brandFileName))
                    {
                        w.WriteLine(e.Input);
                    }
                    Console.WriteLine("{0} sucessfully added to text file", e.Input);
                }
                else
                {
                    Console.WriteLine("{0} was not added to text file because it already exists", e.Input);
                }
            }
        }

        private void ButtonClick_AddNewColour(object sender, RoutedEventArgs e)
        {
            InputPrompt f = new InputPrompt();

            // Add an event handler to update this form
            // When the brand window is updated (when InputUpdated fires)
            f.InputUpdated += new InputPrompt.InputUpdateHandler(InputPrompt_ButtonClicked_Colour);
            f.ShowDialog();
        }

        private void InputPrompt_ButtonClicked_Colour(object sender, InputUpdateEventArgs e)
        {
            // update the forms values from the event args
            if (e.Input != null)
            {
                string[] lines = System.IO.File.ReadAllLines(inkColourFileName);
                bool copy = false;

                //read penbrands.txt to see if added brand already exists
                foreach (string line in lines)
                {
                    if (line == e.Input)
                    {
                        copy = true;
                    }
                }

                //if it doesn't exists add to txt file
                if (!copy)
                {
                    InkColours.Items.Add(e.Input);
                    //Write to file to save new brand

                    using (StreamWriter w = File.AppendText(inkColourFileName))
                    {
                        w.WriteLine(e.Input);
                    }
                    Console.WriteLine("{0} sucessfully added to text file", e.Input);
                }
                else
                {
                    Console.WriteLine("{0} was not added to text file because it already exists", e.Input);
                }
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = tb.Text == inkNameTextDefault ? string.Empty : tb.Text;
        }

        private void PopulateBrandList()
        {
            string[] lines = System.IO.File.ReadAllLines(brandFileName);
            List<string> sortedList = new List<string>();
            foreach(string line in lines)
            {
                sortedList.Add(line);
            }

            sortedList.Sort();

            foreach(var line in sortedList)
            {
                BrandNames.Items.Add(line);
            }
        }

        private void PopulateColourList()
        {
            string[] lines = System.IO.File.ReadAllLines(inkColourFileName);
            List<string> sortedList = new List<string>();
            foreach (string line in lines)
            {
                sortedList.Add(line);
            }

            sortedList.Sort();

            foreach (var line in sortedList)
            {
                InkColours.Items.Add(line);
            }
        }

        private void ButtonClick_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
