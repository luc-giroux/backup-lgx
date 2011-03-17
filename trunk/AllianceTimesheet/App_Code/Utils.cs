using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

/// <summary>
/// Utility class for common use.
/// </summary>
public class Utils
{

	public Utils()
	{
	}

    /// <summary>
    /// To access the application, the user must be member of one of the tested group.
    /// If not, the user is redirected to an accessDenied page
    /// </summary>
    public static  void CheckBaseAccess()
    {
        if (!HttpContext.Current.User.IsInRole(ConfigurationManager.AppSettings["ADMIN_GROUP"]) &&
            !HttpContext.Current.User.IsInRole(ConfigurationManager.AppSettings["PUBLIC_GROUP"]))
        {
            HttpContext.Current.Response.Redirect("./AccessDenied.htm");
        }

    }

    /// <summary>
    /// Check if the user has the supervisor access.
    /// If not, the user is redirected to an accessDenied page.
    /// </summary>
    public static void CheckPublicAccess()
    {
        if (!HttpContext.Current.User.IsInRole(ConfigurationManager.AppSettings["PUBLIC_GROUP"]))
        {
            HttpContext.Current.Response.Redirect("./AccessDenied.htm");
        }

    }

    /// <summary>
    /// Gets the number of contracts that the current user has role(s) on.
    /// If the user has only role on one contract, we put the contract and role in session
    /// </summary>
    public static int getUserNumberOfContracts()
    {
        int retour = 0;

        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();

        try
        {
            thisConnection.Open();
            cmd.CommandText = "SELECT COUNT (Distinct [contract]) FROM [allianceTimesheets].[dbo].[UserRights] " +
                                      " WHERE userlogin='" + HttpContext.Current.User.Identity.Name + "'";
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            retour = reader.GetInt32(0);
            reader.Close();
            thisConnection.Close();

            // If the user has several roles on several contracts, we add this information in session
            // The user will be then redirected to the contractselection page
            if (retour > 1)
            {
                HttpContext.Current.Session.Add("userHasManyContracts", true);
            }

            //If the user has role on only one contract, we put the contract in session
            if (retour == 1)
            {
                thisConnection.Open();

                cmd.CommandText = "SELECT [contract] FROM [allianceTimesheets].[dbo].[UserRights] " +
                                          " WHERE userlogin='" + HttpContext.Current.User.Identity.Name + "'";
                reader = cmd.ExecuteReader();
                reader.Read();
                String contract = reader.GetString(0);
                HttpContext.Current.Session.Add("currentContract", contract);
                reader.Close();
                thisConnection.Close();

                Utils.getUserRolesByContract(contract);

            }

        }
        catch (SqlException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            cmd.Dispose();
            thisConnection.Dispose();
            thisConnection.Close();
        }
        return retour;
    }


    // TODO : a modifier : seulement 1 role par user par contrat
    /// <summary>
    /// Gets the list of roles of the current user for a given contract and puts the roles in session
    /// </summary>
    /// <param name="contractNumber"></param>
    public static void getUserRolesByContract(String contractNumber)
    {
        LinkedList<String> retour = new LinkedList<string>();
        
        //First, we remove the roles that could be in session for a previous contract.
        HttpContext.Current.Session.Remove("userIsOwnerCA");
        HttpContext.Current.Session.Remove("userIsContractorCA");
        HttpContext.Current.Session.Remove("userIsContractorSupervisor");
        HttpContext.Current.Session.Remove("userIsOwnerSupervisor");

        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();

        try
        {
            thisConnection.Open();
            cmd.CommandText = "SELECT role FROM [allianceTimesheets].[dbo].[UserRights] " +
                                      " WHERE userlogin='" + HttpContext.Current.User.Identity.Name + "' AND " +
                                      " Contract='" + contractNumber + "'";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                retour.AddLast(reader["role"].ToString());
            }

            reader.Close();
            thisConnection.Close();

            //Put roles in session
            if (retour.Contains("Owner CA"))
            {
                HttpContext.Current.Session.Add("userIsOwnerCA", true);
            }
            if (retour.Contains("Contractor CA"))
            {
                HttpContext.Current.Session.Add("userIsContractorCA", true);
            }
            if (retour.Contains("ContractorSupervisor"))
            {
                HttpContext.Current.Session.Add("userIsContractorSupervisor", true);
            }
            if (retour.Contains("OwnerSupervisor"))
            {
                HttpContext.Current.Session.Add("userIsOwnerSupervisor", true);
            }

        }
        catch (SqlException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            cmd.Dispose();
            thisConnection.Dispose();
            thisConnection.Close();
        }
    }


    /// <summary>
    /// Register javascript on the page to notify the user
    /// </summary>
    /// <param name="notificationType">can be success, error, warning</param>
    /// <param name="message"></param>
    /// <param name="page"></param>
    public static void NotifyUser(String notificationType, String message, Page page )
    {
        string script = "<SCRIPT LANGUAGE='JavaScript'> ";
        script += "notifyUser('" + notificationType + "','" + message + "')";
        script += "</SCRIPT>";
        page.RegisterStartupScript("ClientScript", script);
    }


    /// <summary>
    /// Track the changes made on the contract's profile.
    /// All changes are inserted in the ContractProfileHistoryTable
    /// </summary>
    /// <param name="TypeOfChange">add/edit/delete</param>
    /// <param name="Table">Table modified</param>
    /// <param name="Description">precision on the modification</param>
    public static void TrackModification(String TypeOfChange, String Table, String Description)
    {
        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();

        try
        {
            thisConnection.Open();
            String user = HttpContext.Current.User.Identity.Name;
            String contractNumber = (String)HttpContext.Current.Session["currentContract"];


            //Ad the WP in database
            cmd.CommandText = "INSERT INTO [allianceTimesheets].[dbo].[ContractProfileHistory] ([ContractNumber],[TypeOfChange], " +
                              "[Object],[Description], [MadeBy], [ModificationDate]) " +
                              " VALUES ('" + contractNumber + "', '" + TypeOfChange + "', '" + "" + Table + "'," +
                              "'" + Description + "', '" + user + "', GETDATE() )";
            cmd.ExecuteNonQuery();
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            cmd.Dispose();
            thisConnection.Dispose();
            thisConnection.Close();
        }
        
    }
}