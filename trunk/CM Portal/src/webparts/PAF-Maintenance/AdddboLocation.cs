using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using PAFMAintenance;

namespace PAFMaintenance
{
    public class AdddboLocation : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;
        private Label lblLocationID;
        private TextBox txtLocationID;
        private Label lblLocationName;
        private TextBox txtLocationName;
        private Label lblCompanyID;
        private TextBox txtCompanyID;
        private Label lblCurrencyID;
        private TextBox txtCurrencyID;
        private Label lblStandardWorkingHours;
        private TextBox txtStandardWorkingHours;
        private Label lblMultiplier;
        private TextBox txtMultiplier;
        private Label lblOtherOfficeSpace;
        private TextBox txtOtherOfficeSpace;
        private Label lblOtherOfficeExpenses;
        private TextBox txtOtherOfficeExpenses;
        private Label lblOtherIT;
        private TextBox txtOtherIT;
        private Label lblOtherITCore;
        private TextBox txtOtherITCore;
        private Label lblOtherTotal;
        private TextBox txtOtherTotal;
        private Label lblRevision;
        private TextBox txtRevision;
        private Label lblRevisionEffectiveDate;
        private TextBox txtRevisionEffectiveDate;
        private Label lblComments;
        private TextBox txtComments;
        private Label lblSortOrder;
        private TextBox txtSortOrder;


        private Button btnNew;

        private Label lblError;

        private Label lblMandatoryFields;

        // Contains all the mandatories fields of the form
        private LinkedList<TextBox> mandatoryFields;

        // Contains all the fields of the form.
        private LinkedList<TextBox> AllFields;

        private string lobSystemInstance = "PAF_Maintenance";
        private string entityName = "dbo.Location";

