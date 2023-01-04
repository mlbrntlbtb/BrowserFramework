namespace TestRunner
{
    partial class About
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelSubProductName = new System.Windows.Forms.Label();
            this.lblApplicationValue = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.lstDrivers = new System.Windows.Forms.ListBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblTargetApplication = new System.Windows.Forms.Label();
            this.lnkUpdateLicense = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.62136F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.17476F));
            this.tableLayoutPanel.Controls.Add(this.labelProductName, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelSubProductName, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.lblApplicationValue, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.okButton, 0, 8);
            this.tableLayoutPanel.Controls.Add(this.lblExpiry, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.labelVersion, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.lstDrivers, 0, 6);
            this.tableLayoutPanel.Controls.Add(this.lblDescription, 0, 7);
            this.tableLayoutPanel.Controls.Add(this.lblTargetApplication, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.lnkUpdateLicense, 0, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(12, 11);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 9;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.101695F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.779661F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.50847F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.442177F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.88435F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.743243F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.14865F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.56705F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(515, 392);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // labelProductName
            // 
            this.tableLayoutPanel.SetColumnSpan(this.labelProductName, 3);
            this.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProductName.ForeColor = System.Drawing.Color.MidnightBlue;
            this.labelProductName.Location = new System.Drawing.Point(8, 0);
            this.labelProductName.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
            this.labelProductName.MaximumSize = new System.Drawing.Size(0, 21);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(503, 21);
            this.labelProductName.TabIndex = 19;
            this.labelProductName.Text = "Product Name";
            this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSubProductName
            // 
            this.tableLayoutPanel.SetColumnSpan(this.labelSubProductName, 2);
            this.labelSubProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSubProductName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubProductName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelSubProductName.Location = new System.Drawing.Point(8, 140);
            this.labelSubProductName.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
            this.labelSubProductName.MaximumSize = new System.Drawing.Size(0, 21);
            this.labelSubProductName.Name = "labelSubProductName";
            this.labelSubProductName.Size = new System.Drawing.Size(326, 20);
            this.labelSubProductName.TabIndex = 21;
            this.labelSubProductName.Text = "Installed Selenium Drivers";
            this.labelSubProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblApplicationValue
            // 
            this.lblApplicationValue.AutoSize = true;
            this.lblApplicationValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblApplicationValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationValue.Location = new System.Drawing.Point(8, 101);
            this.lblApplicationValue.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
            this.lblApplicationValue.Name = "lblApplicationValue";
            this.lblApplicationValue.Size = new System.Drawing.Size(159, 39);
            this.lblApplicationValue.TabIndex = 26;
            this.lblApplicationValue.Text = "APP";
            // 
            // okButton
            // 
            this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.okButton.BackColor = System.Drawing.Color.SteelBlue;
            this.tableLayoutPanel.SetColumnSpan(this.okButton, 3);
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.ForeColor = System.Drawing.Color.White;
            this.okButton.Location = new System.Drawing.Point(207, 366);
            this.okButton.Margin = new System.Windows.Forms.Padding(5);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 21);
            this.okButton.TabIndex = 24;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = false;
            // 
            // lblExpiry
            // 
            this.lblExpiry.AutoSize = true;
            this.tableLayoutPanel.SetColumnSpan(this.lblExpiry, 2);
            this.lblExpiry.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblExpiry.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpiry.Location = new System.Drawing.Point(8, 45);
            this.lblExpiry.Margin = new System.Windows.Forms.Padding(8, 0, 5, 0);
            this.lblExpiry.Name = "lblExpiry";
            this.lblExpiry.Size = new System.Drawing.Size(195, 37);
            this.lblExpiry.TabIndex = 31;
            this.lblExpiry.Text = "License key applied: Expires in ";
            // 
            // labelVersion
            // 
            this.tableLayoutPanel.SetColumnSpan(this.labelVersion, 3);
            this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.Location = new System.Drawing.Point(8, 21);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
            this.labelVersion.MaximumSize = new System.Drawing.Size(0, 21);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(503, 21);
            this.labelVersion.TabIndex = 30;
            this.labelVersion.Text = "Version";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lstDrivers
            // 
            this.tableLayoutPanel.SetColumnSpan(this.lstDrivers, 3);
            this.lstDrivers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDrivers.FormattingEnabled = true;
            this.lstDrivers.ItemHeight = 16;
            this.lstDrivers.Location = new System.Drawing.Point(8, 164);
            this.lstDrivers.Margin = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.lstDrivers.Name = "lstDrivers";
            this.lstDrivers.ScrollAlwaysVisible = true;
            this.lstDrivers.Size = new System.Drawing.Size(499, 121);
            this.lstDrivers.TabIndex = 32;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.tableLayoutPanel.SetColumnSpan(this.lblDescription, 3);
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(5, 289);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(38, 14);
            this.lblDescription.TabIndex = 33;
            this.lblDescription.Text = "label1";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTargetApplication
            // 
            this.lblTargetApplication.AutoSize = true;
            this.lblTargetApplication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTargetApplication.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTargetApplication.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTargetApplication.Location = new System.Drawing.Point(8, 82);
            this.lblTargetApplication.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
            this.lblTargetApplication.Name = "lblTargetApplication";
            this.lblTargetApplication.Size = new System.Drawing.Size(159, 19);
            this.lblTargetApplication.TabIndex = 25;
            this.lblTargetApplication.Text = "Target Application:";
            // 
            // lnkUpdateLicense
            // 
            this.lnkUpdateLicense.AutoSize = true;
            this.lnkUpdateLicense.Dock = System.Windows.Forms.DockStyle.Right;
            this.lnkUpdateLicense.Location = new System.Drawing.Point(383, 45);
            this.lnkUpdateLicense.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkUpdateLicense.Name = "lnkUpdateLicense";
            this.lnkUpdateLicense.Size = new System.Drawing.Size(128, 37);
            this.lnkUpdateLicense.TabIndex = 34;
            this.lnkUpdateLicense.TabStop = true;
            this.lnkUpdateLicense.Text = "Update license key";
            this.lnkUpdateLicense.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lnkUpdateLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpdateLicense_LinkClicked);
            // 
            // About
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(539, 414);
            this.Controls.Add(this.tableLayoutPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.Padding = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelSubProductName;
        private System.Windows.Forms.Label lblTargetApplication;
        private System.Windows.Forms.Label lblApplicationValue;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ListBox lstDrivers;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.LinkLabel lnkUpdateLicense;
    }
}
