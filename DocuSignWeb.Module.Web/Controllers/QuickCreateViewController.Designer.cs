
namespace DocuSignWeb.Module.Web.Controllers
{
    partial class QuickCreateViewController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.actionMDINewButton = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.actionMDINewButtonDashboard = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // actionMDINewButton
            // 
            this.actionMDINewButton.Caption = "New";
            this.actionMDINewButton.Category = "ObjectsCreation";
            this.actionMDINewButton.ConfirmationMessage = null;
            this.actionMDINewButton.Id = "actionMDINewButton";
            this.actionMDINewButton.ImageName = "MenuBar_New";
            this.actionMDINewButton.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.actionMDINewButton.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.actionMDINewButtonDashboard.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.actionMDINewButton.ToolTip = null;
            this.actionMDINewButtonDashboard.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.actionMDINewButton.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.actionMDINewButton_Execute);
            // 
            // actionMDINewButtonDashboard
            // 
            this.actionMDINewButtonDashboard.Caption = "New";
            this.actionMDINewButtonDashboard.Category = "ObjectsCreation";
            this.actionMDINewButtonDashboard.ConfirmationMessage = null;
            this.actionMDINewButtonDashboard.Id = "actionMDINewButtonDashboard";
            this.actionMDINewButtonDashboard.ImageName = "MenuBar_New";
            this.actionMDINewButtonDashboard.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.actionMDINewButtonDashboard.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.actionMDINewButtonDashboard.TargetViewType = DevExpress.ExpressApp.ViewType.DashboardView;
            this.actionMDINewButtonDashboard.ToolTip = null;
            this.actionMDINewButtonDashboard.TypeOfView = typeof(DevExpress.ExpressApp.DashboardView);
            this.actionMDINewButtonDashboard.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.actionMDINewButton_Execute);
            // 
            // QuickCreateViewController
            // 
            this.Actions.Add(this.actionMDINewButton);
            this.Actions.Add(this.actionMDINewButtonDashboard);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.Any;
        }
        public DevExpress.ExpressApp.Actions.SingleChoiceAction actionMDINewButton;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction actionMDINewButtonDashboard;
        #endregion
    }
}
