using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace PAFMaintenance
{
    public class UpdatedboLocation : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;
        private Label lblLocationID;
        private Label lblDisplayLocationID;
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


        private Button btnUpdate;

        private Label lblError;

        private string lobSystemInstance = "PAF_Maintenance";
        private string entityName = "dbo.Location";

        private string LocationID
        { get { return Page.Request.QueryString["LocationID"].ToString(); } }


        protected override void CreateChildControls()
        {
            tblLayout = new Table();

            lblLocationID = new Label();
            lblLocationID.Text = "LocationID";
            lblDisplayLocationID = new Label();
            TableRow row10000 = new TableRow();
            TableCell leftcell0 = new TableCell();
            TableCell rightcell0 = new TableCell();
            leftcell0.Controls.Add(lblLocationID);
            rightcell0.Controls.Add(lblDisplayLocationID);
            row10000.Cells.Add(leftcell0);
            row10000.Cells.Add(rightcell0);
            tblLayout.Rows.Add(row10000);
            lblLocationName = new Label();
            lblLocationName.Text = "LocationName";
            txtLocationName = new TextBox();
            TableRow row0 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblLocationName);
            rightcell1.Controls.Add(txtLocationName);
            row0.Cells.Add(leftcell1);
            row0.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row0);
            lblCompanyID = new Label();
            lblCompanyID.Text = "CompanyID";
            txtCompanyID = new TextBox();
            TableRow row1 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblCompanyID);
            rightcell2.Controls.Add(txtCompanyID);
            row1.Cells.Add(leftcell2);
            row1.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row1);
            lblCurrencyID = new Label();
            lblCurrencyID.Text = "CurrencyID";
            txtCurrencyID = new TextBox();
            TableRow row2 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            leftcell3.Controls.Add(lblCurrencyID);
            rightcell3.Controls.Add(txtCurrencyID);
            row2.Cells.Add(leftcell3);
            row2.Cells.Add(rightcell3);
            tblLayout.Rows.Add(row2);
            lblStandardWorkingHours = new Label();
            lblStandardWorkingHours.Text = "StandardWorkingHours";
            txtStandardWorkingHours = new TextBox();
            TableRow row3 = new TableRow();
            TableCell leftcell4 = new TableCell();
            TableCell rightcell4 = new TableCell();
            leftcell4.Controls.Add(lblStandardWorkingHours);
            rightcell4.Controls.Add(txtStandardWorkingHours);
            row3.Cells.Add(leftcell4);
            row3.Cells.Add(rightcell4);
            tblLayout.Rows.Add(row3);
            lblMultiplier = new Label();
            lblMultiplier.Text = "Multiplier";
            txtMultiplier = new TextBox();
            TableRow row4 = new TableRow();
            TableCell leftcell5 = new TableCell();
            TableCell rightcell5 = new TableCell();
            leftcell5.Controls.Add(lblMultiplier);
            rightcell5.Controls.Add(txtMultiplier);
            row4.Cells.Add(leftcell5);
            row4.Cells.Add(rightcell5);
            tblLayout.Rows.Add(row4);
            lblOtherOfficeSpace = new Label();
            lblOtherOfficeSpace.Text = "OtherOfficeSpace";
            txtOtherOfficeSpace = new TextBox();
            TableRow row5 = new TableRow();
            TableCell leftcell6 = new TableCell();
            TableCell rightcell6 = new TableCell();
            leftcell6.Controls.Add(lblOtherOfficeSpace);
            rightcell6.Controls.Add(txtOtherOfficeSpace);
            row5.Cells.Add(leftcell6);
            row5.Cells.Add(rightcell6);
            tblLayout.Rows.Add(row5);
            lblOtherOfficeExpenses = new Label();
            lblOtherOfficeExpenses.Text = "OtherOfficeExpenses";
            txtOtherOfficeExpenses = new TextBox();
            TableRow row6 = new TableRow();
            TableCell leftcell7 = new TableCell();
            TableCell rightcell7 = new TableCell();
            leftcell7.Controls.Add(lblOtherOfficeExpenses);
            rightcell7.Controls.Add(txtOtherOfficeExpenses);
            row6.Cells.Add(leftcell7);
            row6.Cells.Add(rightcell7);
            tblLayout.Rows.Add(row6);
            lblOtherIT = new Label();
            lblOtherIT.Text = "OtherIT";
            txtOtherIT = new TextBox();
            TableRow row7 = new TableRow();
            TableCell leftcell8 = new TableCell();
            TableCell rightcell8 = new TableCell();
            leftcell8.Controls.Add(lblOtherIT);
            rightcell8.Controls.Add(txtOtherIT);
            row7.Cells.Add(leftcell8);
            row7.Cells.Add(rightcell8);
            tblLayout.Rows.Add(row7);
            lblOtherITCore = new Label();
            lblOtherITCore.Text = "OtherITCore";
            txtOtherITCore = new TextBox();
            TableRow row8 = new TableRow();
            TableCell leftcell9 = new TableCell();
            TableCell rightcell9 = new TableCell();
            leftcell9.Controls.Add(lblOtherITCore);
            rightcell9.Controls.Add(txtOtherITCore);
            row8.Cells.Add(leftcell9);
            row8.Cells.Add(rightcell9);
            tblLayout.Rows.Add(row8);
            lblOtherTotal = new Label();
            lblOtherTotal.Text = "OtherTotal";
            txtOtherTotal = new TextBox();
            TableRow row9 = new TableRow();
            TableCell leftcell10 = new TableCell();
            TableCell rightcell10 = new TableCell();
            leftcell10.Controls.Add(lblOtherTotal);
            rightcell10.Controls.Add(txtOtherTotal);
            row9.Cells.Add(leftcell10);
            row9.Cells.Add(rightcell10);
            tblLayout.Rows.Add(row9);
            lblRevision = new Label();
            lblRevision.Text = "Revision";
            txtRevision = new TextBox();
            TableRow row10 = new TableRow();
            TableCell leftcell11 = new TableCell();
            TableCell rightcell11 = new TableCell();
            leftcell11.Controls.Add(lblRevision);
            rightcell11.Controls.Add(txtRevision);
            row10.Cells.Add(leftcell11);
            row10.Cells.Add(rightcell11);
            tblLayout.Rows.Add(row10);
            lblRevisionEffectiveDate = new Label();
            lblRevisionEffectiveDate.Text = "RevisionEffectiveDate";
            txtRevisionEffectiveDate = new TextBox();
            TableRow row11 = new TableRow();
            TableCell leftcell12 = new TableCell();
            TableCell rightcell12 = new TableCell();
            leftcell12.Controls.Add(lblRevisionEffectiveDate);
            rightcell12.Controls.Add(txtRevisionEffectiveDate);
            row11.Cells.Add(leftcell12);
            row11.Cells.Add(rightcell12);
            tblLayout.Rows.Add(row11);
            lblComments = new Label();
            lblComments.Text = "Comments";
            txtComments = new TextBox();
            TableRow row12 = new TableRow();
            TableCell leftcell13 = new TableCell();
            TableCell rightcell13 = new TableCell();
            leftcell13.Controls.Add(lblComments);
            rightcell13.Controls.Add(txtComments);
            row12.Cells.Add(leftcell13);
            row12.Cells.Add(rightcell13);
            tblLayout.Rows.Add(row12);
            lblSortOrder = new Label();
            lblSortOrder.Text = "SortOrder";
            txtSortOrder = new TextBox();
            TableRow row13 = new TableRow();
            TableCell leftcell14 = new TableCell();
            TableCell rightcell14 = new TableCell();
            leftcell14.Controls.Add(lblSortOrder);
            rightcell14.Controls.Add(txtSortOrder);
            row13.Cells.Add(leftcell14);
            row13.Cells.Add(rightcell14);
            tblLayout.Rows.Add(row13);


            btnUpdate = new Button();
            btnUpdate.Text = "Update";
            btnUpdate.Click += new EventHandler(btnUpdate_Click);

            lblError = new Label();

            this.Controls.Add(tblLayout);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(lblError);

            loaddboLocation();

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

            object[] parameters = new object[15];
            parameters[0] = lblDisplayLocationID.Text;
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
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.LocationUpdater", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblError.Text = "</br></br>Location <b>" + lblLocationID.Text + "</b> Successfully updated!";
            lblError.BackColor = System.Drawing.Color.LightBlue;
        }

        private void loaddboLocation()
        {
            // add the identifiers...
            object[] identifiers = new object[1];
            identifiers[0] = Convert.ToString(LocationID);


            IEntityInstance ie = null;
            try
            {
                ie = BdcHelpers.GetSpecificRecord(lobSystemInstance, entityName, identifiers);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblDisplayLocationID.Text = ie["LocationID"].ToString();
            if (ie["LocationName"] != null)
                txtLocationName.Text = ie["LocationName"].ToString();
            if (ie["CompanyID"] != null)
                txtCompanyID.Text = ie["CompanyID"].ToString();
            if (ie["CurrencyID"] != null)
                txtCurrencyID.Text = ie["CurrencyID"].ToString();
            if (ie["StandardWorkingHours"] != null)
                txtStandardWorkingHours.Text = ie["StandardWorkingHours"].ToString();
            if (ie["Multiplier"] != null)
                txtMultiplier.Text = ie["Multiplier"].ToString();
            if (ie["OtherOfficeSpace"] != null)
                txtOtherOfficeSpace.Text = ie["OtherOfficeSpace"].ToString();
            if (ie["OtherOfficeExpenses"] != null)
                txtOtherOfficeExpenses.Text = ie["OtherOfficeExpenses"].ToString();
            if (ie["OtherIT"] != null)
                txtOtherIT.Text = ie["OtherIT"].ToString();
            if (ie["OtherITCore"] != null)
                txtOtherITCore.Text = ie["OtherITCore"].ToString();
            if (ie["OtherTotal"] != null)
                txtOtherTotal.Text = ie["OtherTotal"].ToString();
            if (ie["Revision"] != null)
                txtRevision.Text = ie["Revision"].ToString();
            if (ie["RevisionEffectiveDate"] != null)
                txtRevisionEffectiveDate.Text = ie["RevisionEffectiveDate"].ToString();
            if (ie["Comments"] != null)
                txtComments.Text = ie["Comments"].ToString();
            if (ie["SortOrder"] != null)
                txtSortOrder.Text = ie["SortOrder"].ToString();

        }
    }
}
