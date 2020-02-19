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

namespace SafeAddress
{
    class CTP_InspectorWrapper
    {
        private Dictionary<Object, CustomTaskPane> windowCtpDict;

        public CTP_InspectorWrapper()
        {
            this.windowCtpDict = new Dictionary<Object, CustomTaskPane>();
        }

        public void add(Object window, CustomTaskPane ctp)
        {
            if (window != null)
            {
                this.windowCtpDict.Add(window, ctp);
            }
        }

        public CustomTaskPane getCtpOf(Object window)
        {
            CustomTaskPane ctp;
            if (window != null)
            {
                if (this.windowCtpDict.TryGetValue(window, out ctp))
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

        public void removeItemOf (Object window)
        {
            this.windowCtpDict.Remove(window);
        }
    }
}
