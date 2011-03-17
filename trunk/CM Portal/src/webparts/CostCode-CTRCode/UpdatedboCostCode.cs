using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace CostCodeCTRCode
{
    public class UpdatedboCostCode : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;

        private Label lblCostCode;
        private Label lblDisplayCostCode;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblMandatoryFields;
        private Label lblActive;
        private CheckBox cbActive;

        private Label lblContract;
        private DropDownList ddlContract;

        private Button btnUpdate;

        private Label lblError;

        private string lobSystemInstance = "CostCode-CTRCode";
        private string entityName = "dbo.CostCode";

        private string CostCode
        { get { return Page.Request.QueryString["CostCode"].ToString(); } }


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

            ddlContract = new DropDownList();
            lblContract = new Label();
            lblContract.Text = "Contract (*)";
            // on charge la dropdownlist des contrats
            // On ajoute une valeur blanche en début de liste
            ddlContract.Items.Insert(0, new ListItem("", ""));
            ddlContract.Items.Insert(1, new ListItem("CM", "CM"));
            ddlContract.Items.Insert(2, new ListItem("COMM", "COMM"));
            ddlContract.Items.Insert(3, new ListItem("EP", "EP"));
            ddlContract.Items.Insert(4, new ListItem("LIS", "LIS"));
            ddlContract.Items.Insert(5, new ListItem("MOD", "MOD"));
            ddlContract.Items.Insert(6, new ListItem("OR", "OR"));
            TableRow row01 = new TableRow();
            TableCell leftcell01 = new TableCell();
            TableCell rightcell01 = new TableCell();
            leftcell01.Controls.Add(lblContract);
            rightcell01.Controls.Add(ddlContract);
            row01.Cells.Add(leftcell01);
            row01.Cells.Add(rightcell01);
            tblLayout.Rows.Add(row01);

            lblDescription = new Label();
            lblDescription.Text = "Description";
            txtDescription = new TextBox();
            txtDescription.TextMode = TextBoxMode.MultiLine;
            TableRow row0 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblDescription);
            rightcell1.Controls.Add(txtDescription);
            row0.Cells.Add(leftcell1);
            row0.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row0);

            lblActive = new Label();
            lblActive.Text = "Active";
            cbActive = new CheckBox();
            TableRow row2 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblActive);
            rightcell2.Controls.Add(cbActive);
            row2.Cells.Add(leftcell2);
            row2.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row2);

            lblMandatoryFields = new Label();
            lblMandatoryFields.Text = "Fields with (*) are mandatory </br></br>";
            TableRow row3 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            rightcell3.Controls.Add(lblMandatoryFields);
            row3.Cells.Add(leftcell3);
            row3.Cells.Add(rightcell3);
            tblLayout.Rows.Add(row3);

            btnUpdate = new Button();
            btnUpdate.Text = "Update";
            btnUpdate.Click += new EventHandler(btnUpdate_Click);

            lblError = new Label();

            this.Controls.Add(tblLayout);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(lblError);

            loaddboCostCode();

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

            if (ddlContract.SelectedValue != "")
            {
                object[] parameters = new object[4];
                parameters[0] = lblDisplayCostCode.Text;
                parameters[1] = ddlContract.SelectedValue;
                parameters[2] = txtDescription.Text;
                if (cbActive.Checked)
                {
                    parameters[3] = "1";
                }
                else
                {
                    parameters[3] = "0";
                }


                try
                {
                    BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.CostCodeUpdater", parameters);
                }
                catch (Exception exception)
                {
                    lblError.Text = exception.ToString();
                }

                lblError.Text = "</br></br>Cost code <b>" + CostCode + " - " + txtDescription.Text + "</b> Successfully updated!";
                lblError.BackColor = System.Drawing.Color.LightBlue;
                lblMandatoryFields.BackColor = System.Drawing.Color.Transparent;
            }
            else
            {
                lblMandatoryFields.BackColor = System.Drawing.Color.Red;
            }
            
        }


        private void loaddboCostCode()
        {
            // add the identifiers...
            object[] identifiers = new object[1];
            identifiers[0] = CostCode;


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
            if (ie["Contract"] != null)
                ddlContract.SelectedValue = ie["Contract"].ToString();
            if (ie["Description"] != null)
                txtDescription.Text = ie["Description"].ToString();
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
