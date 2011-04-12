using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// LGiroux 30/11/10
/// </summary>
public partial class TimesheetSubmit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["currentUser"] = User.Identity.Name;

        // User must be Contractor supervisor to access this page
        if (HttpContext.Current.Session["userIsContractorSupervisor"] == null ||
            !(Boolean)HttpContext.Current.Session["userIsContractorSupervisor"])
        {
            HttpContext.Current.Response.Redirect("./AccessDenied.htm");
        }

        if (!Page.IsPostBack)
        {
            // If the has submitted a timesheet we display him the nofitication of his operation
            if (Request.Params["status"] != null)
            {
                if (Request.Params["status"] == "SubmissionSuccess")
                {
                    Utils.NotifyUser("success", "Timesheet " + Request.Params["TSCreated"] + " successfully submitted!", Page);
                }

                if (Request.Params["status"] == "SaveSuccess")
                {
                    Utils.NotifyUser("success", "Timesheet " + Request.Params["TSCreated"] + " successfully saved as draft!", Page);
                }

                if (Request.Params["status"] == "AdjustmentSuccess")
                {
                    Utils.NotifyUser("success", "Timesheet " + Request.Params["TSCreated"] + " successfully submitted! You can continue your adjusment by submitting another timesheet or finish your adjustment by clicking the (Finish Adjustment) button.", Page);
                }
            }
        }


        //Utils.CheckSupervisorAccess();
        if (!Page.IsPostBack)
        {
            this.bindWSComboBox();
            this.bindWPComboBox("");
            this.bindCWPComboBox("");

            // We are in edition mode of the TS, we have to init the screen with the values of the TS
            if (Request.Params["TSNumber"] != null)
            {
                //It can be EDITION of rejected TS or ADJUSTMENT of approved TS
                if (Request.Params["Adjustment"] != null)
                {
                    // If we are in adjustment, we have 2 cases: init of the ADJ or
                    // the user is doing the adjusment and already submitted a corrective TS (in this case we must load the page with the submitted TS )
                    if (Request.Params["status"] != null) 
                    {
                        if (Request.Params["status"] == "AdjustmentSuccess")
                        {
                            //Get the last TS submitted to init the page
                            this.initScreen(Request.Params["TSCreated"]);
                        }
                    }
                    else
                    {
                        this.initScreen(Request.Params["TSNumber"]);
                    }

                    
                    LabelFieldsetLegend.Text = "Adjustment of the timesheet " + Request.Params["TSNumber"];

                    // To get the total Hours of the TS being adjusted
                    SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
                    SqlCommand cmd = thisConnection.CreateCommand();

                    try
                    {
                        thisConnection.Open();
                        cmd.CommandText = "SELECT REPLACE(RTRIM(REPLACE(REPLACE(RTRIM(REPLACE(SUM(TSD.PPHours + TSD.PUPIndirectHours + TSD.PUPMaterialHours + " + 
                                            " TSD.PUPOtherHours  + TSD.PUPTravelHours + TSD.PUPWeatherHours + TSD.NPOtherHours + TSD.NPReworkHours), '0', ' ')), " + 
                                            " ' ', '0'), '.', ' ')), ' ', '.')  As Hours" +
                                            " FROM [allianceTimesheets].[dbo].[TimesheetHeader] TSH " +
                                            " INNER JOIN [allianceTimesheets].[dbo].[TimesheetDetail] TSD " +
                                            " ON TSH.TimesheetNumber = TSD.TimesheetNumber " +
                                            " WHERE TSH.TimesheetNumber = '" + Request.Params["TSNumber"] + "' " +
                                            " GROUP BY TSH.TimesheetNumber ";
                        LabelFieldsetLegend.Text += " (Total Hours: " + cmd.ExecuteScalar() + ")";
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



                    //TS date cannot be changed so we disable it
                    TextBoxTSDate.Enabled = false;

                    PanelCorrectiveTS.Visible = true;
                    DataSourceCorrectiveTS.SelectCommand = "SELECT TSH.TimesheetNumber, " +
                                                            " SUM(TSD.PPHours + TSD.PUPIndirectHours + TSD.PUPMaterialHours + TSD.PUPOtherHours " + 
                                                            " + TSD.PUPTravelHours + TSD.PUPWeatherHours + TSD.NPOtherHours + TSD.NPReworkHours) AS Hours " + 
                                                            " FROM [allianceTimesheets].[dbo].[TimesheetHeader] TSH " +
                                                            " INNER JOIN [allianceTimesheets].[dbo].[TimesheetDetail] TSD " +
                                                            " ON TSH.TimesheetNumber = TSD.TimesheetNumber " +
                                                            " WHERE [AdjustmentFromTS] = '" + Request.Params["TSNumber"] + "' " +
                                                            " GROUP BY TSH.TimesheetNumber ";
                    GridViewCorrectiveTS.DataBind();

                    if (GridViewCorrectiveTS.Rows.Count == 0)
                    {
                        ButtonFinishAdjustment.Visible = false;
                        LabelCorrectiveTS.Text = "This timesheet is currently replaced by no timesheet. ";
                    }
                    
                }
                else //EDITION of TS
                {
                    //EDition of draft
                    if (Request.Params["Draft"] != null)
                    {
                        LabelFieldsetLegend.Text = "Edition of the draft timesheet " + Request.Params["TSNumber"];
                    }
                    //Edition of rejeted TS
                    else
                    {
                        LabelFieldsetLegend.Text = "Edition of the rejected timesheet " + Request.Params["TSNumber"];
                    }

                    this.initScreen(Request.Params["TSNumber"]);
                }

            }
            else
            {
                LabelFieldsetLegend.Text = "Submit a new Timesheet";
            }
        }

    }

    #region Init of combobox

    /// <summary>
    /// Load the WS combobox. When get only ACTIVE WS (ie completed != 1)
    /// </summary>
    protected void bindWSComboBox()
    {
        DataSetGlobal.WSActiveDataTable wsdatatable = new DataSetGlobal.WSActiveDataTable();
        DataSetGlobalTableAdapters.WSActiveTableAdapter wstableadadpter = new DataSetGlobalTableAdapters.WSActiveTableAdapter();
        wstableadadpter.Fill(wsdatatable, (String)HttpContext.Current.Session["currentContract"]);

        this.ComboBoxWS.DataSource = wsdatatable;
        wsdatatable.AddWSActiveRow("select", "select", "select", "Select WS *", true);
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
                          "WHERE WSNumber = '" + WSNumber + "'" +
                          "AND CS.ContractNumber = '" + (String)HttpContext.Current.Session["currentContract"] + "'" +
                          "AND WP.Completed = 0 ";

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        ComboBoxWP.DataSource = reader;
        ComboBoxWP.DataValueField = "WPNumber";
        ComboBoxWP.DataTextField = "Libelle";
        ComboBoxWP.Items.Add(new ListItem("Select WP *", "select"));
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
                          "WHERE WPNumber = '" + WPNumber + "'" +
                          "AND CS.ContractNumber = '" + (String)HttpContext.Current.Session["currentContract"] + "'" +
                          "AND CWP.Completed = 0 ";

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        ComboBoxCWP.DataSource = reader;
        ComboBoxCWP.DataValueField = "CWPNumber";
        ComboBoxCWP.DataTextField = "Libelle";
        ComboBoxCWP.Items.Add(new ListItem("Select CWP *", "select"));
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
        dt.AddVariationRow("select", "select", "select", "select", "select", "", "", true, "Select Variation Number*");
        this.ComboBoxVariation.DataTextField = dt.LibelleVariationColumn.ToString();
        this.ComboBoxVariation.DataValueField = dt.VariationNumberColumn.ToString();
        this.ComboBoxVariation.SelectedValue = "select";
        this.ComboBoxVariation.DataBind();
    }

    #endregion

    #region SelectedIndexChanged Methods called by user interaction on interface


    /// <summary>
    /// We enable/disable combobox according to the selected scope.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioButtonListScope_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonListScope.SelectedValue == "Base Scope")
        {
            ComboBoxVariationType.Enabled = false;
            ComboBoxVariationType.SelectedValue = "select";
            ComboBoxVariation.Enabled = false;
            ComboBoxVariation.SelectedValue = "select";
            TextBoxVariationOthers.Enabled = false;

            ComboBoxWS.Enabled = true;
            ComboBoxWS.SelectedValue = "select";
        }
        //Variation
        else
        {
            ComboBoxVariationType.Enabled = true;

            ComboBoxWS.Enabled = false;
            ComboBoxWP.Enabled = false;
            ComboBoxCWP.Enabled = false;

            ComboBoxWS.SelectedValue = "select";
            ComboBoxWP.SelectedValue = "select";
            ComboBoxCWP.SelectedValue = "select";

        }
    }

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
    /// Called when a user select a CWP. Update the datasource of the gridview of previous timesheet in order
    /// to filter the list of pervious timesheet with the parameters selected (WS/WP/CWP)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ComboBoxCWP_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlDataSourcePreviousTS.SelectCommand = "SELECT  Replace(Convert(varchar(9),TimesheetDate,6),' ','-') as [TimesheetDate], " +
                                                "[TimesheetNumber], [WSNumber], [WPNumber], [CWPNumber], [VariationNumber], " +
                                                "[SubmittedDate], " +
                                                "[SubmittedBy] FROM [TimesheetHeader] WHERE " +
                                                " [ContractNumber] = '" + HttpContext.Current.Session["currentContract"] + "' " +
                                                "AND [WSNumber] = '" + ComboBoxWS.SelectedValue + "' " +
                                                "AND [WPNumber] = '" + ComboBoxWP.SelectedValue + "' " +
                                                "AND [CWPNumber] = '" + ComboBoxCWP.SelectedValue + "' " +
                                                "ORDER BY [SubmittedDate] DESC";
        GridViewPreviousTS.DataBind();
    }


    /// <summary>
    /// Called when a user select a variation number. Update the datasource of the gridview of previous timesheet in order
    /// to filter the list of pervious timesheet with the variation selected
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ComboBoxVariation_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlDataSourcePreviousTS.SelectCommand = "SELECT  Replace(Convert(varchar(9),TimesheetDate,6),' ','-') as [TimesheetDate], " +
                                        "[TimesheetNumber], [WSNumber], [WPNumber], [CWPNumber], [VariationNumber], " +
                                        "[SubmittedDate], " +
                                        "[SubmittedBy] FROM [TimesheetHeader] WHERE " +
                                        " [ContractNumber] = '" + HttpContext.Current.Session["currentContract"] + "' " +
                                        "AND [VariationNumber] = '" + ComboBoxVariation.SelectedValue + "' " +
                                        "ORDER BY [SubmittedDate] DESC";
        GridViewPreviousTS.DataBind();
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

            if (ComboBoxVariationType.SelectedValue == "Other")
            {
                this.TextBoxVariationOthers.Enabled = true;
                this.ComboBoxVariation.Enabled = false;
            }
            else
            {
                this.TextBoxVariationOthers.Text = "";
                this.TextBoxVariationOthers.Enabled = false;
            }
        }

    }


    protected void RadioButtonListSuspension_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonListSuspension.SelectedValue == "Yes")
        {
            this.TextBoxTimesheetDuration.Visible = true;
            this.LabelTimesheetDuration.Visible = true;
            this.ButtonAddSuspension.Visible = true;
            this.TextBoxSuspensionDuration.Visible = true;
            this.LabelSuspensionDuration.Visible = true;

            this.PutGridviewInSuspensionMode(true);
        }
        else
        {
            this.TextBoxTimesheetDuration.Visible = false;
            this.LabelTimesheetDuration.Visible = false;
            this.ButtonAddSuspension.Visible = false;
            this.TextBoxSuspensionDuration.Visible = false;
            this.LabelSuspensionDuration.Visible = false;

            this.PutGridviewInSuspensionMode(false);
        }
    }

