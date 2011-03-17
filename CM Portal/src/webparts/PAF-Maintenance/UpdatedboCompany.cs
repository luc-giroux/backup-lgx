using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace PAFMaintenance
{
    public class UpdatedboCompany : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;
        private Label lblCompanyID;
        private Label lblDisplayCompanyID;
        private Label lblCompanyName;
        private TextBox txtCompanyName;


        private Button btnUpdate;

        private Label lblError;

        private string lobSystemInstance = "PAF_Maintenance";
        private string entityName = "dbo.Company";

        private string CompanyID
        { get { return Page.Request.QueryString["CompanyID"].ToString(); } }


        protected override void CreateChildControls()
        {
            tblLayout = new Table();

            lblCompanyID = new Label();
            lblCompanyID.Text = "CompanyID";
            lblDisplayCompanyID = new Label();
            TableRow row10000 = new TableRow();
            TableCell leftcell0 = new TableCell();
            TableCell rightcell0 = new TableCell();
            leftcell0.Controls.Add(lblCompanyID);
            rightcell0.Controls.Add(lblDisplayCompanyID);
            row10000.Cells.Add(leftcell0);
            row10000.Cells.Add(rightcell0);
            tblLayout.Rows.Add(row10000);
            lblCompanyName = new Label();
            lblCompanyName.Text = "CompanyName";
            txtCompanyName = new TextBox();
            TableRow row0 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblCompanyName);
            rightcell1.Controls.Add(txtCompanyName);
            row0.Cells.Add(leftcell1);
            row0.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row0);


            btnUpdate = new Button();
            btnUpdate.Text = "Update";
            btnUpdate.Click += new EventHandler(btnUpdate_Click);

            lblError = new Label();

            this.Controls.Add(tblLayout);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(lblError);

            loaddboCompany();

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

            object[] parameters = new object[2];
            parameters[0] = lblDisplayCompanyID.Text;
            parameters[1] = txtCompanyName.Text;


            try
            {
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.CompanyUpdater", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblError.Text = "</br></br>Company <b>" + txtCompanyName.Text + "</b> Successfully updated!";
            lblError.BackColor = System.Drawing.Color.LightBlue;
        }

        private void loaddboCompany()
        {
            // add the identifiers...
            object[] identifiers = new object[1];
            identifiers[0] = Convert.ToString(CompanyID);


            IEntityInstance ie = null;
            try
            {
                ie = BdcHelpers.GetSpecificRecord(lobSystemInstance, entityName, identifiers);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblDisplayCompanyID.Text = ie["CompanyID"].ToString();
            if (ie["CompanyName"] != null)
                txtCompanyName.Text = ie["CompanyName"].ToString();

        }
    }
}
