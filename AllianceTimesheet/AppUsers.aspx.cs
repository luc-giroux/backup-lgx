using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class AppUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["role"] != null)
        {

            SqlDataSourceAppUser.SelectCommand = " SELECT * FROM [AppUser] Inner Join [UserRights] ON " +
                                                 " [AppUser].userLogin = [UserRights].userLogin " + 
                                                 " WHERE contract = '" + (String) HttpContext.Current.Session["currentContract"] + "'" + 
                                                 " AND role = '" + Request.Params["role"] + "'" + 
                                                 " ORDER BY [LastName], [FirstName]";

            if (Request.Params["role"] == "OwnerSupervisor")
            {
                this.LabelOwnerSupervisors.Visible = true;
                this.LabelOwnerSupervisors.Text = "Owner Supervisors for contract " + (String)HttpContext.Current.Session["currentContract"] + ":<br /><br />";
            }
            // It's the contractors supervisors list
            else
            {
                this.LabelContractorsSupervisors.Visible = true;
                this.LabelContractorsSupervisors.Text = "Contractor Supervisors for contract " + (String)HttpContext.Current.Session["currentContract"] + ":<br /><br />";
            }
        }

        if (GridView1.Rows.Count == 0)
        {
            this.LabelNoRecordFound.Visible = true;
        }

        hideContractorCAItems();

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        String WPNumber = GridView1.SelectedRow.Cells[1].Text;
        HttpContext.Current.Session.Add("currentUserLogin", WPNumber);
        HttpContext.Current.Response.Redirect("./UserRights.aspx");
    }


    /// <summary>
    /// Contractor CA have a read only access. This method hide all elements that enable to make modifications.
    /// </summary>
    protected void hideContractorCAItems()
    {

        // If the user is not OWNER CA
        if (HttpContext.Current.Session["userIsOwnerCA"] == null
            || !(Boolean)HttpContext.Current.Session["userIsOwnerCA"])
        {
            // ANd if he's not APPLICATION ADMIN
            if (!Page.User.IsInRole(ConfigurationManager.AppSettings["ADMIN_GROUP"]))
            {
                GridView1.Columns[0].Visible = false;
            }
            
        }
        else
        {
                
        }
    }
}