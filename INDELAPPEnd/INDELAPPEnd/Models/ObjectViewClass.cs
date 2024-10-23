using INDELAPPEnd.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace INDELLAPPEnd.Models
{
    public class ObjectViewClass
    {
        public ObjectViewClass()
        {
            Object = new CommonObjectClass();
            ObjectTree = new ObservableCollection<CustomRegionClass>();
            Streets = new List<BaseItemClass>();
            Groups = new List<BaseItemClass>();
            ConnectionTypes = new List<BaseItemClass>();
            Types = new List<ObjectTypeClass>();
            Consumers = new List<ParentedContactClass>();
            Kinds = new List<ObjectKindClass>();
        }
        //объект
        public CommonObjectClass Object { get; set; }
        //справочник местоположений
        public ObservableCollection<CustomRegionClass> ObjectTree { get; set; }
        //справочник улиц
        public List<BaseItemClass> Streets { get; set; }
        //справочник групп объектов
        public List<BaseItemClass> Groups { get; set; }
        //справочник типов каналов связи
        public List<BaseItemClass> ConnectionTypes { get; set; }
        //справочник типов объектов
        public List<ObjectTypeClass> Types { get; set; }
        //справочник потребителей
        public List<ParentedContactClass> Consumers { get; set; }
        //справочник видов
        public List<ObjectKindClass> Kinds { get; set; }
    }
}