using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using O365Launcher.TaskTrayApp.Model;
using System.Net;
using System.Data.SQLite;
using System.Diagnostics;
using Microsoft.ApplicationInsights;
using System.Text.RegularExpressions;

namespace O365Launcher.TaskTrayApp
{
    public partial class LauncherConfig : Form
    {
        LauncherProfile currentProfile;
        BindingSource bs = new BindingSource();
        BindingSource bsBkmGroups = new BindingSource();
        BindingSource bsBkmLinks = new BindingSource();
        LauncherAppContext launcherCtx;
        private TelemetryClient tc = new TelemetryClient();

        public LauncherConfig()
        {
            tc.InstrumentationKey = "dc41fcc7-bc20-4b9a-bb0a-bf4815df949c";

            // Set session data:
            tc.Context.User.Id = Environment.MachineName;
            tc.Context.Session.Id = Guid.NewGuid().ToString();
            tc.Context.Device.OperatingSystem = Environment.OSVersion.ToString();

            // Log a page view:
            tc.TrackPageView("LauncherConfig");

            InitializeComponent();
        }

        public LauncherProfile CurrentProfile
        {
            get
            {
                if(currentProfile == null)
                {
                    LoadProfile();
                }
                
                return currentProfile;
            }

            set { currentProfile = value; }
        }
        public LauncherAppContext LauncherCtx { get { return launcherCtx; } set { this.launcherCtx = value; } }

        private void Import_Click(object sender, EventArgs e)
        {
            int size = -1;
            openFileDialog1.Filter = "Xml files|*.xml";
            openFileDialog1.Title = "Select the O365 launcher config (xml) file.";
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                //lblImportFile.Text = "File selected: "+ file;
                try
                {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                    CurrentProfile = LauncherProfile.LoadFromXML(text);
                    CurrentProfile.SaveConfiguration();
                    LoadProfile();
                    MessageBox.Show("Imported successfully!");
                    tc.TrackEvent("ImportConfig");
                }
                catch (IOException)
                {
                }
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }

        private void Export_Click(object sender, EventArgs e)
        {
            FileStream fs = null;
            var dialog = new SaveFileDialog();
            dialog.Filter = "Xml files|*.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fs = new FileStream(dialog.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                // put the rest of your file saving code here
                byte[] byteData = Encoding.ASCII.GetBytes(CurrentProfile.ToXml());
                fs.Write(byteData,
                            0, byteData.Length);
                fs.Dispose();
                tc.TrackEvent("ExportConfig");
            }
        }

        private void LauncherConfig_Load(object sender, EventArgs e)
        {
            LoadProfile();
        }

        private void LoadProfile()
        {
            PopulateCheckedListBoxItems();
            var currentConfig = TaskTrayApp.Properties.Settings.Default.Configuration;
            if (string.IsNullOrEmpty(currentConfig))
            {
                currentProfile = new LauncherProfile();
                SelectCheckedListBoxItems(null);
            }
            else
            {
                try
                {
                    currentProfile = LauncherProfile.LoadFromXML(currentConfig);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error loading the profile. Loading empty profile.");
                    currentProfile = new LauncherProfile();
                    SelectCheckedListBoxItems(null);
                    return;
                }

                listBoxTenants.DataSource = currentProfile.Tenants;
                listBoxTenants.DisplayMember = "TenantFriendlyName";
                listBoxTenants.ValueMember = "TenantPrefix";
                
                comboBoxTenants.DataSource = currentProfile.Tenants;
                comboBoxTenants.DisplayMember = "TenantFriendlyName";
                comboBoxTenants.ValueMember = "TenantPrefix";
                comboBoxTenants.SelectedIndexChanged += new System.EventHandler(this.comboBoxTenants_SelectedIndexChanged);


                TenantInfo currentTenant = currentProfile.Tenants.Find(x => x.TenantPrefix == comboBoxTenants.SelectedValue.ToString());
                if (currentTenant != null)
                {
                    bs.DataSource = currentTenant.CustomLinks;
                    comboBoxCustomLinks.DataSource = bs;
                    comboBoxCustomLinks.DisplayMember = "Value";
                    comboBoxCustomLinks.ValueMember = "Key";

                    bsBkmGroups.DataSource = CurrentProfile.Bookmarks;
                    comboBoxBkmGroup.DataSource = bsBkmGroups;//currentProfile.Bookmarks;
                    comboBoxBkmGroup.DisplayMember = "GroupName";
                    comboBoxBkmGroup.ValueMember = "GroupName";
                    comboBoxBkmGroup.SelectedIndexChanged += ComboBoxBkmGroup_SelectedIndexChanged;

                    SelectCheckedListBoxItems(currentTenant);
                }
                #region
                //int count = checkedListBoxBrowsers.Items.Count;
                //for (int i = 0; i < count; i++)
                //{
                //    LauncherEnums.BrowserType browser;
                //    Enum.TryParse(checkedListBoxBrowsers.Items[i].ToString(), out browser);
                //    if (currentProfile.Browsers.Contains(browser))
                //    {
                //        checkedListBoxBrowsers.SetItemChecked(i, true);
                //    }
                //}
                #endregion
            }
            LauncherCtx.BuildContextMenu();
        }

