using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Human_Resource_Management
{
    /// <summary>
    /// Summary description for HolidaysDeleteHandler
    /// </summary>
    public class HolidaysDeleteHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string hyid = context.Request.QueryString["hyid"];

            // Execute SQL DELETE statement to delete the holiday with the specified Hyid from the Holidays table
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("update Holidays set Status='0' WHERE HolidayId = @Hyid", connection))
                {
                    command.Parameters.AddWithValue("@Hyid", hyid);
                    command.ExecuteNonQuery();
                }
            }

            // Return success response if necessary
            context.Response.ContentType = "text/plain";
            context.Response.Write("Holiday deleted successfully.");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}