using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using PAFMAintenance;

namespace PAFMaintenance
{
    public class AdddboCurrency : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;
        private Label lblCurrencyID;
        private TextBox txtCurrencyID;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblCurrencyToProjectrate;
        private TextBox txtCurrencyToProjectrate;
        private Label lblComments;
        private TextBox txtComments;


        private Button btnNew;

        private Label lblError;

        private Label lblMandatoryFields;

        // Contains all the mandatories fields of the form
        private LinkedList<TextBox> mandatoryFields;

        // Contains all the fields of the form.
        private LinkedList<TextBox> AllFields;

        private string lobSystemInstance = "PAF_Maintenance";
        private string entityName = "dbo.Currency";

        protected override void CreateChildControls()
        {
            //Init of the lists
            this.mandatoryFields = new LinkedList<TextBox>();
            this.AllFields = new LinkedList<TextBox>();

            tblLayout = new Table();

            lblCurrencyID = new Label();
            lblCurrencyID.Text = "Currency ID (*)";
            txtCurrencyID = new TextBox();
            this.mandatoryFields.AddLast(txtCurrencyID);
            this.AllFields.AddLast(txtCurrencyID);
            TableRow row0 = new TableRow();
            TableCell leftcell0 = new TableCell();
            TableCell rightcell0 = new TableCell();
            leftcell0.Controls.Add(lblCurrencyID);
            rightcell0.Controls.Add(txtCurrencyID);
            row0.Cells.Add(leftcell0);
            row0.Cells.Add(rightcell0);
            tblLayout.Rows.Add(row0);


            lblDescription = new Label();
            lblDescription.Text = "Description";
            txtDescription = new TextBox();
            this.AllFields.AddLast(txtDescription);
            TableRow row1 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblDescription);
            rightcell1.Controls.Add(txtDescription);
            row1.Cells.Add(leftcell1);
            row1.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row1);


            lblCurrencyToProjectrate = new Label();
            lblCurrencyToProjectrate.Text = "Currency To Project rate";
            txtCurrencyToProjectrate = new TextBox();
            this.AllFields.AddLast(txtCurrencyToProjectrate);
            TableRow row2 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblCurrencyToProjectrate);
            rightcell2.Controls.Add(txtCurrencyToProjectrate);
            row2.Cells.Add(leftcell2);
            row2.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row2);


            lblComments = new Label();
            lblComments.Text = "Comments";
            txtComments = new TextBox();
            this.AllFields.AddLast(txtComments);
            TableRow row3 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            leftcell3.Controls.Add(lblComments);
            rightcell3.Controls.Add(txtComments);
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
                parameters[0] = txtCurrencyID.Text;
                parameters[1] = txtDescription.Text;
                parameters[2] = txtCurrencyToProjectrate.Text;
                parameters[3] = txtComments.Text;


                try
                {
                    BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.CurrencyInserter", parameters);

                    lblError.Text = "<br><br>Currency <b>" + txtCurrencyID.Text + "</b> successfully added!";
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
