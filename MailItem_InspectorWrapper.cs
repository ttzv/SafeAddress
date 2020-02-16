using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;

namespace RecipientHelper
{
    class MailItem_InspectorWrapper
    {

        private Dictionary<Inspector, MailItem> inspectorMailItemDict;

        public MailItem_InspectorWrapper()
        {
            this.inspectorMailItemDict = new Dictionary<Inspector, MailItem>();
        }

        public void add(Inspector key, MailItem value)
        {
            this.inspectorMailItemDict.Add(key, value);
        }

        public MailItem getMailItemBy(Inspector inspector)
        {
            MailItem mailItem;
            if (this.inspectorMailItemDict.TryGetValue(inspector, out mailItem))
            {
                return mailItem;
            } else
            {
                return null;
            }
        }

        public void removeItemOf (Inspector key)
        {
            this.inspectorMailItemDict.Remove(key);
        }


    }
}
