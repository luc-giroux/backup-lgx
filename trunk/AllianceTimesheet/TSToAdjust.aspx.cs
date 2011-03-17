using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TSToAdjust : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LabelCountTS.Text = GridView1.Rows.Count.ToString();

        if (!Page.IsPostBack)
        {
            // If the user has adjusted a timesheet we display him the nofitication of his operation
            if (Request.Params["status"] != null)
            {
                if (Request.Params["status"] == "AdjustmentFinishedOK")
                {
                    Utils.NotifyUser("success", "Timesheet adjustment successfull!", Page);
                }

            }
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        String TSNumber = GridView1.SelectedRow.Cells[1].Text;
        HttpContext.Current.Response.Redirect("./ViewTS.aspx?TSNumber=" + TSNumber + "&adjustment=1");
    }
}