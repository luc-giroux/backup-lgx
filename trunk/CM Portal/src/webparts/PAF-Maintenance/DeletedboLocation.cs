using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace PAFMaintenance
{
    public class DeletedboLocation : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Label lblConfirm;
        private Label lblLocationID;
        private Label lblLocationName;

        private Label lblError;

        private Button btnDelete;

        private string lobSystemInstance = "PAF_Maintenance";
        private string entityName = "dbo.Location";

        private string LocationID
        {
            get
            {
                return Page.Request.QueryString["LocationID"].ToString();
            }
        }


        protected override void CreateChildControls()
        {

            lblConfirm = new Label();
            lblLocationID = new Label();
            lblLocationName = new Label();

            lblError = new Label();

            loaddboLocation();

            lblConfirm.Text = "</br></br>Do you really want to delete Location <b>" + lblLocationName.Text + "</b> ? </br></br>";

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
            parameters[0] = lblLocationID.Text;

            try
            {
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.LocationDeleter", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }
            lblConfirm.Text = "</br></br>Location <b>" + lblLocationName.Text + "</b> successfully deleted!";
            lblConfirm.BackColor = System.Drawing.Color.LightBlue;
            this.btnDelete.Visible = false;
        }

        //Gets the entry in the database
        private void loaddboLocation()
        {
            // add the identifiers...
            object[] identifiers = new object[1];
            identifiers[0] = LocationID;

            IEntityInstance ie = null;
            try
            {
                ie = BdcHelpers.GetSpecificRecord(lobSystemInstance, entityName, identifiers);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblLocationID.Text = ie["LocationID"].ToString();
            if (ie["LocationName"] != null)
                lblLocationName.Text = ie["LocationName"].ToString();

        }
    }
}
