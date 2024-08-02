using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DocuSignWeb.Module.BusinessObjects
{
    public static class ApplicationHelper
    {
        public static bool Updating { get; set; }

        public static XPObjectSpace UpdatingObjectSpace { get; set; }

        private static XafApplication application;
        public static XafApplication Application
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Session != null && HttpContext.Current.Session["Application"] != null)
                    {
                        return (XafApplication)HttpContext.Current.Session["Application"];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return application;
                }
            }
            set
            {
                if (HttpContext.Current?.Session != null)
                {
                    HttpContext.Current.Session["Application"] = value;
                }
                else
                {
                    application = value;
                }
            }
        }
        public static XPObjectSpace ObjectSpace { get; set; }

        public static IObjectSpace CreateObjectSpace()
        {
            return CreateXPObjectSpace();
        }

        private static XPObjectSpaceProvider unrestrictedProvider = null;
        public static IObjectSpace CreateUnrestrictedObjectSpace()
        {
            String csFull = "";
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                csFull = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                if (csFull.Contains("XpoProvider=MyXAFAccess;"))
                {
                    csFull = csFull.Replace("XpoProvider=MyXAFAccess;", "");
                }
            }

            if (unrestrictedProvider == null)
            {
                unrestrictedProvider = new XPObjectSpaceProvider(csFull, null);
            }
            IObjectSpace objectSpace = unrestrictedProvider.CreateObjectSpace();

            return objectSpace;
        }


        public static XPObjectSpace CreateXPObjectSpace()
        {
            if (ObjectSpace != null && !ObjectSpace.IsDisposed)
                return ObjectSpace;
            else
            {
                if (Application != null)
                    return Application.CreateObjectSpace() as XPObjectSpace;
                else return null;
            }
        }



        public static void ClearFlagWithLock(DateTime whenStarted)
        {
            //ensure that no one else can update it 
            lock (_lockObject)
            {
                if (whenStarted == lastEventAdded)
                {
                    lastEventAdded = DateTime.MinValue;
                }
            }
        }
        private static object _lockObject = new object();

        private static DateTime lastEventAdded;

        public static DateTime LastEventAdded
        {
            get { return lastEventAdded; }
            set
            {
                //ensure that no one else can update it.
                lock (_lockObject)
                {
                    lastEventAdded = value;
                }
            }
        }


        public static bool SuppressExceptions = false;

    }
}
