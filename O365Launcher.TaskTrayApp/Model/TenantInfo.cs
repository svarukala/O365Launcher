using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O365Launcher.TaskTrayApp.Model
{
    public class TenantInfo
    {
        public TenantInfo()
        {
            //CustomLinks = new List<KeyValuePair<string, string>>();
        }
        public TenantInfo(string tenantPrefix, string friendlyName)
        {
            this.Id = Guid.NewGuid();
            this.TenantPrefix = tenantPrefix;
            this.TenantFriendlyName = friendlyName;
        }
        public Guid Id { get; set; }
        public string TenantPrefix { get; set; }
        public string TenantFriendlyName { get; set; }

        public List<LauncherEnums.AdminCenters> AdminCenterLinks { get; set; }
        public List<LauncherEnums.FreqUsedLinks> FreqLinks { get; set; }
        public List<KeyValuePair<string, string>> CustomLinks { get; set; }

        [Serializable]
        public struct KeyValuePair<K, V> 
        {
            public KeyValuePair(K k, V v) : this() { Key = k; Value = v; }
            public K Key
            { get; set; }

            public V Value
            { get; set; }
        }
    }
}
