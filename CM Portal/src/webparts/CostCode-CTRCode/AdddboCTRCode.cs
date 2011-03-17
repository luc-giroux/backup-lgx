using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;

namespace CostCodeCTRCode
{
    public class AdddboCTRCode : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;

        private Label lblCostCode;
        private DropDownList ddlCostCode;

        private Label lblCTRCode;
        private TextBox txtCTRCode;

        private Label lblDescription;
        private TextBox txtDescription;

        private Label lblStartDate;
        private TextBox txtStartDate;

        private Label lblEndDate;
        private TextBox txtEndDate;

        private Label lblMandatoryFields;


        private Button btnNew;

        private Label lblError;

        private Label lblFormatDate;
        private Label lblFormatDate2;

        private string lobSystemInstance = "CostCode-CTRCode";
        private string entityName = "dbo.CTRCode";

        protected override void CreateChildControls()
        {
            tblLayout = new Table();

            lblCostCode = new Label();
            lblCostCode.Text = "Cost code (*)";
            //txtCostCode = new TextBox();
            ddlCostCode = new DropDownList();
            // on charge la dropdownlist dse cost code
            this.LoadDdlCostCode();
            // On ajoute une valeur blanche en début de liste
            ddlCostCode.Items.Insert(0, new ListItem("", ""));

            TableRow row0 = new TableRow();
            TableCell leftcell0 = new TableCell();
            TableCell rightcell0 = new TableCell();
            leftcell0.Controls.Add(lblCostCode);
            rightcell0.Controls.Add(ddlCostCode);
            row0.Cells.Add(leftcell0);
            row0.Cells.Add(rightcell0);
            tblLayout.Rows.Add(row0);

            lblCTRCode = new Label();
            lblCTRCode.Text = "CTR code (*)";
            txtCTRCode = new TextBox();
            TableRow row1 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblCTRCode);
            rightcell1.Controls.Add(txtCTRCode);
            row1.Cells.Add(leftcell1);
            row1.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row1);

            lblDescription = new Label();
            lblDescription.Text = "Description (*)";
            txtDescription = new TextBox();
            txtDescription.TextMode = TextBoxMode.MultiLine;
            TableRow row2 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblDescription);
            rightcell2.Controls.Add(txtDescription);
            row2.Cells.Add(leftcell2);
            row2.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row2);

            lblStartDate = new Label();
            lblStartDate.Text = "Start date (*)";
            txtStartDate = new TextBox();
            TableRow row3 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            leftcell3.Controls.Add(lblStartDate);
            rightcell3.Controls.Add(txtStartDate);
            row3.Cells.Add(leftcell3);
            row3.Cells.Add(rightcell3);
            tblLayout.Rows.Add(row3);

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
            lblEndDate.Text = "End date";
            txtEndDate = new TextBox();
            TableRow row4 = new TableRow();
            TableCell leftcell4 = new TableCell();
            TableCell rightcell4 = new TableCell();
            leftcell4.Controls.Add(lblEndDate);
            rightcell4.Controls.Add(txtEndDate);
            row4.Cells.Add(leftcell4);
            row4.Cells.Add(rightcell4);
            tblLayout.Rows.Add(row4);

            lblFormatDate2 = new Label();
            lblFormatDate2.Text = "format : MM/DD/YYYY";
            TableRow row44 = new TableRow();
            TableCell leftcell44 = new TableCell();
            TableCell rightcell44 = new TableCell();
            rightcell44.Controls.Add(lblFormatDate2);
            row44.Cells.Add(leftcell44);
            row44.Cells.Add(rightcell44);
            tblLayout.Rows.Add(row44);

            lblMandatoryFields = new Label();
            lblMandatoryFields.Text = "Fields with (*) are mandatory </br></br>";
            TableRow row5 = new TableRow();
            TableCell leftcell5 = new TableCell();
            TableCell rightcell5 = new TableCell();
            rightcell5.Controls.Add(lblMandatoryFields);
            row5.Cells.Add(leftcell5);
            row5.Cells.Add(rightcell5);
            tblLayout.Rows.Add(row5);


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


        // Action appellée lors du clic sur le bouton "ADD".
        void btnNew_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError.BackColor = System.Drawing.Color.White;
            lblMandatoryFields.BackColor = System.Drawing.Color.White;

            //local variables
            DateTime dtStartDate = new DateTime();
            DateTime dtEndDate = new DateTime();

             //If required fields are not blank
            if (ddlCostCode.SelectedValue != "" && txtCTRCode.Text != "" && txtDescription.Text != "" && txtStartDate.Text != "")
            {

                try
                {
                    //liste des paramètres pour l'update
                    object[] parameters = new object[5];

                    String[] s1 = txtStartDate.Text.Split('/');
                    //new DateTime(Y,M,D)
                    dtStartDate = new DateTime(Convert.ToInt32(s1[2]),Convert.ToInt32(s1[0]),Convert.ToInt32(s1[1]));

                    //si une date de fin a été saisie
                    if (txtEndDate.Text != "")
                    {
                        String[] s2 = txtEndDate.Text.Split('/');
                        dtEndDate = new DateTime(Convert.ToInt32(s2[2]), Convert.ToInt32(s2[0]), Convert.ToInt32(s2[1]));
                        parameters[4] = dtEndDate;
                    }
                    else
                    {
                        parameters[4] = null;
                    }
                 
                   
                    parameters[0] = ddlCostCode.SelectedValue;
                    parameters[1] = txtCTRCode.Text;
                    parameters[2] = txtDescription.Text;
                    parameters[3] = dtStartDate;
                                       

                    BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.CTRCodeInserter", parameters);

                    lblError.Text = "<br><br>CTR Code <b>" + txtCTRCode.Text + " " + txtDescription.Text + " </b> successfully added!";
                    lblError.BackColor = System.Drawing.Color.LightBlue;

                    //Remise a blanc des champs
                    txtCTRCode.Text = "";
                    txtDescription.Text = "";
                    txtStartDate.Text = "";
                    txtEndDate.Text = "";
                    ddlCostCode.SelectedIndex = 0;
                }
                catch (Exception)
                {
                    lblError.BackColor = System.Drawing.Color.Red;
                    lblError.Text = "<br><br>Invalid date format!";
                }
            }
            else
            {
                lblMandatoryFields.BackColor = System.Drawing.Color.Red;
            }

        }


        // Remplit la dropdown liste des cost codes avec uniquement les CC actifs.
        private void LoadDdlCostCode()
        {
            //DataSet ds = BdcHelpers.GetAllRecords(lobSystemInstance, "dbo.CostCode");
            object[] param = new object[1];
            param[0] = "1";
            DataSet ds = BdcHelpers.GetAllRecordsWithParam(lobSystemInstance, "dbo.CostCode", "dbo.GetCostCodesByState", param);
            // link the ddl with the dataset:
            this.ddlCostCode.DataSource = ds;
            this.ddlCostCode.DataTextField = "CostCode";
            this.ddlCostCode.DataValueField = "CostCode";
            this.ddlCostCode.DataBind();
            ds.Dispose();
        }

    }
}
