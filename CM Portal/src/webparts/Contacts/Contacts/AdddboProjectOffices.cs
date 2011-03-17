using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Contacts
{
    public class AdddboProjectOffices : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;

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

        private Label lblMandatoryFields;

        // Contains all the mandatories fields of the form
        private LinkedList<TextBox> mandatoryFields;

        // Contains all the fields of the form.
        private LinkedList<TextBox> AllFields;

        private Button btnNew;

        private Label lblError;

        private string lobSystemInstance = "Contacts";
        private string entityName = "dbo.ProjectOffices";

        protected override void CreateChildControls()
        {
            //Init of the lists
            this.mandatoryFields = new LinkedList<TextBox>();
            this.AllFields = new LinkedList<TextBox>();

            tblLayout = new Table();

            lblProjectOfficeName = new Label();
            lblProjectOfficeName.Text = "Office Name (*)";
            txtProjectOfficeName = new TextBox();
            this.mandatoryFields.AddLast(txtProjectOfficeName);
            this.AllFields.AddLast(txtProjectOfficeName);
            TableRow row0 = new TableRow();
            TableCell leftcell0 = new TableCell();
            TableCell rightcell0 = new TableCell();
            leftcell0.Controls.Add(lblProjectOfficeName);
            rightcell0.Controls.Add(txtProjectOfficeName);
            row0.Cells.Add(leftcell0);
            row0.Cells.Add(rightcell0);
            tblLayout.Rows.Add(row0);

            lblProjectOfficeContact = new Label();
            lblProjectOfficeContact.Text = "Contact";
            txtProjectOfficeContact = new TextBox();
            this.AllFields.AddLast(txtProjectOfficeContact);
            TableRow row1 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblProjectOfficeContact);
            rightcell1.Controls.Add(txtProjectOfficeContact);
            row1.Cells.Add(leftcell1);
            row1.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row1);

            lblProjectOfficePhoneNumber = new Label();
            lblProjectOfficePhoneNumber.Text = "Phone Number (*)";
            txtProjectOfficePhoneNumber = new TextBox();
            this.mandatoryFields.AddLast(txtProjectOfficePhoneNumber);
            this.AllFields.AddLast(txtProjectOfficePhoneNumber);
            TableRow row2 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblProjectOfficePhoneNumber);
            rightcell2.Controls.Add(txtProjectOfficePhoneNumber);
            row2.Cells.Add(leftcell2);
            row2.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row2);

            lblProjectOfficeFaxNumber = new Label();
            lblProjectOfficeFaxNumber.Text = "Fax number";
            txtProjectOfficeFaxNumber = new TextBox();
            this.AllFields.AddLast(txtProjectOfficeFaxNumber);
            TableRow row3 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            leftcell3.Controls.Add(lblProjectOfficeFaxNumber);
            rightcell3.Controls.Add(txtProjectOfficeFaxNumber);
            row3.Cells.Add(leftcell3);
            row3.Cells.Add(rightcell3);
            tblLayout.Rows.Add(row3);

            lblProjectOfficeEmail = new Label();
            lblProjectOfficeEmail.Text = "Email";
            txtProjectOfficeEmail = new TextBox();
            this.AllFields.AddLast(txtProjectOfficeEmail);
            TableRow row4 = new TableRow();
            TableCell leftcell4 = new TableCell();
            TableCell rightcell4 = new TableCell();
            leftcell4.Controls.Add(lblProjectOfficeEmail);
            rightcell4.Controls.Add(txtProjectOfficeEmail);
            row4.Cells.Add(leftcell4);
            row4.Cells.Add(rightcell4);
            tblLayout.Rows.Add(row4);

            lblProjectOfficeAddress = new Label();
            lblProjectOfficeAddress.Text = "Address (*)";
            txtProjectOfficeAddress = new TextBox();
            this.mandatoryFields.AddLast(txtProjectOfficeAddress);
            this.AllFields.AddLast(txtProjectOfficeAddress);
            TableRow row5 = new TableRow();
            TableCell leftcell5 = new TableCell();
            TableCell rightcell5 = new TableCell();
            leftcell5.Controls.Add(lblProjectOfficeAddress);
            rightcell5.Controls.Add(txtProjectOfficeAddress);
            row5.Cells.Add(leftcell5);
            row5.Cells.Add(rightcell5);
            tblLayout.Rows.Add(row5);

            lblProjectOfficeCountry = new Label();
            lblProjectOfficeCountry.Text = "Country";
            txtProjectOfficeCountry = new TextBox();
            this.AllFields.AddLast(txtProjectOfficeCountry);
            TableRow row6 = new TableRow();
            TableCell leftcell6 = new TableCell();
            TableCell rightcell6 = new TableCell();
            leftcell6.Controls.Add(lblProjectOfficeCountry);
            rightcell6.Controls.Add(txtProjectOfficeCountry);
            row6.Cells.Add(leftcell6);
            row6.Cells.Add(rightcell6);
            tblLayout.Rows.Add(row6);

            lblMandatoryFields = new Label();
            lblMandatoryFields.Text = "Fields with (*) are mandatory </br></br>";
            TableRow row7 = new TableRow();
            TableCell leftcell7 = new TableCell();
            TableCell rightcell7 = new TableCell();
            rightcell7.Controls.Add(lblMandatoryFields);
            row7.Cells.Add(leftcell7);
            row7.Cells.Add(rightcell7);
            tblLayout.Rows.Add(row7);


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

        void btnNew_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError.BackColor = System.Drawing.Color.White;
            lblMandatoryFields.BackColor = System.Drawing.Color.White;

            //If required fields are not blank
            if (WebpartUtils.checkMandatoryFields(this.mandatoryFields))
            {

                object[] parameters = new object[7];
                parameters[0] = txtProjectOfficeName.Text;
                parameters[1] = txtProjectOfficeContact.Text;
                parameters[2] = txtProjectOfficePhoneNumber.Text;
                parameters[3] = txtProjectOfficeFaxNumber.Text;
                parameters[4] = txtProjectOfficeEmail.Text;
                parameters[5] = txtProjectOfficeAddress.Text;
                parameters[6] = txtProjectOfficeCountry.Text;


                try
                {
                    BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.ProjectOfficesInserter", parameters);

                    lblError.Text = "<br><br>Office <b>" + txtProjectOfficeName.Text + " " +"</b> successfully added!";
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
