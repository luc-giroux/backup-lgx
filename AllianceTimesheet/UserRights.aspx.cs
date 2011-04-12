using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;

public partial class UserRights : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.bindRolesComboBox();
            this.bindContractComboBox();
        }

        // Only the admin can see the contract combovox
        if (Page.User.IsInRole(ConfigurationManager.AppSettings["ADMIN_GROUP"]))
        {
            this.ComboBoxContract.Visible = true;
            this.LabelContract.Visible = true;
        }
        else
        {
            this.ComboBoxContract.Visible = false;
            this.LabelContract.Visible = false;
        }
    }


    protected void ButtonAddRole_Click(object sender, EventArgs e)
    {

        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();
        if (this.InputFieldsAreOK())
        {
            try
            {
                thisConnection.Open();
                //If the combobox contracts is visible, we insert the role on the selected contract
                if (ComboBoxContract.Visible)
                {
                    cmd.CommandText = "INSERT INTO [allianceTimesheets].[dbo].[UserRights] ([userLogin],[role],[contract]) " +
                                          " VALUES ('" + HttpContext.Current.Session["currentUserLogin"] + "', '" + ComboBoxRoles.SelectedValue + "', '" +
                                          ComboBoxContract.SelectedValue + "')";
                }
                //If the combobox contracts is visible, we insert the role on the current contract in session
                else
                {
                    cmd.CommandText = "INSERT INTO [allianceTimesheets].[dbo].[UserRights] ([userLogin],[role],[contract]) " +
                                          " VALUES ('" + HttpContext.Current.Session["currentUserLogin"] + "', '" + ComboBoxRoles.SelectedValue + "', '" +
                                          HttpContext.Current.Session["currentContract"] + "')";
                }

                

                cmd.ExecuteNonQuery();
                //Reset of the fields
                ComboBoxRoles.SelectedValue = "select";
                //User success notification
                Utils.NotifyUser("success", "Role successfully added!", Page);

                //Remove all the roles in session to force pageHeader.ascx to get the new role and to refresh the menu
                HttpContext.Current.Session.Remove("userIsOwnerCA");
                HttpContext.Current.Session.Remove("userIsContractorCA");
                HttpContext.Current.Session.Remove("userIsContractorSupervisor");
                HttpContext.Current.Session.Remove("userIsOwnerSupervisor");


                //refresh of the page for refreshing the 2 gridview
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
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
        if (this.ComboBoxRoles.SelectedValue == "select")
        {
            Utils.NotifyUser("warning", "Please select a Role", Page);
            return false;
        }

        // If the contract combobox is visible, it has to be selected
        if (this.ComboBoxContract.Visible)
        {
            if (this.ComboBoxContract.SelectedValue == "select")
            {
                Utils.NotifyUser("warning", "Please select a Contract", Page);
                return false;
            }
        }

        return true;
    }

    protected void bindRolesComboBox()
    {
        DataSetGlobal.AppRolesDataTable dt = new DataSetGlobal.AppRolesDataTable();

        DataSetGlobal.AppRolesRow row0 = dt.NewAppRolesRow();
        DataSetGlobal.AppRolesRow row1 = dt.NewAppRolesRow();
        DataSetGlobal.AppRolesRow row2 = dt.NewAppRolesRow();
        DataSetGlobal.AppRolesRow row3 = dt.NewAppRolesRow();
        DataSetGlobal.AppRolesRow row4 = dt.NewAppRolesRow();

        row0.Role = "select";
        row0.Libelle = "select";
        row1.Role = "Owner CA";
        row1.Libelle = "Owner CA (Can edit contract structure)";
        row2.Role = "Contractor CA";
        row2.Libelle = "Contractor CA (Can view contract structure)";
        row3.Role = "ContractorSupervisor";
        row3.Libelle = "Contractor supervisor (Can submit timesheets)";
        row4.Role = "OwnerSupervisor";
        row4.Libelle = "Owner supervisor (Can approve/reject timesheets)";

        dt.AddAppRolesRow(row0);
        dt.AddAppRolesRow(row3);
        dt.AddAppRolesRow(row4);
        dt.AddAppRolesRow(row2);

        // Only the admin can assign the "owner contract admin" roles to a user
        if (Page.User.IsInRole(ConfigurationManager.AppSettings["ADMIN_GROUP"]))
        {
            dt.AddAppRolesRow(row1);
        }

        this.ComboBoxRoles.DataSource = dt;
        this.ComboBoxRoles.SelectedValue = "select";
        this.ComboBoxRoles.DataTextField =  dt.LibelleColumn.ToString();
        this.ComboBoxRoles.DataValueField = dt.RoleColumn.ToString();
        this.ComboBoxRoles.DataBind();
    }


    protected void bindContractComboBox()
    {
        DataSetGlobal.ContractDataTable contractdatatable = new DataSetGlobal.ContractDataTable();
        DataSetGlobalTableAdapters.ContractTableAdapter contracttableadadpter = new DataSetGlobalTableAdapters.ContractTableAdapter();
        contracttableadadpter.Fill(contractdatatable);

        this.ComboBoxContract.DataSource = contractdatatable;
        contractdatatable.AddContractRow("select", "select", "select", "select", "select", "select");
        this.ComboBoxContract.DataTextField = contractdatatable.LibelleColumn.ToString();
        this.ComboBoxContract.DataValueField = contractdatatable.ContractNumberColumn.ToString();
        this.ComboBoxContract.SelectedValue = "select";
        this.ComboBoxContract.DataBind();
    }
}