        private void ComboBoxBkmGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateListBoxBkmLinks();
        }

        private void PopulateListBoxBkmLinks()
        {
            BookmarkInfo bkmInfo = currentProfile.Bookmarks.Find(x => x.GroupName == comboBoxBkmGroup.SelectedValue.ToString());
            //listBoxBkmLinks.DataSource = bkmInfo.Links;
            bsBkmLinks.DataSource = bkmInfo.Links;
            listBoxBkmLinks.DataSource = bsBkmLinks;
            listBoxBkmLinks.DisplayMember = "Value";
            listBoxBkmLinks.ValueMember = "Value";
        }
        private void PopulateCheckedListBoxItems()
        {
            PopulateCheckedListBoxItems(checkedListBoxBrowsers, typeof(LauncherEnums.BrowserType));
            PopulateCheckedListBoxItems(checkedListBoxAdminCenters, typeof(LauncherEnums.AdminCenters));
            PopulateCheckedListBoxItems(checkedListBoxFUL, typeof(LauncherEnums.FreqUsedLinks));
            #region Commented
            //checkedListBoxBrowsers.Items.Clear();
            //foreach (var item in EnumUtil.GetValues<LauncherEnums.BrowserType>())
            //{
            //    checkedListBoxBrowsers.Items.Add(item.ToString());
            //}

            //checkedListBoxAdminCenters.Items.Clear();
            //foreach (var item in EnumUtil.GetValues<LauncherEnums.AdminCenters>())
            //{
            //    checkedListBoxAdminCenters.Items.Add(item.ToString());
            //}

            //checkedListBoxFUL.Items.Clear();
            //foreach (var item in EnumUtil.GetValues<LauncherEnums.FreqUsedLinks>())
            //{
            //    checkedListBoxFUL.Items.Add(item.ToString());
            //}
            #endregion
        }
        private void PopulateCheckedListBoxItems(CheckedListBox chkListBox, Type launcherEnumObj)
        {
            chkListBox.Items.Clear();
            chkListBox.Items.AddRange(Enum.GetValues(launcherEnumObj).Cast<object>().ToArray());
        }

        private void SelectCheckedListBoxItems(TenantInfo currentTenant)
        {
            SelectCheckedListBoxItems(checkedListBoxBrowsers, typeof(LauncherEnums.BrowserType), currentTenant);
            SelectCheckedListBoxItems(checkedListBoxAdminCenters, typeof(LauncherEnums.AdminCenters), currentTenant);
            SelectCheckedListBoxItems(checkedListBoxFUL, typeof(LauncherEnums.FreqUsedLinks), currentTenant);
        }
        private void SelectCheckedListBoxItems(CheckedListBox chkListBox, Type launcherEnumObj, TenantInfo tenant)
        {
            
            int count = chkListBox.Items.Count;
            if (tenant == null)
            {
                for (int i = 0; i < count; i++)
                {
                    chkListBox.SetItemChecked(i, true);
                }
            }
            else
            {
                var browserType = typeof(LauncherEnums.BrowserType);
                if (browserType.Equals(launcherEnumObj))
                {
                    var obj = (LauncherEnums.BrowserType)Activator.CreateInstance(launcherEnumObj);
                    for (int i = 0; i < count; i++)
                    {
                        chkListBox.SetItemChecked(i, false);
                        Enum.TryParse(chkListBox.Items[i].ToString(), out obj);
                        if (currentProfile.Browsers.Contains(obj))
                        {
                            chkListBox.SetItemChecked(i, true);
                        }
                    }
                }
                else
                {
                    var adminCentersType = typeof(LauncherEnums.AdminCenters);
                    if (adminCentersType.Equals(launcherEnumObj))
                    {
                        var obj = (LauncherEnums.AdminCenters)Activator.CreateInstance(launcherEnumObj);
                        for (int i = 0; i < count; i++)
                        {
                            chkListBox.SetItemChecked(i, false);
                            Enum.TryParse(chkListBox.Items[i].ToString(), out obj);
                            if (tenant.AdminCenterLinks.Contains(obj))
                            {
                                chkListBox.SetItemChecked(i, true);
                            }
                        }
                    }
                    else
                    {
                        var fulType = typeof(LauncherEnums.FreqUsedLinks);
                        if (fulType.Equals(launcherEnumObj))
                        {
                            var obj = (LauncherEnums.FreqUsedLinks)Activator.CreateInstance(launcherEnumObj);
                            for (int i = 0; i < count; i++)
                            {
                                chkListBox.SetItemChecked(i, false);
                                Enum.TryParse(chkListBox.Items[i].ToString(), out obj);
                                if (tenant.FreqLinks.Contains(obj))
                                {
                                    chkListBox.SetItemChecked(i, true);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static class EnumUtil
        {
            public static IEnumerable<T> GetValues<T>()
            {
                return Enum.GetValues(typeof(T)).Cast<T>();
            }
        }

        private void btnAddTenant_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtTenantPrefix.Text))
                {
                    txtFriendlyName.Text = string.IsNullOrEmpty(txtFriendlyName.Text) ? txtTenantPrefix.Text : txtFriendlyName.Text;
                    AddTenant(txtTenantPrefix.Text, txtFriendlyName.Text);
                    //MessageBox.Show("Added tenant successfully!");
                    txtTenantPrefix.Clear();
                    txtFriendlyName.Clear();
                }
                else
                    MessageBox.Show("Please provide a valid tenant name.");
                //if (!String.IsNullOrEmpty(txtTenantPrefix.Text) && IsTenantValid(txtTenantPrefix.Text))
                //{
                //    txtFriendlyName.Text = string.IsNullOrEmpty(txtFriendlyName.Text) ? txtTenantPrefix.Text: txtFriendlyName.Text;
                //    currentProfile.Tenants.Add(new TenantInfo(txtTenantPrefix.Text, txtFriendlyName.Text));
                //    currentProfile.SaveConfiguration();
                //    LoadProfile();
                //    MessageBox.Show("Added tenant successfully!");
                //    txtTenantPrefix.Clear();
                //    txtFriendlyName.Clear();
                //}
                //else
                //    MessageBox.Show("Tenant name provided is invalid");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add. Error: " + ex.ToString());
            }
        }

        private void AddTenants(List<string> tenantsList)
        {
            foreach (var tenantPrefix in tenantsList)
            {
                AddTenant(tenantPrefix, tenantPrefix);
            }
            //MessageBox.Show("Done!");
        }
        private void AddTenant(string tenantPrefix, string tenantFriendlyName)
        {
            try
            {
                if (IsTenantValid(tenantPrefix))
                {
                    currentProfile.Tenants.Add(new TenantInfo(tenantPrefix, tenantFriendlyName));
                    currentProfile.SaveConfiguration();
                    LoadProfile();
                    LauncherCtx.BuildContextMenu();
                    tc.TrackMetric("TenantsAdded", currentProfile.Tenants.Count);
                }
                else
                    MessageBox.Show("Tenant name "+ tenantPrefix +" is invalid");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add. Error: " + ex.ToString());
            }
        }


        public static bool IsTenantValid(string tenantPrefix)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadString("https://login.windows.net/"+ tenantPrefix + ".onmicrosoft.com/FederationMetadata/2007-06/FederationMetadata.xml");
                }
            }
            catch (WebException ex)
            {
                var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                return false;
            }
            return true;
        }



        private void btnSaveBrowsers_Click(object sender, EventArgs e)
        {
            try
            { 
                CurrentProfile.Browsers.Clear();
                foreach (var item in checkedListBoxBrowsers.CheckedItems)
                {
                    LauncherEnums.BrowserType browser;
                    Enum.TryParse(item.ToString(), out browser);
                    currentProfile.Browsers.Add(browser);
                }
                currentProfile.SaveConfiguration();
                MessageBox.Show("Successfully saved!");
                LauncherCtx.BuildContextMenu();
                tc.TrackEvent("BrowsersSave");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save. Error: " + ex.ToString());
            }
        }

        private void btnSaveTenantLinks_Click(object sender, EventArgs e)
        {
            try
            {
                TenantInfo currentTenant = currentProfile.Tenants.Find(x => x.TenantPrefix == comboBoxTenants.SelectedValue.ToString());
                currentTenant.AdminCenterLinks.Clear();
                foreach (var item in checkedListBoxAdminCenters.CheckedItems)
                {
                    LauncherEnums.AdminCenters adc;
                    Enum.TryParse(item.ToString(), out adc);
                    currentTenant.AdminCenterLinks.Add(adc);
                }

                currentTenant.FreqLinks.Clear();
                foreach (var item in checkedListBoxFUL.CheckedItems)
                {
                    LauncherEnums.FreqUsedLinks ful;
                    Enum.TryParse(item.ToString(), out ful);
                    currentTenant.FreqLinks.Add(ful);
                }
                currentProfile.SaveConfiguration();
                MessageBox.Show("Successfully saved!");
                LauncherCtx.BuildContextMenu();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Failed to save. Error: " + ex.ToString());
            }
        }

        private void comboBoxTenants_SelectedIndexChanged(object sender, EventArgs e)
        {
            TenantInfo currentTenant = currentProfile.Tenants.Find(x => x.TenantPrefix == comboBoxTenants.SelectedValue.ToString());
            SelectCheckedListBoxItems(currentTenant);
            bs.DataSource = currentTenant.CustomLinks;
            //comboBoxCustomLinks.DataSource = bs;
            //comboBoxCustomLinks.DisplayMember = "Value";
            //comboBoxCustomLinks.ValueMember = "Key";
            bs.ResetBindings(false);
            //LoadProfile();
        }

        private void btnAddCustomLink_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCustomLink.Text) && !string.IsNullOrEmpty(txtCustomLinkName.Text))
                {
                    TenantInfo currentTenant = currentProfile.Tenants.Find(x => x.TenantPrefix == comboBoxTenants.SelectedValue.ToString());
                    TenantInfo.KeyValuePair<string, string> kvPair = new TenantInfo.KeyValuePair<string, string>(txtCustomLink.Text, txtCustomLinkName.Text);
                    currentTenant.CustomLinks.Add(kvPair);
                    currentProfile.SaveConfiguration();
                    bs.ResetBindings(false);
                    //MessageBox.Show("Successfully added!");
                    txtCustomLink.Clear();
                    txtCustomLinkName.Clear();
                    comboBoxCustomLinks.SelectedIndex = comboBoxCustomLinks.Items.Count - 1;
                    LauncherCtx.BuildContextMenu();
                    tc.TrackMetric("TenantCustomLinks", currentTenant.CustomLinks.Count);
                }
                else
                    MessageBox.Show("Please provide valid url and name");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save. Error: " + ex.ToString());
            }
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            tc.TrackEvent("AutoDetectBrowser");
            if (!CheckIfBrowserRunning(LauncherEnums.BrowserType.Chrome))
            {
                List<string> tenants = AutoDetectChromeHistory();
                if (tenants != null && tenants.Count > 0)
                {
                    if (MessageBox.Show("Confirm to add the detected tenants: " + Environment.NewLine + string.Join(",", tenants), "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == System.Windows.Forms.DialogResult.Yes)
                    {
                        AddTenants(tenants);
                    }
                }
                else
                    MessageBox.Show("No tenants detected!");
            }
            else
                MessageBox.Show("Please close all your Chrome browsers for this feaure to work.");
        }

        private static bool CheckIfBrowserRunning(LauncherEnums.BrowserType browser)
        {
            switch (browser)
            {
                case LauncherEnums.BrowserType.Chrome:
                    Process[] chromeInstances = Process.GetProcessesByName("chrome");
                    if (chromeInstances.Length > 0)
                    {
                        return true;
                    }
                    break;

            }
            
            return false;
        }

        public static List<string> AutoDetectChromeHistory()
        {
            //LauncherConfig.tc.TrackEvent("AutoDetectChromeHistory");
            //List<string> t = new List<string>();
            //t.Add("ten1");
            //t.Add("ten2");
            //return t;
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
                                                (@"Data Source=" + documentsFolder + "History");
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
                while (dr.Read())
                {
                    tempUrl = dr[1].ToString();
                    if ((tempUrl.Contains(".sharepoint.com")) && (!tempUrl.Contains("-admin.sharepoint.com")))
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
                List<string> tenantPrefixList = new List<string>();
                foreach (var item in tenants)
                {
                    //Console.WriteLine(item.Substring(item.IndexOf("https://") + 8, item.IndexOf(".sharepoint.com") - 8));
                    tenantPrefixList.Add(item.Substring(item.IndexOf("https://") + 8, item.IndexOf(".sharepoint.com") - 8));
                }
                return tenantPrefixList;
            }
            return null;

        }

        private void btnRemoveTenant_Click(object sender, EventArgs e)
        {
            try
            {
                TenantInfo currentTenant = currentProfile.Tenants.Find(x => x.TenantPrefix == listBoxTenants.SelectedValue.ToString());
                currentProfile.Tenants.Remove(currentTenant); //.Add(new TenantInfo(txtTenantPrefix.Text, txtFriendlyName.Text));
                currentProfile.SaveConfiguration();
                LoadProfile();
                LauncherCtx.BuildContextMenu();
                MessageBox.Show("Removed tenant successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to remove tenant. Error: " + ex.ToString());
            }
        }

        private void btnAddBkmLink_Click(object sender, EventArgs e)
        {
            if (comboBoxBkmGroup.SelectedValue == null)
            {
                MessageBox.Show("Please select the Bookmark group");
            }
            else
            {
                if (string.IsNullOrEmpty(txtLinkToAdd.Text) || string.IsNullOrEmpty(txtLinkName.Text))
                {
                    MessageBox.Show("Please provide valid link and name to add.");
                }
                else
                {
                    BookmarkInfo bkmInfo = CurrentProfile.Bookmarks.Find(x => x.GroupName == comboBoxBkmGroup.SelectedValue.ToString());
                    //BookmarkInfo.
                    BkmKeyValuePair<string, string> bkmPair = new Model.BkmKeyValuePair<string, string>(this.txtLinkToAdd.Text, this.txtLinkName.Text);
                    bkmInfo.Links.Add(bkmPair);
                    CurrentProfile.SaveConfiguration();
                    txtLinkName.Clear();
                    txtLinkToAdd.Clear();
                    LoadProfile();
                    //PopulateListBoxBkmLinks();
                    bsBkmLinks.DataSource = bkmInfo.Links;
                    bsBkmLinks.ResetBindings(false);
                    LauncherCtx.BuildContextMenu();
                    tc.TrackMetric("BookmarksAdded", bkmInfo.Links.Count, 
                        new Dictionary<string, string>() { { "BookmarkGroup", bkmInfo.GroupName } });
                    
                }
            }
        }

        private void AddBkmGroup(string groupName)
        {
            BookmarkInfo bkmInfo = new BookmarkInfo(groupName);
            CurrentProfile.Bookmarks.Add(bkmInfo);
            CurrentProfile.SaveConfiguration();
            LoadProfile();
            LauncherCtx.BuildContextMenu();
            //return 
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtNewGroupName.Text))
            {
                MessageBox.Show("Please provide a group name.");
            }
            else
            {
                AddBkmGroup(txtNewGroupName.Text.Trim());
                txtNewGroupName.Clear();
                //MessageBox.Show("Added the group");
                LoadProfile();

                //currentProfile.Bookmarks.Find(x => x.GroupName == comboBoxBkmGroup.SelectedValue.ToString());

                comboBoxBkmGroup.SelectedIndex = comboBoxBkmGroup.Items.Count-1;
                LauncherCtx.BuildContextMenu();
                //bsBkmGroups.DataSource = CurrentProfile.Bookmarks;
                //bsBkmGroups.ResetBindings(false);
            }
        }

        private void btnRemoveLink_Click(object sender, EventArgs e)
        {
            var val = listBoxBkmLinks.SelectedValue.ToString();
            BookmarkInfo bkmInfo = CurrentProfile.Bookmarks.Find(x => x.GroupName == comboBoxBkmGroup.SelectedValue.ToString());
            //bkmInfo.Links.RemoveAll(l => l.Value == val);
            int index = bkmInfo.Links.FindIndex(l => l.Value == val);

            if (index >= 0)
            {
                bkmInfo.Links.RemoveAt(index);
                CurrentProfile.SaveConfiguration();
                LoadProfile();
                bsBkmLinks.DataSource = bkmInfo.Links;
                bsBkmLinks.ResetBindings(false);
            }
        }

        private void LauncherConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tc != null)
            {
                tc.Flush(); // only for desktop apps

                // Allow time for flushing:
                System.Threading.Thread.Sleep(500);
            }
        }

        private void checkBoxAdminLinks_CheckedChanged(object sender, EventArgs e)
        {
            SelectAllCheckedListBoxItems(checkedListBoxAdminCenters);
        }

        private void SelectAllCheckedListBoxItems(CheckedListBox checkedListBox)
        {
            for (var i = 0; i < checkedListBox.Items.Count; i++)
            {
                checkedListBox.SetItemChecked(i, true);
            }
        }

        private void checkBoxFreqLinks_CheckedChanged(object sender, EventArgs e)
        {
            SelectAllCheckedListBoxItems(checkedListBoxFUL);
        }
    }
}
