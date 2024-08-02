using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DocuSignWeb.Module.BusinessObjects
{
    [ImageName("FaxContact")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "DocuSign Envelope")]
    public class DocuSignEnvelope : BaseObject
    {
        public DocuSignEnvelope(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            ManagerRecipient = new DocuSignRecipient(Session);
            ClientRecipient = new DocuSignRecipient(Session);
            Manager = new DocuSignManager() { Name = "", EmailAddress = "" };
            ClientRecipient.Name = "";
            ClientRecipient.EmailAddress = "";
            Subject = "Test Subject";
            
            base.AfterConstruction();
        }

        private DocuSignRecipient managerRecipient;
        [ExpandObjectMembers(ExpandObjectMembers.Always)]
        [ImmediatePostData(true)]
        public DocuSignRecipient ManagerRecipient
        {
            get { return managerRecipient; }
            set { SetPropertyValue<DocuSignRecipient>("ManagerRecipient", ref managerRecipient, value); }
        }

        private DocuSignRecipient clientRecipient;
        [ExpandObjectMembers(ExpandObjectMembers.Always)]
        public DocuSignRecipient ClientRecipient
        {
            get { return clientRecipient; }
            set { SetPropertyValue<DocuSignRecipient>("ClientRecipient", ref clientRecipient, value); }
        }

        private string subject;
        public string Subject
        {
            get { return subject; }
            set { SetPropertyValue<string>("Subject", ref subject, value); }
        }
        private DocuSignTemplate template;
        [DataSourceProperty("AvailableTemplates")]
        public DocuSignTemplate Template
        {
            get { return template; }
            set { SetPropertyValue<DocuSignTemplate>("Template", ref template, value); }
        }

        private List<DocuSignTemplate> availableTemplates;
        [Browsable(false)]
        public List<DocuSignTemplate> AvailableTemplates
        {
            get
            {
                if (availableTemplates == null)
                {
                    availableTemplates = new List<DocuSignTemplate>();
                    availableTemplates.AddRange(MyDocuSignHelper.GetTemplates());
                }

                return availableTemplates;
            }
        }

        private DocuSignManager manager;
        [DataSourceProperty("AvailableManagers")]
        [ImmediatePostData(true)]
        public DocuSignManager Manager
        {
            get { return manager; }
            set { SetPropertyValue<DocuSignManager>("Manager", ref manager, value); }
        }

        private List<DocuSignManager> availableManagers;

        [Browsable(false)]
        public List<DocuSignManager> AvailableManagers
        {
            get
            {
                if (availableManagers == null)
                {
                    availableManagers = new List<DocuSignManager>();
                    availableManagers.Add(new DocuSignManager() { Name = "Wasif", EmailAddress = "wasif.3751.ali@gmail.com" });
                }
                return availableManagers;
            }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            if (!IsLoading && !IsSaving)
            {
                if (propertyName == "Manager")
                {
                    if (newValue != null)
                    {
                        ManagerRecipient.Name = (newValue as DocuSignManager).Name;
                        ManagerRecipient.EmailAddress = (newValue as DocuSignManager).EmailAddress;
                    }
                }
            }
            base.OnChanged(propertyName, oldValue, newValue);
        }
    }

    [DomainComponent]
    public class DocuSignManager : NonPersistentBaseObject
    {
        [Key(true)]
        public string Name { get; set; }
        public string EmailAddress { get; set; }
    }
}