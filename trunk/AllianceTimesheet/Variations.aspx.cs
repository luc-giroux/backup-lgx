using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Variations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count == 0)
        {
            LabelNoRecordFound.Visible = true;
        }
        hideContractorCAItems();
    }


    /// <summary>
    /// Contractor CA have a read only access. This method hide all elements that enable to make modifications.
    /// </summary>
    protected void hideContractorCAItems()
    {

        if (HttpContext.Current.Session["userIsOwnerCA"] == null
            || !(Boolean)HttpContext.Current.Session["userIsOwnerCA"])
        {
            LabelAdd.Visible = false;
            GridView1.Columns[0].Visible = false;
        }
        else
        {
            LabelAdd.Visible = true;
        }
    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        // We put the selected variation in session and redirect user to EditVariation page
        Variation v = new Variation();
        v.Number = GridView1.SelectedRow.Cells[1].Text;
        v.Discipline = Server.HtmlDecode(GridView1.SelectedRow.Cells[7].Text);
        v.Description = Server.HtmlDecode(GridView1.SelectedRow.Cells[8].Text);
        v.Completed = ((CheckBox)GridView1.SelectedRow.Cells[9].Controls[0]).Checked;

        HttpContext.Current.Session.Add("currentVariation", v);
        HttpContext.Current.Response.Redirect("./EditVariation.aspx");

    }
}