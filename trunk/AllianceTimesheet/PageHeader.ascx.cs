using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class PageHeader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            String currentContract = (String)HttpContext.Current.Session["currentContract"];

            if (currentContract == null)
            {
                int nbContracts = Utils.getUserNumberOfContracts();
                if (nbContracts == 0)
                {
                    // We do nothing.
                    // TODO display a specific message?
                }

                if (nbContracts > 1)
                {
                    // The user must choose the contacts he wants to work on.
                    HttpContext.Current.Response.Redirect("~/ContractSelection.aspx");
                }
            }
            else
            {
                // TODO : a modifier car appel vers a chaque page chargée...
                if (HttpContext.Current.Session["userIsOwnerCA"] == null &&
                    HttpContext.Current.Session["userIsContractorCA"] == null &&
                    HttpContext.Current.Session["userIsContractorSupervisor"] == null &&
                    HttpContext.Current.Session["userIsOwnerSupervisor"] == null)
                {
                    Utils.getUserRolesByContract(currentContract);
                }

            }


            this.lblUser.Text = System.Web.HttpContext.Current.User.Identity.Name;
            this.LblContract.Text = (String)HttpContext.Current.Session["currentContract"];


            if (HttpContext.Current.Session["userHasManyContracts"] != null &&
                (Boolean)HttpContext.Current.Session["userHasManyContracts"])
            {
                this.HyperLinkChangeContract.Visible = true;
                this.HyperLinkChangeContract.NavigateUrl = "~/ContractSelection.aspx";
            }

            this.displayElementAccordingToRights();
        }

    }


    /// <summary>
    /// Because of rendering problem for the asp:menu with chrome
    /// </summary>
    /// <param name="control"></param>
    /// <param name="index"></param>
    protected override void AddedControl(Control control, int index)
    {
        if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
        {
            this.Page.ClientTarget = "uplevel";
        }
        base.AddedControl(control, index);
    }


    /// <summary>
    /// Display elements on page (links) accordingly to the rights of the connected user.
    /// </summary>
    protected void displayElementAccordingToRights()
    {

        if (Parent.Page.User.IsInRole(ConfigurationManager.AppSettings["ADMIN_GROUP"]))
        {
            MenuApplicationAdmin.Visible = true;
            LabelRole.Text = "[ADMIN]"; 
        }
        else
        {
            MenuApplicationAdmin.Visible = false;
        }

        if (HttpContext.Current.Session["userIsOwnerSupervisor"] != null
            && (Boolean)HttpContext.Current.Session["userIsOwnerSupervisor"])
        {
            MenuOwnerSupervisor.Visible = true;
            LabelRole.Text = "[Owner Supervisor] " + LabelRole.Text; 
        }
        else
        {
            MenuOwnerSupervisor.Visible = false;
        }


        if (HttpContext.Current.Session["userIsContractorSupervisor"] != null
            && (Boolean)HttpContext.Current.Session["userIsContractorSupervisor"])
        {
            MenuContractorSupervisor.Visible = true;
            LabelRole.Text = "[Contractor Supervisor] " + LabelRole.Text; 
        }
        else
        {
            MenuContractorSupervisor.Visible = false;
        }

        if (HttpContext.Current.Session["userIsOwnerCA"] != null
            && (Boolean)HttpContext.Current.Session["userIsOwnerCA"])
        {
            MenuOwnerCA.Visible = true;
            LabelRole.Text = "[Owner Contract Admin] " + LabelRole.Text; 
        }

        if (HttpContext.Current.Session["userIsContractorCA"] != null
            && (Boolean)HttpContext.Current.Session["userIsContractorCA"])
        {
            MenuContractorCA.Visible = true;
            LabelRole.Text = "[Contractor Contract Admin] " + LabelRole.Text; 
        }
    }
}