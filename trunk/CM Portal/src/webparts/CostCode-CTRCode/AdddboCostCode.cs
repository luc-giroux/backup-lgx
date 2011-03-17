using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace CostCodeCTRCode
{
    public class AdddboCostCode : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;
        private Label lblCostCode;
        private TextBox txtCostCode;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblMandatoryFields;

        private Label lblContract;
        private DropDownList ddlContract;

        private Button btnNew;

        private Label lblError;

        private string lobSystemInstance = "CostCode-CTRCode";
        private string entityName = "dbo.CostCode";

        protected override void CreateChildControls()
        {
            tblLayout = new Table();

            lblCostCode = new Label();
            lblCostCode.Text = "Cost code (*)";
            txtCostCode = new TextBox();
            TableRow row0 = new TableRow();
            TableCell leftcell0 = new TableCell();
            TableCell rightcell0 = new TableCell();
            leftcell0.Controls.Add(lblCostCode);
            rightcell0.Controls.Add(txtCostCode);
            row0.Cells.Add(leftcell0);
            row0.Cells.Add(rightcell0);
            tblLayout.Rows.Add(row0);

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
            lblDescription.Text = "Description (*)";
            txtDescription = new TextBox();
            txtDescription.TextMode = TextBoxMode.MultiLine;
            TableRow row1 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblDescription);
            rightcell1.Controls.Add(txtDescription);
            row1.Cells.Add(leftcell1);
            row1.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row1);

            lblMandatoryFields = new Label();
            lblMandatoryFields.Text = "Fields with (*) are mandatory </br></br>";
            TableRow row2 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            rightcell2.Controls.Add(lblMandatoryFields);
            row2.Cells.Add(leftcell2);
            row2.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row2);


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
            if (txtCostCode.Text != "" && txtDescription.Text != "" && ddlContract.SelectedValue != "")
            {
                object[] parameters = new object[3];
                parameters[0] = txtCostCode.Text;
                parameters[1] = ddlContract.SelectedValue;
                parameters[2] = txtDescription.Text;

                try
                {
                    BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.CostCodeInserter", parameters);

                    lblError.Text = "<br><br>Cost Code <b>" + txtCostCode.Text + " " + txtDescription.Text + " </b> successfully added!";
                    lblError.BackColor = System.Drawing.Color.LightBlue;

                    //Remise a blanc des champs
                    txtCostCode.Text = "";
                    ddlContract.SelectedValue = "";
                    txtDescription.Text = "";
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
