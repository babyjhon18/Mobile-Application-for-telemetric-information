using System;
using System.Collections.Generic;

namespace INDELLAPPEnd.Models
{
    //базовый класс модели для справочника предприятий
    public class ContactClass : BaseItemClass
    {
        public ContactClass()
        {
        }

        //руководитель
        public String HeadPerson { get; set; }
        //ответственное лицо
        public String ResponsiblePerson { get; set; }
        //юридичесий адрес
        public String LegalAddress { get; set; }
        //почтовый адрес
        public String PostAddress { get; set; }
        //телефон
        public String PhoneNumber { get; set; }
        //факс
        public String FaxNumber { get; set; }
    }

    //класс модели для определения иерархии предприятий
    public class ParentedContactClass : ContactClass
    {
        public ParentedContactClass()
        {
            Branches = new List<ParentedContactClass>();
            Parent = new EntityClass();
        }

        //ссылка на головную организацию
        public EntityClass Parent { get; set; }
        //список подчиненных организаций
        public List<ParentedContactClass> Branches { get; set; }
    }

    //класс модели потребитель
    public class ConsumerClass : ParentedContactClass
    {
        public ConsumerClass()
        {
            Consumer = new EntityClass();
        }

        public ConsumerClass(int ConsumerID)
        {
            Consumer = new EntityClass() { ID = ConsumerID };
        }

        //ссылка на потребителя
        public EntityClass Consumer { get; set; }
        //ссылка на пользователя в системе
        public UserAccountClass User { get; set; }
    }

    //класс модели поставщик (снабжающая организация)
    public class SupplierClass : ParentedContactClass
    {
        public SupplierClass()
        {
            Supplier = new EntityClass();
        }

        public SupplierClass(int SupplierID)
        {
            Supplier = new EntityClass() { ID = SupplierID };
        }

        //ссылка на поставщика
        public EntityClass Supplier { get; set; }
    }
}