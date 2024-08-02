using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace DocuSignWeb.Module.BusinessObjects
{
    [DefaultClassOptions]
       public class Manager : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Manager(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                SetPropertyValue(nameof(FirstName), ref firstName, value);
            }
        }
        string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                SetPropertyValue(nameof(LastName), ref lastName, value);
            }
        }
        int age;
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                SetPropertyValue(nameof(Age), ref age, value);
            }
        }
        DateTime _birthDate;

        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                SetPropertyValue(nameof(BirthDate), ref _birthDate, value);
            }
        }


    }
}