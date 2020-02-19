using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Configuration;

namespace SafeAddress
{
    public partial class RecHlpSettings : Form
    {

        private ArrayList safeDomains;
        private ArrayList userSafeDomains;
        public RecHlpSettings()
        {
            InitializeComponent();
            this.safeDomains = new ArrayList(ConfigurationManager.AppSettings["safeDomains"].Split(','));
            this.userSafeDomains = new ArrayList(Properties.Settings.Default.safeDomains.Split(','));
            if (this.safeDomains.Count > 0)
            {
                this.listBox_safeDomain.Items.AddRange(safeDomains.ToArray());
            }
            if(this.userSafeDomains[0].ToString().Length == 0)
            {
                this.userSafeDomains.RemoveAt(0);
            }
            if (this.userSafeDomains.Count > 0) 
            {
                this.listBox_safeDomain.Items.AddRange(userSafeDomains.ToArray());
            }
        }

        private void button_addDomain_Click(object sender, EventArgs e)
        {
            string newDomain = this.textBox_newDomain.Text;
            if(newDomain != null && newDomain.Length != 0 
                && !safeDomains.Contains(newDomain) 
                && !this.listBox_safeDomain.Items.Contains(newDomain)
                && !newDomain.Contains(","))
            {
                this.listBox_safeDomain.Items.Add(newDomain.Trim().ToLower());
                this.userSafeDomains.Add(newDomain);
                saveSafeDomainsProperty();
                this.textBox_newDomain.Clear();
            }
            
        }

        private void button_removeDomain_Click(object sender, EventArgs e)
        {
            string selectedItem = this.listBox_safeDomain.SelectedItem as string;
            if (selectedItem != null 
                && !this.safeDomains.Contains(selectedItem) 
                && this.userSafeDomains.Contains(selectedItem)){
                this.listBox_safeDomain.Items.Remove(this.listBox_safeDomain.SelectedItem);
                this.userSafeDomains.Remove(selectedItem);
                saveSafeDomainsProperty();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ttzv/SafeAddress");
        }

        private void saveSafeDomainsProperty()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(string safeDomain in this.userSafeDomains)
            {
                stringBuilder.Append(safeDomain);
                stringBuilder.Append(",");
            }
            if (stringBuilder.Length > 2)
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                Properties.Settings.Default.safeDomains = stringBuilder.ToString();
            } else
            {
                Properties.Settings.Default.safeDomains = "";
            }
            
            Properties.Settings.Default.Save();

        }
    }
}
