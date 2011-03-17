using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class TSRejected : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count == 0)
        {
            this.LabelNoRecordFound.Visible = true;
        }

        if (!Page.IsPostBack)
        {
            this.CountTS();
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        String TSNumber = GridView1.SelectedRow.Cells[1].Text;
        HttpContext.Current.Response.Redirect("./ViewTS.aspx?TSNumber=" + TSNumber + "&rejected=1");
    }

    protected void CountTS()
    {
        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();

        try
        {
            string contractNo = (String)HttpContext.Current.Session["currentContract"];

            // Get the contract title from database
            thisConnection.Open();
            cmd.CommandText = "SELECT COUNT(TimesheetNumber) FROM [allianceTimesheets].[dbo].[TimesheetHeader] " +
                              "WHERE ContractNumber = '" + contractNo + "' " +
                              "AND Rejected=1";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                this.LabelCountTS.Text = " (" + reader[0].ToString() + ")";
            }

            reader.Close();
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