using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Human_Resource_Management
{
    /// <summary>
    /// Summary description for ProfilePic
    /// </summary>
    public class ProfilePic : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string imageid = context.Request.QueryString["EmployeeId"];
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("select EmpImage from Employees where EmployeeId=" + imageid, con);

                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                context.Response.BinaryWrite((byte[])dr[0]);
                con.Close();
                context.Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {


            }
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