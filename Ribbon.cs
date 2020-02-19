using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace SafeAddress
{
    public partial class Ribbon
    {
        private RecHlpSettings recHlpSettings;

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            recHlpSettings = new RecHlpSettings();
            recHlpSettings.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            recHlpSettings.ShowDialog();
        }
    }
}
