using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CWP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.GridView1.Rows.Count == 0)
        {
            this.LabelNoRecordFound.Visible = true;
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
}