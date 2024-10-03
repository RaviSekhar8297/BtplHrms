using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace Human_Resource_Management
{
    public partial class AdminAddCompanies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox8.Text = DateTime.Today.ToString("yyyy-MM-dd");
                BindCompany2();
            }
        }
        public void BindCompany2()
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "SELECT CompanyID,CompanyName FROM Companies";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // bind company list for Branch
                        DropDownList5.DataSource = dt;
                        DropDownList5.DataValueField = "CompanyID";
                        DropDownList5.DataTextField = "CompanyName";
                        DropDownList5.DataBind();

                        // bind company list for Department
                        ddldeptcompany.DataSource = dt;
                        ddldeptcompany.DataValueField = "CompanyID";
                        ddldeptcompany.DataTextField = "CompanyName";
                        ddldeptcompany.DataBind();

                       

                        sqlConn.Close();
                        DropDownList5.Items.Insert(0, new ListItem(" -- Select Company -- ", "0"));
                        ddldeptcompany.Items.Insert(0, new ListItem(" -- Select Company -- ", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddldeptcompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "select Branch_Id, BranchName, BranchCode from Branch where [CompanyId] = '" + ddldeptcompany.SelectedValue + "' ";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddldeptbranch.DataSource = dt;
                        ddldeptbranch.DataValueField = "Branch_Id";
                        ddldeptbranch.DataTextField = "BranchName";
                        ddldeptbranch.DataBind();
                        sqlConn.Close();
                        ddldeptbranch.Items.Insert(0, new ListItem(" -- Select Branch -- ", "0"));

                    }
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        protected void addcompanybtn_Click1(object sender, EventArgs e)
        {
            try
            {
                string connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    string insertQuery = @"INSERT INTO Companies (CompanyCode,CompanyName,RegNo,RegDate,Tanno,PanNo,TinNo,Name,Desiganation,Email,Address,Phone,PhoneSecond,MobileNo,FaxNo,WebsiteURL,RecordStatus,GSTNumber,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,DeletedBy,DeletedDate) 
                     VALUES(@CompanyCode,@CompanyName,@RegNo,@RegDate,@Tanno,@PanNo,@TinNo,@Name,@Desiganation,@Email,@Address,@Phone,@PhoneSecond,@MobileNo,@FaxNo,@WebsiteURL,@RecordStatus,@GSTNumber,@CreatedBy,@CreatedDate,@UpdatedBy,@UpdatedDate,@DeletedBy,@DeletedDate);";
                    sqlConn.Open();

                    using (SqlCommand sqlCmd = new SqlCommand(insertQuery, sqlConn))
                    {
                        sqlCmd.Parameters.AddWithValue("@CompanyCode", TextBox2.Text);
                        sqlCmd.Parameters.AddWithValue("@CompanyName", TextBox6.Text);
                        sqlCmd.Parameters.AddWithValue("@RegNo", TextBox7.Text);
                        sqlCmd.Parameters.AddWithValue("@RegDate", TextBox8.Text);
                        sqlCmd.Parameters.AddWithValue("@Tanno", TextBox9.Text);
                        sqlCmd.Parameters.AddWithValue("@PanNo", TextBox10.Text);
                        sqlCmd.Parameters.AddWithValue("@TinNo", TextBox11.Text);
                        sqlCmd.Parameters.AddWithValue("@Name", TextBox12.Text);
                        sqlCmd.Parameters.AddWithValue("@Desiganation", TextBox13.Text);
                        sqlCmd.Parameters.AddWithValue("@Email", TextBox15.Text);
                        sqlCmd.Parameters.AddWithValue("@Address", TextBox14.Text);
                        sqlCmd.Parameters.AddWithValue("@Phone", TextBox16.Text);
                        sqlCmd.Parameters.AddWithValue("@PhoneSecond", TextBox17.Text);
                        sqlCmd.Parameters.AddWithValue("@MobileNo", TextBox18.Text);
                        sqlCmd.Parameters.AddWithValue("@FaxNo", TextBox19.Text);
                        sqlCmd.Parameters.AddWithValue("@WebsiteURL", TextBox20.Text);
                        sqlCmd.Parameters.AddWithValue("@RecordStatus", 1);
                        sqlCmd.Parameters.AddWithValue("@GSTNumber", TextBox21.Text);
                        sqlCmd.Parameters.AddWithValue("@CreatedBy", Session["Name"].ToString());
                        sqlCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        sqlCmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@DeletedBy", DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@DeletedDate", DBNull.Value);
                        int i = sqlCmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Company Created Successfully....')</script>");
                            BindCompany2();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed....')</script>");
                            BindCompany2();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void addcompanybranchbtn_Click(object sender, EventArgs e)
        {

            String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connstrg))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Branch WHERE BranchName = @Name and CompanyId='" + DropDownList5.SelectedValue + "'", con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            con.Open();
                            cmd.Parameters.AddWithValue("@Name", this.TextBox23.Text.Trim());
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                this.Label1.Text = "Duplicate Entry";
                            }
                            else
                            {
                                SqlConnection con3 = new SqlConnection(connstrg);
                                String insert = "insert into Branch(CompanyId,BranchName,CreatedBy,CreatedDate)values('" + DropDownList5.SelectedValue + "','" + TextBox23.Text + "','" + Session["Name"].ToString().Trim() + "','" + DateTime.Now + "')";
                                SqlCommand commForDept = new SqlCommand(insert, con3);
                                con3.Open();
                                int i = commForDept.ExecuteNonQuery();
                                con3.Close();
                                if (i > 0)
                                {
                                    Response.Write("<script>alert('Branch Created successfully')</script>");
                                    TextBox23.Text = null;
                                   
                                    BindCompany2();
                                }
                                else
                                {
                                    Response.Write("<script>alert('Failed....')</script>");
                                    BindCompany2();
                                }
                               
                              
                                //Response.Redirect("~/Branch.aspx");
                            }
                            con.Close();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btndepertment_Click(object sender, EventArgs e)
        {
            String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(connstrg))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Department WHERE Department = @Name and CompanyId='" + ddldeptcompany.SelectedValue + "' and BranchId='" + ddldeptbranch.SelectedValue + "'", con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            con.Open();
                            cmd.Parameters.AddWithValue("@Name", this.TextBox27.Text.Trim());
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                this.Label1.Text = "Duplicate Entry";
                            }
                            else
                            {
                                SqlConnection con5 = new SqlConnection(connstrg);
                                SqlCommand cmdForbranch = new SqlCommand("select BranchCode from branch where Branch_Id='" + ddldeptbranch.SelectedValue + "'", con5);
                                SqlDataAdapter daForBranch = new SqlDataAdapter(cmdForbranch);
                                con5.Open();
                                DataSet dsForBranch = new DataSet();
                                daForBranch.Fill(dsForBranch);
                                string BrnchCode = dsForBranch.Tables[0].Rows[0]["BranchCode"].ToString().Trim();
                                con5.Close();
                                SqlConnection con3 = new SqlConnection(connstrg);
                                String insert = "insert into Department(CompanyId,BranchId,Department,SN,Decripation,BranchCode,CreatedBy,CreatedDate)values('" + ddldeptcompany.SelectedValue + "','" + ddldeptbranch.SelectedValue + "','" + TextBox27.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + BrnchCode.Trim() + "','" + Session["Name"].ToString().Trim() + "','" + DateTime.Now + "')";
                                SqlCommand commForDept = new SqlCommand(insert, con3);
                                con3.Open();
                                int i = commForDept.ExecuteNonQuery();
                                con3.Close();
                                if (i > 0)
                                {
                                    Response.Write("<script>alert('Department Created successfully')</script>");
                                    TextBox27.Text = null;
                                    TextBox3.Text = null;
                                    TextBox4.Text = null;
                                    BrnchCode = null;
                                    BindCompany2();
                                }
                                else
                                {
                                    Response.Write("<script>alert('Failed....')</script>");
                                    BindCompany2();
                                }

                            }
                            con.Close();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnaddshift_Click(object sender, EventArgs e)
        {
            try
            {
                string connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connstrg))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM ShiftTable WHERE ShiftName = @Name and ShiftStatus = '1'", con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            con.Open();
                            cmd.Parameters.AddWithValue("@Name", this.TextBox34.Text.Trim());
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                this.Label1.Text = "You Have " + TextBox34.Text + " Shift already created....";
                                Label1.ForeColor = Color.Red;
                            }
                            else
                            {
                                using (SqlConnection con3 = new SqlConnection(connstrg))
                                {
                                    string insert = "insert into ShiftTable(ShiftName,ShiftId,Shift1Time,Shift2Time,Shift3Time,ShiftInTime,ShiftOutTime,ShiftDuration,ShiftHalfDayDuration,ShiftFullDayDuration,CreatedBy,CreatedDate,ShiftStatus) " +
                                                    "values (@ShiftName,@ShiftId,@Shift1Time,@Shift2Time,@Shift3Time,@ShiftInTime,@ShiftOutTime,@ShiftDuration,@ShiftHalfDayDuration,@ShiftFullDayDuration,@CreatedBy,@CreatedDate,@ShiftStatus)";
                                    using (SqlCommand cmdins = new SqlCommand(insert, con3))
                                    {
                                        con3.Open();

                                        cmdins.Parameters.AddWithValue("@ShiftName", TextBox34.Text);
                                        cmdins.Parameters.AddWithValue("@ShiftId", TextBox35.Text);

                                        string input = TextBox36.Text; // Example: "08:00 AM - 04:00 PM"
                                        string[] times = input.Split(new string[] { " - " }, StringSplitOptions.None);

                                        string startTimeString = times[0].Trim(); // "08:00 AM"
                                        string endTimeString = times[1].Trim();   // "04:00 PM"

                                        // Parse the times into DateTime objects
                                        DateTime startTime = DateTime.Parse(startTimeString);
                                        DateTime endTime = DateTime.Parse(endTimeString);

                                        // Calculate the duration
                                        TimeSpan duration = endTime - startTime;
                                        if (duration < TimeSpan.Zero)
                                        {
                                            duration += TimeSpan.FromDays(1);
                                        }
                                        string fullDurationString = duration.ToString(@"hh\:mm");
                                        TimeSpan halfDuration = TimeSpan.FromTicks(duration.Ticks / 2);
                                        string halfDurationString = halfDuration.ToString(@"hh\:mm");

                                        cmdins.Parameters.AddWithValue("@Shift1Time", input);
                                        cmdins.Parameters.AddWithValue("@Shift2Time", DBNull.Value);
                                        cmdins.Parameters.AddWithValue("@Shift3Time", DBNull.Value);
                                        cmdins.Parameters.AddWithValue("@ShiftInTime", startTimeString);
                                        cmdins.Parameters.AddWithValue("@ShiftOutTime", endTimeString);
                                        cmdins.Parameters.AddWithValue("@ShiftDuration", fullDurationString);
                                        cmdins.Parameters.AddWithValue("@ShiftHalfDayDuration", halfDurationString);
                                        cmdins.Parameters.AddWithValue("@ShiftFullDayDuration", fullDurationString);
                                        cmdins.Parameters.AddWithValue("@CreatedBy", Session["Name"].ToString());
                                        cmdins.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                                        cmdins.Parameters.AddWithValue("@ShiftStatus", "1");

                                        int i = cmdins.ExecuteNonQuery();
                                        con3.Close();

                                        if (i > 0)
                                        {
                                            Response.Write("<script>alert('Shift Created Successfully')</script>");
                                            TextBox34.Text = null;
                                            TextBox35.Text = null;
                                            TextBox36.Text = null;
                                        }
                                        else
                                        {
                                            Response.Write("<script>alert('Failed ....')</script>");
                                            TextBox34.Text = null;
                                        }
                                    }
                                }
                            }
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btncatagory_Click(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connstrg))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Categories WHERE CategoryName = @Name", con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            con.Open();
                            cmd.Parameters.AddWithValue("@Name", this.Category.Text.Trim());

                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                this.Label1.Text = "Duplicate Entry";
                            }
                            else
                            {
                                SqlConnection conn1 = new SqlConnection(connstrg);

                                string str = "insert into Categories (CategoryName,CategorySName,OTFormula,MinOT,GraceTimeForLateComing,GracetimeForEarlyGoing,IsWeeklyOff1,WeeklyOff1Day,IsWeeklyOff2,WeeklyOff2Day,WeeklyOff2days,IsCHalfForLessD,CHalfForLessDMins,IsCAbsentDisLessThan,CAbsentDisLessThanMins,IsMarkHalfDay_LateBy,MarkHalfDay_LateByMins,IsMarkHalfDay_EarlyGoing,MarkHalfDay_EarlyGoing) values ('" + Category.Text + "','" + catShortname.Text + "','" + OTformula.Text + "','" + Minot.Text + "','" + GRT.Text + "','" + NglLstpunch1.Text + "','" + Weekoff1.Checked + "','" + Weeloff11.SelectedItem.Text + "','" + weekoff2.Checked + "','" + Weekoff22.SelectedItem.Text + "','" + weekoff2list.SelectedItem.Text + "','" + CHDW.Checked + "','" + CHWD1.Text + "','" + cawdil.Checked + "','" + CAW.Text + "','" + MHDL.Checked + "','" + MHDL1.Text + "','" + MHDEGB.Checked + "','" + MHDEGB1.Text + "')";

                                SqlCommand commforCategry = new SqlCommand(str, conn1);
                                conn1.Open();
                                commforCategry.ExecuteNonQuery();
                                conn1.Close();
                                Response.Write("<script>alert('Data inserted successfully')</script>");
                                Response.Redirect("~/Categorieslist.aspx");
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
    }
}