using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DocuSignWeb.Module.BusinessObjects
{
    [ImageName("FaxContact")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "DocuSign Recipient")]
    public class DocuSignRecipient : BaseObject
    {
        public DocuSignRecipient(Session session) : base(session) { }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetPropertyValue<string>("Name", ref name, value); }
        }

        private string emailAddress;
        public string EmailAddress
        {
            get { return emailAddress; }
            set { SetPropertyValue<string>("EmailAddress", ref emailAddress, value); }
        }
    }
}