using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using PAFMAintenance;

namespace PAFMaintenance
{
    public class AdddboCompany : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;
        private Label lblCompanyID;
        private TextBox txtCompanyID;
        private Label lblCompanyName;
        private TextBox txtCompanyName;


        private Button btnNew;

        private Label lblError;

        private Label lblMandatoryFields;

        // Contains all the mandatories fields of the form
        private LinkedList<TextBox> mandatoryFields;

        // Contains all the fields of the form.
        private LinkedList<TextBox> AllFields;

        private string lobSystemInstance = "PAF_Maintenance";
        private string entityName = "dbo.Company";

        protected override void CreateChildControls()
        {
            //Init of the lists
            this.mandatoryFields = new LinkedList<TextBox>();
            this.AllFields = new LinkedList<TextBox>();

            tblLayout = new Table();

            lblCompanyID = new Label();
            lblCompanyID.Text = "Company ID (*)";
            txtCompanyID = new TextBox();
            this.mandatoryFields.AddLast(txtCompanyID);
            this.AllFields.AddLast(txtCompanyID);
            TableRow row0 = new TableRow();
            TableCell leftcell0 = new TableCell();
            TableCell rightcell0 = new TableCell();
            leftcell0.Controls.Add(lblCompanyID);
            rightcell0.Controls.Add(txtCompanyID);
            row0.Cells.Add(leftcell0);
            row0.Cells.Add(rightcell0);
            tblLayout.Rows.Add(row0);


            lblCompanyName = new Label();
            lblCompanyName.Text = "Company Name";
            txtCompanyName = new TextBox();
            this.AllFields.AddLast(txtCompanyName);
            TableRow row1 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblCompanyName);
            rightcell1.Controls.Add(txtCompanyName);
            row1.Cells.Add(leftcell1);
            row1.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row1);



            lblMandatoryFields = new Label();
            lblMandatoryFields.Text = "Fields with (*) are mandatory </br></br>";
            TableRow row3 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            rightcell3.Controls.Add(lblMandatoryFields);
            row3.Cells.Add(leftcell3);
            row3.Cells.Add(rightcell3);
            tblLayout.Rows.Add(row3);


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

                object[] parameters = new object[2];
                parameters[0] = txtCompanyID.Text;
                parameters[1] = txtCompanyName.Text;


                try
                {
                    BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.CompanyInserter", parameters);

                    lblError.Text = "<br><br>Company <b>" + txtCompanyName.Text + "</b> successfully added!";
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
