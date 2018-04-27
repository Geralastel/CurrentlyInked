using System;

namespace CurrentlyInked
{
    internal class Ink
    {
        string brandName, inkName, colour;
        DateTime lastTimeUsed;

        public Ink()
        {
            brandName = "NULL";
            inkName = "NULL";
            colour = "NULL";
            lastTimeUsed = DateTime.Now;
        }

        public Ink(string brandName, string inkName, string colour)
        {
            this.brandName = brandName;
            this.inkName = inkName;
            this.colour = colour;
            lastTimeUsed = DateTime.Now;
        }

        public Ink(string brandName, string inkName, string colour, DateTime lastTimeUsed)
        {
            this.brandName = brandName;
            this.inkName = inkName;
            this.colour = colour;
            this.lastTimeUsed = lastTimeUsed;
        }

        public string ToTxt()
        {
            return $"{brandName}*{inkName}*{colour}*{lastTimeUsed}";
        }

        public string Name
        {
            get
            {
                return string.Format("{0} {1}", brandName, inkName);
            }
        }

        public string InkName
        {
            get
            {
                return inkName;
            }
        }

        public string Colour
        {
            get
            {
                return colour;
            }
            set
            {
                this.colour = value;
            }
        }

        public string BrandName
        {
            get
            {
                return brandName;
            }
            private set
            {
                this.brandName = value;
            }
        }

        public DateTime LastTimeUsed
        {
            get
            {
                return lastTimeUsed;
            }
            set
            {
                this.lastTimeUsed = value;
            }
        }
    }
}