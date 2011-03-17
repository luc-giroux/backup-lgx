using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Configuration;

public partial class ViewTS : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {

        // If the user comes from the "timesheet to approve" page and if he has the propers rights,
        // we display him the "approve / reject " panel
        if (Request.Params["ToApprove"] != null)
        {
            if (Request.Params["ToApprove"] == "1")
            {
                if (HttpContext.Current.Session["userIsOwnerSupervisor"] != null
                && (Boolean)HttpContext.Current.Session["userIsOwnerSupervisor"])
                {
                    PanelApprove.Visible = true;
                }
            }

        }

        // If the user comes from the "timesheet rejected" page and if he has the propers rights,
        // we display him the "edit" button
        if (Request.Params["rejected"] != null)
        {
            if (Request.Params["rejected"] == "1")
            {
                if (HttpContext.Current.Session["userIsContractorSupervisor"] != null
                && (Boolean)HttpContext.Current.Session["userIsContractorSupervisor"])
                {
                    ButtonEdiTS.Visible = true;
                }
            }

        }


        // If the user comes from the "pending adjustment timesheet" page and if he has the propers rights,
        // we display him the "edit" button
        if (Request.Params["adjustment"] != null)
        {
            if (Request.Params["adjustment"] == "1")
            {
                if (HttpContext.Current.Session["userIsContractorSupervisor"] != null
                && (Boolean)HttpContext.Current.Session["userIsContractorSupervisor"])
                {
                    ButtonAdjustTS.Visible = true;
                }
            }
        }