        protected override void CreateChildControls()
        {
            //Init of the lists
            this.mandatoryFields = new LinkedList<TextBox>();
            this.AllFields = new LinkedList<TextBox>();

            tblLayout = new Table();

            lblLocationID = new Label();
            lblLocationID.Text = "Location ID (*)";
            txtLocationID = new TextBox();
            this.mandatoryFields.AddLast(txtLocationID);
            this.AllFields.AddLast(txtLocationID);
            TableRow row0 = new TableRow();
            TableCell leftcell0 = new TableCell();
            TableCell rightcell0 = new TableCell();
            leftcell0.Controls.Add(lblLocationID);
            rightcell0.Controls.Add(txtLocationID);
            row0.Cells.Add(leftcell0);
            row0.Cells.Add(rightcell0);
            tblLayout.Rows.Add(row0);


            lblLocationName = new Label();
            lblLocationName.Text = "Location Name (*)";
            txtLocationName = new TextBox();
            this.mandatoryFields.AddLast(txtLocationName);
            this.AllFields.AddLast(txtLocationName);
            TableRow row1 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblLocationName);
            rightcell1.Controls.Add(txtLocationName);
            row1.Cells.Add(leftcell1);
            row1.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row1);


            lblCompanyID = new Label();
            lblCompanyID.Text = "Company ID";
            txtCompanyID = new TextBox();
            this.AllFields.AddLast(txtCompanyID);
            TableRow row2 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblCompanyID);
            rightcell2.Controls.Add(txtCompanyID);
            row2.Cells.Add(leftcell2);
            row2.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row2);


            lblCurrencyID = new Label();
            lblCurrencyID.Text = "Currency ID";
            txtCurrencyID = new TextBox();
            this.AllFields.AddLast(txtCurrencyID);
            TableRow row3 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            leftcell3.Controls.Add(lblCurrencyID);
            rightcell3.Controls.Add(txtCurrencyID);
            row3.Cells.Add(leftcell3);
            row3.Cells.Add(rightcell3);
            tblLayout.Rows.Add(row3);


            lblStandardWorkingHours = new Label();
            lblStandardWorkingHours.Text = "Standard Working Hours";
            txtStandardWorkingHours = new TextBox();
            this.AllFields.AddLast(txtStandardWorkingHours);
            TableRow row4 = new TableRow();
            TableCell leftcell4 = new TableCell();
            TableCell rightcell4 = new TableCell();
            leftcell4.Controls.Add(lblStandardWorkingHours);
            rightcell4.Controls.Add(txtStandardWorkingHours);
            row4.Cells.Add(leftcell4);
            row4.Cells.Add(rightcell4);
            tblLayout.Rows.Add(row4);


            lblMultiplier = new Label();
            lblMultiplier.Text = "Multiplier";
            txtMultiplier = new TextBox();
            this.AllFields.AddLast(txtMultiplier);
            TableRow row5 = new TableRow();
            TableCell leftcell5 = new TableCell();
            TableCell rightcell5 = new TableCell();
            leftcell5.Controls.Add(lblMultiplier);
            rightcell5.Controls.Add(txtMultiplier);
            row5.Cells.Add(leftcell5);
            row5.Cells.Add(rightcell5);
            tblLayout.Rows.Add(row5);


            lblOtherOfficeSpace = new Label();
            lblOtherOfficeSpace.Text = "Other Office Space";
            txtOtherOfficeSpace = new TextBox();
            this.AllFields.AddLast(txtOtherOfficeSpace);
            TableRow row6 = new TableRow();
            TableCell leftcell6 = new TableCell();
            TableCell rightcell6 = new TableCell();
            leftcell6.Controls.Add(lblOtherOfficeSpace);
            rightcell6.Controls.Add(txtOtherOfficeSpace);
            row6.Cells.Add(leftcell6);
            row6.Cells.Add(rightcell6);
            tblLayout.Rows.Add(row6);


            lblOtherOfficeExpenses = new Label();
            lblOtherOfficeExpenses.Text = "Other Office Expenses";
            txtOtherOfficeExpenses = new TextBox();
            this.AllFields.AddLast(txtOtherOfficeExpenses);
            TableRow row7 = new TableRow();
            TableCell leftcell7 = new TableCell();
            TableCell rightcell7 = new TableCell();
            leftcell7.Controls.Add(lblOtherOfficeExpenses);
            rightcell7.Controls.Add(txtOtherOfficeExpenses);
            row7.Cells.Add(leftcell7);
            row7.Cells.Add(rightcell7);
            tblLayout.Rows.Add(row7);


            lblOtherIT = new Label();
            lblOtherIT.Text = "Other IT";
            txtOtherIT = new TextBox();
            this.AllFields.AddLast(txtOtherIT);
            TableRow row8 = new TableRow();
            TableCell leftcell8 = new TableCell();
            TableCell rightcell8 = new TableCell();
            leftcell8.Controls.Add(lblOtherIT);
            rightcell8.Controls.Add(txtOtherIT);
            row8.Cells.Add(leftcell8);
            row8.Cells.Add(rightcell8);
            tblLayout.Rows.Add(row8);


            lblOtherITCore = new Label();
            lblOtherITCore.Text = "Other IT Core";
            txtOtherITCore = new TextBox();
            this.AllFields.AddLast(txtOtherITCore);
            TableRow row9 = new TableRow();
            TableCell leftcell9 = new TableCell();
            TableCell rightcell9 = new TableCell();
            leftcell9.Controls.Add(lblOtherITCore);
            rightcell9.Controls.Add(txtOtherITCore);
            row9.Cells.Add(leftcell9);
            row9.Cells.Add(rightcell9);
            tblLayout.Rows.Add(row9);


            lblOtherTotal = new Label();
            lblOtherTotal.Text = "Other Total";
            txtOtherTotal = new TextBox();
            this.AllFields.AddLast(txtOtherTotal);
            TableRow row10 = new TableRow();
            TableCell leftcell10 = new TableCell();
            TableCell rightcell10 = new TableCell();
            leftcell10.Controls.Add(lblOtherTotal);
            rightcell10.Controls.Add(txtOtherTotal);
            row10.Cells.Add(leftcell10);
            row10.Cells.Add(rightcell10);
            tblLayout.Rows.Add(row10);


            lblRevision = new Label();
            lblRevision.Text = "Revision";
            txtRevision = new TextBox();
            this.AllFields.AddLast(txtRevision);
            TableRow row11 = new TableRow();
            TableCell leftcell11 = new TableCell();
            TableCell rightcell11 = new TableCell();
            leftcell11.Controls.Add(lblRevision);
            rightcell11.Controls.Add(txtRevision);
            row11.Cells.Add(leftcell11);
            row11.Cells.Add(rightcell11);
            tblLayout.Rows.Add(row11);


            lblRevisionEffectiveDate = new Label();
            lblRevisionEffectiveDate.Text = "Revision Effective Date";
            txtRevisionEffectiveDate = new TextBox();
            this.AllFields.AddLast(txtRevisionEffectiveDate);
            TableRow row12 = new TableRow();
            TableCell leftcell12 = new TableCell();
            TableCell rightcell12 = new TableCell();
            leftcell12.Controls.Add(lblRevisionEffectiveDate);
            rightcell12.Controls.Add(txtRevisionEffectiveDate);
            row12.Cells.Add(leftcell12);
            row12.Cells.Add(rightcell12);
            tblLayout.Rows.Add(row12);


            lblComments = new Label();
            lblComments.Text = "Comments";
            txtComments = new TextBox();
            this.AllFields.AddLast(txtComments);
            TableRow row13 = new TableRow();
            TableCell leftcell13 = new TableCell();
            TableCell rightcell13 = new TableCell();
            leftcell13.Controls.Add(lblComments);
            rightcell13.Controls.Add(txtComments);
            row13.Cells.Add(leftcell13);
            row13.Cells.Add(rightcell13);
            tblLayout.Rows.Add(row13);


            lblSortOrder = new Label();
            lblSortOrder.Text = "SortOrder";
            txtSortOrder = new TextBox();
            this.AllFields.AddLast(txtSortOrder);
            TableRow row14 = new TableRow();
            TableCell leftcell14 = new TableCell();
            TableCell rightcell14 = new TableCell();
            leftcell14.Controls.Add(lblSortOrder);
            rightcell14.Controls.Add(txtSortOrder);
            row14.Cells.Add(leftcell14);
            row14.Cells.Add(rightcell14);
            tblLayout.Rows.Add(row14);

            lblMandatoryFields = new Label();
            lblMandatoryFields.Text = "Fields with (*) are mandatory </br></br>";
            TableRow row15 = new TableRow();
            TableCell leftcell15 = new TableCell();
            TableCell rightcell15 = new TableCell();
            rightcell15.Controls.Add(lblMandatoryFields);
            row15.Cells.Add(leftcell15);
            row15.Cells.Add(rightcell15);
            tblLayout.Rows.Add(row15);

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

                object[] parameters = new object[15];
                parameters[0] = txtLocationID.Text;
                parameters[1] = txtLocationName.Text;

                if (txtCompanyID.Text.Equals(""))
                {
                    parameters[2] = null;
                }
                else
                {
                    parameters[2] = txtCompanyID.Text;
                }

                if (txtCurrencyID.Text.Equals(""))
                {
                    parameters[3] = null;
                }
                else
                {
                    parameters[3] = txtCurrencyID.Text;
                }

                parameters[4] = txtStandardWorkingHours.Text;
                parameters[5] = txtMultiplier.Text;

                if (txtOtherOfficeSpace.Text.Equals(""))
                {
                    parameters[6] = null;
                }  
                else
                {
                    parameters[6] = txtOtherOfficeSpace.Text;
                }

                if (txtOtherOfficeExpenses.Text.Equals(""))
                {
                    parameters[7] = null;
                }
                else
                {
                    parameters[7] = txtOtherOfficeExpenses.Text;
                }

                if (txtOtherIT.Text.Equals(""))
                {
                    parameters[8] = null;
                }
                else
                {
                    parameters[8] = txtOtherIT.Text;
                }

                if (txtOtherITCore.Text.Equals(""))
                {
                    parameters[9] = null;
                }
                else
                {
                    parameters[9] = txtOtherITCore.Text;
                }

                if (txtOtherTotal.Text.Equals(""))
                {
                    parameters[10] = null;
                }
                else
                {
                    parameters[10] = txtOtherTotal.Text;
                }

                parameters[11] = txtRevision.Text;
                parameters[12] = txtRevisionEffectiveDate.Text;
                parameters[13] = txtComments.Text;
                parameters[14] = txtSortOrder.Text;


                try
                {
                    BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.LocationInserter", parameters);

                    lblError.Text = "<br><br>Location <b>" + txtLocationName.Text + "</b> successfully added!";
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
