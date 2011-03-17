using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace CostCodeCTRCode
{
    public class UpdatedboCTRCode : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;

        private Label lblCostCode;
        private Label lblDisplayCostCode;

        private Label lblCTRCode;
        private Label lblDisplayCTRCode;

        private Label lblDescription;
        private TextBox txtDescription;

        private Label lblStartDate;
        private TextBox txtStartDate;

        private Label lblEndDate;
        private TextBox txtEndDate;

        private Label lblActive;
        private CheckBox cbActive;

        private Button btnUpdate;

        private Label lblError;

        private Label lblFormatDate;
        private Label lblFormatDate2;

        private string lobSystemInstance = "CostCode-CTRCode";
        private string entityName = "dbo.CTRCode";

        private string CostCode
        { get { return Page.Request.QueryString["CostCode"].ToString(); } }
        private string CTRCode
        { get { return Page.Request.QueryString["CTRCode"].ToString(); } }


        protected override void CreateChildControls()
        {
            tblLayout = new Table();

            lblCostCode = new Label();
            lblCostCode.Text = "CostCode";
            lblDisplayCostCode = new Label();
            TableRow row10000 = new TableRow();
            TableCell leftcell0 = new TableCell();
            TableCell rightcell0 = new TableCell();
            leftcell0.Controls.Add(lblCostCode);
            rightcell0.Controls.Add(lblDisplayCostCode);
            row10000.Cells.Add(leftcell0);
            row10000.Cells.Add(rightcell0);
            tblLayout.Rows.Add(row10000);

            lblCTRCode = new Label();
            lblCTRCode.Text = "CTRCode";
            lblDisplayCTRCode = new Label();
            TableRow row10001 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblCTRCode);
            rightcell1.Controls.Add(lblDisplayCTRCode);
            row10001.Cells.Add(leftcell1);
            row10001.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row10001);

            lblDescription = new Label();
            lblDescription.Text = "Description";
            txtDescription = new TextBox();
            txtDescription.TextMode = TextBoxMode.MultiLine;
            TableRow row0 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblDescription);
            rightcell2.Controls.Add(txtDescription);
            row0.Cells.Add(leftcell2);
            row0.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row0);

            lblStartDate = new Label();
            lblStartDate.Text = "StartDate";
            txtStartDate = new TextBox();
            TableRow row1 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            leftcell3.Controls.Add(lblStartDate);
            rightcell3.Controls.Add(txtStartDate);
            row1.Cells.Add(leftcell3);
            row1.Cells.Add(rightcell3);
            tblLayout.Rows.Add(row1);

            lblFormatDate = new Label();
            lblFormatDate.Text = "format : MM/DD/YYYY";
            TableRow row33 = new TableRow();
            TableCell leftcell33 = new TableCell();
            TableCell rightcell33 = new TableCell();
            rightcell33.Controls.Add(lblFormatDate);
            row33.Cells.Add(leftcell33);
            row33.Cells.Add(rightcell33);
            tblLayout.Rows.Add(row33);

            lblEndDate = new Label();
            lblEndDate.Text = "EndDate";
            txtEndDate = new TextBox();
            TableRow row2 = new TableRow();
            TableCell leftcell4 = new TableCell();
            TableCell rightcell4 = new TableCell();
            leftcell4.Controls.Add(lblEndDate);
            rightcell4.Controls.Add(txtEndDate);
            row2.Cells.Add(leftcell4);
            row2.Cells.Add(rightcell4);
            tblLayout.Rows.Add(row2);

            lblFormatDate2 = new Label();
            lblFormatDate2.Text = "format : MM/DD/YYYY";
            TableRow row44 = new TableRow();
            TableCell leftcell44 = new TableCell();
            TableCell rightcell44 = new TableCell();
            rightcell44.Controls.Add(lblFormatDate2);
            row44.Cells.Add(leftcell44);
            row44.Cells.Add(rightcell44);
            tblLayout.Rows.Add(row44);

            lblActive = new Label();
            lblActive.Text = "Active";
            cbActive = new CheckBox();
            TableRow rowActive = new TableRow();
            TableCell leftcellActive = new TableCell();
            TableCell rightcellActive = new TableCell();
            leftcellActive.Controls.Add(lblActive);
            rightcellActive.Controls.Add(cbActive);
            rowActive.Cells.Add(leftcellActive);
            rowActive.Cells.Add(rightcellActive);
            tblLayout.Rows.Add(rowActive);

            btnUpdate = new Button();
            btnUpdate.Text = "Update";
            btnUpdate.Click += new EventHandler(btnUpdate_Click);

            lblError = new Label();

            this.Controls.Add(tblLayout);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(lblError);

            loaddboCTRCode();

        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            this.tblLayout.RenderControl(writer);
            this.btnUpdate.RenderControl(writer);
            this.lblError.RenderControl(writer);

        }

        void btnUpdate_Click(object sender, EventArgs e)
        {
            
            try
            {
                lblError.Text = "";
                lblError.BackColor = System.Drawing.Color.White;

                object[] parameters = new object[6];
                parameters[0] = lblDisplayCostCode.Text;
                parameters[1] = lblDisplayCTRCode.Text;
                parameters[2] = txtDescription.Text;
                parameters[3] = txtStartDate.Text;
                if (txtEndDate.Text != "")
                {
                    parameters[4] = txtEndDate.Text;
                }
                else 
                {
                     parameters[4] = null;
                }
                if (cbActive.Checked)
                {
                    parameters[5] = "1";
                }
                else
                {
                    parameters[5] = "0";
                }
               

                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.CTRCodeUpdater", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblError.Text = "</br></br>CTR code <b>" + lblCTRCode.Text + " " + lblDescription.Text + "</b> Successfully updated!";
            lblError.BackColor = System.Drawing.Color.LightBlue;
        }

        private void loaddboCTRCode()
        {
            // add the identifiers...
            object[] identifiers = new object[2];
            identifiers[0] = Convert.ToString(CostCode);
            identifiers[1] = Convert.ToString(CTRCode);


            IEntityInstance ie = null;
            try
            {
                ie = BdcHelpers.GetSpecificRecord(lobSystemInstance, entityName, identifiers);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblDisplayCostCode.Text = ie["CostCode"].ToString();
            lblDisplayCTRCode.Text = ie["CTRCode"].ToString();
            if (ie["Description"] != null)
                txtDescription.Text = ie["Description"].ToString();
            if (ie["StartDate"] != null)
                txtStartDate.Text = ie["StartDate"].ToString();
            if (ie["EndDate"] != null)
                txtEndDate.Text = ie["EndDate"].ToString();
            if (ie["Active"] != null)
            {
                if (Convert.ToBoolean(ie["Active"].ToString()))
                {
                    cbActive.Checked = true;
                }
            }

        }
    }
}
