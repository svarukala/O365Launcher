using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O365Launcher.TaskTrayApp.Model
{
    class QuickLinks
    {
        public LauncherEnums.QuickLinksGroup Group { get; set; }
        public List<LauncherEnums.AdminCenters> AdminCenterLink { get; set; }
        public List<LauncherEnums.FreqUsedLinks> FreqLink { get; set; }
        public KeyValuePair<string, string> CustomLinks { get; set; }
    }
}
