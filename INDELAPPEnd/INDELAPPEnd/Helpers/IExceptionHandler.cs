using System.Net;

namespace INDELAPPEnd.Helpers
{
    public interface IExceptionHandler
    {
        void SetMessage<T>(HttpStatusCode code, T data);
    }
}
