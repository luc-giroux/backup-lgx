using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class AddCWP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


    }


    protected void ButtonAddCWP_OnClick(object sender, EventArgs e)
    {
        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();

        if (this.InputFieldsAreOK())
        {
            try
            {
                
                thisConnection.Open();
                
                //Ad the CWP in database
                cmd.CommandText = "INSERT INTO [allianceTimesheets].[dbo].[CWP] ([CWPNumber],[Description],[ContractNumber], " +
                                          " [Discipline], [completed]) " +
                                          " VALUES ('" + TextBoxCWPNumber.Text + "', '" + TextBoxDescription.Text + "', '" +
                                          HttpContext.Current.Session["currentContract"] + "', '" + ComboBoxDiscipline.SelectedValue + "',0)";
                cmd.ExecuteNonQuery();

                //Reset of the fields
                TextBoxCWPNumber.Text = "";
                TextBoxDescription.Text = "";
                ComboBoxDiscipline.SelectedValue = "select";
                //User success notification
                Utils.NotifyUser("success", "CWP successfully added!", Page);
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
        if (this.TextBoxCWPNumber.Text == "")
        {
            Utils.NotifyUser("warning", "Please enter a CWP Number", Page);
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