using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SafeAddress.Properties
{
    public partial class RecipientHlp_TaskPane : UserControl
    {
        public RecipientHlp_TaskPane()
        {
            InitializeComponent();

        }

        public int getMaxHeight()
        {
            int maxHeight = this.Height;
            foreach (Control control in this.Controls)
            {
                if (control.Height > maxHeight)
                    maxHeight = control.Height;
            }
            return maxHeight;
        }
    }
}
