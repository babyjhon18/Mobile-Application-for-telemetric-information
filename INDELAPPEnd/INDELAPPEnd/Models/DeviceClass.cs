using System;
using System.Collections.Generic;

namespace INDELLAPPEnd.Models
{
    public enum DeviceConnectionType
    {
        dctDirect = 0,
        dctModem = 1,
        dctTCP_IP = 2,
        dctIndelOPCServer = 3,
        dctTCP_IP_Indel = 4,
        dctAXModem = 5,
        dctIndelProtocolOld = 6,
        dctIEC104 = 7,
        dctUDP = 9,
        dctIEC104_UDP = 10
    }

    public class DeviceConnectionClass : BaseItemClass
    {
        //тип используемого канала связи
        public new DeviceConnectionType Type { get; set; }
        //строка соединения (номер телефона, IP-адрес etc)
        //public String ConnectionString { get; set; }
        public string TypeString
        {
            get
            {
                switch (Type)
                {
                    case DeviceConnectionType.dctIndelOPCServer:
                        return "Indel OPC Server";
                    case DeviceConnectionType.dctIEC104_UDP:
                        return "MEK-104/UDP";
                    case DeviceConnectionType.dctAXModem:
                        return "Modem (AX Socket)";
                    case DeviceConnectionType.dctTCP_IP:
                        return "TCP/IP";
                    case DeviceConnectionType.dctTCP_IP_Indel:
                        return "TCP/IP (Indel протокол)";
                    case DeviceConnectionType.dctUDP:
                        return "UDP";
                    case DeviceConnectionType.dctIEC104:
                        return "МЭК-104";
                }
                return default;
            }
        }
        //Номер GSM
        public String PhoneNumber { get; set; }
        //Ethernet адрес
        public String IPAddress { get; set; }
        //Удалённый порт
        public String IPPort { get; set; }
        //Локальный порт
        public String LocalPort { get; set; }
    }

    public class DeviceClass : BaseItemClass
    {
        public DeviceClass()
        {
            this.RTU = 1;
            this.Connection = new DeviceConnectionClass();
            this.Counters = new List<CounterClass>();
        }

        //описание используемого канала связи
        public DeviceConnectionClass Connection { get; set; }
        //номер рту
        public int RTU { get; set; }
        //версия контроллера        
        public String Version { get; set; }
        //список счетчиков, подключенных к контоллеру
        public List<CounterClass> Counters { get; set; }
        //синхронизировать время при опросе
        public Boolean SyncTime { get; set; }
    }
}