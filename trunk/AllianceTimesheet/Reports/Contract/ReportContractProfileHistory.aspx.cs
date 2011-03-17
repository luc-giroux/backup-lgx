using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class ReportContractProfileHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ReportViewer1.Visible = false;
        }
    }


    protected void ButtonViewReport_Click(object sender, EventArgs e)
    {
        if (InputFieldsAreOK())
        {
            ReportViewer1.Visible = true;
            DataSetReports.ContractProfileHistoryDataTable cphdt = new DataSetReports.ContractProfileHistoryDataTable();
            DataSetReportsTableAdapters.ContractProfileHistoryTableAdapter ta = new DataSetReportsTableAdapters.ContractProfileHistoryTableAdapter();
            ta.Fill(cphdt, (String)HttpContext.Current.Session["currentContract"], Convert.ToDateTime(TextBoxStartDate.Text), Convert.ToDateTime(TextBoxEndDate.Text));

            System.Data.DataTable dt = cphdt;
            ReportDataSource datasource = new ReportDataSource("", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt));
            ReportViewer1.LocalReport.Refresh();
        }


    }

    /// <summary>
    /// Checks if values entered by the user are correct
    /// </summary>
    /// <returns>true if values are correct, False otherwise</returns>
    protected Boolean InputFieldsAreOK()
    {
        if (this.TextBoxStartDate.Text == "")
        {
            Utils.NotifyUser("warning", "Please select a start date", Page);
            return false;
        }

        if (this.TextBoxEndDate.Text == "")
        {
            Utils.NotifyUser("warning", "Please select an end date", Page);
            return false;
        }

        return true;
    }
}