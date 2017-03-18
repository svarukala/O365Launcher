using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace O365Launcher.TaskTrayApp.Model
{
    [Serializable]
    public class LauncherProfile
    {
        public LauncherProfile()
        {
            this.Browsers = new List<LauncherEnums.BrowserType>();
            this.Tenants = new List<TenantInfo>();
            this.Bookmarks = new List<BookmarkInfo>();
        }
        //public string[] Browsers { get; set; }
        public List<LauncherEnums.BrowserType> Browsers { get; set; }

        public List<TenantInfo> Tenants { get; set; }

        [XmlElement(IsNullable = true)]
        public List<BookmarkInfo> Bookmarks { get; set; }

        public void SaveConfiguration()
        {
            TaskTrayApp.Properties.Settings.Default.Configuration = ToXml();
            TaskTrayApp.Properties.Settings.Default.Save();
        }

        public string ToXml()
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(this.GetType());
            serializer.Serialize(stringwriter, this);
            return stringwriter.ToString();
        }

        public static LauncherProfile LoadFromXML(string xmlText)
        {
            var stringReader = new System.IO.StringReader(xmlText);
            var serializer = new XmlSerializer(typeof(LauncherProfile));
            return serializer.Deserialize(stringReader) as LauncherProfile;
        }
    }
}
