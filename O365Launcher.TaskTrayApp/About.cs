using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Deployment;
using System.Deployment.Application;

namespace O365Launcher.TaskTrayApp
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void About_Load(object sender, EventArgs e)
        {
            Version appVersion;
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                appVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                this.Text = "About (v." + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion + ")";
                //lblVersion.Text = "Version: " + Application.ProductVersion;
            }
            else
                this.Text = "About (v." + Application.ProductVersion + ")";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:svarukal@microsoft.com");
        }
    }
}
