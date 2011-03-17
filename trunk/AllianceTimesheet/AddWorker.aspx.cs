using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class AddWorker : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.bindSubcontractorComboBox();
            this.bindTradeComboBox();
            this.bindPositionCategoryComboBox();
        } 

    }


    /// <summary>
    /// When user click the add worker button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonAddWorker_Click(object sender, EventArgs e)
    {
        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();

        if (this.InputFieldsAreOK())
        {
            try
            {
                thisConnection.Open();

                //Get the name of the company which owns the contract
                cmd.CommandText = "select companyname from [allianceTimesheets].[dbo].[Contract] where contractNumber = " +
                                  "'" + HttpContext.Current.Session["currentContract"] + "'";

                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                string companyName = reader.GetString(0);
                reader.Close();
                
                //Ad the Worker in database WORKER table
                cmd.CommandText = "INSERT INTO [allianceTimesheets].[dbo].[Worker] (" +
                                  "[LastName]," +
                                  "[FirstName]," +
                                  "[Nationality]," +
                                  "[ArrivalDate]," +
                                  "[InductionDone]," +
                                  "[BadgeNumber]," +
                                  "[VCCNumber]," +
                                  "[SubcontractorId]," +
                                  "[Status]," +
                                  "[ConventionHours]," +
                                  "[CreatedDate], " +
                                  "[CompanyName]) " +
                                  " VALUES (@LastName, @FirstName, @Nationality, @ArrivalDate, @InductionDone, " +
                                  " @BadgeNumber, @VCCNumber, @Subcontractor, @Status, @ConventionHours, " +
                                  " GETDATE(), @CompanyName)";

                // We Put the data into the parameters
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@LastName";
                param1.Value = TextBoxLastName.Text;

                SqlParameter param2 = new SqlParameter();
                param2.ParameterName = "@FirstName";
                param2.Value = TextBoxFirstName.Text;

                SqlParameter param3 = new SqlParameter();
                param3.ParameterName = "@Nationality";
                param3.Value = ComboBoxCountry.SelectedValue;

                SqlParameter param4 = new SqlParameter();
                param4.ParameterName = "@ArrivalDate";
                param4.Value = TextBoxArrivalDate.Text;
                    
                SqlParameter param5 = new SqlParameter();
                param5.ParameterName = "@InductionDone";
                param5.Value = CheckBoxInduction.Checked;

                SqlParameter param6 = new SqlParameter();
                param6.ParameterName = "@BadgeNumber";
                param6.Value = TextBoxBadgeNumber.Text;

                SqlParameter param7 = new SqlParameter();
                param7.ParameterName = "@VCCNumber";
                if (TextBoxVCCNumber.Text != "")
                    param7.Value = TextBoxVCCNumber.Text;
                else
                    param7.Value = DBNull.Value;

                SqlParameter param8 = new SqlParameter();
                param8.ParameterName = "@Subcontractor";
                param8.Value = ComboBoxSubcontractor.SelectedValue;

                SqlParameter param9 = new SqlParameter();
                param9.ParameterName = "@Status";
                param9.Value = ComboBoxStatus.SelectedValue;

                SqlParameter param10 = new SqlParameter();
                param10.ParameterName = "@ConventionHours";
                if (ComboBoxConvention.SelectedValue != "select")
                    param10.Value = ComboBoxConvention.SelectedValue;
                else
                    param10.Value = DBNull.Value;

                SqlParameter param11 = new SqlParameter();
                param11.ParameterName = "@CompanyName";
                param11.Value = companyName;


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

                cmd.ExecuteNonQuery();


                //We get the workerId created
                cmd.CommandText = "SELECT MAX(WorkerId) FROM [allianceTimesheets].[dbo].[Worker] ";
                reader = cmd.ExecuteReader();
                reader.Read();
                int workerId = reader.GetInt32(0);
                reader.Close();


                //Ad the Worker on the current contract in database (WORKERCONTRACT table)
                cmd.CommandText = "INSERT INTO [allianceTimesheets].[dbo].[WorkerContract] (" +
                                  "[WorkerId]," +
                                  "[Trade]," +
                                  "[CreatedDate]," +
                                  "[PositionCategory]," +
                                  "[Active]," +
                                  "[ContractNumber]) " +
                                  " VALUES (" +
                                  workerId + ", " +
                                  "'" + ComboBoxTrade.SelectedValue + "'," +
                                  "GETDATE()," +
                                  "'" + ComboBoxPositionCategory.SelectedValue + "'," +
                                  "1," +
                                  "'" + HttpContext.Current.Session["currentContract"] + "')";
                cmd.ExecuteNonQuery();

                // Insert in ContractProfileHistory Table
                Utils.TrackModification("Addition", "Worker", "New worker created : " + TextBoxLastName.Text +
                                        " " + TextBoxFirstName.Text + " " + TextBoxBadgeNumber.Text);

                this.ResetAllFields();

                //User success notification
                Utils.NotifyUser("success", "Worker successfully added!", Page);
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

        if (this.TextBoxLastName.Text == "")
        {
            Utils.NotifyUser("warning", "Please enter a Last name", Page);
            return false;
        }

        if (this.TextBoxFirstName.Text == "")
        {
            Utils.NotifyUser("warning", "Please enter a first name", Page);
            return false;
        }

        if (this.ComboBoxCountry.SelectedValue == "select")
        {
            Utils.NotifyUser("warning", "Please select a nationality", Page);
            return false;
        }

        if (this.ComboBoxTrade.SelectedValue == "select")
        {
            Utils.NotifyUser("warning", "Please select a trade", Page);
            return false;
        }

        if (this.ComboBoxPositionCategory.SelectedValue == "select")
        {
            Utils.NotifyUser("warning", "Please select a position category", Page);
            return false;
        }

        if (this.ComboBoxSubcontractor.SelectedValue == "select")
        {
            Utils.NotifyUser("warning", "Please select a subcontractor", Page);
            return false;
        }


        if (this.TextBoxArrivalDate.Text == "")
        {
            Utils.NotifyUser("warning", "Please enter an arrival date", Page);
            return false;
        }

        if (this.TextBoxBadgeNumber.Text == "")
        {
            Utils.NotifyUser("warning", "Please enter a badge number", Page);
            return false;
        }

        if (this.ComboBoxStatus.SelectedValue == "select")
        {
            Utils.NotifyUser("warning", "Please select a status", Page);
            return false;
        }

        if (this.ComboBoxStatus.SelectedValue == "L")
        {
            if (this.ComboBoxConvention.SelectedValue == "select")
            {
                Utils.NotifyUser("warning", "Please select a convention", Page);
                return false;
            }
            
        }
        return true;
    }

    /// <summary>
    /// If the status "Local" is selected, we display the textbox "Convention Hours"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ComboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ComboBoxStatus.SelectedValue == "L")
        {
            this.LableConventionHours.Visible = true;
            this.ComboBoxConvention.Visible = true;
        }
        else
        {
            this.LableConventionHours.Visible = false;
            this.ComboBoxConvention.Visible = false;
        }
    }


    #region bind of combobox

    protected void bindSubcontractorComboBox()
    {
        DataSetGlobal.SubcontractorDataTable subcontractordatatable = new DataSetGlobal.SubcontractorDataTable();
        DataSetGlobalTableAdapters.SubcontractorTableAdapter subcontractortableadadpter = new DataSetGlobalTableAdapters.SubcontractorTableAdapter();
        subcontractortableadadpter.Fill(subcontractordatatable, (String)HttpContext.Current.Session["currentContract"]);

        this.ComboBoxSubcontractor.DataSource = subcontractordatatable;
        subcontractordatatable.AddSubcontractorRow("select", "select", true,0);
        this.ComboBoxSubcontractor.DataTextField = subcontractordatatable.SubcontractorNameColumn.ToString();
        this.ComboBoxSubcontractor.DataValueField = subcontractordatatable.SubcontractorIdColumn.ToString();
        this.ComboBoxSubcontractor.SelectedValue = "0";
        this.ComboBoxSubcontractor.DataBind();
    }


    protected void bindTradeComboBox()
    {
        DataSetGlobal.TradeDataTable dt = new DataSetGlobal.TradeDataTable();
        DataSetGlobalTableAdapters.TradeTableAdapter ta = new DataSetGlobalTableAdapters.TradeTableAdapter();
        ta.Fill(dt, (String)HttpContext.Current.Session["currentContract"]);

        this.ComboBoxTrade.DataSource = dt;
        dt.AddTradeRow("select", "select");
        this.ComboBoxTrade.DataTextField = dt.TradeColumn.ToString();
        this.ComboBoxTrade.DataValueField = dt.TradeColumn.ToString();
        this.ComboBoxTrade.SelectedValue = "select";
        this.ComboBoxTrade.DataBind();
    }


    protected void bindPositionCategoryComboBox()
    {
        DataSetGlobal.PositionCategoryDataTable dt = new DataSetGlobal.PositionCategoryDataTable();
        DataSetGlobalTableAdapters.PositionCategoryTableAdapter ta = new DataSetGlobalTableAdapters.PositionCategoryTableAdapter();
        ta.Fill(dt, (String)HttpContext.Current.Session["currentContract"]);

        this.ComboBoxPositionCategory.DataSource = dt;
        dt.AddPositionCategoryRow("select", "select");
        this.ComboBoxPositionCategory.DataTextField = dt.PositionCategoryColumn.ToString();
        this.ComboBoxPositionCategory.DataValueField = dt.PositionCategoryColumn.ToString();
        this.ComboBoxPositionCategory.SelectedValue = "select";
        this.ComboBoxPositionCategory.DataBind();
    }

    #endregion

    protected void ResetAllFields()
    {
        TextBoxLastName.Text = "";
        TextBoxFirstName.Text = "";
        ComboBoxCountry.SelectedValue = "select";
        ComboBoxTrade.SelectedValue = "select";
        ComboBoxTrade.SelectedValue = "select";
        ComboBoxPositionCategory.SelectedValue = "select";
        ComboBoxStatus.SelectedValue = "select";
        ComboBoxConvention.SelectedValue = "select";
        ComboBoxSubcontractor.SelectedValue = "0";
        TextBoxArrivalDate.Text = "";
        TextBoxBadgeNumber.Text = "";
        TextBoxVCCNumber.Text = "";
        CheckBoxInduction.Checked = false;
    }
}