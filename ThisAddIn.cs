using System;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using Debug = System.Diagnostics.Debug;
using RecipientHelper.Properties;
using Microsoft.Office.Tools;
using Microsoft.Office.Interop.Outlook;
using System.Configuration;
using System.Collections;

namespace RecipientHelper
{
    public partial class ThisAddIn
    {
        private ArrayList safeDomains;

        Outlook.Inspectors inspectors;
        Outlook.Explorer currentExplorer = null;

        private CTP_InspectorWrapper ctpInspectorWrapper;
        private MailItem_InspectorWrapper mailItemInspectorWrapper;

        private bool inlineResponseActive;
        private CustomTaskPane ctpInExplorer;
        private MailItem mailItemInExplorer;

        private string rechlp_titleBar_title;


        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            this.safeDomains = new ArrayList(ConfigurationManager.AppSettings["safeDomains"].Split(','));

            ctpInspectorWrapper = new CTP_InspectorWrapper();
            mailItemInspectorWrapper = new MailItem_InspectorWrapper();

            currentExplorer = this.Application.ActiveExplorer();
            currentExplorer.InlineResponse += CurrentExplorer_InlineResponse;
            currentExplorer.InlineResponseClose += CurrentExplorer_InlineResponseClose;

            inspectors = this.Application.Inspectors;
            inspectors.NewInspector += new Outlook.InspectorsEvents_NewInspectorEventHandler(Inspectors_NewInspector);
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        private void toggleInspectorCtp(Inspector inspector)
        {
            CustomTaskPane ctp = this.ctpInspectorWrapper.getCtpOf(inspector);
            MailItem mailItem = inspector.CurrentItem as MailItem;
            
            if (containsRestrictedRecipent(mailItem.Recipients) && ctp != null)
            {
                ctp.Visible = true;
            }
            else if (!containsRestrictedRecipent(mailItem.Recipients) && ctp != null && ctp.Visible)
            {
                ctp.Visible = false;
            }
        }

        private void toggleExplorerCtp(MailItem mailItem)
        {
            if (mailItem != null && this.ctpInExplorer != null)
            {
                if (containsRestrictedRecipent(mailItem.Recipients) && this.ctpInExplorer != null)
                {
                    this.ctpInExplorer.Visible = true;
                }
                else if (!containsRestrictedRecipent(mailItem.Recipients) && this.ctpInExplorer != null && this.ctpInExplorer.Visible)
                {
                    this.ctpInExplorer.Visible = false;
                }
            }
        }

        void Inspectors_NewInspector(Outlook.Inspector Inspector)
        {
            InspectorEvents_10_Event inspectorEvents;
            inspectorEvents = (InspectorEvents_10_Event)Inspector;
            inspectorEvents.Activate += _inspectorEvents_Activate;
            inspectorEvents.Deactivate += _inspectorEvents_Deactivate;
            inspectorEvents.Close += _inspectorEvents_Close;

            Debug.WriteLine("Inspector Opened");
            MailItem mailItem = Inspector.CurrentItem as MailItem;
            if (mailItem != null)
            {
                mailItem.PropertyChange += MailItem_PropertyChange;
                addCustomTaskPaneToInspector(Inspector);
                toggleInspectorCtp(Inspector);
            }      
        }

        private void _inspectorEvents_Activate()
        {
            Debug.WriteLine("Inspector activated");
        }

        private void _inspectorEvents_Deactivate()
        {
            Debug.WriteLine("Inspector deactivated");
            //this.currentInspector = this.Application.ActiveInspector();
        }

        private void _inspectorEvents_Close()
        {
            Inspector currentInspector = this.Application.ActiveInspector();
            CustomTaskPane currentTaskPane = this.ctpInspectorWrapper.getCtpOf(currentInspector);
            this.CustomTaskPanes.Remove(currentTaskPane);
            this.ctpInspectorWrapper.removeItemOf(currentInspector);
            this.mailItemInspectorWrapper.removeItemOf(currentInspector);

            Debug.WriteLine("Inspector closed");
        }

