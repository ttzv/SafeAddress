using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using Debug = System.Diagnostics.Debug;
using RecipientHelper.Properties;

namespace RecipientHelper
{
    public partial class ThisAddIn
    {
        private String constDomain = "atal.pl";

        Outlook.Inspectors inspectors;
        Outlook.MailItem mailItem;

        private RecipientHlp_TaskPane userControl;
        private Microsoft.Office.Tools.CustomTaskPane customTaskPane;

        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            inspectors = this.Application.Inspectors;
            inspectors.NewInspector += new Outlook.InspectorsEvents_NewInspectorEventHandler(Inspectors_NewInspector);
            Console.WriteLine(inspectors);

            userControl = new RecipientHlp_TaskPane();
            
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        void Inspectors_NewInspector(Outlook.Inspector Inspector)
        {
            this.mailItem = Inspector.CurrentItem as Outlook.MailItem;
            if (mailItem != null)
            {
                mailItem.PropertyChange += MailItem_PropertyChange;
                customTaskPane = this.CustomTaskPanes.Add(userControl, " ", Inspector);
                customTaskPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionTop;
                customTaskPane.Height = 55;
                customTaskPane.Visible = false;
            }

           /* if (containsRestrictedRecipent())
            {
                customTaskPane.Visible = true;
                Debug.WriteLine(customTaskPane.Visible);
                Debug.WriteLine("External recipient");
            }*/

        }

        private void MailItem_PropertyChange(string Name)
        {
            if (containsRestrictedRecipent())
            {
                customTaskPane.Visible = true;
            } else if(!containsRestrictedRecipent() && customTaskPane.Visible)
            {
                customTaskPane.Visible = false;
            }
           // throw new NotImplementedException();
        }

        private Boolean containsRestrictedRecipent()
        {
            foreach (Outlook.Recipient recipient in this.mailItem.Recipients)
            {
                if (recipient.Address != null) {
                    Debug.WriteLine(recipient.Address);
                    String domain = recipient.Address.ToString().Split('@')[1];
                    if (!domain.Equals(constDomain))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /*private void AccessRecipientHelperForm()
        {
            WindowFormRegionCollection formRegions = Globals.FormRegions[Globals.ThisAddIn.Application.ActiveInspector()];
            this.helperForm = formRegions.FormRegion1;
        }*/

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new EventHandler(ThisAddIn_Startup);
            this.Shutdown += new EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
