﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class AddTrade : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Insert the tradein the database
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();
        if (this.InputFieldsAreOK())
        {
            try
            {
                thisConnection.Open();
                cmd.CommandText = "INSERT INTO [allianceTimesheets].[dbo].[Trade] ([Trade],[ContractNumber]) " +
                                          " VALUES ('" + TextBoxTrade.Text + "', '" + HttpContext.Current.Session["currentContract"] + "')";
                cmd.ExecuteNonQuery();

                //Reset of the fields
                TextBoxTrade.Text = "";
                //User success notification
                Utils.NotifyUser("success", "Trade successfully added!", Page);
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
        if (this.TextBoxTrade.Text == "")
        {
            Utils.NotifyUser("warning", "Please enter a trade", Page);
            return false;
        }

        return true;
    }
}