using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace Contacts
{
    public class DeletedboRooms : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Label lblConfirm;
        private Label lblRoomID;
        private Label lblRoomName;
        private Label lblError;

        private Button btnDelete;

        private string lobSystemInstance = "Contacts";
        private string entityName = "dbo.Rooms";

        private string RoomID
        {
            get
            {
                return Page.Request.QueryString["RoomID"].ToString();
            }
        }


        protected override void CreateChildControls()
        {
            
            lblConfirm = new Label();
            lblRoomID = new Label();
            lblRoomName = new Label();
            lblError = new Label();

            loaddboRooms();

            lblConfirm.Text = "</br></br>Do you really want to delete room <b>" + lblRoomName.Text + "</b> ? </br></br>";

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
            parameters[0] = lblRoomID.Text;

            try
            {
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.RoomsDeleter", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }
            lblConfirm.Text = "</br></br>Room <b>" + lblRoomName.Text + "</b> Successfully deleted!";
            lblConfirm.BackColor = System.Drawing.Color.LightBlue;
            this.btnDelete.Visible = false;
        }

        //Gets the entry in the database
        private void loaddboRooms()
        {
            // add the identifiers...
            object[] identifiers = new object[1];
            identifiers[0] = Convert.ToInt32(RoomID);

            IEntityInstance ie = null;
            try
            {
                ie = BdcHelpers.GetSpecificRecord(lobSystemInstance, entityName, identifiers);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }

            lblRoomID.Text = ie["RoomID"].ToString();
            if (ie["RoomName"] != null)
                lblRoomName.Text = ie["RoomName"].ToString();

        }
    }
}
