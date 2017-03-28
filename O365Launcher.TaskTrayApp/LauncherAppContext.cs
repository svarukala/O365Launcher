using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using O365Launcher.TaskTrayApp.Model;
using Microsoft.ApplicationInsights;
using System.Text.RegularExpressions;

namespace O365Launcher.TaskTrayApp
{
    public class LauncherAppContext : ApplicationContext
    {

        NotifyIcon notifyIcon = new NotifyIcon();
        LauncherConfig configWindow = new LauncherConfig();
        About abtWindow = new About();
        private TelemetryClient tc = new TelemetryClient();
        public LauncherAppContext()
        {
            tc.InstrumentationKey = "dc41fcc7-bc20-4b9a-bb0a-bf4815df949c";
                //"a4a0bc24-f0e2-4bc4-8226-de98a8b215d9";

            // Set session data:
            //Re-thinking about using MachineName as the userid. I hope MahcineName is not okay (not PII)
            tc.Context.User.Id = Environment.MachineName;
            tc.Context.Session.Id = Guid.NewGuid().ToString();
            tc.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
            
            // Log a page view:
            tc.TrackPageView("LauncherAppContext");


            LauncherConfig config = new LauncherConfig();
            configWindow.LauncherCtx = this;
            //config.LauncherCtx = this;aaaaaxcz
            BuildContextMenu();
        }

        public void BuildContextMenu()
        {

            ContextMenu menu = new ContextMenu();
            foreach (var tenant in configWindow.CurrentProfile.Tenants)
            {
                MenuItem tenantMenuItem = new MenuItem(tenant.TenantFriendlyName);
                MenuItem adminMenuItem = new MenuItem("AdminCenters");
                MenuItem fLinksMenuItem = new MenuItem("FreqLinks");
                MenuItem customLinksMenuItem = new MenuItem("CustomLinks");
                string url = string.Empty;
                foreach (var adminLink in tenant.AdminCenterLinks)
                {
                    MenuItem admLinkMenuItem = new MenuItem(adminLink.ToString());
                    url = System.Configuration.ConfigurationManager.AppSettings[adminLink.ToString()];
                    admLinkMenuItem.Tag = ContainsFormatPlaceholders(url) ? string.Format(url, tenant.TenantPrefix) : url;
                    //adminLink.ToString().Equals("SharePoint") ? "https://" + tenant.TenantPrefix + "-admin.sharepoint.com" : System.Configuration.ConfigurationManager.AppSettings[adminLink.ToString()];   //adminLink.ToString();
                    admLinkMenuItem.Name = "AdminLinks";
                        //adminLink.ToString().Equals("SharePoint") ? "https://" + tenant.TenantPrefix +"-admin.sharepoint.com" : "AdminLinks";
                    AddBrowserMenuItems(admLinkMenuItem, configWindow);
                    adminMenuItem.MenuItems.Add(admLinkMenuItem);
                }
                foreach (var fLink in tenant.FreqLinks)
                {
                    MenuItem fLinkMenuItem = new MenuItem(fLink.ToString());
                    url = System.Configuration.ConfigurationManager.AppSettings[fLink.ToString()];
                    fLinkMenuItem.Tag = ContainsFormatPlaceholders(url) ? string.Format(url, tenant.TenantPrefix) : url;
                    //fLink.ToString();
                    fLinkMenuItem.Name = "FreqLinks";
                    AddBrowserMenuItems(fLinkMenuItem, configWindow);
                    fLinksMenuItem.MenuItems.Add(fLinkMenuItem);
                }
                foreach (var cLink in tenant.CustomLinks)
                {
                    MenuItem cLinkMenuItem = new MenuItem(string.IsNullOrEmpty(cLink.Value) ? "blank" : cLink.Value);
                    cLinkMenuItem.Tag = string.IsNullOrEmpty(cLink.Key) ? "blank" : cLink.Key;
                    cLinkMenuItem.Name = "CustomLinks";
                    AddBrowserMenuItems(cLinkMenuItem, configWindow);
                    customLinksMenuItem.MenuItems.Add(cLinkMenuItem);
                }
                tenantMenuItem.MenuItems.Add(adminMenuItem);
                tenantMenuItem.MenuItems.Add(fLinksMenuItem);
                tenantMenuItem.MenuItems.Add(customLinksMenuItem);
                menu.MenuItems.Add(tenantMenuItem); //, new EventHandler(Exit)));
            }
            //Add Bookmarks menu only if user added any bookmarks
            if (configWindow.CurrentProfile.Bookmarks.Count > 0)
            {
                MenuItem bookmarkMenuItem = new MenuItem("Bookmarks");
                foreach (var bkmInfo in configWindow.CurrentProfile.Bookmarks)
                {
                    MenuItem bkmMenuItem = new MenuItem(bkmInfo.GroupName);
                    foreach (var link in bkmInfo.Links)
                    {
                        MenuItem linkMenuItem = new MenuItem(link.Value);
                        linkMenuItem.Name = "Bookmark";
                        linkMenuItem.Tag = link.Key;
                        AddBrowserMenuItems(linkMenuItem, configWindow);
                        bkmMenuItem.MenuItems.Add(linkMenuItem);
                    }
                    bookmarkMenuItem.MenuItems.Add(bkmMenuItem);
                    //menu.MenuItems.Add(bkmMenuItem);
                }
                menu.MenuItems.Add(bookmarkMenuItem);
            }
            menu.MenuItems.Add(new MenuItem("Configuration", new EventHandler(ShowConfig)));
            menu.MenuItems.Add(new MenuItem("About", new EventHandler(ShowAbout)));
            menu.MenuItems.Add(new MenuItem("Exit", new EventHandler(Exit)));

            notifyIcon.Icon = TaskTrayApp.Properties.Resources.launcher;
            notifyIcon.DoubleClick += new EventHandler(ShowMessage);
            notifyIcon.ContextMenu = menu; // new ContextMenu(new MenuItem[] { configMenuItem, exitMenuItem });
            notifyIcon.Visible = true;
        }

