using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuSignWeb.Module.BusinessObjects
{
    [DomainComponent]
    public class DocuSignTemplate : NonPersistentBaseObject
    {
        public string Name { get; set; }

        [Key]
        public string Id { get; set; }

        public string Description { get; set; }
    }
}
