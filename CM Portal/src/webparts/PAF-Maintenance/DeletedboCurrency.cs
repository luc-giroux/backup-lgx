using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace PAFMaintenance
{
    public class DeletedboCurrency : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Label lblConfirm;
        private Label lblCurrencyID;

        private Label lblError;

        private Button btnDelete;

        private string lobSystemInstance = "PAF_Maintenance";
        private string entityName = "dbo.Currency";

        private string CurrencyID
        {
            get
            {
                return Page.Request.QueryString["CurrencyID"].ToString();
            }
        }


        protected override void CreateChildControls()
        {

            lblConfirm = new Label();
            lblCurrencyID = new Label();

            lblError = new Label();

            loaddboCurrency();

            lblConfirm.Text = "</br></br>Do you really want to delete Currency <b>" + lblCurrencyID.Text + "</b> ? </br></br>";

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
            parameters[0] = lblCurrencyID.Text;

            try
            {
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.CurrencyDeleter", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }
            lblConfirm.Text = "</br></br>Currency <b>" + lblCurrencyID.Text + "</b> successfully deleted!";
            lblConfirm.BackColor = System.Drawing.Color.LightBlue;
            this.btnDelete.Visible = false;
        }

        //Gets the entry in the database
        private void loaddboCurrency()
        {
            // add the identifiers...
            object[] identifiers = new object[1];
            identifiers[0] = CurrencyID;

            IEntityInstance ie = null;
            try
            {
                ie = BdcHelpers.GetSpecificRecord(lobSystemInstance, entityName, identifiers);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblCurrencyID.Text = ie["CurrencyID"].ToString();

        }
    }
}
