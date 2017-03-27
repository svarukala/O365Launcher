using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O365Launcher.TaskTrayApp.Model
{
    public class LauncherEnums
    {
        public enum BrowserType
        {
            Chrome,
            IExplorer,
            Firefox
                //,
            //Safari
        }

        public enum QuickLinksGroup
        {
            AdminCenters,
            FreqUsedLinks,
            CustomLinks
        }
        public enum AdminCenters
        {
            AdminHome,
            Exchange,
            Skype,
            SharePointAdmin,
            OneDriveAdmin,
            YammerAdmin,
            PowerAppsAdmin,
            FlowAdmin,
            Compliance
        }

        public enum FreqUsedLinks
        {
            ActiveUsers,
            Groups,
            ServiceHealth,
            MessageCenter,
            UsageReports,
            ServicesAddins,
            Mail,
            Calendar,
            People,
            Yammer,
            PowerApps,
            Flow,
            Sway,
            OneDrive,
            SharePoint,
            Newsfeed,
            Planner,
            Tasks,
            PowerBI,
            Delve,
            Video,
            Word,
            Excel,
            PowerPoint,
            OneNote,
            Dynamics
        }
    }
}
