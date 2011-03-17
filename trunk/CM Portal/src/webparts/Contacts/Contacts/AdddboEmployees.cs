using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Contacts
{
    public class AdddboEmployees : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;

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

        private Button btnNew;

        private Label lblError;

        private Label lblMandatoryFields;

        // Contains all the mandatories fields of the form
        private LinkedList<TextBox> mandatoryFields;

        // Contains all the fields of the form.
        private LinkedList<TextBox> AllFields;

        private string lobSystemInstance = "Contacts";
        private string entityName = "dbo.Employees";

        protected override void CreateChildControls()
        {

            #region Création du formulaire

            //Init of the lists
            this.mandatoryFields = new LinkedList<TextBox>();
            this.AllFields = new LinkedList<TextBox>();


            tblLayout = new Table();

            lblEmployeeFirstname = new Label();
            lblEmployeeFirstname.Text = "First name (*)";
            txtEmployeeFirstname = new TextBox();
            this.mandatoryFields.AddLast(txtEmployeeFirstname);
            this.AllFields.AddLast(txtEmployeeFirstname);
            TableRow row0 = new TableRow();
            TableCell leftcell0 = new TableCell();
            TableCell rightcell0 = new TableCell();
            leftcell0.Controls.Add(lblEmployeeFirstname);
            rightcell0.Controls.Add(txtEmployeeFirstname);
            row0.Cells.Add(leftcell0);
            row0.Cells.Add(rightcell0);
            tblLayout.Rows.Add(row0);

            lblEmployeeLastname = new Label();
            lblEmployeeLastname.Text = "Last name (*)";
            txtEmployeeLastname = new TextBox();
            this.mandatoryFields.AddLast(txtEmployeeLastname);
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
            lblEmployeeEmail.Text = "Email (*)";
            txtEmployeeEmail = new TextBox();
            this.mandatoryFields.AddLast(txtEmployeeEmail);
            this.AllFields.AddLast(txtEmployeeEmail);
            TableRow row2 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblEmployeeEmail);
            rightcell2.Controls.Add(txtEmployeeEmail);
            row2.Cells.Add(leftcell2);
            row2.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row2);

            lblEmployeeInternalPhoneNumber = new Label();
            lblEmployeeInternalPhoneNumber.Text = "Internal phone (*)";
            txtEmployeeInternalPhoneNumber = new TextBox();
            this.mandatoryFields.AddLast(txtEmployeeInternalPhoneNumber);
            this.AllFields.AddLast(txtEmployeeInternalPhoneNumber);
            TableRow row3 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            leftcell3.Controls.Add(lblEmployeeInternalPhoneNumber);
            rightcell3.Controls.Add(txtEmployeeInternalPhoneNumber);
            row3.Cells.Add(leftcell3);
            row3.Cells.Add(rightcell3);
            tblLayout.Rows.Add(row3);

            lblEmployeeExternalPhoneNumber = new Label();
            lblEmployeeExternalPhoneNumber.Text = "External phone";
            txtEmployeeExternalPhoneNumber = new TextBox();
            this.AllFields.AddLast(txtEmployeeExternalPhoneNumber);
            TableRow row4 = new TableRow();
            TableCell leftcell4 = new TableCell();
            TableCell rightcell4 = new TableCell();
            leftcell4.Controls.Add(lblEmployeeExternalPhoneNumber);
            rightcell4.Controls.Add(txtEmployeeExternalPhoneNumber);
            row4.Cells.Add(leftcell4);
            row4.Cells.Add(rightcell4);
            tblLayout.Rows.Add(row4);

            lblEmployeeMobilePhoneNumber = new Label();
            lblEmployeeMobilePhoneNumber.Text = "Mobile phone";
            txtEmployeeMobilePhoneNumber = new TextBox();
            this.AllFields.AddLast(txtEmployeeMobilePhoneNumber);
            TableRow row5 = new TableRow();
            TableCell leftcell5 = new TableCell();
            TableCell rightcell5 = new TableCell();
            leftcell5.Controls.Add(lblEmployeeMobilePhoneNumber);
            rightcell5.Controls.Add(txtEmployeeMobilePhoneNumber);
            row5.Cells.Add(leftcell5);
            row5.Cells.Add(rightcell5);
            tblLayout.Rows.Add(row5);

            lblEmployeeLocation = new Label();
            lblEmployeeLocation.Text = "Location";
            txtEmployeeLocation = new TextBox();
            this.AllFields.AddLast(txtEmployeeLocation);
            TableRow row6 = new TableRow();
            TableCell leftcell6 = new TableCell();
            TableCell rightcell6 = new TableCell();
            leftcell6.Controls.Add(lblEmployeeLocation);
            rightcell6.Controls.Add(txtEmployeeLocation);
            row6.Cells.Add(leftcell6);
            row6.Cells.Add(rightcell6);
            tblLayout.Rows.Add(row6);

            lblEmployeeLocationCode = new Label();
            lblEmployeeLocationCode.Text = "Location code";
            txtEmployeeLocationCode = new TextBox();
            this.AllFields.AddLast(txtEmployeeLocationCode);
            TableRow row7 = new TableRow();
            TableCell leftcell7 = new TableCell();
            TableCell rightcell7 = new TableCell();
            leftcell7.Controls.Add(lblEmployeeLocationCode);
            rightcell7.Controls.Add(txtEmployeeLocationCode);
            row7.Cells.Add(leftcell7);
            row7.Cells.Add(rightcell7);
            tblLayout.Rows.Add(row7);

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

            lblMandatoryFields = new Label();
            lblMandatoryFields.Text = "Fields with (*) are mandatory </br></br>";
            TableRow row8 = new TableRow();
            TableCell leftcell8 = new TableCell();
            TableCell rightcell8 = new TableCell();
            rightcell8.Controls.Add(lblMandatoryFields);
            row8.Cells.Add(leftcell8);
            row8.Cells.Add(rightcell8);
            tblLayout.Rows.Add(row8);

            #endregion

            btnNew = new Button();
            btnNew.Text = "Add";
            btnNew.Click += new EventHandler(btnNew_Click);

            lblError = new Label();

            this.Controls.Add(tblLayout);
            this.Controls.Add(btnNew);
            this.Controls.Add(lblError);           
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            this.tblLayout.RenderControl(writer);
            this.btnNew.RenderControl(writer);
            this.lblError.RenderControl(writer);
        }


        /*
         * Method called when the user clics on the ADD button
         */
        void btnNew_Click(object sender, EventArgs e)
        {
			lblError.Text = "";
            lblError.BackColor = System.Drawing.Color.White;
            lblMandatoryFields.BackColor = System.Drawing.Color.White;

            //If required fields are not blank
            if (WebpartUtils.checkMandatoryFields(this.mandatoryFields))
            {
                object[] parameters = new object[9];
                parameters[0] = txtEmployeeFirstname.Text;
                parameters[1] = txtEmployeeLastname.Text;
                parameters[2] = txtEmployeeEmail.Text;
                parameters[3] = txtEmployeeInternalPhoneNumber.Text;
                parameters[4] = txtEmployeeExternalPhoneNumber.Text;
                parameters[5] = txtEmployeeMobilePhoneNumber.Text;
                parameters[6] = txtEmployeeLocation.Text;
                parameters[7] = txtEmployeeLocationCode.Text;
                parameters[8] = txtEmployeeCompany.Text;


                try
                {
                    BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.EmployeesInserter", parameters);

                    lblError.Text = "<br><br>Employee <b>"+ txtEmployeeFirstname.Text + " "  +
                        txtEmployeeLastname.Text + "</b> successfully added!";
                    lblError.BackColor = System.Drawing.Color.LightBlue;

                    //Remise a blanc des champs
                    WebpartUtils.resetFields(this.AllFields);
                }
                catch (Exception exception)
                {
                    lblError.Text = exception.ToString();
                }
            }
            else
            {
                lblMandatoryFields.BackColor = System.Drawing.Color.Red;
            }

        }
    }
}
