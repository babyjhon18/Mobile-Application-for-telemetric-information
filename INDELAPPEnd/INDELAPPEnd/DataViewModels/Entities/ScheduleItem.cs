using INDELLAPPEnd.Network;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace INDELAPPEnd.DataViewModels.Entities
{
    public class ScheduleItem : IEntityItem
    {
        public KeyValuePair<HttpStatusCode, T> Clone<T>(string link, bool token)
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<HttpStatusCode, T> Create<T>(string link, object dataItem, bool token)
        {
            return Task.Run(() => AppWebRequests.PostRequest<T>(link, dataItem, token)).Result;
        }

        public KeyValuePair<HttpStatusCode, T> Delete<T>(string link, bool token)
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<HttpStatusCode, T> Update<T>(string link, T dataItem, bool token)
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<HttpStatusCode, T> View<T>(string link, T dataItem, bool token)
        {
            return Task.Run(() => AppWebRequests.GetRequest<T>(link, token)).Result;
        }
    }
}
