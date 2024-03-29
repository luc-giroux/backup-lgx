﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// LGiroux 22/11/10.
/// </summary>
public partial class AddWS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Insert the WS in the database
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonAddWS_OnClick(object sender, EventArgs e)
    {

        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();
        if (this.InputFieldsAreOK())
        {
            try
            {
                thisConnection.Open();
                cmd.CommandText = "INSERT INTO [allianceTimesheets].[dbo].[WS] ([WSNumber],[Description],[ContractNumber], [Completed]) " +
                                          " VALUES ('" + TextBoxWSNumber.Text + "', '" + TextBoxDescription.Text + "', '" +
                                          HttpContext.Current.Session["currentContract"] + "', 0)";
                cmd.ExecuteNonQuery();

                //Reset of the fields
                TextBoxWSNumber.Text = "";
                TextBoxDescription.Text = "";
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
        Boolean retour = true;

        if (this.TextBoxWSNumber.Text == "")
        {
            retour = false;
            Utils.NotifyUser("warning", "Please enter a WS Number", Page);
        }

        if (this.TextBoxDescription.Text == "")
        {
            retour = false;
            Utils.NotifyUser("warning", "Please enter a Description", Page);
        }

        return retour;
 
    }
}