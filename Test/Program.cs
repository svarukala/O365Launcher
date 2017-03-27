using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using O365Launcher.TaskTrayApp.Model;
using O365Launcher.TaskTrayApp;
using System.Net;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;

namespace Test
{
    class Program
    {
        //private TelemetryClient tc = new TelemetryClient();
        static void Main(string[] args)
        {
            string str = "sdl{0}sdksld";
            Console.WriteLine(string.Format(str, 111));
            //DoesContainFormatPlaceholders("sdl{0}sdksld");
            
            

            Console.Read();
            return;
            GetChromeHistory();
            Console.WriteLine(Program.IsTenantValid("https://login.windows.net/mod362201.onmicrosoft.com/FederationMetadata/2007-06/FederationMetadata.xml"));
            LauncherProfile profile = new LauncherProfile();
            profile.Browsers.Add(LauncherEnums.BrowserType.Chrome);
            profile.Browsers.Add(LauncherEnums.BrowserType.IExplorer);
            profile.Browsers.Add(LauncherEnums.BrowserType.Firefox);
            profile.Tenants.Add(new TenantInfo("mod362200", "Dev Tenant"));
            profile.Tenants.Add(new TenantInfo("MOD775221", "QA Tenant"));
            profile.Tenants.Add(new TenantInfo("varuk02", "Prod Tenant"));
            var xmlRep = profile.ToXml();
            Console.WriteLine(xmlRep);
            
            Console.Read();
        }

        public static void DoesContainFormatPlaceholders(string inputText)
        {
            var matches = Regex.Matches(inputText, @"(?<!\{)\{([0-9]+).*?\}(?!})");
            int count = 0;
            if (matches.Count > 0)
                count = matches.Cast<Match>().Max(m => int.Parse(m.Groups[1].Value)) + 1;
            Console.WriteLine("Count {0}", count);
        }
        public static void GetChromeHistory()
        {
            // Get Current Users App Data
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string[] tempstr = documentsFolder.Split('\\');
            string tempstr1 = "";
            documentsFolder += "\\Google\\Chrome\\User Data\\Default\\";
            if (tempstr[tempstr.Length - 1] != "Local")
            {
                for (int i = 0; i < tempstr.Length - 1; i++)
                {
                    tempstr1 += tempstr[i] + "\\";
                }
                documentsFolder = tempstr1 + "Local\\Google\\Chrome\\User Data\\Default\\";
            }


            // Check if directory exists
            if (Directory.Exists(documentsFolder))
            {
                SQLiteConnection conn = new SQLiteConnection
                                                (@"Data Source="+ documentsFolder +"History");
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = conn;
                //cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;";
                //  Use the above query to get all the table names
                cmd.CommandText = "Select * From urls";
                SQLiteDataReader dr = cmd.ExecuteReader();
                List<string> tenants = new List<string>();
                var tempUrl = string.Empty;
                var tenantUrl = string.Empty;
                System.Text.StringBuilder sb = new StringBuilder();
                while (dr.Read())
                {
                    sb.Append(dr[1].ToString());
                    tempUrl = dr[1].ToString();
                    if ((tempUrl.Contains(".sharepoint.com")))
                    {
                        tenantUrl = tempUrl.Substring(tempUrl.IndexOf("https://"), tempUrl.IndexOf(".com") + 4);
                        //currentProfile.Tenants.Find(x => x.TenantPrefix == comboBoxTenants.SelectedValue.ToString())
                        if (tenantUrl.Contains("sharepoint.com") && tenants.FirstOrDefault(stringToCheck => stringToCheck.Contains(tenantUrl)) == null)
                        {
                            tenants.Add(tenantUrl);
                            Console.WriteLine(tenantUrl);
                        }

                    }

                }

                Console.WriteLine(tenants.Count);
                foreach (var item in tenants)
                {
                    Console.WriteLine(item.Substring(item.IndexOf("https://") + 8, item.IndexOf(".sharepoint.com") - 8));
                }
            }

        }
        public static bool IsTenantValid(string link)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadString(link);
                }
            }
            catch (WebException ex)
            {
                var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                return false;
            }
            return true;
        }

    }
}
