using System.Collections.Generic;

namespace INDELLAPPEnd.Models
{
    public class ClaimsModel
    {
        public string Access_token { get; set; }
        public string username { get; set; }
        public List<KeyValuePair<string, string>> Claims { get; set; }
        public int id { get; set; }
        public bool IsAdmin { get; set; }
        public ClaimsModel()
        {
            Claims = new List<KeyValuePair<string, string>>();
        }
    }
}
