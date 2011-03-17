using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace Contacts
{
    public class UpdatedboProjectOffices : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;

        private Label lblProjectOfficeID;
        private Label lblDisplayProjectOfficeID;

        private Label lblProjectOfficeName;
        private TextBox txtProjectOfficeName;

        private Label lblProjectOfficeContact;
        private TextBox txtProjectOfficeContact;

        private Label lblProjectOfficePhoneNumber;
        private TextBox txtProjectOfficePhoneNumber;

        private Label lblProjectOfficeFaxNumber;
        private TextBox txtProjectOfficeFaxNumber;

        private Label lblProjectOfficeEmail;
        private TextBox txtProjectOfficeEmail;

        private Label lblProjectOfficeAddress;
        private TextBox txtProjectOfficeAddress;

        private Label lblProjectOfficeCountry;
        private TextBox txtProjectOfficeCountry;


        private Button btnUpdate;

	    private Label lblError;

        private string lobSystemInstance = "Contacts";
        private string entityName = "dbo.ProjectOffices";

        private string ProjectOfficeID
        {
            get
            {
                return Page.Request.QueryString["ProjectOfficeID"].ToString();
            }
        }


        protected override void CreateChildControls()
        {
            tblLayout = new Table();

            lblProjectOfficeID = new Label();
            lblProjectOfficeID.Text = "ProjectOfficeID";
            lblDisplayProjectOfficeID = new Label();

            lblProjectOfficeName = new Label();
            lblProjectOfficeName.Text = "Office name";
            txtProjectOfficeName = new TextBox();
            TableRow row0 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblProjectOfficeName);
            rightcell1.Controls.Add(txtProjectOfficeName);
            row0.Cells.Add(leftcell1);
            row0.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row0);

            lblProjectOfficeContact = new Label();
            lblProjectOfficeContact.Text = "Contact";
            txtProjectOfficeContact = new TextBox();
            TableRow row1 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblProjectOfficeContact);
            rightcell2.Controls.Add(txtProjectOfficeContact);
            row1.Cells.Add(leftcell2);
            row1.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row1);

            lblProjectOfficePhoneNumber = new Label();
            lblProjectOfficePhoneNumber.Text = "Phone number";
            txtProjectOfficePhoneNumber = new TextBox();
            TableRow row2 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            leftcell3.Controls.Add(lblProjectOfficePhoneNumber);
            rightcell3.Controls.Add(txtProjectOfficePhoneNumber);
            row2.Cells.Add(leftcell3);
            row2.Cells.Add(rightcell3);
            tblLayout.Rows.Add(row2);

            lblProjectOfficeFaxNumber = new Label();
            lblProjectOfficeFaxNumber.Text = "Fax number";
            txtProjectOfficeFaxNumber = new TextBox();
            TableRow row3 = new TableRow();
            TableCell leftcell4 = new TableCell();
            TableCell rightcell4 = new TableCell();
            leftcell4.Controls.Add(lblProjectOfficeFaxNumber);
            rightcell4.Controls.Add(txtProjectOfficeFaxNumber);
            row3.Cells.Add(leftcell4);
            row3.Cells.Add(rightcell4);
            tblLayout.Rows.Add(row3);

            lblProjectOfficeEmail = new Label();
            lblProjectOfficeEmail.Text = "Email";
            txtProjectOfficeEmail = new TextBox();
            TableRow row4 = new TableRow();
            TableCell leftcell5 = new TableCell();
            TableCell rightcell5 = new TableCell();
            leftcell5.Controls.Add(lblProjectOfficeEmail);
            rightcell5.Controls.Add(txtProjectOfficeEmail);
            row4.Cells.Add(leftcell5);
            row4.Cells.Add(rightcell5);
            tblLayout.Rows.Add(row4);

            lblProjectOfficeAddress = new Label();
            lblProjectOfficeAddress.Text = "Address";
            txtProjectOfficeAddress = new TextBox();
            TableRow row5 = new TableRow();
            TableCell leftcell6 = new TableCell();
            TableCell rightcell6 = new TableCell();
            leftcell6.Controls.Add(lblProjectOfficeAddress);
            rightcell6.Controls.Add(txtProjectOfficeAddress);
            row5.Cells.Add(leftcell6);
            row5.Cells.Add(rightcell6);
            tblLayout.Rows.Add(row5);

            lblProjectOfficeCountry = new Label();
            lblProjectOfficeCountry.Text = "Country";
            txtProjectOfficeCountry = new TextBox();
            TableRow row6 = new TableRow();
            TableCell leftcell7 = new TableCell();
            TableCell rightcell7 = new TableCell();
            leftcell7.Controls.Add(lblProjectOfficeCountry);
            rightcell7.Controls.Add(txtProjectOfficeCountry);
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

            loaddboProjectOffices();
            
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
            parameters[0] = lblDisplayProjectOfficeID.Text;
            parameters[1] = txtProjectOfficeName.Text;
            parameters[2] = txtProjectOfficeContact.Text;
            parameters[3] = txtProjectOfficePhoneNumber.Text;
            parameters[4] = txtProjectOfficeFaxNumber.Text;
            parameters[5] = txtProjectOfficeEmail.Text;
            parameters[6] = txtProjectOfficeAddress.Text;
            parameters[7] = txtProjectOfficeCountry.Text;


            try
            {
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.ProjectOfficesUpdater", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }
            lblError.Text = "</br></br>Office <b>" + txtProjectOfficeName.Text + "</b> Successfully updated!";
            lblError.BackColor = System.Drawing.Color.LightBlue;
        }

        private void loaddboProjectOffices()
        {
            // add the identifiers...
            object []identifiers = new object[1];
            identifiers[0] = Convert.ToInt32(ProjectOfficeID);


		    IEntityInstance ie = null;
	        try
	        {
            	    ie = BdcHelpers.GetSpecificRecord(lobSystemInstance, entityName, identifiers);
	        }
	        catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblDisplayProjectOfficeID.Text = ie["ProjectOfficeID"].ToString();
            if(ie["ProjectOfficeName"] != null)
            txtProjectOfficeName.Text = ie["ProjectOfficeName"].ToString();
            if(ie["ProjectOfficeContact"] != null)
            txtProjectOfficeContact.Text = ie["ProjectOfficeContact"].ToString();
            if(ie["ProjectOfficePhoneNumber"] != null)
            txtProjectOfficePhoneNumber.Text = ie["ProjectOfficePhoneNumber"].ToString();
            if(ie["ProjectOfficeFaxNumber"] != null)
            txtProjectOfficeFaxNumber.Text = ie["ProjectOfficeFaxNumber"].ToString();
            if(ie["ProjectOfficeEmail"] != null)
            txtProjectOfficeEmail.Text = ie["ProjectOfficeEmail"].ToString();
            if(ie["ProjectOfficeAddress"] != null)
            txtProjectOfficeAddress.Text = ie["ProjectOfficeAddress"].ToString();
            if(ie["ProjectOfficeCountry"] != null)
            txtProjectOfficeCountry.Text = ie["ProjectOfficeCountry"].ToString();

        }
    }
}
