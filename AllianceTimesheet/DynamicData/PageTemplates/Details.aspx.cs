using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;

public partial class Details : System.Web.UI.Page {
    protected MetaTable table;

    protected void Page_Init(object sender, EventArgs e) {
        DynamicDataManager1.RegisterControl(DetailsView1);
    }

    protected void Page_Load(object sender, EventArgs e) {
        table = DetailsDataSource.GetTable();
        Title = table.DisplayName;
        DetailsDataSource.Include = table.ForeignKeyColumnsNames;
        ListHyperLink.NavigateUrl = table.ListActionPath;
    }

    protected void DetailsView1_ItemDeleted(object sender, DetailsViewDeletedEventArgs e) {
        if (e.Exception == null || e.ExceptionHandled) {
            Response.Redirect(table.ListActionPath);
        }
    }

    /// <summary>
    /// LGX : we hide links "edit" and "delete" for table COMPANY and CONTRACT
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DetailsView1_DataBound(object sender, EventArgs e)
    {
        if (table.Name == "Company" || table.Name == "Contract")
        {

            foreach (DetailsViewRow row in DetailsView1.Rows)
            {
                LinkButton lb = (LinkButton)row.FindControl("DeleteLinkButton");
                lb.Visible = false;
                HyperLink hl = (HyperLink)row.FindControl("EditHyperLink");
                hl.Visible = false;
            }
        }
    }
}
