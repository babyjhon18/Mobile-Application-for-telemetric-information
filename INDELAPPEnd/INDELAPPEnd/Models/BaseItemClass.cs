using System;

namespace INDELLAPPEnd.Models
{
    public class EntityClass
    {
        public EntityClass()
        {
            this.ID = -1;
            this.Name = "";
            this.Category = "";
        }

        public int ID { get; set; }

        //название
        public String Name { get; set; }
        //код
        public String Code { get; set; }
        //категория
        public String Category { get; set; }

    }

    public class BaseItemClass : EntityClass
    {
        public BaseItemClass()
        {
            this.Type = new EntityClass();
            this.Active = true;
        }

        //тип элемента (значение справочника)
        public EntityClass Type { get; set; }
        //описание
        public String Description { get; set; }
        //активность
        public bool Active { get; set; }
    }
}