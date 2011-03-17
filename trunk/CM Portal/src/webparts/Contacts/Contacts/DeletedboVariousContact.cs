using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace Contacts
{
    public class DeletedboVariousContact : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Label lblConfirm;
        private Label lblVariousContactID;
        private Label lblVariousContactName;
        private Label lblError;

        private Button btnDelete;

        private string lobSystemInstance = "Contacts";
        private string entityName = "dbo.VariousContact";

        private string VariousContactID
        {
            get
            {
                return Page.Request.QueryString["VariousContactID"].ToString();
            }
        }


        protected override void CreateChildControls()
        {

            lblConfirm = new Label();
            lblVariousContactID = new Label();
            lblVariousContactName = new Label();
            lblError = new Label();

            loaddboVariousContact();

            lblConfirm.Text = "</br></br>Do you really want to delete contact <b>" + lblVariousContactName.Text + "</b> ? </br></br>";

            btnDelete = new Button();
            btnDelete.Text = "Delete";
            btnDelete.Click += new EventHandler(btnDelete_Click);

            this.Controls.Add(lblConfirm);
            this.Controls.Add(btnDelete);
            this.Controls.Add(lblError);
        }


        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            this.lblConfirm.RenderControl(writer);
            this.btnDelete.RenderControl(writer);
            this.lblError.RenderControl(writer);

        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError.BackColor = System.Drawing.Color.White;

            object[] parameters = new object[1];
            parameters[0] = lblVariousContactID.Text;

            try
            {
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.VariousContactDeleter", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }
            lblConfirm.Text = "</br></br>Contact <b>" + lblVariousContactName.Text + "</b> successfully deleted!";
            lblConfirm.BackColor = System.Drawing.Color.LightBlue;
            this.btnDelete.Visible = false;
        }

        //Gets the entry in the database
        private void loaddboVariousContact()
        {
            // add the identifiers...
            object[] identifiers = new object[1];
            identifiers[0] = Convert.ToInt32(VariousContactID);

            IEntityInstance ie = null;
            try
            {
                ie = BdcHelpers.GetSpecificRecord(lobSystemInstance, entityName, identifiers);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblVariousContactID.Text = ie["VariousContactID"].ToString();
            if (ie["VariousContactName"] != null)
                lblVariousContactName.Text = ie["VariousContactName"].ToString();
        }
    }
}
