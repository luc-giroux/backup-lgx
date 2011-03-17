using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;


public partial class ContractStructure : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ReportViewer1.Visible = true;
            DataSetReports.ContractStructureDataTable csdt = new DataSetReports.ContractStructureDataTable();
            DataSetReportsTableAdapters.ContractStructureTableAdapter ta = new DataSetReportsTableAdapters.ContractStructureTableAdapter();
            ta.Fill(csdt, (String)HttpContext.Current.Session["currentContract"]);

            System.Data.DataTable dt = csdt;
            ReportDataSource datasource = new ReportDataSource("", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt));
            ReportViewer1.LocalReport.Refresh();
        }
        
    }
}