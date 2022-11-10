using INDELAPPEnd.Helpers;
using System;
using Xamarin.Forms;

namespace INDELLAPPEnd.Models
{
    public class RequestStatus : BaseItemClass
    {
        public ObjectRequestStatus StatusCode { get; set; }
        public String Status { get; set; }
    }

    public class BaseObjectClass : BaseItemClass
    {
        public Color ColorOfText
        {
            get
            {
                switch (Convert.ToInt32(LastRequestStatus.StatusCode))
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
                return default;
            }
        }

        public string ActiveText
        {
            get
            {
                switch (Active)
                {
                    case (false):
                        return "Не активен";
                    case (true):
                        return "Активен";
                }
                return default;
            }
        }
        public string LastRequestStatusText
        {
            get
            {
                switch (Convert.ToInt32(LastRequestStatus.StatusCode))
                {
                    //Не опрашивался
                    case (0):
                        return "Не опрашивался";
                    //Опрашивается
                    case (1):
                        return "Опрашивается";
                    //Успешный опрос
                    case (2):
                        return "Успешный опрос";
                    //Ошибка связи
                    case (3):
                        return "Ошибка связи";
                    //Нет связи
                    case (4):
                        return "Нет связи";
                    //Номер занят
                    case (5):
                        return "Номер занят";
                    //Таймаут
                    case (6):
                        return "Таймаут";
                    //Ошибка данных
                    case (7):
                        return "Ошибка данных";
                }
                return default;
            }
        }

        public string IconPath
        {
            get
            {
                switch (Convert.ToInt32(LastRequestStatus.StatusCode))
                {
                    case (0):
                        return "NoPoll.png";
                    case (1):
                        return "pollObjectIcon.gif";
                    case (2):
                        return "ok.png";
                    case (3):
                        return "ConnectionError.png";
                    case (6):
                        return "TimeOut.png";
                    case (7):
                        return "DataError.png";
                }
                return default;
            }
        }

        public BaseItemClass Location { get; set; }

        public string LastRequestDateString
        {
            get
            {
                return LastRequestDate.ToString("dd.MM.yyyy HH:mm:ss");
            }
        }
        public DateTime LastRequestDate { get; set; }
        public RequestStatus LastRequestStatus { get; set; }
        //описание контроллера
        public DeviceClass Device { get; set; }
        public bool HasUnactiveDevice { get; set; }
        //вид объекта
        public EntityClass Kind { get; set; }
    }
}