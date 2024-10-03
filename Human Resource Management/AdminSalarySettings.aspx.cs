using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class AdminSalarySettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPFTypeData();
            }
        }
        private void BindPFTypeData()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM PFTax", connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            int S_No = 1;
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string Id = row["Id"].ToString();
                                string PFId = row["PFId"].ToString();
                                string Name = row["Name1"].ToString();
                                string HRA = row["HRA"] == DBNull.Value ? string.Empty : row["HRA"].ToString();
                                string EA = row["EA"].ToString();
                                string DA = row["DA"].ToString();
                                string CA = row["CA"].ToString();
                                string IT = row["IT"] == DBNull.Value ? string.Empty : row["IT"].ToString();
                                string LIC = row["LIC"].ToString();
                                string PF = row["PF"].ToString();
                                string ESI = row["ESI"].ToString();
                                string MA = row["MA"].ToString();
                                string SA = row["SA"].ToString();

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + S_No + "</td>");
                                projectHtml.Append("<td>" + Name + "</td>");
                                projectHtml.Append("<td>" + HRA + "</td>");
                                projectHtml.Append("<td>" + EA + "</td>");
                                projectHtml.Append("<td>" + DA + "</td>");
                                projectHtml.Append("<td>" + CA + "</td>");
                                projectHtml.Append("<td>" + IT + "</td>");
                                projectHtml.Append("<td>" + LIC + "</td>");
                                projectHtml.Append("<td>" + PF + "</td>");
                                projectHtml.Append("<td>" + ESI + "</td>");
                                projectHtml.Append("<td>" + MA + "</td>");
                                projectHtml.Append("<td>" + SA + "</td>");
                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_pfTypeTax' onclick=\"editPfTax('" + Id + "','" + PFId + "','" + Name + "','" + HRA + "','" + EA + "','" + DA + "','" + CA + "','" + IT + "','" + LIC + "','" + PF + "','" + ESI + "','" + MA + "','" + SA + "')\"><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                projectHtml.Append("</tr>");

                                PfTypeList.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                S_No++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception or log it
                throw ex;
            }
        }

        protected void btncountupdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("update PFTax set HRA=@HRA, EA=@EA, DA=@DA, CA=@CA, IT=@IT, LIC=@LIC, PF=@PF, ESI=@ESI, MA=@MA, SA=@SA where Id=@Id and PFId=@PFId", connection))
                    {
                        connection.Open();
                        cmd.Parameters.AddWithValue("@HRA", string.IsNullOrEmpty(txthra.Text) ? (object)DBNull.Value : Convert.ToDecimal(txthra.Text));
                        cmd.Parameters.AddWithValue("@EA", string.IsNullOrEmpty(txtea.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtea.Text));
                        cmd.Parameters.AddWithValue("@DA", string.IsNullOrEmpty(txtda.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtda.Text));
                        cmd.Parameters.AddWithValue("@CA", string.IsNullOrEmpty(txtca.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtca.Text));
                        cmd.Parameters.AddWithValue("@IT", string.IsNullOrEmpty(txtit.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtit.Text));
                        cmd.Parameters.AddWithValue("@LIC", string.IsNullOrEmpty(txtlic.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtlic.Text));
                        cmd.Parameters.AddWithValue("@PF", string.IsNullOrEmpty(txtpf.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtpf.Text));
                        cmd.Parameters.AddWithValue("@ESI", string.IsNullOrEmpty(txtesi.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtesi.Text));
                        cmd.Parameters.AddWithValue("@MA", string.IsNullOrEmpty(txtma.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtma.Text));
                        cmd.Parameters.AddWithValue("@SA", string.IsNullOrEmpty(txtsa.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtsa.Text));
                        cmd.Parameters.AddWithValue("@Id", Convert.ToInt64(HiddenField1.Value));
                        cmd.Parameters.AddWithValue("@PFId", Convert.ToInt64(HiddenField2.Value));

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Updated Successfully...')</script>");
                            BindPFTypeData();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed...')</script>");
                            BindPFTypeData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}