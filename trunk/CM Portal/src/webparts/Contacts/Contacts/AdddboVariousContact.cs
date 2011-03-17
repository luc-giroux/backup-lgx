using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Contacts
{
    public class AdddboVariousContact : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;

        private Label lblVariousContactName;
        private TextBox txtVariousContactName;

        private Label lblVariousContactPhoneNumber;
        private TextBox txtVariousContactPhoneNumber;

        private Label lblVariousContactFaxNumber;
        private TextBox txtVariousContactFaxNumber;

        private Label lblVariousContactEmail;
        private TextBox txtVariousContactEmail;

        private Label lblMandatoryFields;

        // Contains all the mandatories fields of the form
        private LinkedList<TextBox> mandatoryFields;

        // Contains all the fields of the form.
        private LinkedList<TextBox> AllFields;

        private Button btnNew;

        private Label lblError;

        private string lobSystemInstance = "Contacts";
        private string entityName = "dbo.VariousContact";

        protected override void CreateChildControls()
        {
            tblLayout = new Table();

            //Init of the lists
            this.mandatoryFields = new LinkedList<TextBox>();
            this.AllFields = new LinkedList<TextBox>();

            lblVariousContactName = new Label();
            lblVariousContactName.Text = "Name (*)";
            txtVariousContactName = new TextBox();
            this.mandatoryFields.AddLast(txtVariousContactName);
            this.AllFields.AddLast(txtVariousContactName);
            TableRow row0 = new TableRow();
            TableCell leftcell0 = new TableCell();
            TableCell rightcell0 = new TableCell();
            leftcell0.Controls.Add(lblVariousContactName);
            rightcell0.Controls.Add(txtVariousContactName);
            row0.Cells.Add(leftcell0);
            row0.Cells.Add(rightcell0);
            tblLayout.Rows.Add(row0);

            lblVariousContactPhoneNumber = new Label();
            lblVariousContactPhoneNumber.Text = "Phone number (*)";
            txtVariousContactPhoneNumber = new TextBox();
            this.mandatoryFields.AddLast(txtVariousContactPhoneNumber);
            this.AllFields.AddLast(txtVariousContactPhoneNumber);
            TableRow row1 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblVariousContactPhoneNumber);
            rightcell1.Controls.Add(txtVariousContactPhoneNumber);
            row1.Cells.Add(leftcell1);
            row1.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row1);

            lblVariousContactFaxNumber = new Label();
            lblVariousContactFaxNumber.Text = "Fax number";
            txtVariousContactFaxNumber = new TextBox();
            this.AllFields.AddLast(txtVariousContactFaxNumber);
            TableRow row2 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblVariousContactFaxNumber);
            rightcell2.Controls.Add(txtVariousContactFaxNumber);
            row2.Cells.Add(leftcell2);
            row2.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row2);

            lblVariousContactEmail = new Label();
            lblVariousContactEmail.Text = "Email";
            txtVariousContactEmail = new TextBox();
            this.AllFields.AddLast(txtVariousContactEmail);
            TableRow row3 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            leftcell3.Controls.Add(lblVariousContactEmail);
            rightcell3.Controls.Add(txtVariousContactEmail);
            row3.Cells.Add(leftcell3);
            row3.Cells.Add(rightcell3);
            tblLayout.Rows.Add(row3);

            lblMandatoryFields = new Label();
            lblMandatoryFields.Text = "Fields with (*) are mandatory </br></br>";
            TableRow row4 = new TableRow();
            TableCell leftcell4 = new TableCell();
            TableCell rightcell4 = new TableCell();
            rightcell4.Controls.Add(lblMandatoryFields);
            row4.Cells.Add(leftcell4);
            row4.Cells.Add(rightcell4);
            tblLayout.Rows.Add(row4);

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

                object[] parameters = new object[4];
                parameters[0] = txtVariousContactName.Text;
                parameters[1] = txtVariousContactPhoneNumber.Text;
                parameters[2] = txtVariousContactFaxNumber.Text;
                parameters[3] = txtVariousContactEmail.Text;


                try
                {
                    BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.VariousContactInserter", parameters);

                    lblError.Text = "<br><br>Contact <b>" + txtVariousContactName.Text + " " +"</b> successfully added!";
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