        private void AddBrowserMenuItems(MenuItem mItem, LauncherConfig config)
        {
            MenuItem browse = null;
            MenuItem browserPvt = null;
            foreach (var item in config.CurrentProfile.Browsers)
            {
                switch(item)
                {
                    case LauncherEnums.BrowserType.Chrome:
                        browse = new MenuItem("Open in Chrome", new EventHandler(OpenInChrome));
                        browserPvt = new MenuItem("Open in Chrome Incognito", new EventHandler(OpenInChromeIncognito));
                        break;
                    case LauncherEnums.BrowserType.Firefox:
                        browse = new MenuItem("Open in Firefox", new EventHandler(OpenInFF));
                        browserPvt = new MenuItem("Open in Firefox InPrivate", new EventHandler(OpenInFFInPrivate));
                        break;
                    case LauncherEnums.BrowserType.IExplorer:
                        browse = new MenuItem("Open in IE", new EventHandler(OpenInIE));
                        browserPvt = new MenuItem("Open in IE InPrivate", new EventHandler(OpenInIEInPrivate));
                        break;
                }

                mItem.MenuItems.Add(browse);
                mItem.MenuItems.Add(browserPvt);                
            }
            //browse = new MenuItem("Open in Edge", new EventHandler(OpenInEdge));
            //mItem.MenuItems.Add(browse);
        }

        private string FindMenuItemUrl(Menu parentMenu)
        {
            return parentMenu.Tag.ToString();
            //var url = "";
            //if (parentMenu.Name.Equals("CustomLink") || parentMenu.Name.Equals("Bookmark"))
            //{
            //    url = parentMenu.Tag.ToString();
            //}
            //else if (parentMenu.ToString().Contains("SharePoint"))
            //{
            //    url = parentMenu.Name;
            //}
            //else
            //    url = System.Configuration.ConfigurationManager.AppSettings[parentMenu.Tag.ToString()];
            //return url;
        }

        public static bool ContainsFormatPlaceholders(string inputText)
        {
            var matches = Regex.Matches(inputText, @"(?<!\{)\{([0-9]+).*?\}(?!})");
            return matches.Count > 0 ? true : false;
            //int count = 0;
            //if (matches.Count > 0)
            //{
            //    count = matches.Cast<Match>().Max(m => int.Parse(m.Groups[1].Value)) + 1;
            //    return count > 0 ? true : false;
            //}
            //return false;
            //Console.WteLine("Count {0}", count);
        }

