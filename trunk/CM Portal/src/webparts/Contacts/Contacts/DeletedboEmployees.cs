using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace Contacts
{
    public class DeletedboEmployees : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Label lblConfirm;
        private Label lblEmployeeID;
        private Label lblEmployeeLastName;
        private Label lblEmployeeFirstName;
        private Label lblError;

        private Button btnDelete;

        private string lobSystemInstance = "Contacts";
        private string entityName = "dbo.Employees";

        private string EmployeeID
        {
            get
            {
                return Page.Request.QueryString["EmployeeID"].ToString();
            }
        }


        protected override void CreateChildControls()
        {
            
            lblConfirm = new Label();
            lblEmployeeID = new Label();
            lblEmployeeLastName = new Label();
            lblEmployeeFirstName = new Label();
            lblError = new Label();

            loaddboEmployees();

            lblConfirm.Text = "</br></br>Do you really want to delete employee <b>" + lblEmployeeFirstName.Text
                                + " " + lblEmployeeLastName.Text + "</b> ? </br></br>";

            btnDelete = new Button();
            btnDelete.Text = "Delete";
            btnDelete.Click += new EventHandler(btnDelete_Click);

            this.Controls.Add(lblConfirm);
            this.Controls.Add(btnDelete);
            this.Controls.Add(lblError);
        }


        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            this.lblConfirm.RenderControl(writer);
            this.btnDelete.RenderControl(writer);
            this.lblError.RenderControl(writer);

        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError.BackColor = System.Drawing.Color.White;

            object[] parameters = new object[1];
            parameters[0] = lblEmployeeID.Text;

            try
            {
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.EmployeesDeleter", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }
            lblConfirm.Text = "</br></br>Employee <b>" + lblEmployeeFirstName.Text + " " + lblEmployeeLastName.Text + "</b> successfully deleted!";
            lblConfirm.BackColor = System.Drawing.Color.LightBlue;
            this.btnDelete.Visible = false;
        }

        //Gets the entry in the database
        private void loaddboEmployees()
        {
            // add the identifiers...
            object[] identifiers = new object[1];
            identifiers[0] = Convert.ToInt32(EmployeeID);

            IEntityInstance ie = null;
            try
            {
                ie = BdcHelpers.GetSpecificRecord(lobSystemInstance, entityName, identifiers);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblEmployeeID.Text = ie["EmployeeID"].ToString();
            if (ie["EmployeeLastname"] != null)
                lblEmployeeLastName.Text = ie["EmployeeLastname"].ToString();
            if (ie["EmployeeFirstname"] != null)
                lblEmployeeFirstName.Text = ie["EmployeeFirstname"].ToString();

        }
    }
}
