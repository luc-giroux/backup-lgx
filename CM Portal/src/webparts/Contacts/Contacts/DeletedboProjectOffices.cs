using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace Contacts
{
    public class DeletedboProjectOffices : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Label lblConfirm;
        private Label lblProjectOfficeID;
        private Label lblProjectOfficeName;
        private Label lblError;

        private Button btnDelete;

        private string lobSystemInstance = "Contacts";
        private string entityName = "dbo.ProjectOffices";

        private string ProjectOfficeID
        {
            get
            {
                return Page.Request.QueryString["ProjectOfficeID"].ToString();
            }
        }


        protected override void CreateChildControls()
        {

            lblConfirm = new Label();
            lblProjectOfficeID = new Label();
            lblProjectOfficeName = new Label();
            lblError = new Label();

            loaddboProjectOffices();

            lblConfirm.Text = "</br></br>Do you really want to delete project office <b>" + lblProjectOfficeName.Text + "</b> ? </br></br>";

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
            parameters[0] = lblProjectOfficeID.Text;

            try
            {
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.ProjectOfficesDeleter", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }
            lblConfirm.Text = "</br></br>Project office <b>" + lblProjectOfficeName.Text +"</b> successfully deleted!";
            lblConfirm.BackColor = System.Drawing.Color.LightBlue;
            this.btnDelete.Visible = false;
        }

        //Gets the entry in the database
        private void loaddboProjectOffices()
        {
            // add the identifiers...
            object[] identifiers = new object[1];
            identifiers[0] = Convert.ToInt32(ProjectOfficeID);

            IEntityInstance ie = null;
            try
            {
                ie = BdcHelpers.GetSpecificRecord(lobSystemInstance, entityName, identifiers);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblProjectOfficeID.Text = ie["ProjectOfficeID"].ToString();
            if (ie["ProjectOfficeName"] != null)
                lblProjectOfficeName.Text = ie["ProjectOfficeName"].ToString();

        }
    }
}
