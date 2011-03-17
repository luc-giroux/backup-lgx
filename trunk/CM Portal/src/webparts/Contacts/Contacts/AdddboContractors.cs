using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Contacts
{
    public class AdddboContractors : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;

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


        private Button btnNew;

        private Label lblError;

        private Label lblMandatoryFields;

        // Contains all the mandatories fields of the form
        private LinkedList<TextBox> mandatoryFields;

        // Contains all the fields of the form.
        private LinkedList<TextBox> AllFields;

        private string lobSystemInstance = "Contacts";
        private string entityName = "dbo.Contractors";

        protected override void CreateChildControls()
        {

            //Init of the lists
            this.mandatoryFields = new LinkedList<TextBox>();
            this.AllFields = new LinkedList<TextBox>();

            tblLayout = new Table();

            lblContractorCompany = new Label();
            lblContractorCompany.Text = "Company (*)";
            txtContractorCompany = new TextBox();
            this.mandatoryFields.AddLast(txtContractorCompany);
            this.AllFields.AddLast(txtContractorCompany);
            TableRow row0 = new TableRow();
            TableCell leftcell0 = new TableCell();
            TableCell rightcell0 = new TableCell();
            leftcell0.Controls.Add(lblContractorCompany);
            rightcell0.Controls.Add(txtContractorCompany);
            row0.Cells.Add(leftcell0);
            row0.Cells.Add(rightcell0);
            tblLayout.Rows.Add(row0);

            lblContractorLastname = new Label();
            lblContractorLastname.Text = "Last name";
            txtContractorLastname = new TextBox();
            this.AllFields.AddLast(txtContractorLastname);
            TableRow row1 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblContractorLastname);
            rightcell1.Controls.Add(txtContractorLastname);
            row1.Cells.Add(leftcell1);
            row1.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row1);

            lblContractorFirstname = new Label();
            lblContractorFirstname.Text = "First name";
            txtContractorFirstname = new TextBox();
            this.AllFields.AddLast(txtContractorFirstname);
            TableRow row2 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblContractorFirstname);
            rightcell2.Controls.Add(txtContractorFirstname);
            row2.Cells.Add(leftcell2);
            row2.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row2);

            lblContractorEmail = new Label();
            lblContractorEmail.Text = "Email";
            txtContractorEmail = new TextBox();
            this.AllFields.AddLast(txtContractorEmail);
            TableRow row3 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            leftcell3.Controls.Add(lblContractorEmail);
            rightcell3.Controls.Add(txtContractorEmail);
            row3.Cells.Add(leftcell3);
            row3.Cells.Add(rightcell3);
            tblLayout.Rows.Add(row3);

            lblContractorPhoneNumber = new Label();
            lblContractorPhoneNumber.Text = "Phone Number (*)";
            txtContractorPhoneNumber = new TextBox();
            this.mandatoryFields.AddLast(txtContractorPhoneNumber);
            this.AllFields.AddLast(txtContractorPhoneNumber);
            TableRow row4 = new TableRow();
            TableCell leftcell4 = new TableCell();
            TableCell rightcell4 = new TableCell();
            leftcell4.Controls.Add(lblContractorPhoneNumber);
            rightcell4.Controls.Add(txtContractorPhoneNumber);
            row4.Cells.Add(leftcell4);
            row4.Cells.Add(rightcell4);
            tblLayout.Rows.Add(row4);

            lblContractorContractNumber = new Label();
            lblContractorContractNumber.Text = "Contract Number";
            txtContractorContractNumber = new TextBox();
            this.AllFields.AddLast(txtContractorContractNumber);
            TableRow row5 = new TableRow();
            TableCell leftcell5 = new TableCell();
            TableCell rightcell5 = new TableCell();
            leftcell5.Controls.Add(lblContractorContractNumber);
            rightcell5.Controls.Add(txtContractorContractNumber);
            row5.Cells.Add(leftcell5);
            row5.Cells.Add(rightcell5);
            tblLayout.Rows.Add(row5);

            lblContractorLocationCode = new Label();
            lblContractorLocationCode.Text = "Location Code";
            txtContractorLocationCode = new TextBox();
            this.AllFields.AddLast(txtContractorLocationCode);
            TableRow row6 = new TableRow();
            TableCell leftcell6 = new TableCell();
            TableCell rightcell6 = new TableCell();
            leftcell6.Controls.Add(lblContractorLocationCode);
            rightcell6.Controls.Add(txtContractorLocationCode);
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
                parameters[0] = txtContractorCompany.Text;
                parameters[1] = txtContractorLastname.Text;
                parameters[2] = txtContractorFirstname.Text;
                parameters[3] = txtContractorEmail.Text;
                parameters[4] = txtContractorPhoneNumber.Text;
                parameters[5] = txtContractorContractNumber.Text;
                parameters[6] = txtContractorLocationCode.Text;

                try
                {
                    BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.ContractorsInserter", parameters);

                    lblError.Text = "<br><br>Contractor <b>" + txtContractorCompany.Text + " " +
                                    txtContractorLastname.Text + " " + txtContractorFirstname.Text + "</b> successfully added!";
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
