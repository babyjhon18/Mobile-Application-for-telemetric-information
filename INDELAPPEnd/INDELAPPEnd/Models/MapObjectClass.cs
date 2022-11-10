using INDELLAPPEnd.Models;
using System;
using System.Collections.Generic;

namespace ictweb5.Models
{
    //точка
    public class PointClass
    {
        public Double X { get; set; }
        public Double Y { get; set; }
    }

    //рамка
    public class RectClass
    {
        public PointClass LeftTop { get; set; }
        public PointClass RightBottom { get; set; }
        public PointClass LeftBottom { get; set; }
        public PointClass RightTop { get; set; }
    }

    public class MapObjectClass : CommonObjectClass
    {
        public PointClass XY { get; set; }
        public MapObjectClass()
        {
            XY = new PointClass();
        }
    }

    //модель объекта на карте, имеющий рамку
    public class MapObjectRectClass : BaseObjectClass
    {
        //рамка объекта карты
        public RectClass Rect { get; set; }
    }

    //рамки иерархии регион - место
    public class MapRectClass : MapObjectRectClass
    {
        public MapRectClass()
        {
            this.RectList = new List<MapRectClass>();
        }
        //уровень вложенности иерархии регион - место - объект
        public int Level { get; set; }
        //список рамок далее по иерархии
        public List<MapRectClass> RectList { get; set; }
    }
}