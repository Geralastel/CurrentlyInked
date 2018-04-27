namespace CurrentlyInked
{
    public class InputUpdateEventArgs : System.EventArgs
    {
        // add local member variables to hold text
        private string input, input2, input3;

        public InputUpdateEventArgs(string input)
        {
            this.input = input;
        }

        public InputUpdateEventArgs(string input, string input2, string input3)
        {
            this.input = input;
            this.input2 = input2;
            this.input3 = input3;
        }

        public string Input
        {
            get
            {
                return input;
            }
        }

        public string Input2
        {
            get
            {
                return input2;
            }
        }
        public string Input3
        {
            get
            {
                return input3;
            }
        }
    }
}