        // If the user comes from the "timesheet to approve" page and if he has the propers rights,
        // we display the gridview of correctives TS
        if (Request.Params["PendingAdjustmentValidation"] != null)
        {
            /*if (Request.Params["PendingAdjustmentValidation"] == "1")
            {
                if (HttpContext.Current.Session["userIsOwnerSupervisor"] != null
                && (Boolean)HttpContext.Current.Session["userIsOwnerSupervisor"])
                {*/
                    PanelCorrectiveTS.Visible = true;
                    RadioButtonListApprove.Items[0].Text = "Approve the adjustment";
                    RadioButtonListApprove.Items[0].Value = "Approve";
                    RadioButtonListApprove.Items[1].Text = "Reject the adjustment";
                    RadioButtonListApprove.Items[1].Value = "Reject";
               /* }
            }*/
        }

    }
    

    /// <summary>
    /// Occurs when the user clics on the "approve / Reject" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonApproveReject_Click(object sender, EventArgs e)
    {

        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();
        SqlDataReader reader;

        if (this.InputFieldsAreOK())
        {
            try
            {
                thisConnection.Open();

                //------------
                //APPROVE
                //------------
                if (RadioButtonListApprove.SelectedValue == "Approve")
                {
                    //The user approved a TS that was in "pending adjustment validation" status
                    if (Request.Params["PendingAdjustmentValidation"] != null)
                    {
                        // We retrieve CancellationRequestNo and cancelledBy of the TS because we do not want to change it
                        string CancellationRequestNo = "";
                        string cancelledBy = "";

                        cmd.CommandText = "SELECT CancellationRequestNo, CancelledBy FROM  [allianceTimesheets].[dbo].[TimesheetHeader] " +
                                          "WHERE [TimesheetNumber] = '" + Request.Params["TSNumber"] + "'";

                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            CancellationRequestNo = (string)reader["CancellationRequestNo"];
                            cancelledBy           = (string)reader["cancelledBy"];
                        }

                        reader.Close();

                        //Call the stored proc to CANCEL THE TIMESHEET (and create the negative TS)
                        cmd.CommandText = "EXEC [allianceTimesheets].[dbo].[TimesheetCancel] " +
                                          "@TimesheetNumber, @CancellationRequestNo,@CancelledBy";


                        // We Put the data into the parameters
                        SqlParameter param1 = new SqlParameter();
                        param1.ParameterName = "@TimesheetNumber";
                        param1.Value = Request.Params["TSNumber"];

                        // We Put the data into the parameters
                        SqlParameter param2 = new SqlParameter();
                        param2.ParameterName = "@CancellationRequestNo";
                        param2.Value = CancellationRequestNo;

                        // We Put the data into the parameters
                        SqlParameter param3 = new SqlParameter();
                        param3.ParameterName = "@CancelledBy";
                        param3.Value = cancelledBy;

                        //Add the param to the query
                        cmd.Parameters.Add(param1);
                        cmd.Parameters.Add(param2);
                        cmd.Parameters.Add(param3);

                        cmd.ExecuteNonQuery();


                        //We need now to update the corrective TS linked (to approve them)
                        foreach (GridViewRow row in GridViewCorrectiveTS.Rows)
                        {
                            cmd.CommandText = "UPDATE [allianceTimesheets].[dbo].[TimesheetHeader] SET " +
                                              "Approved = 1 ," +
                                              "ApprovedBy = '" + User.Identity.Name + "', " +
                                              "ApprovedDate = GETDATE() " +
                                              "WHERE [TimesheetNumber] = '" + row.Cells[1].Text + "'";
                            cmd.ExecuteNonQuery();
                        }

                        //User redirection on the TS To approve page
                        Response.Redirect("./TSToApprove.aspx?status=ApprovalAdjustmentSuccess");
                    }
                    else
                    {
                        //LGX add 10/03/11: set to NULL rejectedBy, rejectedDate, rejectedReason
                        cmd.CommandText = "UPDATE  [allianceTimesheets].[dbo].[TimesheetHeader] SET Approved = 1, ApprovedBy = '" + User.Identity.Name + "', " +
                                          "ApprovedDate = GETDATE(), RejectedBy = NULL, RejectedDate = NULL, RejectedReason = NULL " +
                                          "WHERE [TimesheetNumber] = '" + Request.Params["TSNumber"] + "'";

                        cmd.ExecuteNonQuery();

                        //User redirection on the TS To approve page
                        Response.Redirect("./TSToApprove.aspx?status=ApprovalSuccess");
                    }

                }
                //------------
                //REJECT
                //------------
                else
                {
                    string RejectReason = this.TextBoxRejectReason.Text.Replace("'", "''");

                    //The user rejected a TS that was in "pending adjustment validation" status
                    if (Request.Params["PendingAdjustmentValidation"] != null)
                    {
                        cmd.CommandText = "UPDATE  [allianceTimesheets].[dbo].[TimesheetHeader] SET " +
                                          "PendingAdjustment = 1, " + 
                                          "PendingAdjustmentValidation = 0, " + 
                                          "Rejected = 1, " +
                                          "RejectedReason = '" + RejectReason + "' , " + 
                                          "RejectedBy = '" + User.Identity.Name + "', " +
                                          "RejectedDate = GETDATE() "+
                                          "WHERE [TimesheetNumber] = '" + Request.Params["TSNumber"] + "'";

                        cmd.ExecuteNonQuery();

                        //We need now to delete the corrective TS linked (to approve them)
                        foreach (GridViewRow row in GridViewCorrectiveTS.Rows)
                        {
                            cmd.CommandText = "DELETE FROM [allianceTimesheets].[dbo].[TimesheetDetail] " +
                                              "WHERE [TimesheetNumber] = '" + row.Cells[1].Text + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM [allianceTimesheets].[dbo].[TimesheetHeader] " +
                                              "WHERE [TimesheetNumber] = '" + row.Cells[1].Text + "'";
                            cmd.ExecuteNonQuery();
                        }

                        //User redirection on the TS To approve page
                        Response.Redirect("./TSToApprove.aspx?status=RejectionAdjustmentSuccess");
                    }
                    else
                    {
                        cmd.CommandText = "UPDATE  [allianceTimesheets].[dbo].[TimesheetHeader] SET Rejected = 1, RejectedBy = '" + User.Identity.Name + "', " +
                                          "RejectedDate = GETDATE() , RejectedReason = '" + RejectReason + "'" +
                                          "WHERE [TimesheetNumber] = '" + Request.Params["TSNumber"] + "'";
                        cmd.ExecuteNonQuery();

                        //User redirection on the TS To approve page
                        Response.Redirect("./TSToApprove.aspx?status=RejectionSuccess");
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
    }



    protected void RadioButtonListApprove_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonListApprove.SelectedValue == "Approve")
        {
            LabelReject.Visible = false;
            TextBoxRejectReason.Visible = false;
            ButtonApproveReject.Text = "Approve";
        }
        else
        {
            LabelReject.Visible = true;
            TextBoxRejectReason.Visible = true;
            ButtonApproveReject.Text = "Reject";
        }
    }


    /// <summary>
    /// Checks if values entered by the user are correct
    /// </summary>
    /// <returns>true if values are correct, False otherwise</returns>
    protected Boolean InputFieldsAreOK()
    {
        if (RadioButtonListApprove.SelectedValue == "Reject")
        {
            if (this.TextBoxRejectReason.Text == "")
            {
                Utils.NotifyUser("warning", "Please enter a reason for the rejection", Page);
                return false;
            }
        }

        return true;
    }


    protected void ButtonEdiTS_Click(object sender, EventArgs e)
    {
        Response.Redirect("./TimesheetSubmit.aspx?TSNumber=" + Request.Params["TSNumber"]);
    }


    /// <summary>
    /// Occurs when user clics on "adjust this TS" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonAdjustTS_Click(object sender, EventArgs e)
    {
        Response.Redirect("./TimesheetSubmit.aspx?TSNumber=" + Request.Params["TSNumber"] + "&Adjustment=1");
    }


    /// <summary>
    /// When user click on "view" link in the corrective TS gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewCorrectiveTS_SelectedIndexChanged(object sender, EventArgs e)
    {
        String TSNumber = GridViewCorrectiveTS.SelectedRow.Cells[1].Text;
        HttpContext.Current.Response.Redirect("./ViewTS.aspx?TSNumber=" + TSNumber );
    }
}