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
    public partial class AdminPayrollItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AdditionsDataBind();
                DeductionsDataBind();
                OverTimeDataBind();
            }
        }
        
        public void AdditionsDataBind()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM PayRollItems where AdditionalStatus='1' ", connection))
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
                                string AdditionsName = row["AdditionsName"].ToString();
                                string AdditionsCatagory = row["AdditionsCatagory"].ToString();

                                string AdditionAmount = row["AdditionAmount"].ToString();
                               

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + S_No + "</td>");
                                projectHtml.Append("<td>" + Id + "</td>");
                                projectHtml.Append("<td>" + AdditionsName + "</td>");
                                projectHtml.Append("<td>" + AdditionsCatagory + "</td>");
                                projectHtml.Append("<td>" + AdditionAmount + "</td>");
                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_addition' onclick =\"editadditions('" + Id + "','" + AdditionsName + "','" + AdditionsCatagory + "','" + AdditionAmount + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                projectHtml.Append("<td><a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#delete_addition' onclick =\"deleteaddadition('" + Id + "','" + AdditionsName + "')\"><i class='fa-regular fa-trash-can m-r-5' style='color:red;'></i></a></td>");
                                projectHtml.Append("</tr>");

                                AdditionsData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                S_No++;
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

        public void OverTimeDataBind()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM PayRollItems where OverTimeStatus='1' ", connection))
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
                                string OverTimeName = row["OverTimeName"].ToString();
                                string OverTimeSalary = row["OverTimeSalary"].ToString();
                                string OverTimeSalaryType = row["OverTimeSalaryType"].ToString();


                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + S_No + "</td>");
                                projectHtml.Append("<td>" + Id + "</td>");
                                projectHtml.Append("<td>" + OverTimeName + "</td>");
                                projectHtml.Append("<td>" + OverTimeSalaryType + "</td>");
                                projectHtml.Append("<td>" + OverTimeSalary + "</td>");
                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_overtime' onclick =\"editovertime('" + Id + "','" + OverTimeName + "','" + OverTimeSalaryType + "','" + OverTimeSalary + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                projectHtml.Append("<td><a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#delete_overtime' onclick =\"deleteovertime('" + Id + "','" + OverTimeName + "')\"><i class='fa-regular fa-trash-can m-r-5' style='color:red;'></i></a></td>");
                                projectHtml.Append("</tr>");

                                OverTimeData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                S_No++;
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

        public void DeductionsDataBind()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM PayRollItems where DeductionStatus='1' ", connection))
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
                                string Deductionname = row["Deductionname"].ToString();
                                string DeductionAmount = row["DeductionAmount"].ToString();
                                string DeductionsType = row["DeductionsType"].ToString();


                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + S_No + "</td>");
                                projectHtml.Append("<td>" + Id + "</td>");
                                projectHtml.Append("<td>" + Deductionname + "</td>");
                                projectHtml.Append("<td>" + DeductionsType + "</td>");
                                projectHtml.Append("<td>" + DeductionAmount + "</td>");

                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_deduction' onclick =\"editdedudctions('" + Id + "','" + Deductionname + "','"+ DeductionsType + "','" + DeductionAmount + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                projectHtml.Append("<td><a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#delete_deduction' onclick =\"deletedeductions('" + Id + "','" + Deductionname + "')\"><i class='fa-regular fa-trash-can m-r-5' style='color:red;'></i></a></td>");
                                projectHtml.Append("</tr>");

                                DeductionData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                S_No++;
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

        protected void btnaddaditions_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand sqlcmd = new SqlCommand("INSERT INTO PayRollItems (AdditionsName,AdditionsCatagory,AdditionAmount,AdditionalStatus,CreatedBy,CreatedDate) values (@AdditionsName,@AdditionsCatagory,@AdditionAmount,@AdditionalStatus,@CreatedBy,@CreatedDate) ", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@AdditionsName", txtaddname.Text);
                        sqlcmd.Parameters.AddWithValue("@AdditionsCatagory", txtaddcatagory.Text);
                        sqlcmd.Parameters.AddWithValue("@AdditionAmount", txtaddamount.Text);
                        sqlcmd.Parameters.AddWithValue("@AdditionalStatus", "1");
                        sqlcmd.Parameters.AddWithValue("@CreatedBy", Session["Name"].ToString());
                        sqlcmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Added Successfully ..')</script>");
                            AdditionsDataBind();
                            DeductionsDataBind();
                            OverTimeDataBind();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failedy ..')</script>");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnaddovertime_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand sqlcmd = new SqlCommand("INSERT INTO PayRollItems (OverTimeName,OverTimeSalaryType,OverTimeSalary,OverTimeStatus,CreatedBy,CreatedDate) values (@OverTimeName,@OverTimeSalaryType,@OverTimeSalary,@OverTimeStatus,@CreatedBy,@CreatedDate) ", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@OverTimeName", txtaddovertimename.Text);
                        sqlcmd.Parameters.AddWithValue("@OverTimeSalaryType", ddladdovertimetype.SelectedItem.Value);
                        sqlcmd.Parameters.AddWithValue("@OverTimeSalary", txtaddoverttimerate.Text);
                        sqlcmd.Parameters.AddWithValue("@OverTimeStatus", "1");
                        sqlcmd.Parameters.AddWithValue("@CreatedBy", Session["Name"].ToString());
                        sqlcmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Added Successfully ..')</script>");
                            AdditionsDataBind();
                            DeductionsDataBind();
                            OverTimeDataBind();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failedy ..')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnadddeductions_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand sqlcmd = new SqlCommand("INSERT INTO PayRollItems (Deductionname,DeductionsType,DeductionAmount,DeductionStatus,CreatedBy,CreatedDate) values (@Deductionname,@DeductionsType,@DeductionAmount,@DeductionStatus,@CreatedBy,@CreatedDate) ", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@Deductionname", txtadddeductionname.Text);
                        sqlcmd.Parameters.AddWithValue("@DeductionsType", txtadddeductiontype.Text);
                        sqlcmd.Parameters.AddWithValue("@DeductionAmount", txtadddeductionamount.Text);
                        sqlcmd.Parameters.AddWithValue("@DeductionStatus", "1");
                        sqlcmd.Parameters.AddWithValue("@CreatedBy", Session["Name"].ToString());
                        sqlcmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Added Successfully ..')</script>"); 
                            AdditionsDataBind();
                            DeductionsDataBind();
                            OverTimeDataBind();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failedy ..')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnupdateadditions_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE  PayRollItems SET AdditionsName=@AdditionsName,AdditionsCatagory=@AdditionsCatagory,AdditionAmount=@AdditionAmount,UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate WHERE Id=@Id ", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@AdditionsName", TextBox2.Text);
                        sqlcmd.Parameters.AddWithValue("@AdditionsCatagory", TextBox3.Text);
                        sqlcmd.Parameters.AddWithValue("@AdditionAmount", TextBox4.Text);
                        sqlcmd.Parameters.AddWithValue("@UpdatedBy", Session["Name"].ToString());
                        sqlcmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                        sqlcmd.Parameters.AddWithValue("@Id", TextBox1.Text);
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Updated Successfully ..')</script>");
                            AdditionsDataBind();
                            DeductionsDataBind();
                            OverTimeDataBind();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failedy ..')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btndeleteaddirtion_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE  PayRollItems SET DeleteBy=@DeleteBy,Deletedate=@Deletedate, AdditionalStatus='0' WHERE Id=@Id ", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@DeleteBy", Session["Name"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Deletedate", DateTime.Now);
                        sqlcmd.Parameters.AddWithValue("@Id",HiddenField1.Value);
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Deleted Successfully ..')</script>");
                            AdditionsDataBind();
                            DeductionsDataBind();
                            OverTimeDataBind();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failedy ..')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnupdateovertime_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE  PayRollItems SET OverTimeName=@OverTimeName,OverTimeSalaryType=@OverTimeSalaryType,OverTimeSalary=@OverTimeSalary,UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate WHERE Id=@Id ", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@OverTimeName", TextBox6.Text);
                        sqlcmd.Parameters.AddWithValue("@OverTimeSalaryType", TextBox7.Text);
                        sqlcmd.Parameters.AddWithValue("@OverTimeSalary", TextBox8.Text);
                        sqlcmd.Parameters.AddWithValue("@UpdatedBy", Session["Name"].ToString());
                        sqlcmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                        sqlcmd.Parameters.AddWithValue("@Id", TextBox5.Text);
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Updated Successfully ..')</script>");
                            AdditionsDataBind();
                            DeductionsDataBind();
                            OverTimeDataBind();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failedy ..')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btndeleteovertime_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE  PayRollItems SET DeleteBy=@DeleteBy,Deletedate=@Deletedate, OverTimeStatus='0' WHERE Id=@Id ", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@DeleteBy", Session["Name"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Deletedate", DateTime.Now);
                        sqlcmd.Parameters.AddWithValue("@Id", HiddenField2.Value);
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Deleted Successfully ..')</script>");
                            AdditionsDataBind();
                            DeductionsDataBind();
                            OverTimeDataBind();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failedy ..')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnupdatededuction_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE  PayRollItems SET Deductionname=@Deductionname,DeductionsType=@DeductionsType,DeductionAmount=@DeductionAmount,UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate WHERE Id=@Id ", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@Deductionname", TextBox10.Text);
                        sqlcmd.Parameters.AddWithValue("@DeductionsType", TextBox11.Text);
                        sqlcmd.Parameters.AddWithValue("@DeductionAmount", TextBox12.Text);
                        sqlcmd.Parameters.AddWithValue("@UpdatedBy", Session["Name"].ToString());
                        sqlcmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                        sqlcmd.Parameters.AddWithValue("@Id", TextBox9.Text);
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Updated Successfully ..')</script>");
                            AdditionsDataBind();
                            DeductionsDataBind();
                            OverTimeDataBind();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failedy ..')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btndeletededuction_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE  PayRollItems SET DeleteBy=@DeleteBy,Deletedate=@Deletedate, DeductionStatus='0' WHERE Id=@Id ", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@DeleteBy", Session["Name"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Deletedate", DateTime.Now);
                        sqlcmd.Parameters.AddWithValue("@Id", HiddenField3.Value);
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Deleted Successfully ..')</script>");
                            AdditionsDataBind();
                            DeductionsDataBind();
                            OverTimeDataBind();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failedy ..')</script>");
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