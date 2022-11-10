using System;
using System.Collections.Generic;

namespace INDELLAPPEnd.Models
{
    public class AdditionalInfoClass
    {
        //список изображений
        public List<String> Images { get; set; }
        //список файлов 
        public List<String> Files { get; set; }
        //доп. информация
        public String Data { get; set; }
    }
}