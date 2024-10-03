using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace Human_Resource_Management
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoginEmployeeDetails();
                editbtndetails();
                imageshow();
                AllDataBind();
                AllDataBindTextbox();
                assets();
                ProjectList();
                PfDetails();
            }
        }
        public void assets()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM assets ", connection))
                    {
                        connection.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        GridView1.DataSource = ds;
                        GridView1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        protected void LoginEmployeeDetails()
        {
            try
            {
                try
                {
                    if (Session["EmpId"] == null)
                    {
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        var LoginWith = Session["Roles"].ToString().Trim();

                        if (LoginWith == "Administrator")
                        {
                            lblempname.Text = Session["Name"].ToString();
                            lbldesignation.Text= Session["Designation"].ToString();
                            lbldepartment.Text = Session["DeptName"].ToString();
                            lblempid.Text = Session["EmpId"].ToString();
                            lblcompany.Text = Session["CompanyName"].ToString();
                            lblbranch.Text = Session["BranchName"].ToString();
                            lblemail.Text = Session["Email"].ToString();
                            lblrole.Text = Session["Roles"].ToString();
                            lbldevicecode.Text = Session["DeviceCode"].ToString();

                            txtname.Text = Session["Name"].ToString();
                            if (Session["Image"] != null)
                            {
                                imagedisplay();
                            }
                        }
                        else if (LoginWith == "SuperAdmin")
                        {
                            lblempname.Text = Session["Name"].ToString();
                            lbldesignation.Text = Session["Designation"].ToString();
                            lbldepartment.Text = Session["DeptName"].ToString();
                            lblempid.Text = Session["EmpId"].ToString();
                            lblcompany.Text = Session["CompanyName"].ToString();
                            lblbranch.Text = Session["BranchName"].ToString();
                            lblemail.Text = Session["Email"].ToString();
                            lblrole.Text = Session["Roles"].ToString();
                            lbldevicecode.Text = Session["DeviceCode"].ToString();
                            if (Session["Image"] != null)
                            {
                                imagedisplay();
                            }
                        }
                        else if (LoginWith == "Manager")
                        {
                            lblempname.Text = Session["Name"].ToString();
                            lbldesignation.Text = Session["Designation"].ToString();
                            lbldepartment.Text = Session["DeptName"].ToString();
                            lblempid.Text = Session["EmpId"].ToString();
                            lblcompany.Text = Session["CompanyName"].ToString();
                            lblbranch.Text = Session["BranchName"].ToString();
                            lblemail.Text = Session["Email"].ToString();
                            lblrole.Text = Session["Roles"].ToString();
                            lbldevicecode.Text = Session["DeviceCode"].ToString();
                            if (Session["Image"] != null)
                            {
                                imagedisplay();
                            }
                        }
                        else if (LoginWith == "Employee")
                        {
                            lblempname.Text = Session["Name"].ToString();
                            lbldesignation.Text = Session["Designation"].ToString();
                            lbldepartment.Text = Session["DeptName"].ToString();
                            lblempid.Text = Session["EmpId"].ToString();
                            lblcompany.Text = Session["CompanyName"].ToString();
                            lblbranch.Text = Session["BranchName"].ToString();
                            lblemail.Text = Session["Email"].ToString();
                            lblrole.Text = Session["Roles"].ToString();
                            lbldevicecode.Text = Session["DeviceCode"].ToString();
                            if (Session["Image"] != null)
                            {
                                imagedisplay();
                            }
                        }
                        else
                        {
                            Response.Redirect("Dashboard.aspx");
                        }
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("MyErrorPage.aspx");
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void imagedisplay()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT Image FROM register WHERE EmpId = @EmpId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", Session["EmpId"]);

                        connection.Open();
                        object empImage = sqlcmd.ExecuteScalar();
                        if (empImage != null)
                        {
                            byte[] imageBytes = (byte[])empImage;
                            string base64String = Convert.ToBase64String(imageBytes);
                            empimage.ImageUrl = "data:image/jpeg;base64," + base64String;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btneditsubmit_Click(object sender, EventArgs e)
        {
            btnupdate();
        }

        public void btnupdate()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("update register set Name='" + txtname.Text + "',OTP='" + txtotp.Text + "',Email='" + txtemail.Text + "',Image=@Image where EmpId='"+lblempid.Text+"' ", connection);
                    connection.Open();
                    int img1 = imgupload.PostedFile.ContentLength;
                    byte[] msdata1 = new byte[img1];
                    imgupload.PostedFile.InputStream.Read(msdata1, 0, img1);
                    cmd.Parameters.AddWithValue("@Image", msdata1);
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Updated..')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Failed..')</script>");
                    }

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        protected void editbtn_Click(object sender, EventArgs e)
        {
            editbtndetails();
        }
        public void editbtndetails()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM register WHERE EmpId = @EmpId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", lblempid.Text);
                        connection.Open();
                        using (SqlDataReader myReader = sqlcmd.ExecuteReader())
                        {
                            if (myReader.Read())
                            {
                                txtname.Text = myReader["Name"].ToString();
                                txtotp.Text = myReader["OTP"].ToString();
                                txtcompany.Text = myReader["CompanyName"].ToString();
                                txtemail.Text = myReader["Email"].ToString();
                                txtdesignation.Text = myReader["Designation"].ToString();
                                txtbranch.Text = myReader["BranchName"].ToString();
                                txtrole.Text = myReader["Roles"].ToString();
                                txtdevicecode.Text = myReader["DeviceCode"].ToString();
                                txtempid.Text = myReader["EmpId"].ToString();
                                editimg.ImageUrl = myReader["Image"].ToString();
                                if (Session["Image"] != null)
                                {
                                    imageshow();
                                }                             

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

        public void imageshow()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT Image FROM register WHERE EmpId = @EmpId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", Session["EmpId"]);

                        connection.Open();
                        object empImage = sqlcmd.ExecuteScalar();
                        if (empImage != null)
                        {
                            byte[] imageBytes = (byte[])empImage;
                            string base64String = Convert.ToBase64String(imageBytes);
                            editimg.ImageUrl = "data:image/jpeg;base64," + base64String;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AllDataBind()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE EmployeeId = @EmployeeId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmployeeId", lblempid.Text);
                        connection.Open();
                        using (SqlDataReader myReader = sqlcmd.ExecuteReader())
                        {
                            if (myReader.Read())
                            {
                                lblpername.Text = myReader["EmployeName"].ToString();
                                lblperdob.Text = myReader["DOB"].ToString();
                                lblpergender.Text = myReader["Gender"].ToString();
                                lblpermobile.Text = myReader["CellNo"].ToString();
                                lblperbloodgroup.Text = myReader["BloodGroup"].ToString();
                                lblpereducation.Text = myReader["EmployeeType"].ToString(); //education
                                lblpermaritialstatus.Text = myReader["PFStatus"].ToString(); //maritialStatus
                                lblperemail.Text = myReader["CompanyEmail"].ToString();   // personal mail
                                lblemcfathername.Text = myReader["CreatedBy"].ToString(); // father name
                                lblemcfatherphone.Text = myReader["EmpAdvanceSalary"].ToString();  //advance salary
                                lblemcmothername.Text = myReader["UpdatedBy"].ToString();  // mother name
                                lblemcmothermobile.Text = myReader["EmpAdvanceSalary"].ToString();
                                lblbankname.Text = myReader["Bankname"].ToString();
                                lblbankaccount.Text = myReader["Bankaccountno"].ToString();
                                lblbankifsc.Text = myReader["IFSC"].ToString();
                                lblbankpan.Text = myReader["PanNo"].ToString();
                                lblfamfathername.Text = myReader["CreatedBy"].ToString();
                                lblfamfatheraadhar.Text = myReader["EmpAdvanceSalary"].ToString();
                                lblfammobile.Text = myReader["EmpAdvanceSalary"].ToString();
                                txtpfsalary.Text = myReader["Salary"].ToString();
                                string pfnumber= myReader["Pfno"].ToString();
                                string esinumber = myReader["ESINo"].ToString();

                                string pfnoStatusid = myReader["PFStatusId"].ToString();
                                if (!string.IsNullOrEmpty(pfnoStatusid) && pfnoStatusid != "1")
                                {
                                    // If pfnoValue is not empty and not "0", select "YES"
                                    ListItem selectedItem = DropDownList1.Items.FindByText("YES");
                                    ListItem selectedDropDown5Item = DropDownList5.Items.FindByText("YES");
                                    if (selectedItem != null)
                                    {
                                        selectedItem.Selected = true;
                                        TextBox38.Text = pfnumber;
                                        TextBox40.Text = esinumber;
                                    }
                                    if (selectedDropDown5Item != null)
                                    {
                                        selectedDropDown5Item.Selected = true;
                                    }
                                }
                                else
                                {
                                    // If pfnoValue is empty or "0", select "NO"
                                    ListItem selectedItem = DropDownList1.Items.FindByText("NO");
                                    ListItem selectedItem5 = DropDownList5.Items.FindByText("NO");
                                    if (selectedItem != null)
                                    {
                                        selectedItem.Selected = true;
                                        TextBox38.Text = "";
                                        TextBox38.Attributes["placeholder"] = "No Pf";
                                        TextBox40.Attributes["placeholder"] = "No Esi";
                                    }
                                    if (selectedItem5 != null)
                                    {
                                        selectedItem5.Selected = true;
                                        TextBox40.Text = "";                                      
                                        TextBox40.Attributes["placeholder"] = "No Esi";
                                    }
                                }

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

        public void AllDataBindTextbox()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE EmployeeId = @EmployeeId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmployeeId", lblempid.Text);
                        connection.Open();
                        using (SqlDataReader myReader = sqlcmd.ExecuteReader())
                        {
                            if (myReader.Read())
                            {
                                 txtpername.Text = myReader["EmployeName"].ToString();                              
                                 DateTime dob = Convert.ToDateTime(myReader["DOB"]);
                                 string formattedDob = dob.ToString("yyyy-MM-dd");
                                 txtperdob.Text = formattedDob;                                                             
                                 txpertmobile.Text = myReader["CellNo"].ToString();
                                 txtperbloodgroup.Text = myReader["BloodGroup"].ToString();
                                 txtpereducation.Text = myReader["EmployeeType"].ToString();                               
                                 
                               
                                string gender = myReader["Gender"].ToString();
                                ListItem selectedItem = dddlgender.Items.FindByText(gender);
                                if (selectedItem != null)
                                {
                                    selectedItem.Selected = true;
                                }

                               string ms = myReader["PFStatus"].ToString();
                                ListItem sms = ddlmaritialstatus.Items.FindByText(ms);
                                if (sms != null)
                                {
                                    sms.Selected = true;
                                }

                                 txtperemail.Text = myReader["CompanyEmail"].ToString();
                                 txtfathernamepri.Text = myReader["CreatedBy"].ToString();
                                 txtfatheradar.Text = myReader["EmpAdvanceSalary"].ToString();
                                 txtfather2adhar.Text = myReader["EmpAdvanceSalary"].ToString();
                                 txtemcmothername.Text = myReader["UpdatedBy"].ToString();
                                 txtemcmotheraadhar1.Text = myReader["EmpAdvanceSalary"].ToString();
                                 txtemcmotheraadhar2.Text = myReader["EmpAdvanceSalary"].ToString();
                                 TextBox1.Text = myReader["Bankname"].ToString();
                                 TextBox2.Text = myReader["Bankaccountno"].ToString();
                                 TextBox3.Text = myReader["IFSC"].ToString();
                                 TextBox4.Text = myReader["PanNo"].ToString();

                                TextBox5.Text = myReader["CreatedBy"].ToString();
                                TextBox7.Text = myReader["EmpAdvanceSalary"].ToString();
                                TextBox8.Text = myReader["EmpEmergencyContactNum"].ToString();
                                TextBox9.Text = myReader["UpdatedBy"].ToString();
                                TextBox11.Text = myReader["EmpAdvanceSalary"].ToString();
                                TextBox12.Text = myReader["CellNo"].ToString();
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

        protected void btnpersonaldetailpopup_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("update Employees set EmployeName='" + txtpername.Text + "',DOB='" + txtperdob.Text + "',CellNo='" + txpertmobile.Text + "',BloodGroup='"+ txtperbloodgroup .Text+ "',EmployeeType='" + txtpereducation .Text+ "',PFStatus='"+ddlmaritialstatus.SelectedItem.Text+ "',Gender='"+dddlgender.SelectedItem.Text+"' where EmployeeId='" + lblempid.Text + "' ", connection);
                    connection.Open();                   
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Employee Details Updated..')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Failed..')</script>");
                    }

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnemccontactdetailsupdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("update Employees set CreatedBy='" + txtfathernamepri.Text + "',EmpAdvanceSalary='" + txtfatheradar.Text + "',UpdatedBy='" + txtemcmothername.Text + "',EmpAdvanceSalary='" + txtemcmotheraadhar1.Text + "' where EmployeeId='" + lblempid.Text + "' ", connection);
                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Family Details Updated..')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Failed..')</script>");
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnbankupdatepopup_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("update Employees set Bankname='" + TextBox1.Text + "',Bankaccountno='" + TextBox2.Text + "',IFSC='" + TextBox3.Text + "',PanNo='" + TextBox4.Text + "' where EmployeeId='" + lblempid.Text + "' ", connection);
                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Bank Details Updated..')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Failed..')</script>");
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnfamilyinfopopup_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("update Employees set CreatedBy='" + TextBox5.Text + "',EmpAdvanceSalary='" + TextBox7.Text + "',EmpAdvanceSalary='" + TextBox8.Text+ "',UpdatedBy='" + TextBox9.Text + "',EmpAdvanceSalary='" + TextBox11.Text + "' where EmployeeId='" + lblempid.Text + "' ", connection);
                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Family Member Details Updated..')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Failed..')</script>");
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public void ProjectList()
       {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM abprojects WHERE Id = @Id", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@Id", lblempid.Text);
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string projectName = row["PrName"].ToString();
                                string description = row["Descp"].ToString();
                                string projectLeader = row["Assign"].ToString();
                                string endDate = row["DndDate"].ToString();
                                string totalTasks = row["Total"].ToString();
                                string completedTasks = row["Completed"].ToString();

                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<div class='col-lg-4 col-sm-6 col-md-4 col-xl-3'>");
                                projectHtml.Append("<div class='card'>");
                                projectHtml.Append("<div class='card-body'>");
                                projectHtml.AppendFormat("<h4 class='project-title'>{0}</h4>", projectName);
                                projectHtml.AppendFormat("<small class='block text-ellipsis m-b-15'>");
                                projectHtml.AppendFormat("<span class='text-xs'>{0}</span> <span class='text-muted'>Completed Tasks, </span>", completedTasks);
                                projectHtml.AppendFormat("<span class='text-xs'>{0}</span> <span class='text-muted'>Total Tasks</span>", totalTasks);
                                projectHtml.Append("</small>");
                                projectHtml.AppendFormat("<p class='text-muted'>{0}</p>", description);
                                projectHtml.Append("<!-- Add other project details here -->");
                                projectHtml.Append("</div>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("</div>");
                                projectsContainer.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            }
                        }
                        else
                        {
                            // Display message if no projects are found
                            projectsContainer.Controls.Add(new LiteralControl("<p>No projects found.</p>"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PfDetails()
        {

        }
    }
}