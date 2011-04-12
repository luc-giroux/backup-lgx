﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class CancelTS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // User must be Owner CA to access the requested page
        if (HttpContext.Current.Session["userIsOwnerCA"] == null ||
            !(Boolean)HttpContext.Current.Session["userIsOwnerCA"])
        {
            HttpContext.Current.Response.Redirect("./AccessDenied.htm");
        }

        if (!Page.IsPostBack)
        {
            this.bindWSComboBox();
            this.bindWPComboBox("");
            this.bindCWPComboBox("");
        }
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
                      " [SubmittedBy], [ApprovedBy], [ApprovedDate], [Cancelled], [PendingAdjustment]  " +
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
        HttpContext.Current.Response.Redirect("./CancelTS.aspx");
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
    /// Occurs when the user clicks on 
    /// -"View TS" link OR
    /// -"Cancel this TS" link OR
    /// -"Cancel and adjust this TS" link 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Get the index of the selected row
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).NamingContainer)).RowIndex;
        // Get the TS number of the selected Row
        string TSNumber = GridView1.Rows[index].Cells[1].Text;

        // Case when user click on "view TS"
        if (e.CommandArgument == "VIEW_TS")
        {
            HttpContext.Current.Response.Redirect("./ViewTS.aspx?TSNumber=" + TSNumber);
        }
        else // It's a TS cancellation or cancellation and adj
        {
            // User must provide "cancellation request reference number"
            if (this.HiddenFieldCancellationRequestNo.Value != "OK_CLICK_BUT_NO_REASON" && this.HiddenFieldCancellationRequestNo.Value != "cancel")
            {

                SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
                SqlCommand cmd = thisConnection.CreateCommand();

                // To avoid SQL error if user put "'" char in the reference
                HiddenFieldCancellationRequestNo.Value = HiddenFieldCancellationRequestNo.Value.Replace("'", "''");

                try
                {
                    thisConnection.Open();

                    //Basic Cancellation: we call the TimesheetCancel Stored Proc
                    if (e.CommandArgument == "CANCELLATION")
                    {
                        //Call the stored proc
                        cmd.CommandText = "EXEC [allianceTimesheets].[dbo].[TimesheetCancel] " +
                                          "@TimesheetNumber, @CancellationRequestNo,@CancelledBy";


                        // We Put the data into the parameters
                        SqlParameter param1 = new SqlParameter();
                        param1.ParameterName = "@TimesheetNumber";
                        param1.Value = TSNumber;

                        // We Put the data into the parameters
                        SqlParameter param2 = new SqlParameter();
                        param2.ParameterName = "@CancellationRequestNo";
                        param2.Value = HiddenFieldCancellationRequestNo.Value;

                        // We Put the data into the parameters
                        SqlParameter param3 = new SqlParameter();
                        param3.ParameterName = "@CancelledBy";
                        param3.Value = HttpContext.Current.User.Identity.Name;

                        //Add the param to the query
                        cmd.Parameters.Add(param1);
                        cmd.Parameters.Add(param2);
                        cmd.Parameters.Add(param3);

                        cmd.ExecuteNonQuery();

                        //User success notification
                        Utils.NotifyUser("success", "Timesheet " + TSNumber + "  successfully cancelled!", Page);
                        // To keep the params of filters
                        this.ButtonSearch_Click(null, null);
                        //Update gridview
                        GridView1.DataBind();
                    }

                    //Cancellation with Adjustment
                    if (e.CommandArgument == "ADJUSTMENT")
                    {
                        // We do not put cancelled = 1
                        cmd.CommandText = "UPDATE [allianceTimesheets].[dbo].[TimesheetHeader] SET PendingAdjustment = 1 ," +
                                          "CancellationRequestNo = '" + HiddenFieldCancellationRequestNo.Value + "', " +
                                          "CancelledDate= GETDATE(), " +
                                          "CancelledBy = '" + HttpContext.Current.User.Identity.Name + "' " +
                                          "WHERE TimesheetNumber = '" + TSNumber + "'";

                        cmd.ExecuteNonQuery();
                        //User success notification
                        Utils.NotifyUser("success", "Timesheet " + TSNumber + "  is now pending for adjustments!", Page);
                        // To keep the params of filters
                        this.ButtonSearch_Click(null, null);
                        //Update gridview
                        GridView1.DataBind();
                    }

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
            else
            {
                // We do not notify user if he clicked on cancel button in prompt box
                if (this.HiddenFieldCancellationRequestNo.Value != "cancel")
                {
                    Utils.NotifyUser("warning", "Please provide a the cancellation request number", Page);
                }

            }

        }


        
    }


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


    /// <summary>
    /// When the gridview is created,  If the TS is cancelled, we hide the links for cancellation
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (((System.Data.DataRowView)e.Row.DataItem) != null) //&& e.Row.RowIndex != 0)
        {
            try
            {
                // If the TS is cancelled or in pendingAdjusment,
                // we hide the links for cancellation ( a TS can be cancelled only one time)
                /*if (((System.Data.DataRowView)e.Row.DataItem)["Cancelled"]  == System.DBNull.Value)
                {
                    
                }
                 * */


                if ((bool)((System.Data.DataRowView)e.Row.DataItem)["Cancelled"] ||
                    (bool)((System.Data.DataRowView)e.Row.DataItem)["PendingAdjustment"])
                {
                    Label lbl = new Label();
                    lbl.Font.Italic = true;
                    
                    e.Row.Cells[0].Controls.Add(lbl);
                    e.Row.Cells[0].FindControl("linkButtonCancel").Visible = false;
                    e.Row.Cells[0].FindControl("linkButtonAdjust").Visible = false;

                    
                    ((LinkButton)e.Row.Cells[0].FindControl("linkButtonAdjust")).OnClientClick = "";

                    if ((bool)((System.Data.DataRowView)e.Row.DataItem)["Cancelled"])
                    {
                        lbl.Text = "Cancelled";
                    }
                    if ((bool)((System.Data.DataRowView)e.Row.DataItem)["PendingAdjustment"])
                    {
                        lbl.Text = "Pending Adjustment";
                    }
                    

                }
            }
            catch (Exception ex)
            {
                
            }

        }
    }
}