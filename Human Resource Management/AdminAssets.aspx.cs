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
    public partial class AdminAssets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAssets();
            }
        }
        public void BindAssets()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Assets where Status='1'", connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string EmpId = row["EmpId"].ToString();
                                string Name = row["Name"].ToString();
                                string Department = row["Department"].ToString();
                                string LapTop = row["LapTop"].ToString();
                                string Mouse = row["Mouse"].ToString();
                                string Pendrive = row["Pendrive"].ToString(); 
                                string Mobile = row["Mobile"].ToString();
                                string Bag = row["Bag"].ToString();
                                string Sim = row["Sim"].ToString();
                                string AssignedBy = row["AssignedBy"].ToString();
                                DateTime? updob = row["AssignDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["AssignDate"]);
                                string AssignDate = updob.HasValue ? updob.Value.ToString("yyyy-MM-dd") : string.Empty;

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + Name + "</td>");
                                projectHtml.Append("<td>" + Department + "</td>");
                                projectHtml.Append("<td>");
                                if (LapTop == "True")
                                {
                                    projectHtml.Append("<img src='https://img.icons8.com/?size=48&id=nK5KokYOqcnT&format=png' alt='' style='width:40px' />");
                                }
                                else
                                {
                                    // Display 'No' if LapTop is not true
                                    projectHtml.Append("-");
                                }
                                projectHtml.Append("</td>");

                                projectHtml.Append("<td>");
                                if (Mouse == "True")
                                {
                                    projectHtml.Append("<img src='https://img.icons8.com/?size=48&id=QnVvJCZUlHaU&format=png' alt='' style='width:40px' />");
                                }
                                else
                                {
                                    // Display 'No' if LapTop is not true
                                    projectHtml.Append("-");
                                }
                                projectHtml.Append("</td>");

                                projectHtml.Append("<td>");
                                if (Pendrive == "True")
                                {
                                    projectHtml.Append("<img src='https://img.icons8.com/?size=48&id=FlnYHAW3wYBn&format=png' alt='' style='width:40px' />");
                                }
                                else
                                {
                                    projectHtml.Append("-");
                                }
                                projectHtml.Append("</td>");

                                projectHtml.Append("<td>");
                                if (Mobile == "True")
                                {
                                    projectHtml.Append("<img src='https://img.icons8.com/?size=48&id=9qJE8HBWstFb&format=png' alt='' style='width:40px' />");
                                }
                                else
                                {
                                    projectHtml.Append("-");
                                }
                                projectHtml.Append("</td>");

                                projectHtml.Append("<td>");
                                if (Bag == "True")
                                {
                                    projectHtml.Append("<img src='https://img.icons8.com/?size=80&id=GRdZj2z1k9UO&format=png' alt='' style='width:40px' />");
                                }
                                else
                                {
                                    projectHtml.Append("-");
                                }
                                projectHtml.Append("</td>");


                                projectHtml.Append("<td>");
                                if (Sim == "True")
                                {
                                    projectHtml.Append("<img src='https://img.icons8.com/?size=40&id=30436&format=png' alt='' style='width:40px' />");
                                }
                                else
                                {
                                    projectHtml.Append("-");
                                }
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td>" + AssignedBy + "</td>");
                                projectHtml.Append("<td>" + AssignDate + "</td>");
                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_assets' onclick =\"editassetst('" + EmpId + "','" + Name + "','" + LapTop + "','" + Mouse + "','" + Pendrive + "','" + Mobile + "','" + Bag + "','" + Sim + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                projectHtml.Append("<td><a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#return_assets' onclick =\"returnassets('" + EmpId + "','" + Name + "','" + LapTop + "','" + Mouse + "','" + Pendrive + "','" + Mobile + "','" + Bag + "','" + Sim + "')\"><i class='fa fa-retweet' style='color:red;'></i></a></td>");

                                projectHtml.Append("</tr>");

                                AssetsData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnupdateassets_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE  Assets SET LapTop=@LapTop,Mouse=@Mouse,Pendrive=@Pendrive,Mobile=@Mobile,Bag=@Bag,Sim=@Sim where EmpId=@EmpId", connection))
                    {
                        connection.Open();
                        sqlcmd.Parameters.AddWithValue("@LapTop", CheckBox1.Checked.ToString());
                        sqlcmd.Parameters.AddWithValue("@Mouse", CheckBox2.Checked.ToString());
                        sqlcmd.Parameters.AddWithValue("@Pendrive", CheckBox3.Checked.ToString());
                        sqlcmd.Parameters.AddWithValue("@Mobile", CheckBox4.Checked.ToString());
                        sqlcmd.Parameters.AddWithValue("@Bag", CheckBox5.Checked.ToString());
                        sqlcmd.Parameters.AddWithValue("@Sim", CheckBox6.Checked.ToString());
                        sqlcmd.Parameters.AddWithValue("@EmpId", HiddenField1.Value);
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Assets Updated Successfully....')</script>");
                            BindAssets();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed....')</script>");
                        }
                    }
                   
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE  Assets SET LapTop=@LapTop,Mouse=@Mouse,Pendrive=@Pendrive,Mobile=@Mobile,Bag=@Bag,Sim=@Sim,Status='0',ReturnStatus='1',ReturnDate=@ReturnDate,Returnremarks=@Returnremarks where EmpId=@EmpId", connection))
                    {
                        connection.Open();
                        sqlcmd.Parameters.AddWithValue("@LapTop", CheckBox7.Checked.ToString());
                        sqlcmd.Parameters.AddWithValue("@Mouse", CheckBox8.Checked.ToString());
                        sqlcmd.Parameters.AddWithValue("@Pendrive", CheckBox9.Checked.ToString());
                        sqlcmd.Parameters.AddWithValue("@Mobile", CheckBox10.Checked.ToString());
                        sqlcmd.Parameters.AddWithValue("@Bag", CheckBox11.Checked.ToString());
                        sqlcmd.Parameters.AddWithValue("@Sim", CheckBox12.Checked.ToString());
                        sqlcmd.Parameters.AddWithValue("@ReturnDate", DateTime.Now);
                        sqlcmd.Parameters.AddWithValue("@Returnremarks", TextBox3.Text);
                        sqlcmd.Parameters.AddWithValue("@EmpId", HiddenField2.Value);
                        if (!CheckBox6.Checked && !CheckBox7.Checked && !CheckBox8.Checked && !CheckBox9.Checked && !CheckBox10.Checked && !CheckBox11.Checked && !CheckBox12.Checked)
                        {
                            int i = sqlcmd.ExecuteNonQuery();
                            if (i > 0)
                            {
                                Response.Write("<script>alert('Assets Returned Successfully....')</script>");
                                BindAssets();
                            }
                            else
                            {
                                Response.Write("<script>alert('Failed....')</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Please Return All Assets....')</script>");
                            BindAssets();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}