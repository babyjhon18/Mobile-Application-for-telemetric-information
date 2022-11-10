using System;
using System.Collections.Generic;

namespace INDELLAPPEnd.Models
{
    public class UserAccountClass : SimpleUserAccountClass
    {
        private Boolean IsAdmin;

        public UserAccountClass(Boolean Admin = false)
        {
            this.AccessRoutes = new List<String>();
            this.Locations = new Dictionary<int, BaseItemClass>();
            this.Actions = new Dictionary<int, BaseItemClass>();
            this.Reports = new Dictionary<String, ReportClass>();
            this.Group = new BaseItemClass();
            this.Role = new BaseItemClass();
            this.IsAdmin = Admin;
        }
        public String Password { get; set; }
        //признак того, что пользователь администратор
        new public Boolean Admin { get { return this.IsAdmin; } }
        //клиентский IP
        public String ClientAddress { get; set; }
        //список доступных для пользователя роутов ввиде строк Controller.Action
        public List<String> AccessRoutes { get; set; }
        //группа пользователя
        public BaseItemClass Group { get; set; }
        //роль пользователя
        public BaseItemClass Role { get; set; }
        //список локаций, доступных пользователю
        public Dictionary<int, BaseItemClass> Locations { get; set; }
        //список доступных для пользователя действий
        public Dictionary<int, BaseItemClass> Actions { get; set; }
        //список доступных для пользователя отчетов
        public Dictionary<String, ReportClass> Reports { get; set; }
    }
}