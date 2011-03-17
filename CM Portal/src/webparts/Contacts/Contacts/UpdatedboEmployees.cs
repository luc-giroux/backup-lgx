using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace Contacts
{
    public class UpdatedboEmployees : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;

        private Label lblEmployeeID;

        private Label lblDisplayEmployeeID;

        private Label lblEmployeeFirstname;
        private TextBox txtEmployeeFirstname;

        private Label lblEmployeeLastname;
        private TextBox txtEmployeeLastname;

        private Label lblEmployeeEmail;
        private TextBox txtEmployeeEmail;

        private Label lblEmployeeInternalPhoneNumber;
        private TextBox txtEmployeeInternalPhoneNumber;

        private Label lblEmployeeExternalPhoneNumber;
        private TextBox txtEmployeeExternalPhoneNumber;

        private Label lblEmployeeMobilePhoneNumber;
        private TextBox txtEmployeeMobilePhoneNumber;

        private Label lblEmployeeLocation;
        private TextBox txtEmployeeLocation;

        private Label lblEmployeeLocationCode;
        private TextBox txtEmployeeLocationCode;

        private Label lblEmployeeCompany;
        private TextBox txtEmployeeCompany;

        private Button btnUpdate;

	    private Label lblError;

        // Contains all the fields of the form.
        private LinkedList<TextBox> AllFields;

        private string lobSystemInstance = "Contacts";
        private string entityName = "dbo.Employees";

        private string EmployeeID
        {
            get
            {
                return Page.Request.QueryString["EmployeeID"].ToString();
            }
        }


        protected override void CreateChildControls()
        {
            tblLayout = new Table();

            //Init of the list
            this.AllFields = new LinkedList<TextBox>();

            lblEmployeeID = new Label();
            lblEmployeeID.Text = "EmployeeID";
            lblDisplayEmployeeID = new Label();


            // First Name
            lblEmployeeFirstname = new Label();
            lblEmployeeFirstname.Text = "First name";
            txtEmployeeFirstname = new TextBox();
            this.AllFields.AddLast(txtEmployeeFirstname);
            TableRow row0 = new TableRow();
            TableCell leftcell0 = new TableCell();
            TableCell rightcell0 = new TableCell();
            leftcell0.Controls.Add(lblEmployeeFirstname);
            rightcell0.Controls.Add(txtEmployeeFirstname);
            row0.Cells.Add(leftcell0);
            row0.Cells.Add(rightcell0);
            tblLayout.Rows.Add(row0);

            // Last name
            lblEmployeeLastname = new Label();
            lblEmployeeLastname.Text = "Last name";
            txtEmployeeLastname = new TextBox();
            this.AllFields.AddLast(txtEmployeeLastname);
            TableRow row1 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblEmployeeLastname);
            rightcell1.Controls.Add(txtEmployeeLastname);
            row1.Cells.Add(leftcell1);
            row1.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row1);

            lblEmployeeEmail = new Label();
            lblEmployeeEmail.Text = "Email";
            txtEmployeeEmail = new TextBox();
            this.AllFields.AddLast(txtEmployeeEmail);
            TableRow rowEmail = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblEmployeeEmail);
            rightcell2.Controls.Add(txtEmployeeEmail);
            rowEmail.Cells.Add(leftcell2);
            rowEmail.Cells.Add(rightcell2);
            tblLayout.Rows.Add(rowEmail);

            lblEmployeeInternalPhoneNumber = new Label();
            lblEmployeeInternalPhoneNumber.Text = "Internal Phone";
            txtEmployeeInternalPhoneNumber = new TextBox();
            this.AllFields.AddLast(txtEmployeeInternalPhoneNumber);
            TableRow row2 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            leftcell3.Controls.Add(lblEmployeeInternalPhoneNumber);
            rightcell3.Controls.Add(txtEmployeeInternalPhoneNumber);
            row2.Cells.Add(leftcell3);
            row2.Cells.Add(rightcell3);
            tblLayout.Rows.Add(row2);

            lblEmployeeExternalPhoneNumber = new Label();
            lblEmployeeExternalPhoneNumber.Text = "External Phone";
            txtEmployeeExternalPhoneNumber = new TextBox();
            this.AllFields.AddLast(txtEmployeeExternalPhoneNumber);
            TableRow row3 = new TableRow();
            TableCell leftcell4 = new TableCell();
            TableCell rightcell4 = new TableCell();
            leftcell4.Controls.Add(lblEmployeeExternalPhoneNumber);
            rightcell4.Controls.Add(txtEmployeeExternalPhoneNumber);
            row3.Cells.Add(leftcell4);
            row3.Cells.Add(rightcell4);
            tblLayout.Rows.Add(row3);

            lblEmployeeMobilePhoneNumber = new Label();
            lblEmployeeMobilePhoneNumber.Text = "Mobile Phone";
            txtEmployeeMobilePhoneNumber = new TextBox();
            this.AllFields.AddLast(txtEmployeeMobilePhoneNumber);
            TableRow row4 = new TableRow();
            TableCell leftcell5 = new TableCell();
            TableCell rightcell5 = new TableCell();
            leftcell5.Controls.Add(lblEmployeeMobilePhoneNumber);
            rightcell5.Controls.Add(txtEmployeeMobilePhoneNumber);
            row4.Cells.Add(leftcell5);
            row4.Cells.Add(rightcell5);
            tblLayout.Rows.Add(row4);

            lblEmployeeLocation = new Label();
            lblEmployeeLocation.Text = "Location";
            txtEmployeeLocation = new TextBox();
            this.AllFields.AddLast(txtEmployeeLocation);
            TableRow row5 = new TableRow();
            TableCell leftcell6 = new TableCell();
            TableCell rightcell6 = new TableCell();
            leftcell6.Controls.Add(lblEmployeeLocation);
            rightcell6.Controls.Add(txtEmployeeLocation);
            row5.Cells.Add(leftcell6);
            row5.Cells.Add(rightcell6);
            tblLayout.Rows.Add(row5);

            lblEmployeeLocationCode = new Label();
            lblEmployeeLocationCode.Text = "Location code";
            txtEmployeeLocationCode = new TextBox();
            this.AllFields.AddLast(txtEmployeeLocationCode);
            TableRow row6 = new TableRow();
            TableCell leftcell7 = new TableCell();
            TableCell rightcell7 = new TableCell();
            leftcell7.Controls.Add(lblEmployeeLocationCode);
            rightcell7.Controls.Add(txtEmployeeLocationCode);
            row6.Cells.Add(leftcell7);
            row6.Cells.Add(rightcell7);
            tblLayout.Rows.Add(row6);

            lblEmployeeCompany = new Label();
            lblEmployeeCompany.Text = "Company";
            txtEmployeeCompany = new TextBox();
            this.AllFields.AddLast(txtEmployeeCompany);
            TableRow row71 = new TableRow();
            TableCell leftcell71 = new TableCell();
            TableCell rightcell71 = new TableCell();
            leftcell71.Controls.Add(lblEmployeeCompany);
            rightcell71.Controls.Add(txtEmployeeCompany);
            row71.Cells.Add(leftcell71);
            row71.Cells.Add(rightcell71);
            tblLayout.Rows.Add(row71);


            btnUpdate = new Button();
            btnUpdate.Text = "Update";
            btnUpdate.Click += new EventHandler(btnUpdate_Click);

            lblError = new Label();

            this.Controls.Add(tblLayout);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(lblError);

            loaddboEmployees();
            
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

            object []parameters = new object[10];
            parameters[0] = lblDisplayEmployeeID.Text;
            parameters[1] = txtEmployeeLastname.Text;
            parameters[2] = txtEmployeeFirstname.Text;
            parameters[3] = txtEmployeeEmail.Text;
            parameters[4] = txtEmployeeInternalPhoneNumber.Text;
            parameters[5] = txtEmployeeExternalPhoneNumber.Text;
            parameters[6] = txtEmployeeMobilePhoneNumber.Text;
            parameters[7] = txtEmployeeLocation.Text;
            parameters[8] = txtEmployeeLocationCode.Text;
            parameters[9] = txtEmployeeCompany.Text;


	        try
	        {
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.EmployeesUpdater", parameters);
	        }
	        catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblError.Text = "</br></br>Employee <b>" + txtEmployeeFirstname.Text  + " " + txtEmployeeLastname.Text + "</b> Successfully updated!";
            lblError.BackColor = System.Drawing.Color.LightBlue;

        }


        private void loaddboEmployees()
        {
            // add the identifiers...
            object []identifiers = new object[1];
            identifiers[0] = Convert.ToInt32(EmployeeID);

		    IEntityInstance ie = null;

	        try
	        {
            	ie = BdcHelpers.GetSpecificRecord(lobSystemInstance, entityName, identifiers);
	        }
	        catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblDisplayEmployeeID.Text = ie["EmployeeID"].ToString();
            if (ie["EmployeeFirstname"] != null)
            {
                txtEmployeeFirstname.Text = ie["EmployeeFirstname"].ToString();
            }
            if (ie["EmployeeLastname"] != null)
            {
                txtEmployeeLastname.Text = ie["EmployeeLastname"].ToString();
            }
            if(ie["EmployeeEmail"] != null)
            {
                txtEmployeeEmail.Text = ie["EmployeeEmail"].ToString();
            }
            if(ie["EmployeeInternalPhoneNumber"] != null)
            {
                txtEmployeeInternalPhoneNumber.Text = ie["EmployeeInternalPhoneNumber"].ToString();
            }
            if(ie["EmployeeExternalPhoneNumber"] != null)
            {
                txtEmployeeExternalPhoneNumber.Text = ie["EmployeeExternalPhoneNumber"].ToString();
            }
            if(ie["EmployeeMobilePhoneNumber"] != null)
            {
                txtEmployeeMobilePhoneNumber.Text = ie["EmployeeMobilePhoneNumber"].ToString();
            }
            if(ie["EmployeeLocation"] != null)
            {
                txtEmployeeLocation.Text = ie["EmployeeLocation"].ToString();
            }
            if(ie["EmployeeLocationCode"] != null)
            {
                txtEmployeeLocationCode.Text = ie["EmployeeLocationCode"].ToString();
            }
            if (ie["EmployeeCompany"] != null)
            {
                txtEmployeeCompany.Text = ie["EmployeeCompany"].ToString();
            }
        }
    }
}
