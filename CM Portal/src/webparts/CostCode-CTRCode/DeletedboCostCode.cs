using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace CostCodeCTRCode
{
    public class DeletedboCostCode : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Label lblConfirm;
        private Label lblDescription;

        private Label lblError;

        private Button btnDelete;

        private string lobSystemInstance = "CostCode-CTRCode";
        private string entityName = "dbo.CostCode";

        private string CostCode
        {
            get
            {
                return Page.Request.QueryString["CostCode"].ToString();
            }
        }


        protected override void CreateChildControls()
        {
            lblConfirm = new Label();
            lblDescription = new Label();
            lblError = new Label();

            loaddboCostCode();

            lblConfirm.Text = "</br></br>Do you really want to delete cost code <b>" + CostCode + " "
                + lblDescription.Text +  "</b> ? </br></br>";

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
            parameters[0] = CostCode;

            try
            {
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.CostCodeDeleter", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }
            lblConfirm.Text = "</br></br>Cost Code <b>" + CostCode + " " + lblDescription.Text + "</b> successfully deleted!";
            lblConfirm.BackColor = System.Drawing.Color.LightBlue;
            this.btnDelete.Visible = false;
        }

        //Gets the entry in the database
        private void loaddboCostCode()
        {
            // add the identifiers...
            object[] identifiers = new object[1];
            identifiers[0] = CostCode;

            IEntityInstance ie = null;
            try
            {
                ie = BdcHelpers.GetSpecificRecord(lobSystemInstance, entityName, identifiers);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            if (ie["Description"] != null)
                lblDescription.Text = ie["Description"].ToString();

        }
    }
}
