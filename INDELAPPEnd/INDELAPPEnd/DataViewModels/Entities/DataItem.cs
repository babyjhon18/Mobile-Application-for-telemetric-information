using INDELLAPPEnd.Network;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace INDELAPPEnd.DataViewModels.Entities
{
    public class DataItem : IBaseEntityItem
    {
        public KeyValuePair<HttpStatusCode, T> View<T>(string link, T dataItem, bool token)
        {
            return Task.Run(() => AppWebRequests.GetRequest<T>(link, token)).Result;
        }
    }
}
