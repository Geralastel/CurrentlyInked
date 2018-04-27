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
    /// Interaction logic for AddPen.xaml
    /// </summary>
    public partial class AddPen : Window
    {
        string modelTextDefault = "Please enter model name";
        string brandFileName = "penbrands.txt";
        string nibWidthFileName = "nibwidths.txt";

        //add a delegate
        public delegate void InputUpdateHandler(object sender, InputUpdateEventArgs e);

        //add an event of the delegate type
        public event InputUpdateHandler PenListUpdated;

        public AddPen()
        {
            InitializeComponent();
            PopulateBrandNameList();
            PopulateNibWidthList();
        }

        private void ButtonClick_Done(object sender, RoutedEventArgs e)
        {
            if(BrandNames.Text.Length == 0 || ModelName.Text == modelTextDefault || ModelName.Text.Length == 0 || NibWidths.Text.Length == 0)
            {
                MessageBox.Show("Please fill out all pen details", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                string brandVal, modelVal, nibWidthVal;

                //read the forum and set the member variables
                brandVal = BrandNames.Text;
                modelVal = ModelName.Text;
                nibWidthVal = NibWidths.Text;

                // instance the event args and pass it the values
                InputUpdateEventArgs args = new InputUpdateEventArgs(brandVal, modelVal, nibWidthVal);

                //raise the event with the updated arguments

                PenListUpdated(this, args);
                this.Close();
            }   
        }

        private void ButtonClick_AddNewBrand(object sender, RoutedEventArgs e)
        {
            InputPrompt f = new InputPrompt();

            // Add an event handler to update this form
            // When the brand window is updated (when InputUpdated fires)
            f.InputUpdated += new InputPrompt.InputUpdateHandler(InputPrompt_ButtonClicked_PenBrand);
            f.ShowDialog();
        }

        // handles the event from InputPrompt
        private void InputPrompt_ButtonClicked_PenBrand(object sender, InputUpdateEventArgs e)
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

        private void ButtonClick_AddNewNibWidth(object sender, RoutedEventArgs e)
        {
            InputPrompt f = new InputPrompt();

            // Add an event handler to update this form
            // When the brand for mis updated (when InputUpdated fires)
            f.InputUpdated += new InputPrompt.InputUpdateHandler(InputPrompt_ButtonClicked_NibWidth);

            f.ShowDialog();
        }

        private void InputPrompt_ButtonClicked_NibWidth(object sender, InputUpdateEventArgs e)
        {
            // update the forms values from the event args
            if (e.Input != null)
            {
                string[] lines = System.IO.File.ReadAllLines(nibWidthFileName);
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
                    NibWidths.Items.Add(e.Input);
                    //Write to file to save new brand
                    using (StreamWriter w = File.AppendText(nibWidthFileName))
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
        private void ButtonClick_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = tb.Text == modelTextDefault ? string.Empty : tb.Text;
        }

       

        private void PopulateBrandNameList()
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

        private void PopulateNibWidthList()
        {
            string[] lines = System.IO.File.ReadAllLines(nibWidthFileName);
            foreach (string line in lines)
            {
                NibWidths.Items.Add(line);
            }
        }
    }
}
