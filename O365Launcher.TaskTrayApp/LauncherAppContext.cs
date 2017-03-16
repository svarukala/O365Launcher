using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using O365Launcher.TaskTrayApp.Model;

namespace O365Launcher.TaskTrayApp
{
    public class LauncherAppContext : ApplicationContext
    {

        NotifyIcon notifyIcon = new NotifyIcon();
        LauncherConfig configWindow = new LauncherConfig();
        About abtWindow = new About();
        public LauncherAppContext()
        {
            
            LauncherConfig config = new LauncherConfig();
            configWindow.LauncherCtx = this;
            //config.LauncherCtx = this;
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
                foreach (var adminLink in tenant.AdminCenterLinks)
                {
                    MenuItem admLinkMenuItem = new MenuItem(adminLink.ToString());
                    admLinkMenuItem.Tag = adminLink.ToString();
                    admLinkMenuItem.Name = adminLink.ToString().Equals("SharePoint") ? "https://" + tenant.TenantPrefix +"-admin.sharepoint.com" : "AdminLinks";
                    AddBrowserMenuItems(admLinkMenuItem, configWindow);
                    adminMenuItem.MenuItems.Add(admLinkMenuItem);
                }
                foreach (var fLink in tenant.FreqLinks)
                {
                    MenuItem fLinkMenuItem = new MenuItem(fLink.ToString());
                    fLinkMenuItem.Tag = fLink.ToString();
                    AddBrowserMenuItems(fLinkMenuItem, configWindow);
                    fLinksMenuItem.MenuItems.Add(fLinkMenuItem);
                }
                foreach (var cLink in tenant.CustomLinks)
                {
                    MenuItem cLinkMenuItem = new MenuItem(string.IsNullOrEmpty(cLink.Value) ? "blank" : cLink.Value);
                    cLinkMenuItem.Tag = string.IsNullOrEmpty(cLink.Key) ? "blank" : cLink.Key;
                    cLinkMenuItem.Name = "CustomLink";
                    AddBrowserMenuItems(cLinkMenuItem, configWindow);
                    customLinksMenuItem.MenuItems.Add(cLinkMenuItem);
                }
                tenantMenuItem.MenuItems.Add(adminMenuItem);
                tenantMenuItem.MenuItems.Add(fLinksMenuItem);
                tenantMenuItem.MenuItems.Add(customLinksMenuItem);
                menu.MenuItems.Add(tenantMenuItem); //, new EventHandler(Exit)));
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

        private void OpenInChrome(object sender, EventArgs e)
        {
            var n = ((MenuItem)sender).Parent.Name;
            var url = "";
            if (n.Equals("CustomLink"))
            {
                url = ((MenuItem)sender).Parent.Tag.ToString();
            }
            else if (((MenuItem)sender).Parent.ToString().Contains("SharePoint"))
            {
                url = ((MenuItem)sender).Parent.Name;
            }
            else
                url = System.Configuration.ConfigurationManager.AppSettings[((MenuItem)sender).Parent.Tag.ToString()];
            var p = Process.Start(@"chrome.exe", url);
            p.Dispose();
        }
        private void OpenInChromeIncognito(object sender, EventArgs e)
        {
            var n = ((MenuItem)sender).Parent.Name;
            var url = "";
            if (n.Equals("CustomLink"))
            {
                url = ((MenuItem)sender).Parent.Tag.ToString();
                
            }
            else if(((MenuItem)sender).Parent.ToString().Contains("SharePoint"))
            {
                url = ((MenuItem)sender).Parent.Name;
            }
            else
                url = System.Configuration.ConfigurationManager.AppSettings[((MenuItem)sender).Parent.Tag.ToString()];


            var p = Process.Start(@"chrome.exe", "--incognito "+ url);
            p.Dispose();
        }
        private void OpenInIE(object sender, EventArgs e)
        {
            var n = ((MenuItem)sender).Parent.Name;
            var url = "";
            if (n.Equals("CustomLink"))
            {
                url = ((MenuItem)sender).Parent.Tag.ToString();
            }
            else if (((MenuItem)sender).Parent.ToString().Contains("SharePoint"))
            {
                url = ((MenuItem)sender).Parent.Name;
            }
            else
                url = System.Configuration.ConfigurationManager.AppSettings[((MenuItem)sender).Parent.Tag.ToString()];
            var p = Process.Start(@"IExplore.exe", url);
            p.Dispose();
        }
        private void OpenInIEInPrivate(object sender, EventArgs e)
        {
            var n = ((MenuItem)sender).Parent.Name;
            var url = "";
            if (n.Equals("CustomLink"))
            {
                url = ((MenuItem)sender).Parent.Tag.ToString();
            }
            else if (((MenuItem)sender).Parent.ToString().Contains("SharePoint"))
            {
                url = ((MenuItem)sender).Parent.Name;
            }
            else
                url = System.Configuration.ConfigurationManager.AppSettings[((MenuItem)sender).Parent.Tag.ToString()];
            var p = Process.Start(@"IExplore.exe", "-private "+ url);
            p.Dispose();
        }
        private void OpenInFF(object sender, EventArgs e)
        {
            var n = ((MenuItem)sender).Parent.Name;
            var url = "";
            if (n.Equals("CustomLink"))
            {
                url = ((MenuItem)sender).Parent.Tag.ToString();
            }
            else if (((MenuItem)sender).Parent.ToString().Contains("SharePoint"))
            {
                url = ((MenuItem)sender).Parent.Name;
            }
            else
                url = System.Configuration.ConfigurationManager.AppSettings[((MenuItem)sender).Parent.Tag.ToString()];
            var p = Process.Start(@"firefox.exe", url);
            p.Dispose();
        }
        private void OpenInFFInPrivate(object sender, EventArgs e)
        {
            var n = ((MenuItem)sender).Parent.Name;
            var url = "";
            if (n.Equals("CustomLink"))
            {
                url = ((MenuItem)sender).Parent.Tag.ToString();
            }
            else if (((MenuItem)sender).Parent.ToString().Contains("SharePoint"))
            {
                url = ((MenuItem)sender).Parent.Name;
            }
            else
                url = System.Configuration.ConfigurationManager.AppSettings[((MenuItem)sender).Parent.Tag.ToString()];
            var p = Process.Start(@"firefox.exe", "-private-window "+ url);
            p.Dispose();
        }
        private void OpenInEdge(object sender, EventArgs e)
        {
            var n = ((MenuItem)sender).Parent.Name;
            var url = "";
            if (n.Equals("CustomLink"))
            {
                url = ((MenuItem)sender).Parent.Tag.ToString();
            }
            else if (((MenuItem)sender).Parent.ToString().Contains("SharePoint"))
            {
                url = ((MenuItem)sender).Parent.Name;
            }
            else
                url = System.Configuration.ConfigurationManager.AppSettings[((MenuItem)sender).Parent.Tag.ToString()];
            var p = Process.Start("microsoft-edge:"+ url);
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

            Application.Exit();
        }
    }
}
