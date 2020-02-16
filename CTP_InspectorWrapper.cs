using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;
using OfficeTools = Microsoft.Office.Tools;
using System.Diagnostics;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Tools;

namespace RecipientHelper
{
    class CTP_InspectorWrapper
    {
        private Dictionary<Outlook.Inspector, OfficeTools.CustomTaskPane> inspectorCtpDict;

        public CTP_InspectorWrapper()
        {
            this.inspectorCtpDict = new Dictionary<Inspector, CustomTaskPane>();
        }

        public void add(Outlook.Inspector inspector, OfficeTools.CustomTaskPane ctp)
        {
            if (inspector != null)
            {
                this.inspectorCtpDict.Add(inspector, ctp);
            }
        }

        public OfficeTools.CustomTaskPane getCtpOf(Outlook.Inspector inspector)
        {
            CustomTaskPane ctp;
            if (inspector != null)
            {
                if (this.inspectorCtpDict.TryGetValue(inspector, out ctp))
                {
                    return ctp;
                }
                else
                {
                    Debug.WriteLine("Key does not exist");
                    return null;
                }
            }
            return null;
        }

        public void removeItemOf (Inspector key)
        {
            this.inspectorCtpDict.Remove(key);
        }
    }
}
