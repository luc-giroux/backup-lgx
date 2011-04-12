using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class TSApproved : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //LGX: disable because now we've got the filters
            //this.CountTS();
            this.bindWSComboBox();
            this.bindWPComboBox("");
            this.bindCWPComboBox("");
        }
    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        String TSNumber = GridView1.SelectedRow.Cells[1].Text;
        HttpContext.Current.Response.Redirect("./ViewTS.aspx?TSNumber=" + TSNumber);
    }


    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count == 0)
        {
            this.LabelRecordFound.Text = "No record found.";
        }
        else
        {
            this.LabelRecordFound.Text = "Record(s) found : " + GridView1.Rows.Count.ToString();
        }
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
                              "AND Approved=1";

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



    #region All methods for the search filter

    #region button_click


    /// <summary>
    /// When the user click on a "FIND !" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSearch_Click(object sender, EventArgs e)
    {

        String sqlFilter = "";

        if (TextBoxTSFromDate.Text != "")
        {
            sqlFilter += " AND TSH.[TimesheetDate] >= '" + TextBoxTSFromDate.Text + "' ";
        }

        if (TextBoxTSToDate.Text != "")
        {
            sqlFilter += " AND TSH.[TimesheetDate] <= '" + TextBoxTSToDate.Text + "' ";
        }

        if (TextBoxTSNumber.Text != "")
        {
            sqlFilter += " AND TSH.[TimesheetNumber] LIKE '%" + TextBoxTSNumber.Text + "%'  ";
        }

        // Display TS from base scope AND variation
        if (CheckBoxBaseScope.Checked && CheckBoxVariation.Checked)
        {
            // We do nothing
        }

        else
        {
            //Only Base scope
            if (CheckBoxBaseScope.Checked)
            {
                sqlFilter += " AND (TSH.VariationNumber IS NULL AND TSH.Other IS NULL ) ";

                if (ComboBoxWS.SelectedValue != "select")
                {
                    sqlFilter += " AND TSH.WSNumber = '" + ComboBoxWS.SelectedValue + "' ";
                }
                if (ComboBoxWP.SelectedValue != "select")
                {
                    sqlFilter += " AND TSH.WPNumber = '" + ComboBoxWP.SelectedValue + "' ";
                }
                if (ComboBoxCWP.SelectedValue != "select")
                {
                    sqlFilter += " AND TSH.CWPNumber = '" + ComboBoxCWP.SelectedValue + "' ";
                }
            }
            //Only Variation
            else
            {
                sqlFilter += " AND TSH.WSNumber IS NULL ";

                //user just selected a type of variation
                if (ComboBoxVariationType.SelectedValue != "select" && ComboBoxVariation.SelectedValue == "select")
                {
                    sqlFilter += " AND V.VariationType = '" + ComboBoxVariationType.SelectedValue + "' ";
                }
                if (ComboBoxVariation.SelectedValue != "select" && ComboBoxVariation.SelectedValue != "")
                {
                    sqlFilter += " AND TSH.VariationNumber = '" + ComboBoxVariation.SelectedValue + "' ";
                }
            }

        }


        //Build SQL select
        SqlDataSourceTS.SelectCommand = "SELECT [TimesheetNumber], Replace(Convert(varchar(9),TimesheetDate,6),' ','-') as TimesheetDate, " +
                      "TSH.[WSNumber],TSH.[WPNumber],TSH.[CWPNumber],  " +
                      " CASE WHEN TSH.[VariationNumber] IS NULL AND  TSH.[WSNumber] IS NULL " +
                      "      THEN 'OTHER' ELSE TSH.[VariationNumber] END AS VariationNumber, " +
                      " CASE  " +
                      "      WHEN CWP.Discipline like '%Mechanical%' OR V.Discipline like '%Mechanical%' THEN 'MECHANICAL' " +
                      "      WHEN CWP.Discipline like '%CW%' OR V.Discipline like '%CW%' THEN 'CIVIL' " +
                      "      WHEN CWP.Discipline like '%E&I%' OR V.Discipline like '%E&I%' THEN 'E&I' " +
                      "      ELSE 'OTHERS' END AS Discipline, " +
                      " [SubmittedBy], [ApprovedBy], [ApprovedDate]  " +
                      " FROM [TimesheetHeader] TSH " +
                      " LEFT OUTER JOIN allianceTimesheets.dbo.CWP " +
                      "      ON CWP.CWPNumber = TSH.CWPNumber " +
                      " LEFT OUTER JOIN allianceTimesheets.dbo.Variation V " +
                      "      ON V.VariationNumber = TSH.VariationNumber " +
                      " WHERE [Approved] = 1  " +
                      " AND TSH.[ContractNumber] = '" + HttpContext.Current.Session["currentContract"] + "' " +
                      sqlFilter +
                      " ORDER BY TSH.[TimesheetDate] DESC, [TimesheetNumber]";



    }


    /// <summary>
    /// Reset all filters of search to init
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonResetFilters_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("./TSApproved.aspx");
    }


    #endregion

    #region selectedIndex_changed

    /// <summary>
    /// Occurs when user select a WS.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ComboBoxWS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ComboBoxWS.SelectedValue != "select")
        {
            this.bindWPComboBox(ComboBoxWS.SelectedValue);
            this.ComboBoxWP.Enabled = true;

            this.bindCWPComboBox("");
            this.ComboBoxCWP.Enabled = false;
        }
    }


    /// <summary>
    /// Occurs when user select a WP.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ComboBoxWP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ComboBoxWP.SelectedValue != "select")
        {
            this.bindCWPComboBox(ComboBoxWP.SelectedValue);
            this.ComboBoxCWP.Enabled = true;
        }
    }

    /// <summary>
    /// Occurs when user select a Variation Type.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ComboBoxVariationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ComboBoxVariationType.SelectedValue != "select")
        {
            this.bindVariationComboBox(ComboBoxVariationType.SelectedValue);
            this.ComboBoxVariation.Enabled = true;
        }
    }

    #endregion

    #region Init of combobox

    /// <summary>
    /// Load the WS combobox. When get ALL WS
    /// </summary>
    protected void bindWSComboBox()
    {
        DataSetGlobal.WSDataTable wsdatatable = new DataSetGlobal.WSDataTable();
        DataSetGlobalTableAdapters.WSTableAdapter wstableadadpter = new DataSetGlobalTableAdapters.WSTableAdapter();
        wstableadadpter.Fill(wsdatatable, (String)HttpContext.Current.Session["currentContract"]);

        this.ComboBoxWS.DataSource = wsdatatable;
        wsdatatable.AddWSRow("select", "select", "select", "All WS");
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
        cmd.CommandText = "SELECT DISTINCT CS.[WPNumber], WP.WPNumber + ' - ' + LEFT(Description, 100) AS Libelle " +
                          "FROM [allianceTimesheets].[dbo].[ContractStructure] CS " +
                          "INNER JOIN [allianceTimesheets].[dbo].[WP] WP " +
                          "ON WP.WPNumber = CS.WPNumber " +
                          "WHERE WSNumber = '" + WSNumber + "'";

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        ComboBoxWP.DataSource = reader;
        ComboBoxWP.DataValueField = "WPNumber";
        ComboBoxWP.DataTextField = "Libelle";
        ComboBoxWP.Items.Add(new ListItem("ALL WP", "select"));
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
        cmd.CommandText = "SELECT DISTINCT CS.[CWPNumber], CWP.CWPNumber + ' - ' + LEFT(Description, 100) AS Libelle " +
                          "FROM [allianceTimesheets].[dbo].[ContractStructure] CS " +
                          "INNER JOIN [allianceTimesheets].[dbo].[CWP] CWP " +
                          "ON CWP.CWPNumber = CS.CWPNumber " +
                          "WHERE WPNumber = '" + WPNumber + "'";

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        ComboBoxCWP.DataSource = reader;
        ComboBoxCWP.DataValueField = "CWPNumber";
        ComboBoxCWP.DataTextField = "Libelle";
        ComboBoxCWP.Items.Add(new ListItem("ALL CWP", "select"));
        ComboBoxCWP.SelectedValue = "select";
        ComboBoxCWP.DataBind();

        cmd.Connection.Close();
        cmd.Connection.Dispose();
    }


    /// <summary>
    /// Load the Variation combobox according to the VariationType selected.
    /// The variation loaded are ACTIVE (ie COMPLETED != 1)
    /// </summary>
    /// <param name="WPNumber"></param>
    protected void bindVariationComboBox(String VariationType)
    {
        DataSetGlobal.VariationDataTable dt = new DataSetGlobal.VariationDataTable();
        DataSetGlobalTableAdapters.VariationTableAdapter ta = new DataSetGlobalTableAdapters.VariationTableAdapter();
        ta.Fill(dt, (String)HttpContext.Current.Session["currentContract"], VariationType, false);

        this.ComboBoxVariation.Items.Clear();
        this.ComboBoxVariation.DataSource = dt;
        dt.AddVariationRow("select", "select", "select", "select", "select", "", "", true, "All variations");
        this.ComboBoxVariation.DataTextField = dt.LibelleVariationColumn.ToString();
        this.ComboBoxVariation.DataValueField = dt.VariationNumberColumn.ToString();
        this.ComboBoxVariation.SelectedValue = "select";
        this.ComboBoxVariation.DataBind();
    }

    #endregion

    #region checkboxes

    /// <summary>
    /// Occurs whent the user click on the "variation" checkbox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckBoxVariation_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBoxVariation.Checked)
        {
            if (CheckBoxBaseScope.Checked)
            {
                this.BaseScopeActivation(false);
                this.VariationActivation(false);
            }
            else
            {
                this.BaseScopeActivation(false);
                this.VariationActivation(true);
            }

        }
        else
        {
            CheckBoxBaseScope.Checked = true;
            this.BaseScopeActivation(true);
            this.VariationActivation(false);
        }
    }


    /// <summary>
    /// Occurs whent the user click on the "base scope" checkbox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckBoxBaseScope_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBoxBaseScope.Checked)
        {
            if (CheckBoxVariation.Checked)
            {
                this.VariationActivation(false);
                this.BaseScopeActivation(false);
            }
            else
            {
                this.VariationActivation(false);
                this.BaseScopeActivation(true);
            }

        }
        else
        {
            CheckBoxVariation.Checked = true;
            this.VariationActivation(true);
            this.BaseScopeActivation(false);
        }
    }


    #endregion

    #region Various


    /// <summary>
    /// Activate Or unactivate the Base scope search filter 
    /// </summary>
    /// <param name="activate"></param>
    protected void BaseScopeActivation(bool activate)
    {
        if (activate)
        {
            this.ComboBoxWS.Enabled = true;
            this.ComboBoxWS.BackColor = System.Drawing.Color.Transparent;
            this.ComboBoxWP.BackColor = System.Drawing.Color.Transparent;
            this.ComboBoxCWP.BackColor = System.Drawing.Color.Transparent;
        }
        else
        {
            this.ComboBoxWS.Enabled = false;
            this.ComboBoxWP.Enabled = false;
            this.ComboBoxCWP.Enabled = false;
            this.ComboBoxWS.BackColor = System.Drawing.Color.Gray;
            this.ComboBoxWP.BackColor = System.Drawing.Color.Gray;
            this.ComboBoxCWP.BackColor = System.Drawing.Color.Gray;
            this.ComboBoxWS.SelectedValue = "select";
            this.ComboBoxWP.SelectedValue = "select";
            this.ComboBoxCWP.SelectedValue = "select";
        }
    }


    /// <summary>
    /// Activate Or unactivate the variation search filter 
    /// </summary>
    /// <param name="activate"></param>
    protected void VariationActivation(bool activate)
    {
        if (activate)
        {
            this.ComboBoxVariationType.Enabled = true;
            this.ComboBoxVariationType.BackColor = System.Drawing.Color.Transparent;
            this.ComboBoxVariation.BackColor = System.Drawing.Color.Transparent;
        }
        else
        {
            this.ComboBoxVariationType.Enabled = false;
            this.ComboBoxVariation.Enabled = false;
            this.ComboBoxVariationType.BackColor = System.Drawing.Color.Gray;
            this.ComboBoxVariation.BackColor = System.Drawing.Color.Gray;
            this.ComboBoxVariationType.SelectedValue = "select";
            this.ComboBoxVariation.SelectedValue = "select";
        }
    }


    #endregion

    #endregion


}