using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using PAFMAintenance;

namespace PAFMaintenance
{
    public class AdddboDiscipline : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;
        private Label lblDisciplineID;
        private TextBox txtDisciplineID;
        private Label lblDisciplineNo;
        private TextBox txtDisciplineNo;
        private Label lblDescription;
        private TextBox txtDescription;


        private Button btnNew;

        private Label lblError;

        private Label lblMandatoryFields;

        // Contains all the mandatories fields of the form
        private LinkedList<TextBox> mandatoryFields;

        // Contains all the fields of the form.
        private LinkedList<TextBox> AllFields;

        private string lobSystemInstance = "PAF_Maintenance";
        private string entityName = "dbo.Discipline";

        protected override void CreateChildControls()
        {
            //Init of the lists
            this.mandatoryFields = new LinkedList<TextBox>();
            this.AllFields = new LinkedList<TextBox>();

            tblLayout = new Table();

            lblDisciplineID = new Label();
            lblDisciplineID.Text = "Discipline ID (*)";
            txtDisciplineID = new TextBox();
            this.mandatoryFields.AddLast(txtDisciplineID);
            this.AllFields.AddLast(txtDisciplineID);
            TableRow row0 = new TableRow();
            TableCell leftcell0 = new TableCell();
            TableCell rightcell0 = new TableCell();
            leftcell0.Controls.Add(lblDisciplineID);
            rightcell0.Controls.Add(txtDisciplineID);
            row0.Cells.Add(leftcell0);
            row0.Cells.Add(rightcell0);
            tblLayout.Rows.Add(row0);


            lblDisciplineNo = new Label();
            lblDisciplineNo.Text = "Discipline No (*)";
            txtDisciplineNo = new TextBox();
            this.mandatoryFields.AddLast(txtDisciplineNo);
            this.AllFields.AddLast(txtDisciplineNo);
            TableRow row1 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblDisciplineNo);
            rightcell1.Controls.Add(txtDisciplineNo);
            row1.Cells.Add(leftcell1);
            row1.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row1);


            lblDescription = new Label();
            lblDescription.Text = "Description";
            txtDescription = new TextBox();
            this.AllFields.AddLast(txtDescription);
            TableRow row2 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblDescription);
            rightcell2.Controls.Add(txtDescription);
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

                object[] parameters = new object[3];
                parameters[0] = txtDisciplineID.Text;
                parameters[1] = txtDisciplineNo.Text;
                parameters[2] = txtDescription.Text;


                try
                {
                    BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.DisciplineInserter", parameters);

                    lblError.Text = "<br><br>Discipline <b>" + txtDisciplineID.Text + "</b> successfully added!";
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
