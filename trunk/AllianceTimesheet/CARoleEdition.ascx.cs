using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class CARoleEdition : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.User.IsInRole(ConfigurationManager.AppSettings["ADMIN_GROUP"]))
        {
            GridView1.Visible = true;
            if (GridView1.Rows.Count == 0)
            {
                this.LabelNoRecordFound.Visible = true;
            }
        }
    }
}