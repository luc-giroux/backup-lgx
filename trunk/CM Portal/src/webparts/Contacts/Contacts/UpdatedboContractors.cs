using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace Contacts
{
    public class UpdatedboContractors : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;

        private Label lblContractorID;

        private Label lblDisplayContractorID;

        private Label lblContractorCompany;
        private TextBox txtContractorCompany;

        private Label lblContractorLastname;
        private TextBox txtContractorLastname;

        private Label lblContractorFirstname;
        private TextBox txtContractorFirstname;

        private Label lblContractorEmail;
        private TextBox txtContractorEmail;

        private Label lblContractorPhoneNumber;
        private TextBox txtContractorPhoneNumber;

        private Label lblContractorContractNumber;
        private TextBox txtContractorContractNumber;

        private Label lblContractorLocationCode;
        private TextBox txtContractorLocationCode;


        private Button btnUpdate;

        private Label lblError;

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
            tblLayout = new Table();

            lblContractorID = new Label();
            lblContractorID.Text = "ContractorID";
            lblDisplayContractorID = new Label();

            lblContractorCompany = new Label();
            lblContractorCompany.Text = "Company";
            txtContractorCompany = new TextBox();
            TableRow row0 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblContractorCompany);
            rightcell1.Controls.Add(txtContractorCompany);
            row0.Cells.Add(leftcell1);
            row0.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row0);

            lblContractorLastname = new Label();
            lblContractorLastname.Text = "Last name";
            txtContractorLastname = new TextBox();
            TableRow row1 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblContractorLastname);
            rightcell2.Controls.Add(txtContractorLastname);
            row1.Cells.Add(leftcell2);
            row1.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row1);

            lblContractorFirstname = new Label();
            lblContractorFirstname.Text = "First name";
            txtContractorFirstname = new TextBox();
            TableRow row2 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            leftcell3.Controls.Add(lblContractorFirstname);
            rightcell3.Controls.Add(txtContractorFirstname);
            row2.Cells.Add(leftcell3);
            row2.Cells.Add(rightcell3);
            tblLayout.Rows.Add(row2);

            lblContractorEmail = new Label();
            lblContractorEmail.Text = "Email";
            txtContractorEmail = new TextBox();
            TableRow row3 = new TableRow();
            TableCell leftcell4 = new TableCell();
            TableCell rightcell4 = new TableCell();
            leftcell4.Controls.Add(lblContractorEmail);
            rightcell4.Controls.Add(txtContractorEmail);
            row3.Cells.Add(leftcell4);
            row3.Cells.Add(rightcell4);
            tblLayout.Rows.Add(row3);

            lblContractorPhoneNumber = new Label();
            lblContractorPhoneNumber.Text = "Phone number";
            txtContractorPhoneNumber = new TextBox();
            TableRow row4 = new TableRow();
            TableCell leftcell5 = new TableCell();
            TableCell rightcell5 = new TableCell();
            leftcell5.Controls.Add(lblContractorPhoneNumber);
            rightcell5.Controls.Add(txtContractorPhoneNumber);
            row4.Cells.Add(leftcell5);
            row4.Cells.Add(rightcell5);
            tblLayout.Rows.Add(row4);

            lblContractorContractNumber = new Label();
            lblContractorContractNumber.Text = "Contract number";
            txtContractorContractNumber = new TextBox();
            TableRow row5 = new TableRow();
            TableCell leftcell6 = new TableCell();
            TableCell rightcell6 = new TableCell();
            leftcell6.Controls.Add(lblContractorContractNumber);
            rightcell6.Controls.Add(txtContractorContractNumber);
            row5.Cells.Add(leftcell6);
            row5.Cells.Add(rightcell6);
            tblLayout.Rows.Add(row5);

            lblContractorLocationCode = new Label();
            lblContractorLocationCode.Text = "Location code";
            txtContractorLocationCode = new TextBox();
            TableRow row6 = new TableRow();
            TableCell leftcell7 = new TableCell();
            TableCell rightcell7 = new TableCell();
            leftcell7.Controls.Add(lblContractorLocationCode);
            rightcell7.Controls.Add(txtContractorLocationCode);
            row6.Cells.Add(leftcell7);
            row6.Cells.Add(rightcell7);
            tblLayout.Rows.Add(row6);


            btnUpdate = new Button();
            btnUpdate.Text = "Update";
            btnUpdate.Click += new EventHandler(btnUpdate_Click);

            lblError = new Label();

            this.Controls.Add(tblLayout);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(lblError);

            loaddboContractors();

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

            object[] parameters = new object[8];
            parameters[0] = lblDisplayContractorID.Text;
            parameters[1] = txtContractorCompany.Text;
            parameters[2] = txtContractorLastname.Text;
            parameters[3] = txtContractorFirstname.Text;
            parameters[4] = txtContractorEmail.Text;
            parameters[5] = txtContractorPhoneNumber.Text;
            parameters[6] = txtContractorContractNumber.Text;
            parameters[7] = txtContractorLocationCode.Text;


            try
            {
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.ContractorsUpdater", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblError.Text = "</br></br>Contractor <b>" + txtContractorCompany.Text + " " + txtContractorLastname.Text + " "
                            + txtContractorFirstname.Text + "</b> Successfully updated!";
            lblError.BackColor = System.Drawing.Color.LightBlue;

        }

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

            lblDisplayContractorID.Text = ie["ContractorID"].ToString();
            if (ie["ContractorCompany"] != null)
                txtContractorCompany.Text = ie["ContractorCompany"].ToString();
            if (ie["ContractorLastname"] != null)
                txtContractorLastname.Text = ie["ContractorLastname"].ToString();
            if (ie["ContractorFirstname"] != null)
                txtContractorFirstname.Text = ie["ContractorFirstname"].ToString();
            if (ie["ContractorEmail"] != null)
                txtContractorEmail.Text = ie["ContractorEmail"].ToString();
            if (ie["ContractorPhoneNumber"] != null)
                txtContractorPhoneNumber.Text = ie["ContractorPhoneNumber"].ToString();
            if (ie["ContractorContractNumber"] != null)
                txtContractorContractNumber.Text = ie["ContractorContractNumber"].ToString();
            if (ie["ContractorLocationCode"] != null)
                txtContractorLocationCode.Text = ie["ContractorLocationCode"].ToString();

        }
    }
}