#endregion

    #region ButtonClick Methods called by user interaction on interface

    /// <summary>
    /// Occurs when user click the "add" button of the workers selection modal.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonAddSelectedWorkers_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("WorkerID"));
        dt.Columns.Add(new DataColumn("WorkerInfo"));
        dt.Columns.Add(new DataColumn("PPHours"));
        dt.Columns.Add(new DataColumn("PUPTravelHours"));
        dt.Columns.Add(new DataColumn("PUPMaterialHours"));
        dt.Columns.Add(new DataColumn("PUPWeatherHours"));
        dt.Columns.Add(new DataColumn("PUPIndirectHours"));
        dt.Columns.Add(new DataColumn("PUPOtherHours"));
        dt.Columns.Add(new DataColumn("NPReworkHours"));
        dt.Columns.Add(new DataColumn("NPOtherHours"));

        // First, we get the existing rows in the gridview and add them to the datatable
        foreach (GridViewRow gvRow in GridViewWorkerList.Rows)
	    {
		    DataRow dr = dt.NewRow();
            dr["WorkerID"] = gvRow.Cells[1].Text;
            dr["WorkerInfo"] = Server.HtmlDecode(gvRow.Cells[2].Text);
            dr["PPHours"] = ((TextBox)gvRow.FindControl("TBProductive")).Text;
            dr["PUPTravelHours"] = ((TextBox)gvRow.FindControl("TBTravel")).Text;
            dr["PUPMaterialHours"] = ((TextBox)gvRow.FindControl("TBMaterial")).Text;
            dr["PUPWeatherHours"] = ((TextBox)gvRow.FindControl("TBWeather")).Text;
            dr["PUPIndirectHours"] = ((TextBox)gvRow.FindControl("TBIndirect")).Text;
            dr["PUPOtherHours"] = ((TextBox)gvRow.FindControl("TBOthersPayable")).Text;
            dr["NPReworkHours"] = ((TextBox)gvRow.FindControl("TBRework")).Text;
            dr["NPOtherHours"] = ((TextBox)gvRow.FindControl("TBOthersNonPayable")).Text;
            dt.Rows.Add(dr);
	    }

        // Then, we get every workers checked and create new row in the datatable
        foreach (GridViewRow row in GridViewWorkers.Rows)
        {
            bool canAddTheWorker = true;

            CheckBox cb = (CheckBox)row.FindControl("CheckBoxWorkerSelect");
            if (cb.Checked)
            {

                //We if the worker selected is not already present in the list
                foreach (DataRow dtrow in dt.Rows)
                {
                    // row.Cells[1] == WORKER ID in the gridview of worker selection
                    // dtrow["WorkerID"] == WORKER ID in the datatale we are building
                    if (row.Cells[1].Text == dtrow["WorkerID"].ToString())
                    {
                        canAddTheWorker = false;
                    }
                }

                //We add the worker only if he's not already present in the list
                if (canAddTheWorker)
                {
                    DataRow dr = dt.NewRow();
                    dr["WorkerID"] = row.Cells[1].Text;
                    dr["WorkerInfo"] = Server.HtmlDecode(row.Cells[2].Text) + " " +
                                       Server.HtmlDecode(row.Cells[3].Text) + " " + row.Cells[4].Text;
                    dt.Rows.Add(dr);
                }
            }
        }

        GridViewWorkerList.DataSource = dt;
        GridViewWorkerList.DataBind();

        // We have to rebind the values of hours for each worker
        foreach (GridViewRow gvRow in GridViewWorkerList.Rows)
        {
            foreach (DataRow dtRow in dt.Rows)
            {
                // we are on the same worker
                if (gvRow.Cells[1].Text == dtRow["WorkerID"].ToString())
                {
                    ((TextBox)gvRow.FindControl("TBProductive")).Text = dtRow["PPHours"].ToString();
                    ((TextBox)gvRow.FindControl("TBTravel")).Text = dtRow["PUPTravelHours"].ToString();
                    ((TextBox)gvRow.FindControl("TBMaterial")).Text = dtRow["PUPMaterialHours"].ToString();
                    ((TextBox)gvRow.FindControl("TBWeather")).Text = dtRow["PUPWeatherHours"].ToString();
                    ((TextBox)gvRow.FindControl("TBIndirect")).Text = dtRow["PUPIndirectHours"].ToString();
                    ((TextBox)gvRow.FindControl("TBOthersPayable")).Text = dtRow["PUPOtherHours"].ToString();
                    ((TextBox)gvRow.FindControl("TBRework")).Text = dtRow["NPReworkHours"].ToString();
                    ((TextBox)gvRow.FindControl("TBOthersNonPayable")).Text = dtRow["NPOtherHours"].ToString();
                }
            }
        }

        //Activate Submit button
        if (GridViewWorkerList.Rows.Count != 0)
        {
            this.ButtonSubmitTS.Visible = true;

            //We show "save as draft button" only for new submission or for edition of draft
            if (Request.Params["TSNumber"] == null ||
                Request.Params["Draft"] != null)
            {
                this.ButtonSaveAsDraft.Visible = true;
            }
        }
        else
        {
            this.ButtonSubmitTS.Visible = false;
            this.ButtonSaveAsDraft.Visible = false;
        }

        if (RadioButtonListSuspension.SelectedValue == "Yes")
        {
            this.PutGridviewInSuspensionMode(true);
        }
        else
        {
            this.PutGridviewInSuspensionMode(false);
        }
    }


    /// <summary>
    /// Occurs when the user click on the "submit timesheet " button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSubmitTS_Click(object sender, EventArgs e)
    {

        if (InputFieldsAreOK())
        {
            this.InsertTimesheet(false);
        }
    }

    /// <summary>
    /// Occurs when the user click on the "submit as draft " button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSaveAsDraft_Click(object sender, EventArgs e)
    {

        if (InputFieldsAreOK())
        {
            this.InsertTimesheet(true);
        }
    }

    #endregion

    #region Others methods

    /// <summary>
    /// Checks if values entered by the user are correct.
    /// </summary>
    /// <returns>true if values are correct, False otherwise</returns>
    protected Boolean InputFieldsAreOK()
    {

        //TS Date
        if (this.TextBoxTSDate.Text == "")
        {
            Utils.NotifyUser("warning", "Please enter a timesheet date", Page);
            return false;
        }
        else
        {
            if (Convert.ToDateTime(this.TextBoxTSDate.Text) > DateTime.Now)
            {
                Utils.NotifyUser("warning", "Timesheet date cannot be superior to current date", Page);
                return false;
            }
        }

        //Base Scope
        if (this.RadioButtonListScope.SelectedValue == "Base Scope")
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
        }

        //Variation
        else
        {
            if (this.ComboBoxVariationType.SelectedValue == "select")
            {
                Utils.NotifyUser("warning", "Please select a variation type", Page);
                return false;
            }

            if (this.ComboBoxVariationType.SelectedValue == "Other")
            {
                if (this.TextBoxVariationOthers.Text == "")
                {
                    Utils.NotifyUser("warning", "Please fill the <b>others</b> variation field", Page);
                    return false;
                }
            }
            else
            {
                if (this.ComboBoxVariation.SelectedValue == "select")
                {
                    Utils.NotifyUser("warning", "Please select a variation", Page);
                    return false;
                }
            }

        }

        if (TextBoxMainActivities.Text == "")
        {
             Utils.NotifyUser("warning", "Please enter the activities of the day", Page);
            return false;
        }

        //Every worker must have at least 1 hour
        foreach (GridViewRow gvRow in GridViewWorkerList.Rows)
        {

                string s1 = ((TextBox)gvRow.FindControl("TBProductive")).Text;
                string s2 = ((TextBox)gvRow.FindControl("TBTravel")).Text;
                string s3 = ((TextBox)gvRow.FindControl("TBMaterial")).Text;
                string s4 = ((TextBox)gvRow.FindControl("TBWeather")).Text;
                string s5 = ((TextBox)gvRow.FindControl("TBIndirect")).Text;
                string s6 = ((TextBox)gvRow.FindControl("TBOthersPayable")).Text;
                string s7 = ((TextBox)gvRow.FindControl("TBRework")).Text;
                string s8 = ((TextBox)gvRow.FindControl("TBOthersNonPayable")).Text;

                if (s1 == "")
                {
                    s1 = "0";
                }
                if (s2 == "")
                {
                    s2 = "0";
                }
                if (s3 == "")
                {
                    s3 = "0";
                }
                if (s4 == "")
                {
                    s4 = "0";
                }
                if (s5 == "")
                {
                    s5 = "0";
                }
                if (s6 == "")
                {
                    s6 = "0";
                }
                if (s7 == "")
                {
                    s7 = "0";
                }
                if (s8 == "")
                {
                    s8 = "0";
                }

                if (Decimal.Parse(s1) + Decimal.Parse(s2) + Decimal.Parse(s3) + Decimal.Parse(s4) + Decimal.Parse(s5) +
                    Decimal.Parse(s6) + Decimal.Parse(s7) + +Decimal.Parse(s8) == 0)
                {
                    Utils.NotifyUser("warning", "You must enter hours for every worker", Page);
                    return false;
                }

        }


        return true;
    }


    /// <summary>
    /// create the parameter to pass to the stored proc to insert all workers hours in db in 1 call
    /// parameter order : workerid,productive,travel,Material,Weather,Indirect,Others,Rework(nonpayable), indirect(nonpayable);
    /// </summary>
    /// <returns></returns>
    protected String getInsertWorkersHoursParam()
    {

        String paramWorkersHours = "";

        foreach (GridViewRow row in GridViewWorkerList.Rows)
        {
            //Get the worker ID
            paramWorkersHours = paramWorkersHours + row.Cells[1].Text;
            paramWorkersHours = paramWorkersHours + ",";

            String productive = ((TextBox)row.FindControl("TBProductive")).Text;
            if (productive == "")
                productive = "0";
            paramWorkersHours = paramWorkersHours + productive;
            paramWorkersHours = paramWorkersHours + ",";

            String travel = ((TextBox)row.FindControl("TBTravel")).Text;
            if (travel == "")
                travel = "0";
            paramWorkersHours = paramWorkersHours + travel;
            paramWorkersHours = paramWorkersHours + ",";

            String material = ((TextBox)row.FindControl("TBMaterial")).Text;
            if (material == "")
                material = "0";
            paramWorkersHours = paramWorkersHours + material;
            paramWorkersHours = paramWorkersHours + ",";

            String weather = ((TextBox)row.FindControl("TBWeather")).Text;
            if (weather == "")
                weather = "0";
            paramWorkersHours = paramWorkersHours + weather;
            paramWorkersHours = paramWorkersHours + ",";

            String indirect = ((TextBox)row.FindControl("TBIndirect")).Text;
            if (indirect == "")
                indirect = "0";
            paramWorkersHours = paramWorkersHours + indirect;
            paramWorkersHours = paramWorkersHours + ",";

            String others = ((TextBox)row.FindControl("TBOthersPayable")).Text;
            if (others == "")
                others = "0";
            paramWorkersHours = paramWorkersHours + others;
            paramWorkersHours = paramWorkersHours + ",";

            String rework = ((TextBox)row.FindControl("TBRework")).Text;
            if (rework == "")
                rework = "0";
            paramWorkersHours = paramWorkersHours + rework;
            paramWorkersHours = paramWorkersHours + ",";

            String othersnp = ((TextBox)row.FindControl("TBOthersNonPayable")).Text;
            if (othersnp == "")
                othersnp = "0";
            paramWorkersHours = paramWorkersHours + othersnp;
            paramWorkersHours = paramWorkersHours + ";";

        }

        return paramWorkersHours;
    }


    /// <summary>
    /// Put all fields to blank
    /// </summary>
    protected void ResetAllFields()
    {

        TextBoxTSDate.Text = "";
        RadioButtonListScope.SelectedValue = "Base Scope";
        ComboBoxWS.SelectedValue = "select";
        ComboBoxWP.SelectedValue = "select";
        ComboBoxCWP.SelectedValue = "select";
        ComboBoxVariationType.SelectedValue = "select";
        ComboBoxVariation.SelectedValue = "select";
        TextBoxVariationOthers.Text = "";
        TextBoxMainActivities.Text = "";
        RadioButtonListShifts.SelectedValue = "Day Shift";
        RadioButtonListSuspension.SelectedValue = "No";
        TextBoxTimesheetDuration.Text = "";
        TextBoxSuspensionDuration.Text = "";
        ButtonSubmitTS.Visible = false;
        GridViewWorkers.DataBind();
        GridViewWorkerList.DataSource = null;
    }

    #endregion


    /// <summary>
    /// When the user select a TS in the "PREVIOUS TS" popup.
    /// We retrieve all the workers linked to the selected TS in database.
    /// Then we add each worker in a table that we finally bind to the gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewPreviousTS_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Prepare the datatable
        DataTable dt = new DataTable();
        DataColumn col1 = new DataColumn("WorkerID");
        dt.Columns.Add(col1);
        DataColumn col2 = new DataColumn("WorkerInfo");
        dt.Columns.Add(col2);


        //Prepare the connection to get all workers of the selected TS
        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        thisConnection.Open();
        SqlCommand cmd = thisConnection.CreateCommand();

        //Call the stored proc
        cmd.CommandText = "SELECT W.WorkerID, LastName, FirstName, BadgeNumber FROM [allianceTimesheets].[dbo].[TimesheetHeader] TSH " +
                          "INNER JOIN [allianceTimesheets].[dbo].[TimesheetDetail] TSD ON TSH.TimesheetNumber = TSD.TimesheetNumber " +
                          "INNER JOIN [allianceTimesheets].[dbo].[Worker] W ON W.WorkerID = TSD.WorkerID " +
                          "WHERE TSH.TimesheetNumber = '" + GridViewPreviousTS.SelectedRow.Cells[1].Text + "' " +
                          "ORDER BY LastName";

        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            // Add every worker to a row in the datatable
            DataRow dr = dt.NewRow();
            dr["WorkerID"] = reader["WorkerID"].ToString();
            dr["WorkerInfo"] = reader["LastName"].ToString() + " " + reader["FirstName"].ToString() + " " + reader["BadgeNumber"].ToString(); 
            dt.Rows.Add(dr);
        }

        reader.Close();
        thisConnection.Close();

        //Bind datatable created to the gridview
        GridViewWorkerList.DataSource = dt;
        GridViewWorkerList.DataBind();

        //Activate Submit button
        if (GridViewWorkerList.Rows.Count != 0)
        {
            this.ButtonSubmitTS.Visible = true;

            //We show "save as draft button" only for new submission or for edition of draft
            if (Request.Params["TSNumber"] == null ||
                Request.Params["Draft"] != null)
            {
                this.ButtonSaveAsDraft.Visible = true;
            }
        }
        else
        {
            this.ButtonSubmitTS.Visible = false;
            this.ButtonSaveAsDraft.Visible = false;
        }

        if (RadioButtonListSuspension.SelectedValue == "Yes")
        {
            this.PutGridviewInSuspensionMode(true);
        }
        else
        {
            this.PutGridviewInSuspensionMode(false);
        }
    }


    /// <summary>
    /// Occurs when the user click on the "remove" link in the gridview of workers
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewWorkerList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            // get the workerID of the clicked row
            string workerIDToRemove = e.CommandArgument.ToString();

            // Delete the record
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("WorkerID"));
            dt.Columns.Add(new DataColumn("WorkerInfo"));
            dt.Columns.Add(new DataColumn("PPHours"));
            dt.Columns.Add(new DataColumn("PUPTravelHours"));
            dt.Columns.Add(new DataColumn("PUPMaterialHours"));
            dt.Columns.Add(new DataColumn("PUPWeatherHours"));
            dt.Columns.Add(new DataColumn("PUPIndirectHours"));
            dt.Columns.Add(new DataColumn("PUPOtherHours"));
            dt.Columns.Add(new DataColumn("NPReworkHours"));
            dt.Columns.Add(new DataColumn("NPOtherHours"));

            // First, we get the existing rows in the gridview and add them to the datatable
            // Create a new datatable same as previous but without the removed worker.
            foreach (GridViewRow gvRow in GridViewWorkerList.Rows)
            {
                if (gvRow.Cells[1].Text != workerIDToRemove)
                {
                    DataRow dr = dt.NewRow();
                    dr["WorkerID"] = gvRow.Cells[1].Text;
                    dr["WorkerInfo"] = gvRow.Cells[2].Text;
                    dr["PPHours"] = ((TextBox)gvRow.FindControl("TBProductive")).Text;
                    dr["PUPTravelHours"] = ((TextBox)gvRow.FindControl("TBTravel")).Text;
                    dr["PUPMaterialHours"] = ((TextBox)gvRow.FindControl("TBMaterial")).Text;
                    dr["PUPWeatherHours"] = ((TextBox)gvRow.FindControl("TBWeather")).Text;
                    dr["PUPIndirectHours"] = ((TextBox)gvRow.FindControl("TBIndirect")).Text;
                    dr["PUPOtherHours"] = ((TextBox)gvRow.FindControl("TBOthersPayable")).Text;
                    dr["NPReworkHours"] = ((TextBox)gvRow.FindControl("TBRework")).Text;
                    dr["NPOtherHours"] = ((TextBox)gvRow.FindControl("TBOthersNonPayable")).Text;
                    dt.Rows.Add(dr);
                }
            }


            //Refresh gridview with new data
            GridViewWorkerList.DataSource = dt;
            GridViewWorkerList.DataBind();


            // We have to rebind the values of hours for each worker
            foreach (GridViewRow gvRow in GridViewWorkerList.Rows)
            {
                foreach (DataRow dtRow in dt.Rows)
                {
                    // we are on the same worker
                    if (gvRow.Cells[1].Text == dtRow["WorkerID"].ToString())
                    {
                        ((TextBox)gvRow.FindControl("TBProductive")).Text = dtRow["PPHours"].ToString();
                        ((TextBox)gvRow.FindControl("TBTravel")).Text = dtRow["PUPTravelHours"].ToString();
                        ((TextBox)gvRow.FindControl("TBMaterial")).Text = dtRow["PUPMaterialHours"].ToString();
                        ((TextBox)gvRow.FindControl("TBWeather")).Text = dtRow["PUPWeatherHours"].ToString();
                        ((TextBox)gvRow.FindControl("TBIndirect")).Text = dtRow["PUPIndirectHours"].ToString();
                        ((TextBox)gvRow.FindControl("TBOthersPayable")).Text = dtRow["PUPOtherHours"].ToString();
                        ((TextBox)gvRow.FindControl("TBRework")).Text = dtRow["NPReworkHours"].ToString();
                        ((TextBox)gvRow.FindControl("TBOthersNonPayable")).Text = dtRow["NPOtherHours"].ToString();
                    }
                }
            }
        }

        if (RadioButtonListSuspension.SelectedValue == "Yes")
        {
            this.PutGridviewInSuspensionMode(true);
        }
        else
        {
            this.PutGridviewInSuspensionMode(false);
        }
    }


    /// <summary>
    /// Has to exist otherwise error compilation.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewWorkerList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Leave blank.
    }


    /// <summary>
    /// Init the screen with the values of the TSNumber passed in parameter.
    /// </summary>
    /// <param name="TSNumber"></param>
    protected void initScreen(string TSNumber)
    {
        // variables
        string TSDate = "";
        string WSNumber = "";
        string WPNumber = "";
        string CWPNumber = "";
        string VariationNumber = "";
        string VariationType = "";
        string Other = "";
        string MainActivities = "";
        string Suspension = "";
        string SuspensionTime = "";
        string NightShift = "";
        string DayShift = "";

        //Get TSHeader data from database
        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        thisConnection.Open();
        SqlCommand cmd = thisConnection.CreateCommand();
        cmd.CommandText = "SELECT [TimesheetDate], " + 
                                 "TSH.[WSNumber], " +
                                 "TSH.[WPNumber], " +
                                 "TSH.[CWPNumber], " +
                                 "TSH.[VariationNumber], " +
                                 "[Other], " +
                                 "[MainActivities], " +
                                 "[Suspension], " +
                                 "REPLACE(RTRIM(REPLACE(REPLACE(RTRIM(REPLACE(SuspensionTime, '0', ' ')), ' ', '0'), '.', ' ')), ' ', '.') AS SuspensionTime, " +
                                 "[NightShift], " +
                                 "[DayShift], " +
                                 "[VariationType] " +
                            "FROM [allianceTimesheets].[dbo].[TimesheetHeader] TSH " +
                            "LEFT OUTER JOIN [allianceTimesheets].[dbo].[Variation] on Variation.VariationNumber = TSH.VariationNumber " +
                            "WHERE [TimesheetNumber]='" + TSNumber + "'";

        SqlDataReader reader = cmd.ExecuteReader();

        // Assign TSHeader data to local variables
        while (reader.Read())
        {
            TSDate = String.Format("{0:dd-MMM-yy}", (DateTime)reader["TimesheetDate"]);
            WSNumber = reader["WSNumber"].ToString();
            WPNumber = reader["WPNumber"].ToString();
            CWPNumber = reader["CWPNumber"].ToString();
            VariationNumber = reader["VariationNumber"].ToString();
            Other = reader["Other"].ToString();
            MainActivities = reader["MainActivities"].ToString();
            Suspension = reader["Suspension"].ToString();
            SuspensionTime = reader["SuspensionTime"].ToString();
            NightShift = reader["NightShift"].ToString();
            DayShift = reader["DayShift"].ToString();
            VariationType = reader["VariationType"].ToString();
        }

        reader.Close();

        //Bind header section of the sreen with data retrieved
        TextBoxTSDate.Text = TSDate;

        if (VariationNumber == "" && Other == "")
        {
            RadioButtonListScope.SelectedValue = "Base Scope";
            ComboBoxWS.SelectedValue = WSNumber;
            this.bindWPComboBox(WSNumber);
            ComboBoxWP.SelectedValue = WPNumber;
            ComboBoxWP.Enabled = true;
            this.bindCWPComboBox(WPNumber);
            ComboBoxCWP.SelectedValue = CWPNumber;
            ComboBoxCWP.Enabled = true;
        }
        else
        {
            ComboBoxWS.Enabled = false;
            RadioButtonListScope.SelectedValue = "Variation";
            ComboBoxVariationType.Enabled = true;
            if (VariationNumber == "")
	        {
		        TextBoxVariationOthers.Text = Other;
                TextBoxVariationOthers.Enabled = true;
                
	        }
            else
	        {
                this.bindVariationComboBox(VariationType);
                ComboBoxVariationType.SelectedValue = VariationType;
                ComboBoxVariation.SelectedValue = VariationNumber;
                ComboBoxVariation.Enabled = true;
	        }
        }

        TextBoxMainActivities.Text = MainActivities;

        if (DayShift == "True")
        {
            RadioButtonListShifts.SelectedValue = "Day Shift";
        }
        else
        {
            RadioButtonListShifts.SelectedValue = "Night Shift";
        }

        if (Suspension == "True")
        {
            RadioButtonListSuspension.SelectedValue = "Yes";
            TextBoxSuspensionDuration.Visible = true;
            TextBoxSuspensionDuration.Text = SuspensionTime;
            LabelSuspensionDuration.Visible = true;
            TextBoxTimesheetDuration.Visible = true;
            LabelTimesheetDuration.Visible = true;
        }


        //Get TSDetail data from database
        cmd.CommandText = "SELECT TS.[WorkerId]" + 
	                      ",W.[LastName]" + 
	                      ",W.[FirstName]" + 
	                      ",W.[BadgeNumber]" +
                          ", REPLACE(RTRIM(REPLACE(REPLACE(RTRIM(REPLACE(TS.PPHours, '0', ' ')), ' ', '0'), '.', ' ')), ' ', '.') AS PPHours" +
                          ", REPLACE(RTRIM(REPLACE(REPLACE(RTRIM(REPLACE(TS.PUPTravelHours, '0', ' ')), ' ', '0'), '.', ' ')), ' ', '.') AS PUPTravelHours" +
                          ", REPLACE(RTRIM(REPLACE(REPLACE(RTRIM(REPLACE(TS.PUPMaterialHours, '0', ' ')), ' ', '0'), '.', ' ')), ' ', '.') AS PUPMaterialHours" +
                          ", REPLACE(RTRIM(REPLACE(REPLACE(RTRIM(REPLACE(TS.PUPWeatherHours, '0', ' ')), ' ', '0'), '.', ' ')), ' ', '.') AS PUPWeatherHours" +
                          ", REPLACE(RTRIM(REPLACE(REPLACE(RTRIM(REPLACE(TS.PUPIndirectHours, '0', ' ')), ' ', '0'), '.', ' ')), ' ', '.') AS PUPIndirectHours" +
                          ", REPLACE(RTRIM(REPLACE(REPLACE(RTRIM(REPLACE(TS.PUPOtherHours, '0', ' ')), ' ', '0'), '.', ' ')), ' ', '.') AS PUPOtherHours" +
                          ", REPLACE(RTRIM(REPLACE(REPLACE(RTRIM(REPLACE(TS.NPReworkHours, '0', ' ')), ' ', '0'), '.', ' ')), ' ', '.') AS NPReworkHours" +
                          ", REPLACE(RTRIM(REPLACE(REPLACE(RTRIM(REPLACE(TS.NPOtherHours, '0', ' ')), ' ', '0'), '.', ' ')), ' ', '.') AS NPOtherHours" + 	
                     " FROM [allianceTimesheets].[dbo].[TimesheetDetail] TS " +
                     " Inner join [allianceTimesheets].[dbo].[Worker] W "+ 
                     " on TS.WorkerId = W.WorkerId " + 
                     " WHERE [TimesheetNumber] = '" + TSNumber + "'" ;

        reader = cmd.ExecuteReader();

        //Construction of the datatable
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("WorkerID"));
        dt.Columns.Add(new DataColumn("WorkerInfo"));
        dt.Columns.Add(new DataColumn("PPHours"));
        dt.Columns.Add(new DataColumn("PUPTravelHours"));
        dt.Columns.Add(new DataColumn("PUPMaterialHours"));
        dt.Columns.Add(new DataColumn("PUPWeatherHours"));
        dt.Columns.Add(new DataColumn("PUPIndirectHours"));
        dt.Columns.Add(new DataColumn("PUPOtherHours"));
        dt.Columns.Add(new DataColumn("NPReworkHours"));
        dt.Columns.Add(new DataColumn("NPOtherHours"));

        while (reader.Read())
        {
            DataRow dr = dt.NewRow();
            dr["WorkerID"]          = reader["WorkerId"].ToString();
            dr["WorkerInfo"]        = reader["LastName"].ToString() + " " + 
                                      reader["FirstName"].ToString() + " " + 
                                      reader["BadgeNumber"].ToString();
            dr["PPHours"]           = reader["PPHours"].ToString();
            dr["PUPTravelHours"]    = reader["PUPTravelHours"].ToString();
            dr["PUPMaterialHours"]  = reader["PUPMaterialHours"].ToString();
            dr["PUPWeatherHours"]   = reader["PUPWeatherHours"].ToString();
            dr["PUPIndirectHours"]  = reader["PUPIndirectHours"].ToString();
            dr["PUPOtherHours"]     = reader["PUPOtherHours"].ToString();
            dr["NPReworkHours"]     = reader["NPReworkHours"].ToString();
            dr["NPOtherHours"]      = reader["NPOtherHours"].ToString();
            dt.Rows.Add(dr);
        }

        reader.Close();
        thisConnection.Close();

        //Assign Datatable to gridview 
        GridViewWorkerList.DataSource = dt;
        GridViewWorkerList.DataBind();

        // We can not bind the Hours of workers to the gridview because it's templateFields
        // So we parse the gridview created and add retrieved hours in the correct fields
        foreach (GridViewRow gvRow in GridViewWorkerList.Rows)
        {
            foreach (DataRow dtRow in dt.Rows)
            {
                // we are on the same worker
                if (gvRow.Cells[1].Text == dtRow["WorkerID"].ToString())
                {
                    ((TextBox)gvRow.FindControl("TBProductive")).Text       = dtRow["PPHours"].ToString();
                    ((TextBox)gvRow.FindControl("TBTravel")).Text           = dtRow["PUPTravelHours"].ToString();
                    ((TextBox)gvRow.FindControl("TBMaterial")).Text         = dtRow["PUPMaterialHours"].ToString();
                    ((TextBox)gvRow.FindControl("TBWeather")).Text          = dtRow["PUPWeatherHours"].ToString();
                    ((TextBox)gvRow.FindControl("TBIndirect")).Text         = dtRow["PUPIndirectHours"].ToString();
                    ((TextBox)gvRow.FindControl("TBOthersPayable")).Text    = dtRow["PUPOtherHours"].ToString();
                    ((TextBox)gvRow.FindControl("TBRework")).Text           = dtRow["NPReworkHours"].ToString();
                    ((TextBox)gvRow.FindControl("TBOthersNonPayable")).Text = dtRow["NPOtherHours"].ToString();
                }
            }
        }


        ButtonSubmitTS.Visible = true;

        //We show "save as draft button" only for new submission or for edition of draft
        if (Request.Params["Draft"] != null)
        {
            this.ButtonSaveAsDraft.Visible = true;
        }
    }


    /// <summary>
    /// Activate or unactivate columns in the gridview of workers.
    /// Applies when the user select suspension
    /// </summary>
    /// <param name="Active">if Active = True, the gridview will be in the suspsension mode</param>
    protected void PutGridviewInSuspensionMode(bool Active)
    {
        if (Active)
        {
            foreach (GridViewRow row in GridViewWorkerList.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    TableCell cell = row.Cells[i];
                    if (i == 3 || i == 4 || i == 5 || i == 7 || i == 8 || i == 9 || i == 10)
                    {
                        cell.Enabled = false;
                        //cell.Text = "";
                        ((TextBox)cell.Controls[1]).BackColor = System.Drawing.Color.Gray;
                        ((TextBox)cell.Controls[1]).Text = "";
                    }
                }
            }
        }
        else
        {
            foreach (GridViewRow row in GridViewWorkerList.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    TableCell cell = row.Cells[i];
                    if (i == 3 || i == 4 || i == 5 || i == 7 || i == 8 || i == 9 || i == 10)
                    {
                        cell.Enabled = true;
                        ((TextBox)cell.Controls[1]).BackColor = System.Drawing.Color.White;
                    }
                }
            }
        }

    }



    /// <summary>
    /// Function to create a timesheet in the database.
    /// </summary>
    /// <param name="draft">True if the TS is sumbitted as draft</param>
    protected void InsertTimesheet(bool draft)
    {
        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();

        try
        {
            //TODO cas du Variation Other pas référencé
            thisConnection.Open();

            //Call the stored proc
            cmd.CommandText = "EXEC [allianceTimesheets].[dbo].[TimesheetInsert] " +
                              "@TSDate, @ContractNumber,@WSNumber, @WPNumber, @CWPNumber, " +
                              "@VariationNumber, @Other, @MainActivities, @Suspension, @SuspensionTime, " +
                              "@DayShift, @NightShift, @SubmittedBy, @WorkersHours, @TSNumber, @SundayDerogation, @Adjustment, @Submitted";


            // We Put the data into the parameters
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@TSDate";
            param1.Value = TextBoxTSDate.Text;

            SqlParameter param2 = new SqlParameter();
            param2.ParameterName = "@ContractNumber";
            param2.Value = HttpContext.Current.Session["currentContract"];

            SqlParameter param3 = new SqlParameter();
            param3.ParameterName = "@WSNumber";
            if (ComboBoxWS.SelectedValue != "select")
                param3.Value = ComboBoxWS.SelectedValue;
            else
                param3.Value = DBNull.Value;

            SqlParameter param4 = new SqlParameter();
            param4.ParameterName = "@WPNumber";
            if (ComboBoxWP.SelectedValue != "select")
                param4.Value = ComboBoxWP.SelectedValue;
            else
                param4.Value = DBNull.Value;

            SqlParameter param5 = new SqlParameter();
            param5.ParameterName = "@CWPNumber";
            if (ComboBoxCWP.SelectedValue != "select")
                param5.Value = ComboBoxCWP.SelectedValue;
            else
                param5.Value = DBNull.Value;

            SqlParameter param6 = new SqlParameter();
            param6.ParameterName = "@VariationNumber";
            if (RadioButtonListScope.SelectedValue == "Base Scope" ||
                ComboBoxVariationType.SelectedValue == "Other")
            {
                param6.Value = DBNull.Value;
            }
            else
            {
                param6.Value = ComboBoxVariation.SelectedValue;
            }

            SqlParameter param7 = new SqlParameter();
            param7.ParameterName = "@Other";
            if (TextBoxVariationOthers.Text != "")
                param7.Value = TextBoxVariationOthers.Text;
            else
                param7.Value = DBNull.Value;

            SqlParameter param8 = new SqlParameter();
            param8.ParameterName = "@MainActivities";
            param8.Value = TextBoxMainActivities.Text.Replace("'", "''");

            SqlParameter param9 = new SqlParameter();
            param9.ParameterName = "@Suspension";

            SqlParameter param10 = new SqlParameter();
            param10.ParameterName = "@SuspensionTime";

            if (RadioButtonListSuspension.SelectedValue == "Yes")
            {
                param9.Value = "1";
                param10.Value = TextBoxSuspensionDuration.Text;
            }
            else
            {
                param9.Value = "0";
                param10.Value = DBNull.Value;
            }


            SqlParameter param11 = new SqlParameter();
            param11.ParameterName = "@DayShift";

            SqlParameter param12 = new SqlParameter();
            param12.ParameterName = "@NightShift";

            if (RadioButtonListShifts.SelectedValue == "Day Shift")
            {
                param11.Value = "1";
                param12.Value = "0";
            }
            else
            {
                param11.Value = "0";
                param12.Value = "1";
            }

            SqlParameter param13 = new SqlParameter();
            param13.ParameterName = "@SubmittedBy";
            param13.Value = User.Identity.Name;

            SqlParameter param14 = new SqlParameter();
            param14.ParameterName = "@WorkersHours";
            param14.Value = this.getInsertWorkersHoursParam();

            SqlParameter param15 = new SqlParameter();
            param15.ParameterName = "@TSNumber";
            if (Request.Params["TSNumber"] != null)
            {
                param15.Value = Request.Params["TSNumber"];
            }
            else
            {
                param15.Value = DBNull.Value;
            }

            SqlParameter param16 = new SqlParameter();
            param16.ParameterName = "@SundayDerogation";
            if (RadioButtonListSundayDerogation.SelectedValue == "Yes")
            {
                param16.Value = "1";
            }
            else
            {
                param16.Value = "0";
            }

            SqlParameter param17 = new SqlParameter();
            param17.ParameterName = "@Adjustment";
            if (Request.Params["Adjustment"] != null)
            {
                param17.Value = "1";
            }
            else
            {
                param17.Value = "0";
            }

            SqlParameter param18 = new SqlParameter();
            param18.ParameterName = "@Submitted";
            if (draft)
            {
                param18.Value = "0";
            }
            else
            {
                param18.Value = "1";
            }

            //Add the param to the query
            cmd.Parameters.Add(param1);
            cmd.Parameters.Add(param2);
            cmd.Parameters.Add(param3);
            cmd.Parameters.Add(param4);
            cmd.Parameters.Add(param5);
            cmd.Parameters.Add(param6);
            cmd.Parameters.Add(param7);
            cmd.Parameters.Add(param8);
            cmd.Parameters.Add(param9);
            cmd.Parameters.Add(param10);
            cmd.Parameters.Add(param11);
            cmd.Parameters.Add(param12);
            cmd.Parameters.Add(param13);
            cmd.Parameters.Add(param14);
            cmd.Parameters.Add(param15);
            cmd.Parameters.Add(param16);
            cmd.Parameters.Add(param17);
            cmd.Parameters.Add(param18);

            //cmd.ExecuteNonQuery();
            string TSNumberCreated = (string)cmd.ExecuteScalar();

            //Reset of the fields
            this.ResetAllFields();

            //User redirection
            if (Request.Params["Adjustment"] != null)
            {
                Response.Redirect("./TimesheetSubmit.aspx?TSNumber=" + Request.Params["TSNumber"] + "&Adjustment=1&status=AdjustmentSuccess&TSCreated=" + TSNumberCreated);
            }
            else
            {
                if (draft)
                {
                    Response.Redirect("./TimesheetSubmit.aspx?status=SaveSuccess&TSCreated=" + TSNumberCreated);
                }
                else
                {
                    Response.Redirect("./TimesheetSubmit.aspx?status=SubmissionSuccess&TSCreated=" + TSNumberCreated);
                }
                
            }
            
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


    /// <summary>
    /// When user click on "view" link in the corrective TS gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewCorrectiveTS_SelectedIndexChanged(object sender, EventArgs e)
    {
        String TSNumber = GridViewCorrectiveTS.SelectedRow.Cells[1].Text;
        HttpContext.Current.Response.Redirect("./ViewTS.aspx?TSNumber=" + TSNumber);
    }


    /// <summary>
    /// When user click on the "finish adjustment" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonFinishAdjustment_Click(object sender, EventArgs e)
    {
        //We must put the TS in "PendingAdjustmentValidation" status

        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();

        try
        {
            thisConnection.Open();
            cmd.CommandText = "UPDATE [allianceTimesheets].[dbo].[TimesheetHeader] SET " +
                              "PendingAdjustmentValidation = 1, " +
                              "PendingAdjustment = 0 " +
                              "WHERE TimesheetNumber = '" + Request.Params["TSNumber"] + "'";

            cmd.ExecuteNonQuery();

            //Redirect user to the pending adjustment page
            Response.Redirect("./TSToAdjust.aspx?status=AdjustmentFinishedOK");
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
