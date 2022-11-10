using INDELAPPEnd.Helpers;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace INDELAPPEnd.Models
{
    public class LastDataByObject
    {
        public int ObjectID { get; set; }
        public List<CounterListDataPoint> ListOfCounters { get; set; }
        public LastDataByObject()
        {
            ListOfCounters = new List<CounterListDataPoint>();
        }
    }

    public class CounterListDataPoint
    {
        public int CounterID { get; set; }
        public List<ListDataPoint> Data { get; set; }
        public CounterListDataPoint()
        {
            Data = new List<ListDataPoint>();
        }
    }

    public class ListDataPoint
    {
        public string ArchiveTypeString
        {
            get
            {
                switch (ArchiveType)
                {
                    case (ArchiveType.atCurrent):
                        return "Текущие";
                    case (ArchiveType.atDay):
                        return "Суточные";
                    case (ArchiveType.atHour):
                        return "Часовые";
                    case (ArchiveType.atMinute):
                        return "Минутные";
                }
                return default;
            }
        }
        public ArchiveType ArchiveType { get; set; }
        public List<DataPointClass> Points { get; set; }
        public ListDataPoint()
        {
            Points = new List<DataPointClass>();
        }
    }

    public class DataPointClass
    {
        public string TimeStampString
        {
            get
            {
                return TimeStamp.ToString("dd.MM.yyyy HH:mm:ss");
            }
        }
        public DateTime TimeStamp { get; set; }
        public List<FieldsClass> Fields { get; set; }
        public DataPointClass()
        {
            Fields = new List<FieldsClass>();
        }
    }

    public class FieldsClass
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }

        private Color color;
        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
            }
        }
    }


}
