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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrentlyInked
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string penListTxt = "penlist.txt";
        string inkListTxt = "inklist.txt";
        List<FountainPen> penList = new List<FountainPen>();
        List<Ink> inkList = new List<Ink>();

        public MainWindow()
        {
            InitializeComponent();
            PopulatePenList();
            PopulateInkList();

        }


        private void PensListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InkListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonClick_Sort(object sender, RoutedEventArgs e)
        {
            //Sort pens and ink button
        }

        private void ButtonClick_AddPen(object sender, RoutedEventArgs e)
        {
            AddPen f = new AddPen();

            //Add an event handler to update this form when the pen window is updated
            f.PenListUpdated += new AddPen.InputUpdateHandler(AddPen_ButtonClicked);
            f.ShowDialog();
        }

        private void AddPen_ButtonClicked(object sender, InputUpdateEventArgs e)
        {
            if (e.Input != null && e.Input2 != null && e.Input3 != null)
            {
                FountainPen pen = new FountainPen(e.Input, e.Input2, e.Input3);
                penList.Add(pen);

                //Add pen to penlist.txt
                using (StreamWriter w = File.AppendText(penListTxt))
                {
                    w.WriteLine(pen.ToTxt());
                }
                Console.WriteLine("{0} has been added", pen.Name);
            }

            PensListBox.Items.Refresh();
        }

        private void ButtonClick_AddInk(object sender, RoutedEventArgs e)
        {
            //Add ink button
            AddInk f = new AddInk();

            //Add an event handler to update this form when the pen window is updated
            f.InkListUpdated += new AddInk.InputUpdateHandler(AddInk_ButtonClicked);
            f.ShowDialog();

        }

        private void AddInk_ButtonClicked(object sender, InputUpdateEventArgs e)
        {
            if (e.Input != null && e.Input2 != null && e.Input3 != null)
            {
                Ink ink = new Ink(e.Input, e.Input2, e.Input3);
                inkList.Add(ink);

                //Add pen to penlist.txt
                using (StreamWriter w = File.AppendText(inkListTxt))
                {
                    w.WriteLine(ink.ToTxt());
                }
                Console.WriteLine("{0} has been added", ink.InkName);
            }

            InkListBox.Items.Refresh();
        }

        private void ButtonClick_Clear(object sender, RoutedEventArgs e)
        {
            if(InkListBox.SelectedIndex != -1)
            {
                InkListBox.SelectedIndex = -1;
            }
            if(PensListBox.SelectedIndex != -1)
            {
                PensListBox.SelectedIndex = -1;
            }
        }

        private void ButtonClick_Remove(object sender, RoutedEventArgs e)
        {
            //Remove pen or ink button

            //checks if an item is selected in the inklistbox
            if (InkListBox.SelectedIndex != -1)
            {
                if (InkListBox.SelectedIndex > -1)
                {
                    string line = null;
                    string inkInfo = inkList[InkListBox.SelectedIndex].ToTxt();
                    string tempFile = System.IO.Path.GetTempFileName();

                    //reads inklist file, writes to a temp file, deletes original file then moves temp file to original file location
                    using (StreamReader reader = new StreamReader(inkListTxt))
                    {
                        using (StreamWriter writer = new StreamWriter(tempFile))
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (String.Compare(line, inkInfo) == 0)
                                {
                                    Console.WriteLine("Comparing {0} and {1}", line, inkInfo);
                                    continue;
                                }

                                writer.WriteLine(line);
                            }
                        }
                    }

                    File.Delete(inkListTxt);
                    File.Move(tempFile, inkListTxt);

                    inkList.RemoveAt(InkListBox.SelectedIndex);
                    InkListBox.Items.Refresh();
                }
            }
            if (PensListBox.SelectedIndex != -1)
            {
                if (PensListBox.SelectedIndex > -1)
                {
                    string line = null;
                    string penInfo = penList[PensListBox.SelectedIndex].ToTxt();
                    string tempFile = System.IO.Path.GetTempFileName();

                    //reads inklist file, writes to a temp file, deletes original file then moves temp file to original file location
                    using (StreamReader reader = new StreamReader(penListTxt))
                    {
                        using (StreamWriter writer = new StreamWriter(tempFile))
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (String.Compare(line, penInfo) == 0)
                                {
                                    Console.WriteLine("Comparing {0} and {1}", line, penInfo);
                                    continue;
                                }

                                writer.WriteLine(line);
                            }
                        }
                    }

                    File.Delete(penListTxt);
                    File.Move(tempFile, penListTxt);
                    penList.RemoveAt(PensListBox.SelectedIndex);
                    PensListBox.Items.Refresh();
                }
            }
        }

        private void ButtonClick_AddToCurrentlyInked(object sender, RoutedEventArgs e)
        {
            //Add pen and ink to current rotation
        }

        private void ButtonClick_Done(object sender, RoutedEventArgs e)
        {
            //Done button, return to main menu
        }

        private void PensListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PopulatePenList()
        {
            string[] lines = System.IO.File.ReadAllLines(penListTxt);
            List<string> sortedList = new List<string>();

            foreach (string line in lines)
            {
                sortedList.Add(line);
            }

            sortedList.Sort();

            foreach (var line in sortedList)
            {
                String[] substrings = line.Split('*');
                string brand, model, nib;
                DateTime time;
                brand = substrings[0];
                model = substrings[1];
                nib = substrings[2];
                time = Convert.ToDateTime(substrings[3]);
                penList.Add(new FountainPen(brand, model, nib, time));
            }

            PensListBox.ItemsSource = penList;
        }

        private void PopulateInkList()
        {
            string[] lines = System.IO.File.ReadAllLines(inkListTxt);
            List<string> sortedList = new List<string>();

            foreach (string line in lines)
            {
                sortedList.Add(line);
            }

            sortedList.Sort();

            foreach (var line in sortedList)
            {
                string[] substrings = line.Split('*');
                string brand, name, colour;
                DateTime time;
                brand = substrings[0];
                name = substrings[1];
                colour = substrings[2];
                time = Convert.ToDateTime(substrings[3]);

                inkList.Add(new Ink(brand, name, colour, time));
            }

            InkListBox.ItemsSource = inkList;
        }

        public string GetSubstringByString(string a, string b, string c)
        {
            return c.Substring((c.IndexOf(a) + a.Length), (c.IndexOf(b) - c.IndexOf(a) - a.Length));
        }

        private void PensListBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
