using System;

namespace CurrentlyInked
{
    internal class FountainPen
    {
        string brandName, modelName, nibWidth;
        DateTime lastTimeUsed;

        public FountainPen()
        {
            brandName = "NULL";
            modelName = "NULL";
            nibWidth = "NULL";
            lastTimeUsed = DateTime.Now;
        }
        public FountainPen(string brandName, string modelName, string nibWidth)
        {
            this.brandName = brandName;
            this.modelName = modelName;
            this.nibWidth = nibWidth;
            lastTimeUsed = DateTime.Now;
        }

        public FountainPen(string brandName, string modelName, string nibWidth, DateTime lastTimeUsed)
        {
            this.brandName = brandName;
            this.modelName = modelName;
            this.nibWidth = nibWidth;
            this.lastTimeUsed = lastTimeUsed;
        }
        public string ToTxt()
        {
            return $"{brandName}*{modelName}*{nibWidth}*{lastTimeUsed}";
        }

        public string Name
        {
            get
            {
                return string.Format("{0} {1}", brandName, modelName, nibWidth);
            }
        }


        public string BrandName
        {
            get
            {
                return brandName;
            }

            set
            {
                this.brandName = value;
            }
        }



        public string ModelName
        {
            get
            {
                return modelName;
            }
            set
            {
                this.modelName = value;
            }
        }

        public string NibWidth
        {
            get
            {
                return nibWidth;
            }
            set
            {
                this.nibWidth = value;
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

