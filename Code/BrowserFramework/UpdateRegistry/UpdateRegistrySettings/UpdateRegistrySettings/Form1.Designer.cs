namespace UpdateRegistrySettings
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnUpdateReg = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUpdateReg
            // 
            this.btnUpdateReg.Location = new System.Drawing.Point(92, 69);
            this.btnUpdateReg.Name = "btnUpdateReg";
            this.btnUpdateReg.Size = new System.Drawing.Size(115, 23);
            this.btnUpdateReg.TabIndex = 0;
            this.btnUpdateReg.Text = "Update Registry";
            this.btnUpdateReg.UseVisualStyleBackColor = true;
            this.btnUpdateReg.Click += new System.EventHandler(this.btnUpdateReg_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 53);
            this.label1.TabIndex = 1;
            this.label1.Text = "Running this tool will update the Enable Protected Mode setting in Intenet Option" +
    "s and the Zoom level of your IE browser to conform to the standards of the autom" +
    "ation framework.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 113);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUpdateReg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Update Registry";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUpdateReg;
        private System.Windows.Forms.Label label1;
    }
}

