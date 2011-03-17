using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace Contacts
{
    public class UpdatedboVariousContact : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;

        private Label lblVariousContactID;
        private Label lblDisplayVariousContactID;

        private Label lblVariousContactName;
        private TextBox txtVariousContactName;

        private Label lblVariousContactPhoneNumber;
        private TextBox txtVariousContactPhoneNumber;

        private Label lblVariousContactFaxNumber;
        private TextBox txtVariousContactFaxNumber;

        private Label lblVariousContactEmail;
        private TextBox txtVariousContactEmail;


        private Button btnUpdate;

        private Label lblError;

        private string lobSystemInstance = "Contacts";
        private string entityName = "dbo.VariousContact";

        private string VariousContactID
        { get { return Page.Request.QueryString["VariousContactID"].ToString(); } }


        protected override void CreateChildControls()
        {
            tblLayout = new Table();

            lblVariousContactID = new Label();
            lblVariousContactID.Text = "VariousContactID";
            lblDisplayVariousContactID = new Label();

            lblVariousContactName = new Label();
            lblVariousContactName.Text = "Name";
            txtVariousContactName = new TextBox();
            TableRow row0 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblVariousContactName);
            rightcell1.Controls.Add(txtVariousContactName);
            row0.Cells.Add(leftcell1);
            row0.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row0);

            lblVariousContactPhoneNumber = new Label();
            lblVariousContactPhoneNumber.Text = "Phone number";
            txtVariousContactPhoneNumber = new TextBox();
            TableRow row1 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblVariousContactPhoneNumber);
            rightcell2.Controls.Add(txtVariousContactPhoneNumber);
            row1.Cells.Add(leftcell2);
            row1.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row1);

            lblVariousContactFaxNumber = new Label();
            lblVariousContactFaxNumber.Text = "Fax number";
            txtVariousContactFaxNumber = new TextBox();
            TableRow row2 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            leftcell3.Controls.Add(lblVariousContactFaxNumber);
            rightcell3.Controls.Add(txtVariousContactFaxNumber);
            row2.Cells.Add(leftcell3);
            row2.Cells.Add(rightcell3);
            tblLayout.Rows.Add(row2);

            lblVariousContactEmail = new Label();
            lblVariousContactEmail.Text = "Email";
            txtVariousContactEmail = new TextBox();
            TableRow row3 = new TableRow();
            TableCell leftcell4 = new TableCell();
            TableCell rightcell4 = new TableCell();
            leftcell4.Controls.Add(lblVariousContactEmail);
            rightcell4.Controls.Add(txtVariousContactEmail);
            row3.Cells.Add(leftcell4);
            row3.Cells.Add(rightcell4);
            tblLayout.Rows.Add(row3);


            btnUpdate = new Button();
            btnUpdate.Text = "Update";
            btnUpdate.Click += new EventHandler(btnUpdate_Click);

            lblError = new Label();

            this.Controls.Add(tblLayout);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(lblError);

            loaddboVariousContact();

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

            object[] parameters = new object[5];
            parameters[0] = lblDisplayVariousContactID.Text;
            parameters[1] = txtVariousContactName.Text;
            parameters[2] = txtVariousContactPhoneNumber.Text;
            parameters[3] = txtVariousContactFaxNumber.Text;
            parameters[4] = txtVariousContactEmail.Text;


            try
            {
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.VariousContactUpdater", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }
            lblError.Text = "</br></br>Contact <b>" + txtVariousContactName.Text + "</b> Successfully updated!";
            lblError.BackColor = System.Drawing.Color.LightBlue;
        }

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

            lblDisplayVariousContactID.Text = ie["VariousContactID"].ToString();
            if (ie["VariousContactName"] != null)
                txtVariousContactName.Text = ie["VariousContactName"].ToString();
            if (ie["VariousContactPhoneNumber"] != null)
                txtVariousContactPhoneNumber.Text = ie["VariousContactPhoneNumber"].ToString();
            if (ie["VariousContactFaxNumber"] != null)
                txtVariousContactFaxNumber.Text = ie["VariousContactFaxNumber"].ToString();
            if (ie["VariousContactEmail"] != null)
                txtVariousContactEmail.Text = ie["VariousContactEmail"].ToString();

        }
    }
}
