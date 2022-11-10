using INDELAPPEnd.DataViewModels.Entities;

namespace INDELAPPEnd.DataViewModels
{
    public static class AppRepository
    {
        static AppRepository()
        {
            Region = new RegionItem();
            User = new UserItem();
            Location = new LocationItem();
            Counter = new CounterItem();
            Object = new ObjectItem();
            Data = new DataItem();
            Map = new MapItem();
            Schedule = new ScheduleItem();
            System = new SystemItem();
        }
        public static IEntityItem Region { get; }
        public static IEntityItem Location { get; }
        public static IEntityItem Object { get; }
        public static IEntityItem Counter { get; }
        public static IEntityItem User { get; }
        public static IBaseEntityItem Data { get; }
        public static IBaseEntityItem Map { get; }
        public static IEntityItem Schedule { get; }
        public static IBaseEntityItem System { get; }
    }
}