        private void OpenInChrome(object sender, EventArgs e)
        {
            //var n = ((MenuItem)sender).Parent.Name;
            //var url = "";
            //if (n.Equals("CustomLink") || n.Equals("Bookmark"))
            //{
            //    url = ((MenuItem)sender).Parent.Tag.ToString();
            //}
            //else if (((MenuItem)sender).Parent.ToString().Contains("SharePoint"))
            //{
            //    url = ((MenuItem)sender).Parent.Name;
            //}
            //else
            //    url = System.Configuration.ConfigurationManager.AppSettings[((MenuItem)sender).Parent.Tag.ToString()];
            var p = Process.Start(@"chrome.exe", FindMenuItemUrl(((MenuItem)sender).Parent));
            p.Dispose();

            tc.TrackEvent(LauncherEnums.BrowserType.Chrome.ToString());
            //,
              //                      new Dictionary<string, string>() { { "zip", zip } },
                //                    new Dictionary<string, double>() { { "duration", timer.ElapsedMilliseconds } });
        }
        private void OpenInChromeIncognito(object sender, EventArgs e)
        {
            var p = Process.Start(@"chrome.exe", "--incognito "+ FindMenuItemUrl(((MenuItem)sender).Parent));
            p.Dispose();
            tc.TrackEvent(LauncherEnums.BrowserType.Chrome.ToString()+ " InPrivate");
        }
        private void OpenInIE(object sender, EventArgs e)
        {
            var p = Process.Start(@"IExplore.exe", FindMenuItemUrl(((MenuItem)sender).Parent));
            p.Dispose();
            tc.TrackEvent(LauncherEnums.BrowserType.IExplorer.ToString());
        }
        private void OpenInIEInPrivate(object sender, EventArgs e)
        {
            var p = Process.Start(@"IExplore.exe", "-private "+ FindMenuItemUrl(((MenuItem)sender).Parent));
            p.Dispose();
            tc.TrackEvent(LauncherEnums.BrowserType.IExplorer.ToString() + " InPrivate");
        }
        private void OpenInFF(object sender, EventArgs e)
        {
            var p = Process.Start(@"firefox.exe", FindMenuItemUrl(((MenuItem)sender).Parent));
            p.Dispose();
            tc.TrackEvent(LauncherEnums.BrowserType.Firefox.ToString());
        }
        private void OpenInFFInPrivate(object sender, EventArgs e)
        {
            var p = Process.Start(@"firefox.exe", "-private-window "+ FindMenuItemUrl(((MenuItem)sender).Parent));
            p.Dispose();
            tc.TrackEvent(LauncherEnums.BrowserType.Firefox.ToString() + " InPrivate");
        }
        private void OpenInEdge(object sender, EventArgs e)
        {
            var p = Process.Start("microsoft-edge:"+ FindMenuItemUrl(((MenuItem)sender).Parent));
            p.Dispose();
        }
        private void Item2_Click(object sender, EventArgs e)
        {
            var p = Process.Start(@"IExplore.exe", "-private http://bing.com");
            p.Dispose();
        }

        private void Item1_Click(object sender, EventArgs e)
        {
            var url = "http://www.google.com";

            //using (var process = new Process())
            //{
            //    process.StartInfo.FileName = @"C:\Users\<UserName>\AppData\Local\Google\Chrome\chrome.exe";
            //    process.StartInfo.Arguments = url + " --incognito";

            //    process.Start()
            //}

            var p = Process.Start(@"chrome.exe", "--incognito http://yahoo.com");
            p.Dispose();
            //throw new NotImplementedException();
        }

        void ShowMessage(object sender, EventArgs e)
        {
            // Only show the message if the settings say we can.
            if (TaskTrayApp.Properties.Settings.Default.ShowMessage)
                MessageBox.Show("Hello World");
        }

        void ShowConfig(object sender, EventArgs e)
        {
            // If we are already showing the window meerly focus it.
            if (configWindow.Visible)
                configWindow.Focus();
            else
                configWindow.ShowDialog();
            
        }

        void ShowAbout(object sender, EventArgs e)
        {
            // If we are already showing the window meerly focus it.
            if (abtWindow.Visible)
                abtWindow.Focus();
            else
                abtWindow.ShowDialog();
        }

        void Exit(object sender, EventArgs e)
        {
            // We must manually tidy up and remove the icon before we exit.
            // Otherwise it will be left behind until the user mouses over.
            notifyIcon.Visible = false;
            if (tc != null)
            {
                tc.Flush(); // only for desktop apps

                // Allow time for flushing:
                System.Threading.Thread.Sleep(1000);
            }
            Application.Exit();
        }
    }
}
