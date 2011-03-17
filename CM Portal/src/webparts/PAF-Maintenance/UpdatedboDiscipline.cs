using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace PAFMaintenance
{
    public class UpdatedboDiscipline : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;
        private Label lblDisciplineID;
        private Label lblDisplayDisciplineID;
        private Label lblDisciplineNo;
        private TextBox txtDisciplineNo;
        private Label lblDescription;
        private TextBox txtDescription;


        private Button btnUpdate;

        private Label lblError;

        private string lobSystemInstance = "PAF_Maintenance";
        private string entityName = "dbo.Discipline";

        private string DisciplineID
        { get { return Page.Request.QueryString["DisciplineID"].ToString(); } }


        protected override void CreateChildControls()
        {
            tblLayout = new Table();

            lblDisciplineID = new Label();
            lblDisciplineID.Text = "DisciplineID";
            lblDisplayDisciplineID = new Label();
            TableRow row10000 = new TableRow();
            TableCell leftcell0 = new TableCell();
            TableCell rightcell0 = new TableCell();
            leftcell0.Controls.Add(lblDisciplineID);
            rightcell0.Controls.Add(lblDisplayDisciplineID);
            row10000.Cells.Add(leftcell0);
            row10000.Cells.Add(rightcell0);
            tblLayout.Rows.Add(row10000);
            lblDisciplineNo = new Label();
            lblDisciplineNo.Text = "DisciplineNo";
            txtDisciplineNo = new TextBox();
            TableRow row0 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblDisciplineNo);
            rightcell1.Controls.Add(txtDisciplineNo);
            row0.Cells.Add(leftcell1);
            row0.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row0);
            lblDescription = new Label();
            lblDescription.Text = "Description";
            txtDescription = new TextBox();
            TableRow row1 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblDescription);
            rightcell2.Controls.Add(txtDescription);
            row1.Cells.Add(leftcell2);
            row1.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row1);


            btnUpdate = new Button();
            btnUpdate.Text = "Update";
            btnUpdate.Click += new EventHandler(btnUpdate_Click);

            lblError = new Label();

            this.Controls.Add(tblLayout);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(lblError);

            loaddboDiscipline();

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

            object[] parameters = new object[3];
            parameters[0] = lblDisplayDisciplineID.Text;
            parameters[1] = txtDisciplineNo.Text;
            parameters[2] = txtDescription.Text;


            try
            {
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.DisciplineUpdater", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblError.Text = "</br></br>Discipline <b>" + lblDisciplineID.Text + "</b> Successfully updated!";
            lblError.BackColor = System.Drawing.Color.LightBlue;
        }

        private void loaddboDiscipline()
        {
            // add the identifiers...
            object[] identifiers = new object[1];
            identifiers[0] = Convert.ToInt32(DisciplineID);


            IEntityInstance ie = null;
            try
            {
                ie = BdcHelpers.GetSpecificRecord(lobSystemInstance, entityName, identifiers);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblDisplayDisciplineID.Text = ie["DisciplineID"].ToString();
            if (ie["DisciplineNo"] != null)
                txtDisciplineNo.Text = ie["DisciplineNo"].ToString();
            if (ie["Description"] != null)
                txtDescription.Text = ie["Description"].ToString();

        }
    }
}
