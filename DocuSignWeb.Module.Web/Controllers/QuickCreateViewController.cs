using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DocuSign.eSign.Model;
using DocuSignWeb.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace DocuSignWeb.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class QuickCreateViewController : ViewController
    {
        static SortedDictionary<string, ChoiceActionItem> groups = new SortedDictionary<string, ChoiceActionItem>();
        public QuickCreateViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            actionMDINewButton.Active.SetItemValue("active", true);
            actionMDINewButtonDashboard.Active.SetItemValue("active", true);
            FillActions();
            //actionMDINewButton.ShowItemsOnClick = true;
        }
        private void FillActions()
        {
            //Clears initial groups
            foreach (ChoiceActionItem item in groups.Values)
            {
                item.Items.Clear();
            }

            int x = 0;
            ChoiceActionItemHelper.CreateChoiceActionItem(typeof(DocuSignEnvelope), actionMDINewButton, actionMDINewButton.Items.Count);

            SortedDictionary<string, ChoiceActionItem> itemlist = new SortedDictionary<string, ChoiceActionItem>();

            foreach (ChoiceActionItem item in actionMDINewButton.Items)
            {
                if (itemlist.ContainsKey(item.Caption) == false)
                {
                    itemlist.Add(item.Caption, item);
                }
            }

            actionMDINewButton.Items.Clear();
            actionMDINewButtonDashboard.Items.Clear();

            foreach (ChoiceActionItem item in itemlist.Values)
            {
                actionMDINewButton.Items.Add(item);
                actionMDINewButtonDashboard.Items.Add(item);

            }

            itemlist.Clear();
            itemlist = null;
        }
        private void actionMDINewButton_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            if ((e.SelectedChoiceActionItem as ChoiceActionItem).Items.Count == 0)
            {
                IObjectSpace space = Application.CreateObjectSpace() as XPObjectSpace;
                Type type = (e.SelectedChoiceActionItem.Data as Type);
                if (type == typeof(DocuSignEnvelope))
                {
                    DocuSignEnvelope envelope = space.CreateObject<DocuSignEnvelope>(); ;
                    ShowViewParameters svp = MyDocuSignHelper.ShowDocuSignForm(envelope, space);
                    e.ShowViewParameters.Context = svp.Context;
                    e.ShowViewParameters.Controllers.AddRange(svp.Controllers);
                    e.ShowViewParameters.CreatedView = svp.CreatedView;
                    e.ShowViewParameters.NewWindowTarget = svp.NewWindowTarget;
                    e.ShowViewParameters.TargetWindow = svp.TargetWindow;
                }
            }
        }
    }

    public static class ChoiceActionItemHelper
    {
        public static ChoiceActionItem CreateChoiceActionItem(Type type, string caption = null)
        {
            if (caption == null)
            {
                caption = CaptionHelper.GetClassCaption(type.FullName);
            }

            ChoiceActionItem item = new ChoiceActionItem(caption, type);
            //{ ImageName = ImageLoader.Instance.GetImageInfo(ApplicationHelper.Application.FindModelClass(type).ImageName).ImageName };
            return item;
        }
        
        public static void CreateChoiceActionItem(Type type, SingleChoiceAction action, int index)
        {
            action.Items.Insert(index, CreateChoiceActionItem(type));
        }
    }

    
}
