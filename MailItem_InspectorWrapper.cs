using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;

namespace SafeAddress
{
    class MailItem_InspectorWrapper
    {

        private Dictionary<Object, MailItem> windowMailItemDict;

        public MailItem_InspectorWrapper()
        {
            this.windowMailItemDict = new Dictionary<Object, MailItem>();
        }

        public void add(Object window, MailItem mailItem)
        {
            this.windowMailItemDict.Add(window, mailItem);
        }

        public MailItem getMailItemBy(Object window)
        {
            MailItem mailItem;
            if (this.windowMailItemDict.TryGetValue(window, out mailItem))
            {
                return mailItem;
            } else
            {
                return null;
            }
        }

        public void removeItemOf (Object window)
        {
            this.windowMailItemDict.Remove(window);
        }


    }
}
