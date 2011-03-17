using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace PAFMaintenance
{
    public class UpdatedboCurrency : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;
        private Label lblCurrencyID;
        private Label lblDisplayCurrencyID;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblCurrencyToProjectrate;
        private TextBox txtCurrencyToProjectrate;
        private Label lblComments;
        private TextBox txtComments;


        private Button btnUpdate;

        private Label lblError;

        private string lobSystemInstance = "PAF_Maintenance";
        private string entityName = "dbo.Currency";

        private string CurrencyID
        { get { return Page.Request.QueryString["CurrencyID"].ToString(); } }


        protected override void CreateChildControls()
        {
            tblLayout = new Table();

            lblCurrencyID = new Label();
            lblCurrencyID.Text = "CurrencyID";
            lblDisplayCurrencyID = new Label();
            TableRow row10000 = new TableRow();
            TableCell leftcell0 = new TableCell();
            TableCell rightcell0 = new TableCell();
            leftcell0.Controls.Add(lblCurrencyID);
            rightcell0.Controls.Add(lblDisplayCurrencyID);
            row10000.Cells.Add(leftcell0);
            row10000.Cells.Add(rightcell0);
            tblLayout.Rows.Add(row10000);
            lblDescription = new Label();
            lblDescription.Text = "Description";
            txtDescription = new TextBox();
            TableRow row0 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblDescription);
            rightcell1.Controls.Add(txtDescription);
            row0.Cells.Add(leftcell1);
            row0.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row0);
            lblCurrencyToProjectrate = new Label();
            lblCurrencyToProjectrate.Text = "CurrencyToProjectrate";
            txtCurrencyToProjectrate = new TextBox();
            TableRow row1 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblCurrencyToProjectrate);
            rightcell2.Controls.Add(txtCurrencyToProjectrate);
            row1.Cells.Add(leftcell2);
            row1.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row1);
            lblComments = new Label();
            lblComments.Text = "Comments";
            txtComments = new TextBox();
            TableRow row2 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            leftcell3.Controls.Add(lblComments);
            rightcell3.Controls.Add(txtComments);
            row2.Cells.Add(leftcell3);
            row2.Cells.Add(rightcell3);
            tblLayout.Rows.Add(row2);


            btnUpdate = new Button();
            btnUpdate.Text = "Update";
            btnUpdate.Click += new EventHandler(btnUpdate_Click);

            lblError = new Label();

            this.Controls.Add(tblLayout);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(lblError);

            loaddboCurrency();

        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            this.tblLayout.RenderControl(writer);
            this.btnUpdate.RenderControl(writer);
            this.lblError.RenderControl(writer);

        }

        void btnUpdate_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError.BackColor = System.Drawing.Color.White;

            object[] parameters = new object[4];
            parameters[0] = lblDisplayCurrencyID.Text;
            parameters[1] = txtDescription.Text;
            parameters[2] = txtCurrencyToProjectrate.Text;
            parameters[3] = txtComments.Text;


            try
            {
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.CurrencyUpdater", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblError.Text = "</br></br>Currency <b>" + lblCurrencyID.Text + "</b> Successfully updated!";
            lblError.BackColor = System.Drawing.Color.LightBlue;
        }

        private void loaddboCurrency()
        {
            // add the identifiers...
            object[] identifiers = new object[1];
            identifiers[0] = Convert.ToString(CurrencyID);


            IEntityInstance ie = null;
            try
            {
                ie = BdcHelpers.GetSpecificRecord(lobSystemInstance, entityName, identifiers);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblDisplayCurrencyID.Text = ie["CurrencyID"].ToString();
            if (ie["Description"] != null)
                txtDescription.Text = ie["Description"].ToString();
            if (ie["CurrencyToProjectrate"] != null)
                txtCurrencyToProjectrate.Text = ie["CurrencyToProjectrate"].ToString();
            if (ie["Comments"] != null)
                txtComments.Text = ie["Comments"].ToString();

        }
    }
}
