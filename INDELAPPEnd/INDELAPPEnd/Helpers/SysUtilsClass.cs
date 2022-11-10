namespace INDELAPPEnd.Helpers
{
    //статус задания
    public enum ScheduleStatus
    {
        ssNone = 0,
        ssInProgress = 1,
        ssDone = 2
    }

    //тип задания
    public enum ScheduleType
    {
        stReadCurrent = 0,
        stReadArchive = 1,
        stReadAlarms = 2,
        stReadEvents = 3,
        stReadParams = 4,
        stWriteParams = 5
    }
    public enum ArchiveType
    {
        atCurrent = 0,
        atDay = 1,
        atHour = 2,
        atMinute = 3
    }
    public enum SearchType
    {
        stObjectByName = 0, //по имени
        stObjectByRTU = 1, //по номеру РТУ
        stObjectByCounterSerialNumber = 2, //по номеру прибора учета
        stObjectByTelephoneNumber = 3, //по номеру телефона
        stObjectByIP = 4, //по IP
        stConsumerByName = 5 //потребитель по имени
    }
    public enum ObjectRequestStatus
    {
        ossNone = 0,
        ossInProgress = 1,
        ossSuccessful = 2,
        ossLinkError = 3,
        ossNoCarier = 4,
        ossBusy = 5,
        ossTimeout = 6,
        ossErrorData = 7
    }

    public class Links
    {
        static string GetServerString()
        {
            if (Settings.CurrentServer != "" && Settings.CurrentPort != "")
                return Settings.CurrentServer + ":" + Settings.CurrentPort;
            else
                return Settings.CurrentServer;
        }
        //Login
        public static string APILogin => $"http://{GetServerString()}/api/Login";
        //Object 
        public static string APIObjectGet => $"http://{GetServerString()}/api/Object";
        public static string APIObjectCreate => $"http://{GetServerString()}/api/Object";
        public static string APIObjectUpdate => $"http://{GetServerString()}/api/Object";
        public static string APIObjectDelete => $"http://{GetServerString()}/api/Object";
        public static string APIObjectClone => $"http://{GetServerString()}/api/Object/Clone";
        //Location 
        public static string APILocationGet => $"http://{GetServerString()}/api/Location";
        public static string APILocationCreate => $"http://{GetServerString()}/api/Location";
        public static string APILocationUpdate => $"http://{GetServerString()}/api/Location";
        public static string APILocationDelete => $"http://{GetServerString()}/api/Location";
        //Region 
        public static string APIRegionGet => $"http://{GetServerString()}/api/Region";
        public static string APIRegionCreate => $"http://{GetServerString()}/api/Region";
        public static string APIRegionUpdate => $"http://{GetServerString()}/api/Region";
        public static string APIRegionDelete => $"http://{GetServerString()}/api/Region";
        //Counter  
        public static string APICounterGet => $"http://{GetServerString()}/api/Counter";
        public static string APICounterCreate => $"http://{GetServerString()}/api/Counter";
        public static string APICounterUpdate => $"http://{GetServerString()}/api/Counter";
        public static string APICounterDelete => $"http://{GetServerString()}/api/Counter";
        public static string APICounterClone => $"http://{GetServerString()}/api/Counter/Clone";
        //Data
        public static string APIGetCurrentData => $"http://{GetServerString()}/api/Data/Current";
        public static string APIGetArchiveData => $"http://{GetServerString()}/api/Data/Archive";
        public static string APIGetLastData => $"http://{GetServerString()}/api/Data/Last";
        //Report
        public static string APIReportHeatObjectCard => $"http://{GetServerString()}/api/Report/Heat/ObjectCard";
        public static string APIReportHeatLocationHeatCurrent => $"http://{GetServerString()}/api/Report/Heat/LocationHeatCurrent";
        public static string APIReportWaterLocationFlatWaterTotal => $"http://{GetServerString()}/api/Report/Water/LocationFlatWaterTotal";
        //ObjectTree
        public static string APIObjectTreeGet => $"http://{GetServerString()}/api/ObjectTree";
        //System utils
        public static string APISystemSearch => $"http://{GetServerString()}/api/System/Search";
        //Poll requests
        public static string APIScheduleCurrent => $"http://{GetServerString()}/api/ScheduleEngine/ReadCurrent";
        public static string APIScheduleArchive => $"http://{GetServerString()}/api/ScheduleEngine/ReadArchive";
        public static string APIScheduleCheckStatus => $"http://{GetServerString()}/api/ScheduleEngine/CheckScheduleStatus";
        //Map
        public static string APIMap => $"http://{GetServerString()}/api/Map";
    }
}
