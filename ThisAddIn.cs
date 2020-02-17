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

        Inspectors inspectors;
        Explorers explorers;

        private CTP_InspectorWrapper ctpWindowWrapper;
        private MailItem_InspectorWrapper mailItemWindowWrapper;

        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            this.safeDomains = new ArrayList(ConfigurationManager.AppSettings["safeDomains"].Split(','));
            ArrayList userSafeDomains = new ArrayList(Properties.Settings.Default.safeDomains.Split(','));
            this.safeDomains.AddRange(userSafeDomains);

            ctpWindowWrapper = new CTP_InspectorWrapper();
            mailItemWindowWrapper = new MailItem_InspectorWrapper();

            explorers = this.Application.Explorers;
            explorers.NewExplorer += Explorers_NewExplorer;

            Explorer currentExplorer = this.Application.ActiveExplorer();
            currentExplorer.InlineResponse += CurrentExplorer_InlineResponse;
            currentExplorer.InlineResponseClose += CurrentExplorer_InlineResponseClose;

            inspectors = this.Application.Inspectors;
            inspectors.NewInspector += new InspectorsEvents_NewInspectorEventHandler(Inspectors_NewInspector);
        }

        private void Explorers_NewExplorer(Explorer Explorer)
        {
            Debug.WriteLine("Opened new explorer");
            Explorer.InlineResponse += CurrentExplorer_InlineResponse;
            Explorer.InlineResponseClose += CurrentExplorer_InlineResponseClose;
            ExplorerEvents_10_Event explorerEvents = Explorer;
            explorerEvents.Close += ExplorerEvents_Close;
        }

        private void ExplorerEvents_Close()
        {
            Explorer explorer = this.Application.ActiveExplorer();
            if (explorer != null)
            {
                explorer.InlineResponse -= CurrentExplorer_InlineResponse;
                explorer.InlineResponseClose -= CurrentExplorer_InlineResponseClose;
                ExplorerEvents_10_Event explorerEvents = explorer;
                explorerEvents.Close -= ExplorerEvents_Close;
            }
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        private void toggleWindowCtp(Object window)
        {
            CustomTaskPane ctp = this.ctpWindowWrapper.getCtpOf(window);
            MailItem mailItem = this.mailItemWindowWrapper.getMailItemBy(window);
            
            if (containsRestrictedRecipent(mailItem.Recipients) && ctp != null)
            {
                ctp.Visible = true;
            }
            else if (!containsRestrictedRecipent(mailItem.Recipients) && ctp != null && ctp.Visible)
            {
                ctp.Visible = false;
            }
        }

        void Inspectors_NewInspector(Inspector Inspector)
        {
            InspectorEvents_10_Event inspectorEvents;
            inspectorEvents = (InspectorEvents_10_Event)Inspector;
            inspectorEvents.Close += _inspectorEvents_Close;

            Debug.WriteLine("Inspector Opened");
            MailItem mailItem = Inspector.CurrentItem as MailItem;
            if (mailItem != null)
            {
                mailItem.PropertyChange += MailItem_PropertyChange;
                addCtpToWindow(Inspector, mailItem);
                toggleWindowCtp(Inspector);
            }      
        }

        private void _inspectorEvents_Close()
        {
            Inspector currentInspector = this.Application.ActiveInspector();
            if (currentInspector != null)
            {
                CustomTaskPane currentTaskPane = this.ctpWindowWrapper.getCtpOf(currentInspector);
                MailItem currentMailItem = this.mailItemWindowWrapper.getMailItemBy(currentInspector);
                if (currentTaskPane != null && currentMailItem != null)
                {
                    this.CustomTaskPanes.Remove(currentTaskPane);
                    this.ctpWindowWrapper.removeItemOf(currentInspector);
                    this.mailItemWindowWrapper.removeItemOf(currentInspector);

                    InspectorEvents_10_Event currentInspectorEvents = (InspectorEvents_10_Event)currentInspector;                    
                    currentInspectorEvents.Close -= _inspectorEvents_Close;
                    currentMailItem.PropertyChange -= MailItem_PropertyChange;

                    Debug.WriteLine("Inspector closed");
                }
            }
        }

        private void CurrentExplorer_InlineResponse(object Item)
        {
            MailItem mailItemInExplorer = Item as MailItem;
            Explorer currentExplorer = this.Application.ActiveExplorer();

            mailItemInExplorer.PropertyChange += MailItem_PropertyChange;
            addCtpToWindow(currentExplorer, mailItemInExplorer);
            toggleWindowCtp(currentExplorer);
            Debug.WriteLine("Opened Item in reading pane");
        }

       private void CurrentExplorer_InlineResponseClose()
        {
            Explorer explorer = this.Application.ActiveExplorer();
            CustomTaskPane customTaskPane = this.ctpWindowWrapper.getCtpOf(explorer);
            this.CustomTaskPanes.Remove(customTaskPane);
            this.ctpWindowWrapper.removeItemOf(explorer);
            this.mailItemWindowWrapper.removeItemOf(explorer);
        }

        private void MailItem_PropertyChange(string Name)
        {
            Inspector inspector = this.Application.ActiveInspector();
            Explorer explorer = this.Application.ActiveExplorer();
            if (inspector == null)
            {
                toggleWindowCtp(explorer);
            } else
            {
                toggleWindowCtp(inspector);
            }
        }

        private Boolean containsRestrictedRecipent(Recipients recipients)
        {
           foreach (Recipient recipient in recipients)
            {
                string host;
                try
                {
                    host = new System.Net.Mail.MailAddress(recipient.Address).Host;
                } catch (FormatException)
                {
                    host = null;
                    return false;
                } catch (ArgumentNullException)
                {
                    host = null;
                    return false;
                }
                if (!this.safeDomains.Contains(host))
                {
                    return true;
                }
            }
            return false;
        }

        private void addCtpToWindow(Object window, MailItem mailItem)
        {
             //add new ctp only if there is none added already
            if (window != null && this.ctpWindowWrapper.getCtpOf(window) == null)
            {
                RecipientHlp_TaskPane userControl = new RecipientHlp_TaskPane();
                CustomTaskPane customTaskPane = this.CustomTaskPanes.Add(userControl, Strings.rechlp_titleBar_title, window);
                this.ctpWindowWrapper.add(window, customTaskPane);
                this.mailItemWindowWrapper.add(window, mailItem);

                ctpConfig(customTaskPane, userControl);
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
