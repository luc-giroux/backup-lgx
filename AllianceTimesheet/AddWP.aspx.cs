using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;


public partial class AddWP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {      

    }


    protected void ButtonAddWP_OnClick(object sender, EventArgs e)
    {
        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();

        if (this.InputFieldsAreOK())
        {
            try
            {
                thisConnection.Open();
                //Add the WP in database
                cmd.CommandText = "INSERT INTO [allianceTimesheets].[dbo].[WP] ([WPNumber],[Description],[ContractNumber],[Completed]) " +
                                          " VALUES ('" + TextBoxWPNumber.Text + "', '" + TextBoxDescription.Text + "', '" +
                                          HttpContext.Current.Session["currentContract"] + "', 0)";
                cmd.ExecuteNonQuery();

                //Reset of the fields
                TextBoxWPNumber.Text = "";
                TextBoxDescription.Text = "";
                //User success notification
                Utils.NotifyUser("success", "WP successfully added!", Page);
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
        if (this.TextBoxWPNumber.Text == "")
        {
            Utils.NotifyUser("warning", "Please enter a WP Number", Page);
            return false;
        }

        if (this.TextBoxDescription.Text == "")
        {
            Utils.NotifyUser("warning", "Please enter a Description", Page);
            return false;
        }

        return true;
    }
}