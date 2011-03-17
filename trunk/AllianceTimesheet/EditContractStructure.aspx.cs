using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class EditContractStructure : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.bindWSComboBox();
            this.bindWPComboBox();
            this.bindCWPComboBox();
        }
    }


    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {

        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();
        if (this.InputFieldsAreOK())
        {
            try
            {
                thisConnection.Open();
                //Ad the CWP in database
                cmd.CommandText = "INSERT INTO [allianceTimesheets].[dbo].[ContractStructure] " + 
                                  "([WSNumber],[WPNumber],[CWPNumber],[ContractNumber], [AllocatedHours])" +
                                  " VALUES ('" + ComboBoxWS.SelectedValue + "', " +
                                  "'" + ComboBoxWP.SelectedValue + "', " +
                                  "'" + ComboBoxCWP.SelectedValue + "', " +
                                  "'" + HttpContext.Current.Session["currentContract"] + "', " +
                                  "'" + TextBoxHours.Text + "')";
                cmd.ExecuteNonQuery();

                // Insert in ContractProfileHistory Table
                Utils.TrackModification("Addition", "ContractStructure", "New association WS " + ComboBoxWS.SelectedValue +
                                        " / WP " + ComboBoxWP.SelectedValue + " / CWP " + ComboBoxCWP.SelectedValue);

                //Reset of the fields
                ComboBoxWS.SelectedValue = "select";
                ComboBoxWP.SelectedValue = "select";
                ComboBoxCWP.SelectedValue = "select";
                TextBoxHours.Text = "";

                //User success notification
                Utils.NotifyUser("success", "Contract structure edition successfull!", Page);
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
    /// Load the WS combobox
    /// </summary>
    protected void bindWSComboBox()
    {
        DataSetGlobal.WSDataTable wsdatatable = new DataSetGlobal.WSDataTable();
        DataSetGlobalTableAdapters.WSTableAdapter wstableadadpter = new DataSetGlobalTableAdapters.WSTableAdapter();
        wstableadadpter.Fill(wsdatatable, (String)HttpContext.Current.Session["currentContract"]);

        this.ComboBoxWS.DataSource = wsdatatable;
        wsdatatable.AddWSRow("select", "select", "select", "select");
        this.ComboBoxWS.DataTextField = wsdatatable.LibelleColumn.ToString();
        this.ComboBoxWS.DataValueField = wsdatatable.WSNumberColumn.ToString();
        this.ComboBoxWS.SelectedValue = "select";
        this.ComboBoxWS.DataBind();
    }


    /// <summary>
    /// Load the WP combobox
    /// </summary>
    protected void bindWPComboBox()
    {
        DataSetGlobal.WPDataTable wpdatatable = new DataSetGlobal.WPDataTable();
        DataSetGlobalTableAdapters.WPTableAdapter wptableadadpter = new DataSetGlobalTableAdapters.WPTableAdapter();
        wptableadadpter.Fill(wpdatatable, (String)HttpContext.Current.Session["currentContract"]);

        this.ComboBoxWP.DataSource = wpdatatable;
        wpdatatable.AddWPRow("select", "select", "select", true, "select");
        this.ComboBoxWP.DataTextField = wpdatatable.LibelleColumn.ToString();
        this.ComboBoxWP.DataValueField = wpdatatable.WPNumberColumn.ToString();
        this.ComboBoxWP.SelectedValue = "select";
        this.ComboBoxWP.DataBind();
    }


    /// <summary>
    /// Load the CWP combobox
    /// </summary>
    protected void bindCWPComboBox()
    {
        DataSetGlobal.CWPDataTable cwpdatatable = new DataSetGlobal.CWPDataTable();
        DataSetGlobalTableAdapters.CWPTableAdapter cwptableadadpter = new DataSetGlobalTableAdapters.CWPTableAdapter();
        cwptableadadpter.Fill(cwpdatatable, (String)HttpContext.Current.Session["currentContract"]);

        this.ComboBoxCWP.DataSource = cwpdatatable;
        cwpdatatable.AddCWPRow("select", "select", "select", "select", true, "select");
        this.ComboBoxCWP.DataTextField = cwpdatatable.LibelleColumn.ToString();
        this.ComboBoxCWP.DataValueField = cwpdatatable.CWPNumberColumn.ToString();
        this.ComboBoxCWP.SelectedValue = "select";
        this.ComboBoxCWP.DataBind();
    }

    /// <summary>
    /// Checks if values entered by the user are correct
    /// </summary>
    /// <returns>true if values are correct, False otherwise</returns>
    protected Boolean InputFieldsAreOK()
    {
        if (this.ComboBoxWS.SelectedValue == "select")
        {
            Utils.NotifyUser("warning", "Please select a WS", Page);
            return false;
        }

        if (this.ComboBoxWP.SelectedValue == "select")
        {
            Utils.NotifyUser("warning", "Please select a WP", Page);
            return false;
        }

        if (this.ComboBoxCWP.SelectedValue == "select")
        {
            Utils.NotifyUser("warning", "Please select a CWP", Page);
            return false;
        }

        if (this.TextBoxHours.Text == "")
        {
            Utils.NotifyUser("warning", "Please enter allocated hours", Page);
            return false;
        }

        return true;
    }
}