using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class AddSubcontractor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Insert the Subcontractor in the database
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonAddSubcontractor_Click(object sender, EventArgs e)
    {
        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();
        if (this.InputFieldsAreOK())
        {
            try
            {
                thisConnection.Open();
                cmd.CommandText = "INSERT INTO [allianceTimesheets].[dbo].[Subcontractor] ([SubcontractorName],[Active],[ContractNumber]) " +
                                          " VALUES ('" + TextBoxSubcontractorName.Text + "', '1', '" +
                                          HttpContext.Current.Session["currentContract"] + "')";
                cmd.ExecuteNonQuery();

                //Reset of the fields
                TextBoxSubcontractorName.Text = "";
                //User success notification
                Utils.NotifyUser("success", "WS successfully added!", Page);
            }
            catch (SqlException ex)
            {
                //User error notification
                Utils.NotifyUser("error", ex.Message.Replace("\r", "").Replace("\n", "").Replace("'", ""), Page);
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
        if (this.TextBoxSubcontractorName.Text == "")
        {
            Utils.NotifyUser("warning", "Please enter a Subcontractor name", Page);
            return false;
        }

        return true;
    }
}