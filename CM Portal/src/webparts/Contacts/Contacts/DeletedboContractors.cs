using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace Contacts
{
    public class DeletedboContractors : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Label lblConfirm;
        private Label lblContractorID;
        private Label lblContractorCompany;
        private Label lblContractorLastName;
        private Label lblContractorFirstName;
        private Label lblError;

        private Button btnDelete;

        private string lobSystemInstance = "Contacts";
        private string entityName = "dbo.Contractors";

        private string ContractorID
        {
            get
            {
                return Page.Request.QueryString["ContractorID"].ToString();
            }
        }


        protected override void CreateChildControls()
        {

            lblConfirm = new Label();
            lblContractorID = new Label();
            lblContractorCompany = new Label();
            lblContractorLastName = new Label();
            lblContractorFirstName = new Label();
            lblError = new Label();

            loaddboContractors();

            lblConfirm.Text = "</br></br>Do you really want to delete contractor <b>" + lblContractorCompany.Text + " "
                + lblContractorFirstName.Text + " " + lblContractorLastName.Text + "</b> ? </br></br>";

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
            parameters[0] = lblContractorID.Text;

            try
            {
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.ContractorsDeleter", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }
            lblConfirm.Text = "</br></br>Contractor <b>" + lblContractorFirstName.Text + " " + lblContractorLastName.Text + "</b> successfully deleted!";
            lblConfirm.BackColor = System.Drawing.Color.LightBlue;
            this.btnDelete.Visible = false;
        }

        //Gets the entry in the database
        private void loaddboContractors()
        {
            // add the identifiers...
            object[] identifiers = new object[1];
            identifiers[0] = Convert.ToInt32(ContractorID);

            IEntityInstance ie = null;
            try
            {
                ie = BdcHelpers.GetSpecificRecord(lobSystemInstance, entityName, identifiers);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblContractorID.Text = ie["ContractorID"].ToString();
            if (ie["ContractorCompany"] != null)
                lblContractorCompany.Text = ie["ContractorCompany"].ToString();
            if (ie["ContractorLastname"] != null)
                lblContractorLastName.Text = ie["ContractorLastname"].ToString();
            if (ie["ContractorFirstname"] != null)
                lblContractorFirstName.Text = ie["ContractorFirstname"].ToString();

        }
    }
}
