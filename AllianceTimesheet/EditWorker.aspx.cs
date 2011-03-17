using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class EditWorker : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.bindTradeComboBox();
            this.bindPositionCategoryComboBox();
            TextBoxBadgeNumber.Text = ((Worker)HttpContext.Current.Session["currentWorker"]).BadgeNumber.ToString();
            CheckBoxActive.Checked = ((Worker)HttpContext.Current.Session["currentWorker"]).Active;
        }
        if (GridView1.Rows.Count == 0)
        {
            LabelNoRecordFound.Visible = true;
        }
    }


    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();
        Worker w = (Worker)HttpContext.Current.Session["currentWorker"];

        if (InputFieldsAreOK() && (workerHasBeenModified() || workerContractHasBeenModified()))
        {
            try
            {
                thisConnection.Open();

                if (workerHasBeenModified())
                {
                    //Update the Worker in database WORKERCONTRACT table
                    cmd.CommandText = " UPDATE [allianceTimesheets].[dbo].[Worker] " +
                                      " SET [BadgeNumber] = " + TextBoxBadgeNumber.Text +
                                      " WHERE [WorkerId] = " + w.ID ;

                    cmd.ExecuteNonQuery();

                    //Update of the object in session
                    w.ID = Convert.ToInt32(TextBoxBadgeNumber.Text);
                    HttpContext.Current.Session.Add("currentWorker",w);
                }

                if (workerContractHasBeenModified())
                {
                    //Update the Worker in database WORKERCONTRACT table
                    cmd.CommandText = " UPDATE [allianceTimesheets].[dbo].[WorkerContract] " +
                                      " SET [Trade] = '" + ComboBoxTrade.SelectedValue + "'" +
                                      "    ,[PositionCategory] = '" + ComboBoxPositionCategory.SelectedValue + "'" +
                                      "    ,[Active] = '" + CheckBoxActive.Checked + "'" +
                                      "  WHERE [WorkerId] = " + w.ID +
                                      "  AND [ContractNumber] =  '" + HttpContext.Current.Session["currentContract"] + "'";

                    cmd.ExecuteNonQuery();
                    

                    // Insert in ContractProfileHistory Table:
                    string tracking = w.LastName + " " + w.FirstName + " : ";

                    if (w.Trade != ComboBoxTrade.SelectedValue)
                    {
                        tracking = tracking + "Trade modified from " + w.Trade + " to " + ComboBoxTrade.SelectedValue + ". " ;
                    }
                    
                    if ( w.PositionCategory != ComboBoxPositionCategory.SelectedValue)
                    {
                        tracking = tracking + "Position category modified from " + w.PositionCategory + " to " + ComboBoxPositionCategory.SelectedValue + ". ";
                    }
                    if (w.Active != CheckBoxActive.Checked)
                    {
                        if (CheckBoxActive.Checked)
                        {
                            tracking = tracking + "Changed from Unactive to Active. ";
                        }
                        else
                        {
                            tracking = tracking + "Changed from Active to Unactive. ";
                        }
                        
                    }

                    Utils.TrackModification("Edition", "Worker", tracking);
                    

                    //Update of the object in session
                    w.Trade = ComboBoxTrade.SelectedValue;
                    w.PositionCategory = ComboBoxPositionCategory.SelectedValue;
                    w.Active = CheckBoxActive.Checked;
                    HttpContext.Current.Session.Add("currentWorker", w);

                    //The insertion of modification in the WorkerContractHistory table 
                    // is made by a trigger in the DB.
                }

                //User success notification
                Utils.NotifyUser("success", "Worker successfully updated!", Page);

                //refresh history gridview
                GridView1.DataBind();

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


    protected void bindTradeComboBox()
    {
        DataSetGlobal.TradeDataTable dt = new DataSetGlobal.TradeDataTable();
        DataSetGlobalTableAdapters.TradeTableAdapter ta = new DataSetGlobalTableAdapters.TradeTableAdapter();
        ta.Fill(dt, (String)HttpContext.Current.Session["currentContract"]);

        this.ComboBoxTrade.DataSource = dt;
        this.ComboBoxTrade.DataTextField = dt.TradeColumn.ToString();
        this.ComboBoxTrade.DataValueField = dt.TradeColumn.ToString();
        this.ComboBoxTrade.SelectedValue = ((Worker)HttpContext.Current.Session["currentWorker"]).Trade;
        this.ComboBoxTrade.DataBind();
    }


    protected void bindPositionCategoryComboBox()
    {
        DataSetGlobal.PositionCategoryDataTable dt = new DataSetGlobal.PositionCategoryDataTable();
        DataSetGlobalTableAdapters.PositionCategoryTableAdapter ta = new DataSetGlobalTableAdapters.PositionCategoryTableAdapter();
        ta.Fill(dt, (String)HttpContext.Current.Session["currentContract"]);

        this.ComboBoxPositionCategory.DataSource = dt;
        this.ComboBoxPositionCategory.DataTextField = dt.PositionCategoryColumn.ToString();
        this.ComboBoxPositionCategory.DataValueField = dt.PositionCategoryColumn.ToString();
        this.ComboBoxPositionCategory.SelectedValue = ((Worker)HttpContext.Current.Session["currentWorker"]).PositionCategory;
        this.ComboBoxPositionCategory.DataBind();
    }


    /// <summary>
    /// Checks if values entered by the user are correct
    /// </summary>
    /// <returns>true if values are correct, False otherwise</returns>
    protected Boolean InputFieldsAreOK()
    {
        if (this.TextBoxBadgeNumber.Text == "")
        {
            Utils.NotifyUser("warning", "Please enter a badge number", Page);
            return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if the user made modification on the worker (table Worker).
    /// </summary>
    /// <returns></returns>
    protected Boolean workerHasBeenModified()
    {
        Worker w = (Worker)HttpContext.Current.Session["currentWorker"];

        if (w.BadgeNumber.ToString() == TextBoxBadgeNumber.Text)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Checks if the user made modification on the worker (table Worker).
    /// </summary>
    /// <returns></returns>
    protected Boolean workerContractHasBeenModified()
    {

        Worker w = (Worker)HttpContext.Current.Session["currentWorker"];

        if (w.Trade == ComboBoxTrade.SelectedValue &&
            w.PositionCategory == ComboBoxPositionCategory.SelectedValue &&
            w.Active == CheckBoxActive.Checked)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Used to pass the workerId in session as a parameter to the datasource
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void SqlDataSourceWorkerHistory_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        Worker w = (Worker)HttpContext.Current.Session["currentWorker"];
        e.Command.Parameters["WorkerId"].Value = w.ID;           
    }
}