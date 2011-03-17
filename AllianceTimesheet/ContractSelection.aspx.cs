using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ContractSelection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["CurrentUser"] = User.Identity.Name;

        //Remove all the roles in session to force pageHeader.ascx to get the new role and to refresh the menu
        HttpContext.Current.Session.Remove("userIsOwnerCA");
        HttpContext.Current.Session.Remove("userIsContractorCA");
        HttpContext.Current.Session.Remove("userIsContractorSupervisor");
        HttpContext.Current.Session.Remove("userIsOwnerSupervisor");
    }

    /// <summary>
    /// Called when the user select a contract
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewContract_SelectedIndexChanged(object sender, EventArgs e)
    {
        // We put the contract number in session and redirect user to home page
        String currentContract = GridViewContract.SelectedRow.Cells[1].Text;
        HttpContext.Current.Session.Add("currentContract", currentContract);
        HttpContext.Current.Response.Redirect("./Home.aspx");
    }
}