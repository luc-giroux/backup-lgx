using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class EditVariation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TextBoxDescription.Text = ((Variation)HttpContext.Current.Session["currentVariation"]).Description;
            CheckBoxCompleted.Checked = ((Variation)HttpContext.Current.Session["currentVariation"]).Completed;

            // We can edit the discipline only if the discipline has never been defined
            if (((Variation)HttpContext.Current.Session["currentVariation"]).Discipline.Trim() == "")
            {
                ComboBoxDiscipline.Enabled = true;
            }
            else
            {
                ComboBoxDiscipline.Enabled = false;
                ComboBoxDiscipline.SelectedValue = ((Variation)HttpContext.Current.Session["currentVariation"]).Discipline;
            }

        }
    }



    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        SqlConnection thisConnection = new SqlConnection(ConfigurationManager.AppSettings["DB_CONNECTION"]);
        SqlCommand cmd = thisConnection.CreateCommand();
        Variation v = (Variation)HttpContext.Current.Session["currentVariation"];


        try
        {
            thisConnection.Open();

            cmd.CommandText = "UPDATE [allianceTimesheets].[dbo].[Variation] SET " +
                                  "[Discipline] = @Discipline, " +
                                  "[Description] = @Description, " +
                                  "[Completed] = @Completed " +
                                  " WHERE [VariationNumber] = '" + v.Number + "' AND " +
                                  " [ContractNumber] = '" + HttpContext.Current.Session["currentContract"] + "'";

            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@Discipline";
            if (ComboBoxDiscipline.SelectedValue != "select")
                param1.Value = ComboBoxDiscipline.SelectedValue;
            else
                param1.Value = DBNull.Value;

            SqlParameter param2 = new SqlParameter();
            param2.ParameterName = "@Description";
            param2.Value = TextBoxDescription.Text;

            SqlParameter param3 = new SqlParameter();
            param3.ParameterName = "@Completed";
            param3.Value = CheckBoxCompleted.Checked;

            cmd.Parameters.Add(param1);
            cmd.Parameters.Add(param2);
            cmd.Parameters.Add(param3);
            cmd.ExecuteNonQuery();


            //Update of the object in session
            v.Discipline= ComboBoxDiscipline.SelectedValue;
            v.Description = TextBoxDescription.Text;
            v.Completed = CheckBoxCompleted.Checked;
            HttpContext.Current.Session.Add("currentVariation", v);

            //User success notification
            Utils.NotifyUser("success", "Variation successfully updated!", Page);


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