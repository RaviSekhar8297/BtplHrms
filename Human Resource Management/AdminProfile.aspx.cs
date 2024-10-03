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
    public partial class AdminProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoginEmployeeDetails();
                // editbtndetails();
                imageshow();
                AllDataBindLabels();
                AllDataBindTextbox();
              //  ProjectList();

            }
        }


        protected void LoginEmployeeDetails()
        {
            try
            {
                if (Session["EmpId"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    var LoginWith = Session["Role"]?.ToString().Trim();

                    if (LoginWith == "Admin" || LoginWith == "SuperAdmin" || LoginWith == "Manager" || LoginWith == "Employee")
                    {
                        lblrole.Text = Session["Role"].ToString();
                        if (Session["Image"] != null)
                        {
                            imagedisplay();

                        }
                        else
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
                Response.Redirect("Error.aspx"); // Make sure Error.aspx exists
            }
        }

        public void imagedisplay()
        {

            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT Image FROM WebLogins WHERE EmpId = @EmpId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", Session["EmpId"]);

                        connection.Open();
                        object empImage = sqlcmd.ExecuteScalar();
                        if (empImage != null && empImage != DBNull.Value)
                        {
                            byte[] imageBytes = (byte[])empImage;
                            if (imageBytes.Length > 0)
                            {
                                string base64String = Convert.ToBase64String(imageBytes);
                                empimage.ImageUrl = "data:image/jpeg;base64," + base64String;
                            }
                            else
                            {
                                // Set a default empty image or handle the empty case as per your application logic
                                empimage.ImageUrl = "path/to/empty/image.jpg"; // Example: Default empty image path
                            }
                        }
                        else
                        {
                            // Handle the case where empImage is null or DBNull.Value
                            // Set a default empty image or handle the empty case as per your application logic
                            empimage.ImageUrl = "path/to/empty/image.jpg"; // Example: Default empty image path
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
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    try
                    {
                        byte[] msdata1 = null;

                        if (imgupload.HasFile)
                        {
                            int img1 = imgupload.PostedFile.ContentLength;
                            msdata1 = new byte[img1];
                            imgupload.PostedFile.InputStream.Read(msdata1, 0, img1);
                        }

                        // Update WebLogins table
                        var sql1 = "Update WebLogins set Name=@LastName, Phone=@CompanyCellNo, Email=@CompanyEmail" +
                                   (imgupload.HasFile ? ", Image=@Image" : "") + " where EmpId=@EmpId ";

                        SqlCommand cmd1 = new SqlCommand(sql1, connection);
                        cmd1.Parameters.AddWithValue("@LastName", txtname.Text);
                        cmd1.Parameters.AddWithValue("@CompanyCellNo", txtcompanymobile.Text);
                        cmd1.Parameters.AddWithValue("@CompanyEmail", txtemail.Text);
                        cmd1.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());

                        if (imgupload.HasFile)
                        {
                            cmd1.Parameters.AddWithValue("@Image", msdata1);
                        }

                        int rowsAffected1 = cmd1.ExecuteNonQuery();

                        // Update Employees table
                        var sql2 = "Update Employees set FirstName=@FirstName, CompanyCellNo=@CompanyCellNo, CompanyEmail=@CompanyEmail" +
                                   (imgupload.HasFile ? ", Image=@Image" : "") + " where EmpId=@EmpId";

                        SqlCommand cmd2 = new SqlCommand(sql2, connection);
                        cmd2.Parameters.AddWithValue("@FirstName", txtname.Text);
                        cmd2.Parameters.AddWithValue("@CompanyCellNo", txtcompanymobile.Text);
                        cmd2.Parameters.AddWithValue("@CompanyEmail", txtemail.Text);
                        cmd2.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());

                        if (imgupload.HasFile)
                        {
                            cmd2.Parameters.AddWithValue("@Image", msdata1);
                        }

                        int rowsAffected2 = cmd2.ExecuteNonQuery();

                        // Check if both updates were successful
                        if (rowsAffected1 > 0 && rowsAffected2 > 0)
                        {
                            Response.Write("<script>alert('Updated Successfully..')</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed..')</script>");
                        }


                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                        Response.Redirect("EmployeeProfile.aspx", false);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while updating the database.", ex);
                    }
                }

            }
            catch (Exception ex)
            {
                // Handle the exception (consider using a logging framework)
                Response.Write("<script>alert('An error occurred: " + ex.Message + "')</script>");
            }


        }

        public void editbtndetails()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM WebLogins WHERE EmpId = @EmpId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", lblempid.Text);
                        connection.Open();
                        using (SqlDataReader myReader = sqlcmd.ExecuteReader())
                        {
                            if (myReader.Read())
                            {
                                txtname.Text = myReader["Name"].ToString();
                                txtempid.Text = myReader["EmpId"].ToString();
                                txtdepartment.Text = myReader["DepartmentName"].ToString();
                                txtcompany.Text = myReader["CompanyName"].ToString();
                                txtDOJ.Text = myReader["DOJ"].ToString();
                                txtemail.Text = myReader["Email"].ToString();
                                txtdesignation.Text = myReader["Designation"].ToString();
                                txtbranch.Text = myReader["BranchName"].ToString();
                                txtrole.Text = myReader["Role"].ToString();
                                //  txtbranchcode.Text = myReader["BranchCode"].ToString();                               
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
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM WebLogins WHERE EmpId = @EmpId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());

                        connection.Open();
                        using (SqlDataReader reader = sqlcmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string Managername = reader["ReportName"].ToString();
                                lblreportername.Text = Managername;
                                TextBox5.Text = Managername;

                                //if (!reader.IsDBNull(reader.GetOrdinal("Image")))
                                //{
                                //    byte[] imageBytes = (byte[])reader["Image"];
                                //    string base64String = Convert.ToBase64String(imageBytes);
                                //    editimg.ImageUrl = "data:image/jpeg;base64," + base64String;
                                //}
                                //if (!reader.IsDBNull(reader.GetOrdinal("ReportImage")))
                                //{
                                //    byte[] imageBytes = (byte[])reader["ReportImage"];
                                //    string base64String = Convert.ToBase64String(imageBytes);
                                //    reporterimage.ImageUrl = "data:image/jpeg;base64," + base64String;
                                //}



                                if (!reader.IsDBNull(reader.GetOrdinal("Image")))
                                {
                                    byte[] imageBytes = (byte[])reader["Image"];
                                    if (imageBytes.Length > 0)
                                    {
                                        string base64String = Convert.ToBase64String(imageBytes);
                                        editimg.ImageUrl = "data:image/jpeg;base64," + base64String;
                                    }
                                    else
                                    {
                                        // Set a default empty image or handle the empty case as per your application logic
                                        editimg.ImageUrl = "path/to/empty/image.jpg"; // Example: Default empty image path
                                    }
                                }
                                else
                                {
                                    // Handle the case where the database field 'Image' is NULL
                                    // Set a default empty image or handle the empty case as per your application logic
                                    editimg.ImageUrl = "path/to/empty/image.jpg"; // Example: Default empty image path
                                }

                                if (!reader.IsDBNull(reader.GetOrdinal("ReportImage")))
                                {
                                    byte[] reportImageBytes = (byte[])reader["ReportImage"];
                                    if (reportImageBytes.Length > 0)
                                    {
                                        string base64String = Convert.ToBase64String(reportImageBytes);
                                        reporterimage.ImageUrl = "data:image/jpeg;base64," + base64String;
                                    }
                                    else
                                    {
                                        // Set a default empty image or handle the empty case as per your application logic
                                        reporterimage.ImageUrl = "Images/pngtree-lotus-flower-jpg-pink-lotus-flower-image_13023952.jpg"; // Example: Default empty image path
                                    }
                                }
                                else
                                {
                                    // Handle the case where the database field 'ReportImage' is NULL
                                    // Set a default empty image or handle the empty case as per your application logic
                                    reporterimage.ImageUrl = "Images/pngtree-lotus-flower-jpg-pink-lotus-flower-image_13023952.jpg"; // Example: Default empty image path
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

        public void AllDataBindLabels()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE EmpId = @EmployeeId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmployeeId", Session["EmpId"].ToString());
                        connection.Open();
                        using (SqlDataReader myReader = sqlcmd.ExecuteReader())
                        {
                            if (myReader.Read())
                            {
                                lblpername.Text = myReader["FirstName"].ToString();

                                DateTime dob = Convert.ToDateTime(myReader["DOB"]);
                                string formattedDOB = dob.ToString("yyyy-MM-dd");
                                lblperdob.Text = formattedDOB;
                                DateTime doj = Convert.ToDateTime(myReader["DOJ"]);
                                string formattedDOJ = doj.ToString("yyyy-MM-dd");

                                lbldoj.Text = formattedDOJ;
                                lblpergender.Text = myReader["Gender"].ToString();
                                lblpermobile.Text = myReader["PersonalCelloNo"].ToString();
                                lblperbloodgroup.Text = myReader["BloodGroup"].ToString();
                                lblpereducation.Text = myReader["EmployeType"].ToString(); //education
                                lblpermaritialstatus.Text = myReader["PFStatus"].ToString(); //maritialStatus
                                lblperemail.Text = myReader["PersonalEmail"].ToString();   // personal mail
                                lblemcfathername.Text = myReader["FatherName"].ToString(); // father name
                                lblemcfatherphone.Text = myReader["FatherMobile"].ToString();  //advance salary
                                lblemcmothername.Text = myReader["MotherName"].ToString();  // mother name
                                lblemcmothermobile.Text = myReader["MotherMobile"].ToString();
                                lblbankname.Text = myReader["BankName"].ToString();
                                lblbankaccount.Text = myReader["BankAccNo"].ToString();
                                lblbankifsc.Text = myReader["IFSC"].ToString();
                                lblbankpan.Text = myReader["PanNo"].ToString();

                                lblnomineename.Text = myReader["NomineeName"].ToString();
                                lblnominerelation.Text = myReader["NomineeRelation"].ToString();
                                lblnomineeaadhar.Text = myReader["NomineeAadhar"].ToString();
                                lblnomineemobile.Text = myReader["NomineeMobile"].ToString();
                                txtpfsalary.Text = myReader["SalaryMonth"].ToString();


                                string pfst = myReader["PfStatusId"].ToString();
                                string pfNum = myReader["PfNo"].ToString();

                                string esist = myReader["Esino"].ToString();
                                string esinum = myReader["Esino"].ToString();
                                if (pfst == "2")
                                {
                                    TextBox39.Text = "No";
                                    TextBox40.Text = "0";
                                    TextBox41.Text = "No";
                                    TextBox42.Text = "0";
                                    TextBox6.Text = "12";
                                    TextBox7.Text = "12";
                                    TextBox8.Text = "0.75";
                                    TextBox9.Text = "3.25";
                                    double value1 = Convert.ToDouble(TextBox6.Text);
                                    double value2 = Convert.ToDouble(TextBox7.Text);
                                    double total = value1 + value2;
                                    TextBox36.Text = total.ToString();

                                    double value11 = Convert.ToDouble(TextBox8.Text);
                                    double value21 = Convert.ToDouble(TextBox9.Text);
                                    double total11 = value11 + value21;
                                    TextBox37.Text = total11.ToString();

                                }
                                else
                                {
                                    TextBox39.Text = "Yes";
                                    TextBox40.Text = pfNum.ToString();
                                    TextBox41.Text = "Yes";
                                    TextBox42.Text = esinum.ToString();
                                    TextBox6.Text = "12";
                                    TextBox7.Text = "12";
                                    TextBox8.Text = "0.75";
                                    TextBox9.Text = "3.25";
                                    double value1 = Convert.ToDouble(TextBox6.Text);
                                    double value2 = Convert.ToDouble(TextBox7.Text);
                                    double total = value1 + value2;
                                    TextBox36.Text = total.ToString();

                                    double value11 = Convert.ToDouble(TextBox8.Text);
                                    double value21 = Convert.ToDouble(TextBox9.Text);
                                    double total11 = value11 + value21;
                                    TextBox37.Text = total11.ToString();
                                }

                                // education details

                                Label1.Text = myReader["EducationUniversity1"].ToString();
                                Label2.Text = myReader["HighEducation1"].ToString();
                                Label3.Text = myReader["EducationYear1"].ToString();

                                Label11.Text = myReader["EducationUniversity2"].ToString();
                                Label12.Text = myReader["HighEducation2"].ToString();
                                Label14.Text = myReader["EducationYear2"].ToString();

                                Label4.Text = myReader["LastOneCompanyName"].ToString();
                                Label5.Text = myReader["LastOnePrevSalaryAnnum"].ToString();
                                Label6.Text = myReader["LastTwoCompanyName"].ToString();
                                Label7.Text = myReader["LastTwoPrevSalaryAnnum"].ToString();


                                lblempname.Text = myReader["FirstName"].ToString();
                                lbldesignation.Text = myReader["Designation"].ToString();
                                lbldepartment.Text = myReader["Department"].ToString();
                                lblempid.Text = myReader["EmpId"].ToString();
                                lblcompany.Text = myReader["Company"].ToString();
                                lblbranch.Text = myReader["Branch"].ToString();
                                lblemail.Text = myReader["CompanyEmail"].ToString();
                                lbldevicecode.Text = myReader["BranchCode"].ToString();
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
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE EmpId = @EmployeeId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmployeeId", Session["EmpId"].ToString());
                        connection.Open();
                        using (SqlDataReader myReader = sqlcmd.ExecuteReader())
                        {
                            if (myReader.Read())
                            {
                                txtname.Text = myReader["FirstName"].ToString();
                                txtsurname.Text = myReader["LastName"].ToString();
                                txtempid.Text = myReader["EmpId"].ToString();
                                txtdepartment.Text = myReader["Department"].ToString();
                                txtcompany.Text = myReader["Company"].ToString();
                                DateTime doj = Convert.ToDateTime(myReader["DOJ"]);
                                string formattedDoj = doj.ToString("yyyy-MM-dd");
                                txtDOJ.Text = formattedDoj;
                                txtperDOJ.Text = formattedDoj;
                                txtemail.Text = myReader["CompanyEmail"].ToString();
                                txtdesignation.Text = myReader["Designation"].ToString();
                                txtbranch.Text = myReader["Branch"].ToString();
                                //   txtrole
                                txtbranchcode.Text = myReader["BranchCode"].ToString();

                                //Personal Information

                                txtpername.Text = myReader["FirstName"].ToString();
                                DateTime dob = Convert.ToDateTime(myReader["DOB"]);
                                string formattedDob = dob.ToString("yyyy-MM-dd");
                                txtperdob.Text = formattedDob;
                                txpertmobile.Text = myReader["PersonalCelloNo"].ToString();
                                txtperbloodgroup.Text = myReader["BloodGroup"].ToString();
                                txtemployeetype.Text = myReader["EmployeType"].ToString();

                                string ms = myReader["MaritialStatus"].ToString();
                                ListItem sms = ddlmaritialstatus.Items.FindByText(ms);
                                if (sms != null)
                                {
                                    sms.Selected = true;
                                }

                                string gender = myReader["Gender"].ToString();
                                ListItem selectedItem = dddlgender.Items.FindByText(gender);
                                if (selectedItem != null)
                                {
                                    selectedItem.Selected = true;
                                }
                                txtperemail.Text = myReader["PersonalEmail"].ToString();

                                //Emergancy Contact Details
                                txtfathernamepri.Text = myReader["FatherName"].ToString();
                                txtfathernamerelation1.Text = myReader["Relation1"].ToString();
                                txtfatherphone.Text = myReader["FatherMobile"].ToString();
                                txtfather1adhar.Text = myReader["FatherAadhar"].ToString();

                                txtemcmothername.Text = myReader["MotherName"].ToString();
                                txtmotherrelation.Text = myReader["Relation2"].ToString();
                                txtmotherphone.Text = myReader["MotherMobile"].ToString();
                                txtemcmotheraadhar2.Text = myReader["MotherAdhar"].ToString();

                                //Bank Information

                                txtbankname.Text = myReader["BankName"].ToString();
                                txtbankaccountnumber.Text = myReader["BankAccNo"].ToString();
                                txtifsc.Text = myReader["IFSC"].ToString();
                                txtpanno.Text = myReader["PanNo"].ToString();


                                // Education Information Bind to Textboxes
                                TextBox14.Text = myReader["EducationUniversity1"].ToString();
                                TextBox15.Text = myReader["HighEducation1"].ToString();
                                TextBox16.Text = myReader["EducationYear1"].ToString();
                                TextBox17.Text = myReader["EducationPerventage1"].ToString();
                                TextBox20.Text = myReader["EducationUniversity2"].ToString();
                                TextBox21.Text = myReader["HighEducation2"].ToString();
                                TextBox22.Text = myReader["EducationYear2"].ToString();
                                TextBox23.Text = myReader["EducationPerventage2"].ToString();


                                TextBox26.Text = myReader["LastOneCompanyName"].ToString();
                                TextBox27.Text = myReader["LastOnePrevSalaryAnnum"].ToString();
                                txtlastonedesignation.Text = myReader["Last1CompanyDesignation"].ToString();

                                TextBox31.Text = myReader["LastTwoCompanyName"].ToString();
                                TextBox32.Text = myReader["LastTwoPrevSalaryAnnum"].ToString();
                                txtlasttwodesignation.Text = myReader["Last2CompanyDesignation"].ToString();

                                txtrole.Text = Session["Role"].ToString();
                                txtcompanymobile.Text = myReader["CompanyCellNo"].ToString();
                                TextBox1.Text = myReader["Last1CompanyFromYear"].ToString();
                                TextBox2.Text = myReader["Last1CompanyToYear"].ToString();
                                TextBox3.Text = myReader["Last1CompanyMobileNumber"].ToString();

                                TextBox4.Text = myReader["Last2CompanyFromYear"].ToString();
                                TextBox13.Text = myReader["Last2CompanyToYear"].ToString();
                                TextBox18.Text = myReader["Last2CompanyMobileYear"].ToString();

                                // nominee details
                                TextBox19.Text = myReader["NomineeName"].ToString();
                                TextBox24.Text = myReader["NomineeRelation"].ToString();
                                TextBox28.Text = myReader["NomineeMobile"].ToString();
                                TextBox25.Text = myReader["NomineeAadhar"].ToString();
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
                    SqlCommand cmd = new SqlCommand("update Employees set DOB='" + txtperdob.Text + "',PersonalCelloNo='" + txpertmobile.Text + "'," +
                        "BloodGroup='" + txtperbloodgroup.Text + "',EmployeType='" + txtemployeetype.Text + "',MaritialStatus='" + ddlmaritialstatus.SelectedItem.Text + "'," +
                        "Gender='" + dddlgender.SelectedItem.Text + "' where EmpId='" + Session["EmpId"].ToString() + "' ", connection);
                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Employee Personal Details Updated Successfully..')</script>");
                        AllDataBindLabels();
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

        protected void btnemccontactdetailsupdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Update Employees set FatherName='" + txtfathernamepri.Text + "',Relation1='" + txtfathernamerelation1.Text + "'," +
                        "FatherAadhar='" + txtfather1adhar.Text + "',FatherMobile='" + txtfatherphone.Text + "',MotherName='" + txtemcmothername.Text + "',Relation2='" + txtmotherrelation.Text + "',MotherAdhar='" + txtemcmotheraadhar2.Text + "',MotherMobile='" + txtmotherphone.Text + "' where EmpId='" + Session["EmpId"].ToString() + "' ", connection);
                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Family Details Updated..')</script>");
                        AllDataBindLabels();
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
                    SqlCommand cmd = new SqlCommand("update Employees set BankName='" + txtbankname.Text + "',BankAccNo='" + txtbankaccountnumber.Text + "',IFSC='" + txtifsc.Text + "',PanNo='" + txtpanno.Text + "' where EmpId='" + Session["EmpId"] + "' ", connection);
                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Bank Details Updated..')</script>");
                        AllDataBindLabels();
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
                    SqlCommand cmd = new SqlCommand("Update Employees set NomineeName='" + TextBox19.Text + "',NomineeRelation='" + TextBox24.Text + "',NomineeAadhar='" + TextBox25.Text + "',NomineeMobile='" + TextBox28.Text + "' where EmployeeId='" + Session["EmpId"].ToString() + "' ", connection);
                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Family Member Details Updated..')</script>");
                        AllDataBindLabels();
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

        //public void ProjectList()
        //{

        //    try
        //    {
        //        using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
        //        {
        //            using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM ProjectsList WHERE Status = '0' and AssignDepartment=@AssignDepartment", connection))
        //            {
        //                sqlcmd.Parameters.AddWithValue("@AssignDepartment", Session["DepartmentName"].ToString());
        //                connection.Open();
        //                SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
        //                DataSet ds1 = new DataSet();
        //                da1.Fill(ds1);
        //                if (ds1.Tables[0].Rows.Count > 0)
        //                {
        //                    foreach (DataRow row in ds1.Tables[0].Rows)
        //                    {
        //                        string projectName = row["ProjectName"].ToString();
        //                        string description = row["Description"].ToString();
        //                        string AssignBy = row["AssignBy"].ToString();
        //                        // string TargetDate = row["TargetDate"].ToString();

        //                        DateTime? cdob = row["TargetDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["TargetDate"]);
        //                        string TargetDate = cdob.HasValue ? cdob.Value.ToString("yyyy-MM-dd") : string.Empty;
        //                        //string totalTasks = row["Total"].ToString();
        //                        //string completedTasks = row["Completed"].ToString();

        //                        StringBuilder projectHtml = new StringBuilder();
        //                        projectHtml.Append("<div class='col-lg-4 col-sm-6 col-md-4 col-xl-3'>");
        //                        projectHtml.Append("<div class='card'>");
        //                        projectHtml.Append("<div class='card-body'>");
        //                        projectHtml.AppendFormat("<h4 class='project-title'>" + projectName + "</h4>");
        //                        projectHtml.AppendFormat("<small class='block text-ellipsis m-b-15'>");
        //                        projectHtml.AppendFormat("<span class='text-xs'></span> <span class='text-muted'>Assign By :  </span>" + AssignBy + "<br/>");
        //                        projectHtml.AppendFormat("<span class='text-xs'></span> <span class='text-muted'>End Date : </span>" + TargetDate + " ");
        //                        projectHtml.Append("</small>");
        //                        projectHtml.AppendFormat("<p class='text-muted'></p>" + description + " ");
        //                        projectHtml.Append("<!-- Add other project details here -->");
        //                        projectHtml.Append("</div>");
        //                        projectHtml.Append("</div>");
        //                        projectHtml.Append("</div>");
        //                        projectsContainer.Controls.Add(new LiteralControl(projectHtml.ToString()));
        //                    }
        //                }
        //                else
        //                {
        //                    projectsContainer.Controls.Add(new LiteralControl("<p>No projects found.</p>"));
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        protected void btneducationinfopopupupdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Update Employees set HighEducation1 = @HighEducation1, EducationYear1 = @EducationYear1, EducationUniversity1 = @EducationUniversity1, EducationPerventage1 = @EducationPerventage1, HighEducation2 = @HighEducation2, EducationYear2 = @EducationYear2, EducationUniversity2 = @EducationUniversity2, EducationPerventage2 = @EducationPerventage2 where EmpId = @EmpId", connection))
                    {
                        // Set the parameters with appropriate data types

                        cmd.Parameters.AddWithValue("@EducationUniversity1", TextBox14.Text);
                        cmd.Parameters.AddWithValue("@HighEducation1", TextBox15.Text);
                        cmd.Parameters.AddWithValue("@EducationYear1", int.Parse(TextBox16.Text));
                        cmd.Parameters.AddWithValue("@EducationPerventage1", TextBox17.Text);
                        cmd.Parameters.AddWithValue("@EducationUniversity2", TextBox20.Text);
                        cmd.Parameters.AddWithValue("@HighEducation2", TextBox21.Text);
                        cmd.Parameters.AddWithValue("@EducationYear2", int.Parse(TextBox22.Text));
                        cmd.Parameters.AddWithValue("@EducationPerventage2", TextBox23.Text);
                        cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());

                        connection.Open();
                        int i = cmd.ExecuteNonQuery();
                        connection.Close();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Education Details Updated Successfully..')</script>");
                            AllDataBindLabels();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed..')</script>");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnexpupdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection strcon = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    strcon.Open();
                    SqlCommand selectCmd = new SqlCommand("SELECT * FROM [Employees] WHERE status = 1 AND EmpId = @EmpId", strcon);
                    selectCmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());

                    using (SqlDataReader sdr = selectCmd.ExecuteReader())
                    {
                        if (sdr.Read())
                        {
                            string EmpExpType = sdr["EmpExpType"].ToString();

                            if (EmpExpType == "Expireinced")
                            {
                                sdr.Close();

                                using (SqlCommand cmd = new SqlCommand("Update Employees set LastOneCompanyName = @LastOneCompanyName, LastOnePrevSalaryAnnum = @LastOnePrevSalaryAnnum," +
              " Last1CompanyDesignation = @Last1CompanyDesignation, Last1CompanyFromYear = @Last1CompanyFromYear, Last1CompanyToYear = @Last1CompanyToYear," +
              " Last1CompanyMobileNumber = @Last1CompanyMobileNumber," +
              " LastTwoCompanyName = @LastTwoCompanyName, LastTwoPrevSalaryAnnum = @LastTwoPrevSalaryAnnum,Last2CompanyDesignation = @Last2CompanyDesignation, Last2CompanyFromYear = @Last2CompanyFromYear,Last2CompanyToYear = @Last2CompanyToYear, Last2CompanyMobileYear = @Last2CompanyMobileYear where EmpId = @EmpId", strcon))
                                {

                                    cmd.Parameters.AddWithValue("@LastOneCompanyName", TextBox26.Text);
                                    cmd.Parameters.AddWithValue("@LastOnePrevSalaryAnnum", TextBox27.Text);
                                    cmd.Parameters.AddWithValue("@Last1CompanyDesignation", txtlastonedesignation.Text);
                                    cmd.Parameters.AddWithValue("@Last1CompanyFromYear", TextBox1.Text);
                                    cmd.Parameters.AddWithValue("@Last1CompanyToYear", TextBox2.Text);
                                    cmd.Parameters.AddWithValue("@Last1CompanyMobileNumber", TextBox3.Text);

                                    cmd.Parameters.AddWithValue("@LastTwoCompanyName", TextBox31.Text);
                                    cmd.Parameters.AddWithValue("@LastTwoPrevSalaryAnnum", TextBox32.Text);
                                    cmd.Parameters.AddWithValue("@Last2CompanyDesignation", txtlasttwodesignation.Text);
                                    cmd.Parameters.AddWithValue("@Last2CompanyFromYear", TextBox4.Text);
                                    cmd.Parameters.AddWithValue("@Last2CompanyToYear", TextBox13.Text);
                                    cmd.Parameters.AddWithValue("@Last2CompanyMobileYear", TextBox18.Text);

                                    cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());

                                    strcon.Open();
                                    int i = cmd.ExecuteNonQuery();
                                    strcon.Close();
                                    if (i > 0)
                                    {
                                        Response.Write("<script>alert('Experience Details Updated Successfully..')</script>");
                                        AllDataBindLabels();
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('Failed..')</script>");
                                    }

                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('You are Freser ......')</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Employee not found.')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }

        }


    }
}