        private void CurrentExplorer_InlineResponse(object Item)
        {
            this.inlineResponseActive = true;
            this.mailItemInExplorer = Item as MailItem;

            this.mailItemInExplorer.PropertyChange += MailItem_PropertyChange;
            addCustomTaskPaneToExplorer();
            toggleExplorerCtp(this.mailItemInExplorer);
            Debug.WriteLine("Opened Item in reading pane");
        }

       private void CurrentExplorer_InlineResponseClose()
        {
            this.inlineResponseActive = false;
            this.CustomTaskPanes.Remove(this.ctpInExplorer);
            this.ctpInExplorer = null;
        }

        private void MailItem_PropertyChange(string Name)
        {
            Inspector inspector = this.Application.ActiveInspector();
            if (inspector == null && this.inlineResponseActive)
            {
                addCustomTaskPaneToExplorer();
                toggleExplorerCtp(this.mailItemInExplorer);
            } else
            {
                addCustomTaskPaneToInspector(inspector);
                toggleInspectorCtp(inspector);
            }
        }

        private Boolean containsRestrictedRecipent(Recipients recipients)
        {
            foreach (Recipient recipient in recipients)
            {
                if (recipient.Address != null) {
                    Debug.WriteLine(recipient.Address);
                    String domain = recipient.Address.ToString().Split('@')[1];
                    if (!this.safeDomains.Contains(domain))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void addCustomTaskPaneToInspector(Inspector inspector)
        {
             //add new ctp only if there is none added already
            if (inspector != null && this.ctpInspectorWrapper.getCtpOf(inspector) == null)
            {
                RecipientHlp_TaskPane userControl = new RecipientHlp_TaskPane();
                CustomTaskPane customTaskPane = this.CustomTaskPanes.Add(userControl, Strings.rechlp_titleBar_title, inspector);
                this.ctpInspectorWrapper.add(inspector, customTaskPane);
                this.mailItemInspectorWrapper.add(inspector, (MailItem)inspector.CurrentItem);
                customTaskPane = this.ctpInspectorWrapper.getCtpOf(inspector);
                Debug.WriteLine("Elements: " + this.CustomTaskPanes.Count);

                ctpConfig(customTaskPane, userControl);
            }
        }

        private void addCustomTaskPaneToExplorer()
        {
            if (inlineResponseActive && this.ctpInExplorer == null)
            {
                RecipientHlp_TaskPane userControl = new RecipientHlp_TaskPane();
                this.ctpInExplorer = this.CustomTaskPanes.Add(userControl, Strings.rechlp_titleBar_title);
                ctpConfig(this.ctpInExplorer, userControl);
            }
        }

        private void Control_SizeChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Height: ");
        }

        /// <summary>
        /// Returns CustomTaskPane Title Bar Height for currently running Outlook version.
        /// </summary>
        /// <returns>CustomTaskPane Title Bar Height for currently running Outlook version.</returns> 
        private int getTitleHeightForCurrVersion()
        {
            int height = 0;

            String version = Globals.ThisAddIn.Application.Version;
            Debug.WriteLine(version);
            version = version.Substring(0, 2);
            Debug.WriteLine(version);
            switch (version)
            {
                case "15":
                    height = 24;
                    break;
                case "16":
                    height = 40;
                    break;
                default:
                    height = 0;
                    break;
            }

            return height;
        }

        private void ctpConfig(CustomTaskPane customTaskPane, RecipientHlp_TaskPane userControl)
        {
            if (customTaskPane != null)
            {
                customTaskPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionTop;
                customTaskPane.Height = userControl.getMaxHeight() + getTitleHeightForCurrVersion();
                customTaskPane.Control.SizeChanged += Control_SizeChanged;
                customTaskPane.Visible = false;
            }
        }

        

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
