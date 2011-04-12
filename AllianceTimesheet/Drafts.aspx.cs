using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Drafts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count == 0)
        {
            this.LabelNoRecordFound.Visible = true;
        }
        else
        {
            this.LabelCountTS.Text = " (" + GridView1.Rows.Count + ")";
        }

        //User just submitted the Draft as TS
        if (Request.Params["status"] != null)
        {
            if (Request.Params["status"] == "SubmitSuccess")
            {
                Utils.NotifyUser("success", "Draft timesheet " + Request.Params["TSNumber"] + " successfully submitted!", Page);
            }
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        String TSNumber = GridView1.SelectedRow.Cells[1].Text;
        HttpContext.Current.Response.Redirect("./ViewTS.aspx?TSNumber=" + TSNumber + "&draft=1");
    }

}