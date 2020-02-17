namespace RecipientHelper
{
    partial class RecHlpSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecHlpSettings));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox_safeDomain = new System.Windows.Forms.ListBox();
            this.label_newDomain = new System.Windows.Forms.Label();
            this.button_removeDomain = new System.Windows.Forms.Button();
            this.button_addDomain = new System.Windows.Forms.Button();
            this.textBox_newDomain = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lab_version = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label_version_text = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.listBox_safeDomain);
            this.tabPage1.Controls.Add(this.label_newDomain);
            this.tabPage1.Controls.Add(this.button_removeDomain);
            this.tabPage1.Controls.Add(this.button_addDomain);
            this.tabPage1.Controls.Add(this.textBox_newDomain);
            this.tabPage1.Name = "tabPage1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // listBox_safeDomain
            // 
            resources.ApplyResources(this.listBox_safeDomain, "listBox_safeDomain");
            this.listBox_safeDomain.FormattingEnabled = true;
            this.listBox_safeDomain.Name = "listBox_safeDomain";
            // 
            // label_newDomain
            // 
            resources.ApplyResources(this.label_newDomain, "label_newDomain");
            this.label_newDomain.Name = "label_newDomain";
            // 
            // button_removeDomain
            // 
            resources.ApplyResources(this.button_removeDomain, "button_removeDomain");
            this.button_removeDomain.Name = "button_removeDomain";
            this.button_removeDomain.UseVisualStyleBackColor = true;
            this.button_removeDomain.Click += new System.EventHandler(this.button_removeDomain_Click);
            // 
            // button_addDomain
            // 
            resources.ApplyResources(this.button_addDomain, "button_addDomain");
            this.button_addDomain.Name = "button_addDomain";
            this.button_addDomain.UseVisualStyleBackColor = true;
            this.button_addDomain.Click += new System.EventHandler(this.button_addDomain_Click);
            // 
            // textBox_newDomain
            // 
            resources.ApplyResources(this.textBox_newDomain, "textBox_newDomain");
            this.textBox_newDomain.Name = "textBox_newDomain";
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.lab_version);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.linkLabel1);
            this.tabPage2.Controls.Add(this.label_version_text);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lab_version
            // 
            resources.ApplyResources(this.lab_version, "lab_version");
            this.lab_version.Name = "lab_version";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label_version_text
            // 
            resources.ApplyResources(this.label_version_text, "label_version_text");
            this.label_version_text.Name = "label_version_text";
            // 
            // RecHlpSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "RecHlpSettings";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label_newDomain;
        private System.Windows.Forms.Button button_removeDomain;
        private System.Windows.Forms.Button button_addDomain;
        private System.Windows.Forms.TextBox textBox_newDomain;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label_version_text;
        private System.Windows.Forms.ListBox listBox_safeDomain;
        private System.Windows.Forms.Label lab_version;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}