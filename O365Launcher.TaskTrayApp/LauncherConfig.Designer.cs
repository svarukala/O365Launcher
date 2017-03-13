namespace O365Launcher.TaskTrayApp
{
    partial class LauncherConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LauncherConfig));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnDetect = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddTenant = new System.Windows.Forms.Button();
            this.txtFriendlyName = new System.Windows.Forms.TextBox();
            this.txtTenantPrefix = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxTenants = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCustomLinkName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAddCustomLink = new System.Windows.Forms.Button();
            this.txtCustomLink = new System.Windows.Forms.TextBox();
            this.comboBoxCustomLinks = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnSaveTenantLinks = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkedListBoxAdminCenters = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxFUL = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxTenants = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSaveBrowsers = new System.Windows.Forms.Button();
            this.checkedListBoxBrowsers = new System.Windows.Forms.CheckedListBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblImportFile = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(781, 724);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox8);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.listBoxTenants);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(765, 677);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tenants";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnDetect);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Location = new System.Drawing.Point(17, 495);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(742, 176);
            this.groupBox8.TabIndex = 3;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Auto-detect tenants";
            // 
            // btnDetect
            // 
            this.btnDetect.Location = new System.Drawing.Point(553, 114);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(168, 41);
            this.btnDetect.TabIndex = 1;
            this.btnDetect.Text = "Auto-Detect";
            this.btnDetect.UseVisualStyleBackColor = true;
            this.btnDetect.Click += new System.EventHandler(this.btnDetect_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 44);
            this.label11.MaximumSize = new System.Drawing.Size(600, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(596, 75);
            this.label11.TabIndex = 0;
            this.label11.Text = "Use this feature to auto-detect the tenants based on the Chrome browser history. " +
    "Chrome browser must be closed for this to work.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddTenant);
            this.groupBox1.Controls.Add(this.txtFriendlyName);
            this.groupBox1.Controls.Add(this.txtTenantPrefix);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(17, 264);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(742, 215);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add New Tenant";
            // 
            // btnAddTenant
            // 
            this.btnAddTenant.Location = new System.Drawing.Point(553, 147);
            this.btnAddTenant.Name = "btnAddTenant";
            this.btnAddTenant.Size = new System.Drawing.Size(168, 43);
            this.btnAddTenant.TabIndex = 4;
            this.btnAddTenant.Text = "Add Tenant";
            this.btnAddTenant.UseVisualStyleBackColor = true;
            this.btnAddTenant.Click += new System.EventHandler(this.btnAddTenant_Click);
            // 
            // txtFriendlyName
            // 
            this.txtFriendlyName.Location = new System.Drawing.Point(159, 95);
            this.txtFriendlyName.Name = "txtFriendlyName";
            this.txtFriendlyName.Size = new System.Drawing.Size(562, 31);
            this.txtFriendlyName.TabIndex = 3;
            // 
            // txtTenantPrefix
            // 
            this.txtTenantPrefix.Location = new System.Drawing.Point(159, 51);
            this.txtTenantPrefix.Name = "txtTenantPrefix";
            this.txtTenantPrefix.Size = new System.Drawing.Size(562, 31);
            this.txtTenantPrefix.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "Friendly Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tenant Name";
            // 
            // listBoxTenants
            // 
            this.listBoxTenants.FormattingEnabled = true;
            this.listBoxTenants.ItemHeight = 25;
            this.listBoxTenants.Location = new System.Drawing.Point(176, 53);
            this.listBoxTenants.Name = "listBoxTenants";
            this.listBoxTenants.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxTenants.Size = new System.Drawing.Size(562, 179);
            this.listBoxTenants.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tenants";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(765, 677);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tenants Config";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox7);
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.comboBoxTenants);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(759, 671);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tenant Configuration";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.txtCustomLinkName);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Controls.Add(this.btnAddCustomLink);
            this.groupBox7.Controls.Add(this.txtCustomLink);
            this.groupBox7.Controls.Add(this.comboBoxCustomLinks);
            this.groupBox7.Location = new System.Drawing.Point(6, 428);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(737, 231);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Custom Links";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 25);
            this.label10.TabIndex = 10;
            this.label10.Text = "Links added";
            // 
            // txtCustomLinkName
            // 
            this.txtCustomLinkName.Location = new System.Drawing.Point(190, 131);
            this.txtCustomLinkName.Name = "txtCustomLinkName";
            this.txtCustomLinkName.Size = new System.Drawing.Size(529, 31);
            this.txtCustomLinkName.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 25);
            this.label9.TabIndex = 8;
            this.label9.Text = "Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 25);
            this.label8.TabIndex = 7;
            this.label8.Text = "Custom Link";
            // 
            // btnAddCustomLink
            // 
            this.btnAddCustomLink.Location = new System.Drawing.Point(577, 182);
            this.btnAddCustomLink.Name = "btnAddCustomLink";
            this.btnAddCustomLink.Size = new System.Drawing.Size(142, 45);
            this.btnAddCustomLink.TabIndex = 7;
            this.btnAddCustomLink.Text = "Add Link";
            this.btnAddCustomLink.UseVisualStyleBackColor = true;
            this.btnAddCustomLink.Click += new System.EventHandler(this.btnAddCustomLink_Click);
            // 
            // txtCustomLink
            // 
            this.txtCustomLink.Location = new System.Drawing.Point(190, 81);
            this.txtCustomLink.Name = "txtCustomLink";
            this.txtCustomLink.Size = new System.Drawing.Size(529, 31);
            this.txtCustomLink.TabIndex = 5;
            // 
            // comboBoxCustomLinks
            // 
            this.comboBoxCustomLinks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCustomLinks.FormattingEnabled = true;
            this.comboBoxCustomLinks.Location = new System.Drawing.Point(190, 30);
            this.comboBoxCustomLinks.MaxDropDownItems = 20;
            this.comboBoxCustomLinks.Name = "comboBoxCustomLinks";
            this.comboBoxCustomLinks.Size = new System.Drawing.Size(529, 33);
            this.comboBoxCustomLinks.TabIndex = 4;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnSaveTenantLinks);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.checkedListBoxAdminCenters);
            this.groupBox6.Controls.Add(this.checkedListBoxFUL);
            this.groupBox6.Location = new System.Drawing.Point(6, 88);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(737, 334);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Tenant Links";
            // 
            // btnSaveTenantLinks
            // 
            this.btnSaveTenantLinks.Location = new System.Drawing.Point(577, 287);
            this.btnSaveTenantLinks.Name = "btnSaveTenantLinks";
            this.btnSaveTenantLinks.Size = new System.Drawing.Size(142, 41);
            this.btnSaveTenantLinks.TabIndex = 6;
            this.btnSaveTenantLinks.Text = "Save";
            this.btnSaveTenantLinks.UseVisualStyleBackColor = true;
            this.btnSaveTenantLinks.Click += new System.EventHandler(this.btnSaveTenantLinks_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(402, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(175, 25);
            this.label7.TabIndex = 5;
            this.label7.Text = "Freq. Used Links";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(153, 25);
            this.label6.TabIndex = 4;
            this.label6.Text = "Admin Centers";
            // 
            // checkedListBoxAdminCenters
            // 
            this.checkedListBoxAdminCenters.FormattingEnabled = true;
            this.checkedListBoxAdminCenters.Location = new System.Drawing.Point(14, 62);
            this.checkedListBoxAdminCenters.Name = "checkedListBoxAdminCenters";
            this.checkedListBoxAdminCenters.Size = new System.Drawing.Size(307, 212);
            this.checkedListBoxAdminCenters.TabIndex = 2;
            // 
            // checkedListBoxFUL
            // 
            this.checkedListBoxFUL.FormattingEnabled = true;
            this.checkedListBoxFUL.Location = new System.Drawing.Point(407, 62);
            this.checkedListBoxFUL.Name = "checkedListBoxFUL";
            this.checkedListBoxFUL.Size = new System.Drawing.Size(312, 212);
            this.checkedListBoxFUL.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 25);
            this.label4.TabIndex = 1;
            this.label4.Text = "Select Tenant";
            // 
            // comboBoxTenants
            // 
            this.comboBoxTenants.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTenants.FormattingEnabled = true;
            this.comboBoxTenants.Location = new System.Drawing.Point(177, 41);
            this.comboBoxTenants.MaxDropDownItems = 20;
            this.comboBoxTenants.Name = "comboBoxTenants";
            this.comboBoxTenants.Size = new System.Drawing.Size(548, 33);
            this.comboBoxTenants.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Location = new System.Drawing.Point(8, 39);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(765, 677);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Browsers";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.btnSaveBrowsers);
            this.groupBox2.Controls.Add(this.checkedListBoxBrowsers);
            this.groupBox2.Location = new System.Drawing.Point(4, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(757, 505);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Browsers";
            // 
            // btnSaveBrowsers
            // 
            this.btnSaveBrowsers.Location = new System.Drawing.Point(356, 56);
            this.btnSaveBrowsers.Name = "btnSaveBrowsers";
            this.btnSaveBrowsers.Size = new System.Drawing.Size(135, 57);
            this.btnSaveBrowsers.TabIndex = 1;
            this.btnSaveBrowsers.Text = "Save";
            this.btnSaveBrowsers.UseVisualStyleBackColor = true;
            this.btnSaveBrowsers.Click += new System.EventHandler(this.btnSaveBrowsers_Click);
            // 
            // checkedListBoxBrowsers
            // 
            this.checkedListBoxBrowsers.FormattingEnabled = true;
            this.checkedListBoxBrowsers.Location = new System.Drawing.Point(30, 56);
            this.checkedListBoxBrowsers.Name = "checkedListBoxBrowsers";
            this.checkedListBoxBrowsers.Size = new System.Drawing.Size(301, 316);
            this.checkedListBoxBrowsers.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox5);
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Location = new System.Drawing.Point(8, 39);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(765, 677);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Import/Export Config";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button3);
            this.groupBox5.Location = new System.Drawing.Point(3, 243);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(758, 201);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Export Config";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(18, 42);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(173, 41);
            this.button3.TabIndex = 0;
            this.button3.Text = "Export";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblImportFile);
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(4, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(757, 201);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Import Config";
            // 
            // lblImportFile
            // 
            this.lblImportFile.AutoSize = true;
            this.lblImportFile.Location = new System.Drawing.Point(12, 136);
            this.lblImportFile.Name = "lblImportFile";
            this.lblImportFile.Size = new System.Drawing.Size(0, 25);
            this.lblImportFile.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 85);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(185, 44);
            this.button2.TabIndex = 1;
            this.button2.Text = "Browse...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(183, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "Browse to the file:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Title = "Export configruation to XML file";
            // 
            // LauncherConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 724);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LauncherConfig";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.LauncherConfig_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddTenant;
        private System.Windows.Forms.TextBox txtFriendlyName;
        private System.Windows.Forms.TextBox txtTenantPrefix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxTenants;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxTenants;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox checkedListBoxBrowsers;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblImportFile;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnSaveBrowsers;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnAddCustomLink;
        private System.Windows.Forms.TextBox txtCustomLink;
        private System.Windows.Forms.ComboBox comboBoxCustomLinks;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckedListBox checkedListBoxAdminCenters;
        private System.Windows.Forms.CheckedListBox checkedListBoxFUL;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCustomLinkName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSaveTenantLinks;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.Label label11;
    }
}