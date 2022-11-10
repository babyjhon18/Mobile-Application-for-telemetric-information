using INDELAPPEnd.ViewModels;
using System.Collections.Generic;

namespace INDELLAPPEnd.Models
{
    public class ProfileSettingsModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<KeyValuePair<string, string>> Claims { get; set; }
        public ProfileSettingsModel()
        {
            Claims = new List<KeyValuePair<string, string>>();
        }
    }
}
