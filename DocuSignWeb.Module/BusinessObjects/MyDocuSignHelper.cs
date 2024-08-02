using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DocuSignWeb.Module.BusinessObjects
{
    public class MyDocuSignHelper
    {
        public static ShowViewParameters ShowDocuSignForm(DocuSignEnvelope envelope, IObjectSpace space)
        {
            DialogController dc = new DialogController();
            dc.AcceptAction.Caption = "Send to DocuSign";
            dc.Accepting += Dc_Accepting;
            ShowViewParameters svp = new ShowViewParameters();
            svp.CreatedView = ApplicationHelper.Application.CreateDetailView(space, envelope);
            svp.TargetWindow = TargetWindow.NewModalWindow;
            svp.Context = TemplateContext.PopupWindow;
            svp.Controllers.Add(dc);
            return svp;
        }
        private static void Dc_Accepting(object sender, DialogControllerAcceptingEventArgs e)
        {
            DocuSignEnvelope docuSignEnvelope = e.AcceptActionArgs.CurrentObject as DocuSignEnvelope;
            if (CheckDocuSignEnvelope(docuSignEnvelope))
            {
                ViewUrl viewUrl = CreateEmbeddedSigningViewTest(e.AcceptActionArgs.CurrentObject as DocuSignEnvelope);
                string script = "window.open('"+ viewUrl.Url + "')";
                DevExpress.ExpressApp.Web.WebWindow.CurrentRequestWindow.RegisterStartupScript("clientScriptForNonModalWindow", script);

                //WebApplication.Redirect(viewUrl.Url);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private static bool CheckDocuSignEnvelope(DocuSignEnvelope docuSignEnvelope)
        {
            if (docuSignEnvelope.Template == null) return false;
            else if (String.IsNullOrEmpty(docuSignEnvelope.Subject)) return false;
            else if (String.IsNullOrEmpty(docuSignEnvelope.ClientRecipient.Name)) return false;
            else if (String.IsNullOrEmpty(docuSignEnvelope.ClientRecipient.EmailAddress)) return false;
            else if (String.IsNullOrEmpty(docuSignEnvelope.ManagerRecipient.Name)) return false;
            else if (String.IsNullOrEmpty(docuSignEnvelope.ManagerRecipient.EmailAddress)) return false;
            return true;
        }
        public static void ConfigureApiClient(string basePath)
        {
            if (basePath != null && basePath != "")
            {
                ApiClient apiClient = new ApiClient(basePath);
                Configuration.Default.ApiClient = apiClient;
            }
            else
                throw new UserFriendlyException("Tools>Show options>Import options and open the “DocuSign” tab to enter Docu Sign Service URL");
        }
        public static string LoginApi()
        {
            string accountId = "";
            try
            {
                string userName = "";
                string password = "";

                ApiClient apiClient = Configuration.Default.ApiClient;
                Configuration.Default.DefaultHeader.Clear();
                string authHeader = "{\"Username\":\"" + userName + "\", \"Password\":\"" + password + "\", \"IntegratorKey\":\"" + "" + "\"}";
                Configuration.Default.AddDefaultHeader("X-DocuSign-Authentication", authHeader);

                accountId = null;

                AuthenticationApi authApi = new AuthenticationApi();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                LoginInformation loginInfo = authApi.Login();

                foreach (LoginAccount loginAcct in loginInfo.LoginAccounts)
                {
                    if (loginAcct.IsDefault == "true")
                    {
                        accountId = loginAcct.AccountId;
                        break;
                    }
                }
                if (accountId == null)
                {
                    accountId = loginInfo.LoginAccounts[0].AccountId;
                }

            }
            catch (Exception ex)
            {
            }
            return accountId;
        }
        public static ViewUrl CreateEmbeddedSigningViewTest(DocuSignEnvelope envelope)
        {
            string clientUserId = Guid.NewGuid().ToString();

            string recipientName = envelope.ManagerRecipient.Name;
            string recipientEmail = envelope.ManagerRecipient.EmailAddress;

            ConfigureApiClient("https://demo.docusign.net/restapi/");

            string accountId = LoginApi();

            List<TemplateRole> rolesList = new List<TemplateRole>();
            rolesList.Add(new TemplateRole() { ClientUserId = clientUserId, RoleName = "Manager", Email = envelope.ManagerRecipient.EmailAddress, Name = envelope.ManagerRecipient.Name, RoutingOrder = "1" });
            rolesList.Add(new TemplateRole() { RoleName = "Client", Email = envelope.ClientRecipient.EmailAddress, Name = envelope.ClientRecipient.Name, RoutingOrder = "2" });

            EnvelopeDefinition envDef = new EnvelopeDefinition();
            envDef.EmailSubject = envelope.Subject;
            envDef.TemplateRoles = rolesList;
            envDef.TemplateId = envelope.Template.Id;

            envDef.Status = "sent";

            EnvelopesApi envelopesApi = new EnvelopesApi();
            EnvelopeSummary envelopeSummary = envelopesApi.CreateEnvelope(accountId, envDef);


            RecipientViewRequest viewOptions = new RecipientViewRequest()
            {
                ReturnUrl = "https://www.google.com",
                ClientUserId = clientUserId,
                AuthenticationMethod = "Password",
                UserName = envelope.ManagerRecipient.Name,
                Email = envelope.ManagerRecipient.EmailAddress,
            };
            ViewUrl recipientView = envelopesApi.CreateRecipientView(accountId, envelopeSummary.EnvelopeId, viewOptions);
            return recipientView;
        }
        public static XPObjectSpace ObjectSpace { get; set; }
       
        public static List<DocuSignTemplate> GetTemplates()
        {

            ConfigureApiClient("https://demo.docusign.net/restapi/");
            List<DocuSignTemplate> templateList = new List<DocuSignTemplate>();
            try
            {
                TemplatesApi templates = new TemplatesApi();
                EnvelopeTemplateResults result = templates.ListTemplates(LoginApi());

                if (result.EnvelopeTemplates != null)
                {
                    foreach (EnvelopeTemplateResult templateResult in result.EnvelopeTemplates)
                    {
                        if (templateResult.Shared == "true")
                        {
                            templateList.Add(new DocuSignTemplate() { Id = templateResult.TemplateId, Name = templateResult.Name, Description = templateResult.Description });
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
            return templateList;
        }
    }
}
