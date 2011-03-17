using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class AddVariation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.bindWSComboBox();
            this.bindWPComboBox("");
            this.bindCWPComboBox("");
        }
    }


    /// <summary>
    /// Occurs when the user click on the add vairation button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonAddVariation_Click(object sender, EventArgs e)
    {

        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);

        SqlCommand cmd = thisConnection.CreateCommand();
        if (this.InputFieldsAreOK())
        {
            try
            {
                thisConnection.Open();
                //Add the Variation in database
                cmd.CommandText = "INSERT INTO [allianceTimesheets].[dbo].[Variation] (" +
                                  "[VariationNumber]," +
                                  "[WSNumber]," +
                                  "[WPNumber]," +
                                  "[CWPNumber]," +
                                  "[VariationType]," +
                                  "[Description]," +
                                  "[Completed]," +
                                  "[ContractNumber]," +
                                  "[AllocatedHours]," +
                                  "[Discipline]) " +
                                  " VALUES (" +
                                  "@VariationNumber, @WSNumber, @WPNumber, @CWPNumber, @VariationType, @Description, 0, @ContractNumber,@AllocatedHours, @Discipline)";
                                  //"'" + TextBoxVariationNumber.Text + "'," +
                                  //ComboBoxWS.SelectedValue == "select" ? null : ("'" + ComboBoxWS.SelectedValue + "'") + "," +
                                  //ComboBoxWP.SelectedValue == "select" ? null : ("'" + ComboBoxWP.SelectedValue + "'") + "," +
                                  //ComboBoxCWP.SelectedValue == "select" ? null : ("'" + ComboBoxCWP.SelectedValue + "'") + "," +
                                  //"'" + ComboBoxVariationType.SelectedValue + "'," +
                                  //"'" + TextBoxDescription.Text + "'," +
                                  //"'0'," +
                                  //"'" + HttpContext.Current.Session["currentContract"] + "')";

                // We Put the data into the parameters
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@VariationNumber";
                param1.Value = TextBoxVariationNumber.Text;

                SqlParameter param2 = new SqlParameter();
                param2.ParameterName = "@WSNumber";
                if (ComboBoxWS.SelectedValue != "select")
                    param2.Value = ComboBoxWS.SelectedValue;
                else
                    param2.Value = DBNull.Value;

                SqlParameter param3 = new SqlParameter();
                param3.ParameterName = "@WPNumber";
                if (ComboBoxWP.SelectedValue != "select")
                    param3.Value = ComboBoxWP.SelectedValue;
                else
                    param3.Value = DBNull.Value;

                SqlParameter param4 = new SqlParameter();
                param4.ParameterName = "@CWPNumber";
                if (ComboBoxCWP.SelectedValue != "select")
                    param4.Value = ComboBoxCWP.SelectedValue;
                else
                    param4.Value = DBNull.Value;

                SqlParameter param5 = new SqlParameter();
                param5.ParameterName = "@VariationType";
                param5.Value = ComboBoxVariationType.SelectedValue;

                SqlParameter param6 = new SqlParameter();
                param6.ParameterName = "@Description";
                param6.Value = TextBoxDescription.Text;

                SqlParameter param7 = new SqlParameter();
                param7.ParameterName = "@ContractNumber";
                param7.Value = HttpContext.Current.Session["currentContract"];


                SqlParameter param8 = new SqlParameter();
                param8.ParameterName = "@AllocatedHours";
                if (TextBoxHours.Text != "")
                    param8.Value = TextBoxHours.Text;
                else
                    param8.Value = DBNull.Value;

                SqlParameter param9 = new SqlParameter();
                param9.ParameterName = "@Discipline";
                if (ComboBoxDiscipline.SelectedValue != "select")
                    param9.Value = ComboBoxDiscipline.SelectedValue;
                else
                    param9.Value = DBNull.Value;

	            cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                cmd.Parameters.Add(param3);
                cmd.Parameters.Add(param4);
                cmd.Parameters.Add(param5);
                cmd.Parameters.Add(param6);
                cmd.Parameters.Add(param7);
                cmd.Parameters.Add(param8);
                cmd.Parameters.Add(param9);
                cmd.ExecuteNonQuery();

                //User success notification
                Utils.NotifyUser("success", "Variation successfully added!", Page);

                //Tracking
                Utils.TrackModification("Addition", "Variation", "Variation " + ComboBoxVariationType.SelectedValue +
                                        " " + TextBoxVariationNumber.Text + " " + TextBoxDescription.Text + " added");

                //Reset of the fields
                TextBoxVariationNumber.Text = "";
                ComboBoxWS.SelectedValue = "select";
                ComboBoxWP.SelectedValue = "select";
                ComboBoxCWP.SelectedValue = "select";
                ComboBoxVariationType.SelectedValue = "select";
                ComboBoxDiscipline.SelectedValue = "select";
                TextBoxDescription.Text = "";
                TextBoxHours.Text = "";
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
    /// Load the WP combobox according to the WS selected.
    /// </summary>
    /// <param name="WSNumber"></param>
    protected void bindWPComboBox(String WSNumber)
    {
        ComboBoxWP.Items.Clear();

        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        thisConnection.Open();
        SqlCommand cmd = thisConnection.CreateCommand();
        cmd.CommandText = "SELECT DISTINCT CS.[WPNumber], WP.WPNumber + ' - ' + LEFT(Description, 40) AS Libelle " +
                          "FROM [allianceTimesheets].[dbo].[ContractStructure] CS " +
                          "INNER JOIN [allianceTimesheets].[dbo].[WP] WP " +
                          "ON WP.WPNumber = CS.WPNumber " +
                          "WHERE WSNumber = '" + WSNumber + "'" +
                          "AND WP.Completed = 0 ";

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        ComboBoxWP.DataSource = reader;
        ComboBoxWP.DataValueField = "WPNumber";
        ComboBoxWP.DataTextField = "Libelle";
        ComboBoxWP.Items.Add(new ListItem("select", "select"));
        ComboBoxWP.SelectedValue = "select";
        ComboBoxWP.DataBind();

        cmd.Connection.Close();
        cmd.Connection.Dispose();

    }


    /// <summary>
    /// Load the CWP combobox according to the WP selected.
    /// </summary>
    /// <param name="WPNumber"></param>
    protected void bindCWPComboBox(String WPNumber)
    {
        ComboBoxCWP.Items.Clear();

        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        thisConnection.Open();
        SqlCommand cmd = thisConnection.CreateCommand();
        cmd.CommandText = "SELECT DISTINCT CS.[CWPNumber], CWP.CWPNumber + ' - ' + LEFT(Description, 40) AS Libelle " +
                          "FROM [allianceTimesheets].[dbo].[ContractStructure] CS " +
                          "INNER JOIN [allianceTimesheets].[dbo].[CWP] CWP " +
                          "ON CWP.CWPNumber = CS.CWPNumber " +
                          "WHERE WPNumber = '" + WPNumber + "'" +
                          "AND CWP.Completed = 0 ";

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        ComboBoxCWP.DataSource = reader;
        ComboBoxCWP.DataValueField = "CWPNumber";
        ComboBoxCWP.DataTextField = "Libelle";
        ComboBoxCWP.Items.Add(new ListItem("select", "select"));
        ComboBoxCWP.SelectedValue = "select";
        ComboBoxCWP.DataBind();

        cmd.Connection.Close();
        cmd.Connection.Dispose();
    }


    /// <summary>
    /// Checks if values entered by the user are correct
    /// </summary>
    /// <returns>true if values are correct, False otherwise</returns>
    protected Boolean InputFieldsAreOK()
    {
        if (this.TextBoxVariationNumber.Text == "")
        {
            Utils.NotifyUser("warning", "Please enter a variation Number", Page);
            return false;
        }

        if (this.TextBoxDescription.Text == "")
        {
            Utils.NotifyUser("warning", "Please enter a Description", Page);
            return false;
        }

        if (this.ComboBoxVariationType.SelectedValue == "select")
        {
            Utils.NotifyUser("warning", "Please select a variation type", Page);
            return false;
        }

        return true;
    }



    /// <summary>
    /// A variation can be on a WS , a WP or a CWP but not on the 3 at the same time.
    /// So we disable the other combobox.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ComboBoxWS_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindWPComboBox(ComboBoxWS.SelectedValue);
        bindCWPComboBox("");
    }


    /// <summary>
    /// A variation can be on a WS , a WP or a CWP but not on the 3 at the same time.
    /// So we disable the other combobox.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ComboBoxWP_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindCWPComboBox(ComboBoxWP.SelectedValue);
    }

}