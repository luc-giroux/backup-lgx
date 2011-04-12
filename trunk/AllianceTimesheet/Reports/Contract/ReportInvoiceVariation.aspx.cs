using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Configuration;

public partial class Reports_Contract_ReportInvoiceVariation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ReportViewer1.Visible = false;
        }
        // To manage the display of "Report is loading . Please wait " panel
        ButtonViewReport.Attributes.Add("onclick", GetPostBackEventReference(ButtonViewReport).ToString() + ";WaitReportLoading();");
    }


    protected void ButtonViewReport_Click(object sender, EventArgs e)
    {
        if (InputFieldsAreOK())
        {
            SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
            SqlCommand cmd = thisConnection.CreateCommand();

            try
            {
                string contractNo = (String)HttpContext.Current.Session["currentContract"];
                string contractTitle = "Contract " + contractNo + " - ";

                // Get the contract title from database
                thisConnection.Open();
                cmd.CommandText = "SELECT Title FROM [allianceTimesheets].[dbo].[Contract] " +
                                  "WHERE Contract.ContractNumber = '" + contractNo + "' ";

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    contractTitle = contractTitle + reader["Title"].ToString();
                }

                reader.Close();

                ReportViewer1.Visible = true;
                DataSetReports.ReportInvoiceVariationDataTable variationDT = new DataSetReports.ReportInvoiceVariationDataTable();
                ReportInvoiceVariationTableAdapterExtended ta = new ReportInvoiceVariationTableAdapterExtended();
                ta.SelectCommandTimeout = 360;
                ta.Fill(variationDT, TextBoxStartDate.Text, TextBoxEndDate.Text, (String)HttpContext.Current.Session["currentContract"]);

                // We add several parameters in order to display them in the report
                System.Data.DataTable dt = variationDT;
                ReportDataSource datasource = new ReportDataSource("", dt);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt));
                ReportParameter rp0 = new ReportParameter("ReportContractTitle", contractTitle);
                ReportParameter rp1 = new ReportParameter("ReportTitle", "Extra Work INVOICE report from " + TextBoxStartDate.Text + " to " + TextBoxEndDate.Text);
                ReportParameter rp2 = new ReportParameter("ReportStartDate", TextBoxStartDate.Text);
                ReportParameter rp3 = new ReportParameter("ReportEndDate", TextBoxEndDate.Text);
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp0, rp1, rp2, rp3 });
                ReportViewer1.LocalReport.Refresh();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                thisConnection.Dispose();
                thisConnection.Close();
            }
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