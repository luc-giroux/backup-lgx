using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;


public partial class Home : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

       // Utils.CheckBaseAccess();


        // TEST : affiche la liste des groupes de l'utilisateur 
        /* 
        ArrayList groups = new ArrayList();
        foreach (System.Security.Principal.IdentityReference group in
        System.Web.HttpContext.Current.Request.LogonUserIdentity.Groups)
        {
            groups.Add(group.Translate(typeof(System.Security.Principal.NTAccount)).ToString());
        }

        foreach (String s in groups)
        {
            Response.Write(s);
            Response.Write("<br/>");
        }
        */


        //If there's no Contract in session, we have to check the user rights
        //String currentContract = (String)HttpContext.Current.Session["currentContract"];
        //if (currentContract == null)
        //{
        //    int nbContracts = Utils.getUserNumberOfContracts();
        //    if (nbContracts == 0)
        //    {
        //        // We do nothing
        //    }
        //    if (nbContracts == 1)
        //    {
        //    }
        //    if (nbContracts > 1)
        //    {
        //        // The user must choose the contacts he wants to work on.
        //        HttpContext.Current.Response.Redirect("./ContractSelection.aspx");
        //    }
        //}
        //else
        //{
        //    Utils.getUserRolesByContract(currentContract);
        //}
    }



}