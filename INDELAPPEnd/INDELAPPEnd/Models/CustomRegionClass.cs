using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace INDELLAPPEnd.Models
{
    public class CustomRegionClass
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<CustomLocationClass> Locations { get; set; }

        public CustomRegionClass()
        {
            Locations = new List<CustomLocationClass>();
        }
    }

    public class CustomLocationClass
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<SimpleObjectClass> Objects { get; set; }

        public CustomLocationClass()
        {
            Objects = new List<SimpleObjectClass>();
        }
    }

    public class SimpleObjectClass
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
        public string description { get; set; }
        public DateTime lastRequestDate { get; set; }
        public SimpleDevice Device { get; set; }
        public CustomLastRequestStatus lastRequestStatus { get; set; }
        public Color Color
        {
            get
            {
                if (!active)
                {
                    return Color.FromHex("#8c8c8c");
                }
                else
                {
                    switch (lastRequestStatus.code)
                    {
                        //Не опрашивался
                        case (0):
                            return Color.FromHex("#337ab7");
                        //Опрашивается
                        case (1):
                            return Color.FromHex("#337ab7");
                        //Успешный опрос
                        case (2):
                            return Color.Green;
                        //Ошибка связи
                        case (3):
                            return Color.Maroon;
                        //Нет связи
                        case (4):
                            return Color.Maroon;
                        //Номер занят
                        case (5):
                            return Color.Maroon;
                        //Таймаут
                        case (6):
                            return Color.Purple;
                        //Ошибка данных
                        case (7):
                            return Color.Red;
                    }
                }
                return default;
            }
        }
        public SimpleObjectClass()
        {
            Device = new SimpleDevice();
            lastRequestStatus = new CustomLastRequestStatus();
        }
    }

    public class SimpleDevice
    {
        public int id { get; set; }
        public int rtu { get; set; }
    }

    public class CustomLastRequestStatus
    {
        public string status { get; set; }
        public int code { get; set; }
    }
}
