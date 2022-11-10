using System;

namespace INDELLAPPEnd.Models
{
    public class CommonObjectClass : BaseObjectClass
    {
        //группа объекта
        public BaseItemClass Group { get; set; }
        //улица
        public BaseItemClass Street { get; set; }
        //номер дома
        public String House { get; set; }
        //корпус
        public String Building { get; set; }
        //потребитель
        public ConsumerClass Consumer { get; set; }
        //код дома
        public String HouseCode { get; set; }
        //дополнительная информация
        public AdditionalInfoClass AdditionalInfo { get; set; }

        public CommonObjectClass()
        {
            this.Kind = new EntityClass();
            this.Device = new DeviceClass();
            this.Consumer = new ConsumerClass();
            this.AdditionalInfo = new AdditionalInfoClass();
        }
    }
}