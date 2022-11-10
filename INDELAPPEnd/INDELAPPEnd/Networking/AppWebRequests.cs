using INDELAPPEnd.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace INDELLAPPEnd.Network
{
    public class AppWebRequests
    {
        public static async Task<KeyValuePair<HttpStatusCode, T>> GetRequest<T>(string link, bool token)
        {
            try
            {
                HttpClient client = new HttpClient();
                if (token)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                HttpResponseMessage result = await client.GetAsync(link);
                if (DataClass.ReturnedResult(result.StatusCode))
                    return new KeyValuePair<HttpStatusCode, T>(result.StatusCode,
                        JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result));
                return new KeyValuePair<HttpStatusCode, T>(result.StatusCode, default);
            }
            catch (Exception e)
            {
                return new KeyValuePair<HttpStatusCode, T>(HttpStatusCode.InternalServerError, default);
            }
        }

        public static async Task<KeyValuePair<HttpStatusCode, T>> PostRequest<T>(string link, object content, bool token)
        {
            try
            {
                string json = JsonConvert.SerializeObject(content);
                HttpContent httpcontent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                if (token)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                HttpResponseMessage response = await client.PostAsync(link, httpcontent);
                if (DataClass.ReturnedResult(response.StatusCode))
                    return new KeyValuePair<HttpStatusCode, T>(response.StatusCode,
                        JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result));
                return new KeyValuePair<HttpStatusCode, T>(response.StatusCode, default);
            }
            catch (Exception e)
            {
                return new KeyValuePair<HttpStatusCode, T>(HttpStatusCode.InternalServerError, default);
            }
        }

        public static async Task<KeyValuePair<HttpStatusCode, T>> DeleteRequest<T>(string link, bool token)
        {
            try
            {
                HttpClient client = new HttpClient();
                if (token)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                var result = await client.DeleteAsync(link);
                return new KeyValuePair<HttpStatusCode, T>(result.StatusCode,
                    JsonConvert.DeserializeObject<T>(""));
            }
            catch (Exception e)
            {
                return new KeyValuePair<HttpStatusCode, T>(HttpStatusCode.InternalServerError, default);
            }
        }
    }
}
