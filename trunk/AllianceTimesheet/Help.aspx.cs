using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Help : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // We display quick guides links per application role

        if (HttpContext.Current.Session["userIsContractorSupervisor"] != null )
        {
            PanelContractorSupervisor.Visible = true;
        }

        if (HttpContext.Current.Session["userIsOwnerSupervisor"] != null)
        {
            PanelOwnerSupervisor.Visible = true;
        }

        if (HttpContext.Current.Session["userIsOwnerCA"] != null)
        {
            PanelOwnerCA.Visible = true;
            PanelContractorCA.Visible = true;
        }

        if (HttpContext.Current.Session["userIsContractorCA"] != null)
        {
            PanelContractorCA.Visible = true;
        }

        
        
    }
}