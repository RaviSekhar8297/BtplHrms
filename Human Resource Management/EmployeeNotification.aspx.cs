using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class EmployeeNotification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNotifications();
                UpdateNotification();
            }
           
        }
        public void BindNotifications()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Notifications WHERE EmpId=@EmpId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataTable dt = new DataTable();
                        da1.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            int S_No = 1;
                            foreach (DataRow row in dt.Rows)
                            {
                                string Company = row["Company"].ToString();
                                string Branch = row["Branch"].ToString();
                                string Department = row["Department"].ToString();
                                string Subject = row["Subject"].ToString();
                                string Message = row["Message"].ToString();
                                string SendBy = row["SendBy"].ToString();
                                DateTime? Date = row["Date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["Date"]);
                                string CreatedDate = Date.HasValue ? Date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + S_No + "</td>");
                                projectHtml.Append("<td>" + Company + "</td>");
                                projectHtml.Append("<td>" + Branch + "</td>");
                                projectHtml.Append("<td>" + Department + "</td>");
                                projectHtml.Append("<td>" + Subject + "</td>");
                                projectHtml.Append("<td>" + Message + "</td>");
                                projectHtml.Append("<td>" + SendBy + "</td>");
                                projectHtml.Append("<td>" + CreatedDate + "</td>");
                                projectHtml.Append("</tr>");

                                NotificationData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                S_No++;
                            }

                            ViewState["NotificationDataTable"] = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                throw ex;
            }
        }


        public void UpdateNotification()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Notifications SET Status='True' WHERE Status='False' and EmpId=@EmpId", connection);
                    cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //protected void btnExportToExcel_Click(object sender, EventArgs e)
        //{
        //    if (ViewState["NotificationDataTable"] != null)
        //    {
        //        DataTable dt = ViewState["NotificationDataTable"] as DataTable;
        //        ExportToExcel(dt);
        //    }
        //}
        //protected void ExportToExcel(DataTable dt)
        //{
        //    string attachment = "attachment; filename=Notifications.xls";
        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", attachment);
        //    Response.ContentType = "application/vnd.ms-excel";
        //    string tab = "";

        //    foreach (DataColumn dc in dt.Columns)
        //    {
        //        Response.Write(tab + dc.ColumnName);
        //        tab = "\t";
        //    }
        //    Response.Write("\n");

        //    int i;
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        tab = "";
        //        for (i = 0; i < dt.Columns.Count; i++)
        //        {
        //            Response.Write(tab + dr[i].ToString());
        //            tab = "\t";
        //        }
        //        Response.Write("\n");
        //    }
        //    Response.End();
        //}

    }
}