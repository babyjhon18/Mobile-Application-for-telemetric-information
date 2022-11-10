using INDELAPPEnd.Helpers;
using System;


namespace INDELLAPPEnd.Models
{
    //модель данных - задание пользователя на опрос данных
    public class ScheduleClass : BaseItemClass
    {
        public new String ID { get; set; }
        public ScheduleStatus Status { get; set; }
        public new ScheduleType Type { get; set; }
        public DateTime Date { get; set; }
        public String Params { get; set; }
    }

    //параметры задания
    public class ScheduleParamClass
    {
        public ScheduleParamClass()
        {
            this.DayCount = 0;
            this.HourCount = 0;
            this.MinuteCount = 0;
            this.EventCount = 0;
            this.Params = "";
            this.ReadUnSuccessful = false;
        }

        //тип задания
        public ScheduleType Type { get; set; }
        //количество записей суточного архива
        public int DayCount { get; set; }
        //количество записей часового архива
        public int HourCount { get; set; }
        //количество записей минутного архива
        public int MinuteCount { get; set; }
        //количество записей событийного архива (аварии/события)
        public int EventCount { get; set; }
        //строка параметров типа имя-значение
        public String Params { get; set; }
        //переопросить
        public Boolean ReadUnSuccessful { get; set; }
    }
    //параметры журнала
    public class JournalParamClass
    {
        public JournalParamClass()
        {
            this.DateFrom = DateTime.Now;
            this.DateTo = DateTime.Now;
        }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
    //параметры отображения 
    public class ViewParamClass
    {
        public ViewParamClass()
        {
            this.ObjectID = 0;
            this.RedirectID = 0;
            this.Counters = "";
            this.Archive = 0;
        }
        public int ObjectID { get; set; }
        public int RedirectID { get; set; }
        public String Counters { get; set; }
        public int Archive { get; set; }
    }
    //модель данных для представления статуса задания для отдельного объекта
    public class ObjectScheduleClass : BaseItemClass
    {
        //объект
        public BaseItemClass Object { get; set; }
        //ПУ
        public BaseItemClass Counter { get; set; }
        //время создания
        public DateTime CreateTime { get; set; }
        //время запуска
        public DateTime RunTime { get; set; }
        //время окончания
        public DateTime ResultTime { get; set; }
        //статус
        public ObjectRequestStatus Status { get; set; }
        //описание статуса
        new public String Description
        {
            get
            {
                switch (Status)
                {
                    case ObjectRequestStatus.ossNone:
                        return "ожидание";
                    case ObjectRequestStatus.ossInProgress:
                        return "выполнение";
                    case ObjectRequestStatus.ossSuccessful:
                        return "успешно выполнено";
                    case ObjectRequestStatus.ossLinkError:
                        return "ошибка связи";
                    case ObjectRequestStatus.ossNoCarier:
                        return "нет ответа";
                    case ObjectRequestStatus.ossBusy:
                        return "номер занят";
                    case ObjectRequestStatus.ossTimeout:
                        return "таймаут";
                    case ObjectRequestStatus.ossErrorData:
                        return "ошибка данных";
                    default:
                        return "не известно";
                }
            }
        }
    }
}