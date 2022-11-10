using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace INDELLAPPEnd.Models
{
    //модель - физическое подключение прибора учета
    public class CounterPhysicalConnection : BaseItemClass
    {
        public CounterPhysicalConnection()
        {
            Port = 1;
            Speed = 9600;
            Address = 1;
            Channel = 1;
        }

        //физический номер порта
        public Byte Port { get; set; }
        //скорость подключения
        public int Speed { get; set; }
        //адрес
        public int Address { get; set; }
        //пароль
        public String Password { get; set; }
        //тип контроллера, к которому подключен прибор
        public BaseItemClass Device { get; set; }
        //номер канала
        public int Channel { get; set; }
    }

    //модель - шаблон полей для многопоточников и регуляторов
    public class CounterTemplate : BaseItemClass
    {
        //тип учета
        public BaseItemClass Accounting { get; set; }
        //шаблон
        public String Template { get; set; }
    }

    //модель - прибор учета
    public class CounterClass : BaseItemClass
    {
        public int DeviceID { get; set; }
        //номер слота подключения
        public int SlotNumber { get; set; }
        //номер модуля подключения
        public int ModuleNumber { get; set; }
        //серийный номер прибора
        public String SerialNumber { get; set; }
        //регистрационный номер прибора
        public String RegNumber { get; set; }
        //статус опроса прибора
        public String Status { get; set; }
        public Color BackOfFrameOfCounters
        {
            get
            {
                if (Active && Status == "норма")
                    return Color.FromRgba(0, 255, 0, 45);
                else if (Active && Status == "не отвечает")
                    return Color.FromRgba(255, 0, 0, 45);
                else if (Active && Status == "")
                    return Color.FromRgba(0, 0, 0, 0);
                else
                    return Color.FromRgba(207, 207, 207, 45);
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
                return "Не опрашивался";
            }
        }
        //вид установки (тип учета)
        public BaseItemClass Accounting { get; set; }
        //среда учета
        public BaseItemClass Environment { get; set; }
        //контроллер
        public BaseItemClass Device { get; set; }
        //параметры физического подключения прибора
        public CounterPhysicalConnection PhysicalConnection { get; set; }
        //шаблоны полей для прибора
        public List<CounterTemplate> Templates { get; set; }
        //состояние прибора
        public EntityClass State { get; set; }

        public CounterClass()
        {
            SlotNumber = 1;
            ModuleNumber = 1;
            Device = new BaseItemClass();
        }
    }
}