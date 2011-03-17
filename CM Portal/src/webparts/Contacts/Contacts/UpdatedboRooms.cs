using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;

namespace Contacts
{
    public class UpdatedboRooms : System.Web.UI.WebControls.WebParts.WebPart
    {
        private Table tblLayout;

        private Label lblRoomID;
        private Label lblDisplayRoomID;

        private Label lblRoomName;
        private TextBox txtRoomName;

        private Label lblRoomPhoneNumber;
        private TextBox txtRoomPhoneNumber;

        private Label lblRoomLocationCode;
        private TextBox txtRoomLocationCode;


        private Button btnUpdate;

        private Label lblError;

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
            tblLayout = new Table();

            lblRoomID = new Label();
            lblRoomID.Text = "RoomID";
            lblDisplayRoomID = new Label();


            lblRoomName = new Label();
            lblRoomName.Text = "Name";
            txtRoomName = new TextBox();
            TableRow row0 = new TableRow();
            TableCell leftcell1 = new TableCell();
            TableCell rightcell1 = new TableCell();
            leftcell1.Controls.Add(lblRoomName);
            rightcell1.Controls.Add(txtRoomName);
            row0.Cells.Add(leftcell1);
            row0.Cells.Add(rightcell1);
            tblLayout.Rows.Add(row0);

            lblRoomPhoneNumber = new Label();
            lblRoomPhoneNumber.Text = "Phone number";
            txtRoomPhoneNumber = new TextBox();
            TableRow row1 = new TableRow();
            TableCell leftcell2 = new TableCell();
            TableCell rightcell2 = new TableCell();
            leftcell2.Controls.Add(lblRoomPhoneNumber);
            rightcell2.Controls.Add(txtRoomPhoneNumber);
            row1.Cells.Add(leftcell2);
            row1.Cells.Add(rightcell2);
            tblLayout.Rows.Add(row1);

            lblRoomLocationCode = new Label();
            lblRoomLocationCode.Text = "Location code";
            txtRoomLocationCode = new TextBox();
            TableRow row2 = new TableRow();
            TableCell leftcell3 = new TableCell();
            TableCell rightcell3 = new TableCell();
            leftcell3.Controls.Add(lblRoomLocationCode);
            rightcell3.Controls.Add(txtRoomLocationCode);
            row2.Cells.Add(leftcell3);
            row2.Cells.Add(rightcell3);
            tblLayout.Rows.Add(row2);


            btnUpdate = new Button();
            btnUpdate.Text = "Update";
            btnUpdate.Click += new EventHandler(btnUpdate_Click);

            lblError = new Label();

            this.Controls.Add(tblLayout);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(lblError);

            loaddboRooms();

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

            object[] parameters = new object[4];
            parameters[0] = lblDisplayRoomID.Text;
            parameters[1] = txtRoomName.Text;
            parameters[2] = txtRoomPhoneNumber.Text;
            parameters[3] = txtRoomLocationCode.Text;

            try
            {
                BdcHelpers.ExecuteGenericInvoker(lobSystemInstance, entityName, "dbo.RoomsUpdater", parameters);
            }
            catch (Exception exception)
            {
                lblError.Text = exception.ToString();
            }
            lblError.Text = "</br></br>Room <b>" + txtRoomName.Text + "</b> Successfully updated!";
            lblError.BackColor = System.Drawing.Color.LightBlue;
        }

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

            lblDisplayRoomID.Text = ie["RoomID"].ToString();
            if (ie["RoomName"] != null)
                txtRoomName.Text = ie["RoomName"].ToString();
            if (ie["RoomPhoneNumber"] != null)
                txtRoomPhoneNumber.Text = ie["RoomPhoneNumber"].ToString();
            if (ie["RoomLocationCode"] != null)
                txtRoomLocationCode.Text = ie["RoomLocationCode"].ToString();

        }
    }
}
