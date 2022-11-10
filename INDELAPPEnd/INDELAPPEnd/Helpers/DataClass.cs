using System.Net;

namespace INDELAPPEnd.Helpers
{
    public static class DataClass
    {
        public static bool ReturnedResult(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.OK:
                    return true;
                case HttpStatusCode.BadRequest:
                    return false;
                case HttpStatusCode.NotModified:
                    return false;
                case HttpStatusCode.InternalServerError:
                    return false;
            }
            return false;
        }
    }
}
