using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Data.SqlClient;
using System.Configuration;

public partial class Workers : System.Web.UI.Page
{
    DataSetGlobal.TradeDataTable dtTrade;
    DataSetGlobal.PositionCategoryDataTable dtPositionCategory;

    protected void Page_Load(object sender, EventArgs e)
    {

        //Get list of trades and position categories of the current contract
        dtTrade = new DataSetGlobal.TradeDataTable();
        DataSetGlobalTableAdapters.TradeTableAdapter ta = new DataSetGlobalTableAdapters.TradeTableAdapter();
        ta.Fill(dtTrade, (String)HttpContext.Current.Session["currentContract"]);
        dtTrade.AddTradeRow("select", "select");

        dtPositionCategory = new DataSetGlobal.PositionCategoryDataTable();
        DataSetGlobalTableAdapters.PositionCategoryTableAdapter ta2 = new DataSetGlobalTableAdapters.PositionCategoryTableAdapter();
        ta2.Fill(dtPositionCategory, (String)HttpContext.Current.Session["currentContract"]);
        dtPositionCategory.AddPositionCategoryRow("select", "select");

        if (this.GridView1.Rows.Count == 0)
        {
            this.LabelNoRecordFound.Visible = true;
        }

        if (GridViewAddWorkers.Rows.Count == 0)
        {
            LabelNoExistingWorker.Visible = true;
        }

        hideContractorCAItems();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        // We put the selected worker in session and redirect user to EditWorker page
        Worker w = new Worker();
        w.ID = Convert.ToInt32(GridView1.SelectedRow.Cells[1].Text);
        w.BadgeNumber = Convert.ToInt32(GridView1.SelectedRow.Cells[4].Text);
        w.LastName = GridView1.SelectedRow.Cells[2].Text;
        w.FirstName = GridView1.SelectedRow.Cells[3].Text;
        w.Trade = Server.HtmlDecode(GridView1.SelectedRow.Cells[7].Text);
        w.PositionCategory = Server.HtmlDecode(GridView1.SelectedRow.Cells[8].Text);
        w.Active = ((CheckBox)GridView1.SelectedRow.Cells[11].Controls[0]).Checked;

        HttpContext.Current.Session.Add("currentWorker", w);
        HttpContext.Current.Response.Redirect("./EditWorker.aspx");
    }


    protected void GridViewAddWorkers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType.ToString() != "Header" &&
            e.Row.RowType.ToString() != "Footer")
        {
            DropDownList cbTrade = ((DropDownList)e.Row.FindControl("DropDownListTrade"));
            cbTrade.DataSource = dtTrade;
            //dtTrade.AddTradeRow("select", "select");
            cbTrade.DataTextField = dtTrade.TradeColumn.ToString();
            cbTrade.DataValueField = dtTrade.TradeColumn.ToString();
            cbTrade.SelectedValue = "select";
            cbTrade.DataBind();

            DropDownList cbPositionCategory = ((DropDownList)e.Row.FindControl("DropDownListPositionCategory"));
            cbPositionCategory.DataSource = dtPositionCategory;
            //dtPositionCategory.AddPositionCategoryRow("select", "select");
            cbPositionCategory.DataTextField = dtPositionCategory.PositionCategoryColumn.ToString();
            cbPositionCategory.DataValueField = dtPositionCategory.PositionCategoryColumn.ToString();
            cbPositionCategory.SelectedValue = "select";
            cbPositionCategory.DataBind();
        }

    }



    /// <summary>
    /// Add workers of another contract
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonAddSelectedWorkers_Click(object sender, EventArgs e)
    {
        List<Worker> workerList = new List<Worker>();
        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();

        // Then, we get every workers checked and verify the values are correct
        foreach (GridViewRow row in GridViewAddWorkers.Rows)
        {
            CheckBox cb = (CheckBox)row.FindControl("CheckBoxWorkerSelect");
            if (cb.Checked)
            {
                if (((DropDownList)row.FindControl("DropDownListTrade")).SelectedValue == "select")
                {
                    Utils.NotifyUser("warning", "Please select a trade for each selected worker", Page);
                    return;
                }
                // the trade and position category must be provided
                if (((DropDownList)row.FindControl("DropDownListPositionCategory")).SelectedValue == "select")
                {
                    Utils.NotifyUser("warning", "Please select a position category for each selected worker", Page);
                    return ;
                }

                Worker w = new Worker();
                w.ID = Convert.ToInt32(row.Cells[1].Text);
                w.Trade = ((DropDownList)row.FindControl("DropDownListTrade")).SelectedValue;
                w.PositionCategory = ((DropDownList)row.FindControl("DropDownListPositionCategory")).SelectedValue;
                workerList.Add(w);
                
            }
        }

        try
        {
            if (workerList.Count != 0)
            {
                thisConnection.Open();
                //All workers selected are ok, we add them into db
                foreach (Worker w in workerList)
                {
                    //Ad the Worker on the current contract in database (WORKERCONTRACT table)
                    cmd.CommandText = "INSERT INTO [allianceTimesheets].[dbo].[WorkerContract] (" +
                                      "[WorkerId]," +
                                      "[Trade]," +
                                      "[CreatedDate]," +
                                      "[PositionCategory]," +
                                      "[Active]," +
                                      "[ContractNumber]) " +
                                      " VALUES (" +
                                      w.ID + ", " +
                                      "'" + w.Trade + "'," +
                                      "GETDATE()," +
                                      "'" + w.PositionCategory + "'," +
                                      "1," +
                                      "'" + HttpContext.Current.Session["currentContract"] + "')";
                    cmd.ExecuteNonQuery();
                }

                //Refresh gridview
                GridView1.DataBind();
                GridViewAddWorkers.DataBind();
                Utils.NotifyUser("success", "Workers successfully added!", Page);
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
    /// Contractor CA have a read only access. This method hide all elements that enable to make modifications.
    /// </summary>
    protected void hideContractorCAItems()
    {

        if (HttpContext.Current.Session["userIsOwnerCA"] == null
            || !(Boolean)HttpContext.Current.Session["userIsOwnerCA"])
        {
            LinkButtonAddWorkers.CssClass = "hiddencolumn";
            LabelCreateWorker.Visible = false;
            GridView1.Columns[0].Visible = false;
        }
    }
}