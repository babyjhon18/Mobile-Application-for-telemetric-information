using System;
using System.Collections.Generic;
using System.Net;

namespace INDELAPPEnd.DataViewModels
{
    public interface IEntityItem : IBaseEntityItem
    {
        KeyValuePair<HttpStatusCode, T> Create<T>(string link, Object dataItem, bool token);
        KeyValuePair<HttpStatusCode, T> Update<T>(string link, T dataItem, bool token);
        KeyValuePair<HttpStatusCode, T> Delete<T>(string link, bool token);
        KeyValuePair<HttpStatusCode, T> Clone<T>(string link, bool token);
    }

    public interface IBaseEntityItem
    {
        KeyValuePair<HttpStatusCode, T> View<T>(string link, T dataItem, bool token);
    }
}
