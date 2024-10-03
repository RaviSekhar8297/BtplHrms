using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace Human_Resource_Management
{
    public partial class EmployeeAddEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCompany2();
                BindShiftData();
                BindPFTypeData();
            }
        }
        public void BindCompany2()
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "SELECT CompanyId,CompanyName FROM Companies ";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);


                        // add employee company
                        ddladdcompany.DataSource = dt;
                        ddladdcompany.DataValueField = "CompanyId";
                        ddladdcompany.DataTextField = "CompanyName";
                        ddladdcompany.DataBind();
                        sqlConn.Close();

                        ddladdcompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Company--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindShiftData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ShiftId, ShiftName FROM ShiftTable WHERE ShiftStatus = '1'";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    ddladdshifttype.DataSource = reader;
                    ddladdshifttype.DataTextField = "ShiftName";
                    ddladdshifttype.DataValueField = "ShiftId";
                    ddladdshifttype.DataBind();

                    // Optionally add a default item
                    ddladdshifttype.Items.Insert(0, new ListItem("--Select Shift--", "0"));
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Response.Write("An error occurred: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private void BindPFTypeData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT PFID, PfType FROM PFTypeTable";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    ddlpfstatus.DataSource = reader;
                    ddlpfstatus.DataTextField = "PfType";
                    ddlpfstatus.DataValueField = "PFID";
                    ddlpfstatus.DataBind();

                    // Optionally add a default item
                    ddlpfstatus.Items.Insert(0, new ListItem("--Select PF Status --", "0"));
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Response.Write("An error occurred: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected void ddladdcompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "select Branch_Id, BranchName, BranchCode from Branch where [CompanyId] = '" + ddladdcompany.SelectedValue + "' ";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddladdbranch.DataSource = dt;
                        ddladdbranch.DataValueField = "Branch_Id";
                        ddladdbranch.DataTextField = "BranchName";
                        ddladdbranch.DataBind();
                        sqlConn.Close();
                        ddladdbranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
                    }
                }
                HiddenField2.Value = "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddladdbranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        // Make sure ddladdbranch.SelectedValue contains the BranchId, not the BranchCode
                        sqlCmd.CommandText = "SELECT DeptId, Department FROM Department WHERE BranchId = @BranchId";
                        sqlCmd.Parameters.AddWithValue("@BranchId", ddladdbranch.SelectedValue);
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt); // Fill the DataTable before setting it as DataSource
                        ddladddepartment.DataSource = dt;
                        ddladddepartment.DataValueField = "DeptId";
                        ddladddepartment.DataTextField = "Department";
                        ddladddepartment.DataBind();
                        sqlConn.Close();
                        ddladddepartment.Items.Insert(0, new ListItem("-- Select Department --", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        protected void ddlpfstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id,Name1 FROM PFTAX where PFId='" + ddlpfstatus.SelectedValue + "'";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    ddlpfnpf.DataSource = reader;
                    ddlpfnpf.DataTextField = "Name1";
                    ddlpfnpf.DataValueField = "Id";
                    ddlpfnpf.DataBind();
                    ddlpfnpf.Items.Insert(0, new ListItem("--Select PF Type--", "0"));
                }
                catch (Exception ex)
                {
                    Response.Write("An error occurred: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            if (ddlpfstatus.SelectedValue == "1")
            {
                pfnumberdiv.Visible = true;
            }
            else if (ddlpfstatus.SelectedValue == "2")
            {
                pfnumberdiv.Visible = false;
            }
        }

        protected void ddladdshifttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM ShiftTable WHERE ShiftStatus = '1' and ShiftId='" + ddladdshifttype.SelectedItem.Value + "'", connection))
                    {
                        connection.Open();
                        using (SqlDataReader myReader = sqlcmd.ExecuteReader())
                        {
                            if (myReader.Read())
                            {
                                string ddlstatus = ddladdshifttype.SelectedItem.Value;
                                if (ddlstatus == "1")
                                {
                                    txtaddshift1.Text = myReader["Shift1Time"].ToString();
                                    txtaddshift2.Text = "0";
                                    txtaddshift3.Text = "0";

                                }
                                else
                                {
                                    txtaddshift1.Text = myReader["Shift1Time"].ToString();
                                    txtaddshift2.Text = myReader["Shift2Time"].ToString();
                                    txtaddshift3.Text = myReader["Shift3Time"].ToString();
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


        protected void ddlmaritialstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maritialstatus = ddlmaritialstatus.SelectedItem.Text;
            if (maritialstatus == "Single")
            {
                spoucediv.Visible = false;
                chaild1div.Visible = false;
                chaild2div.Visible = false;
            }
            else
            {
                spoucediv.Visible = true;
                chaild1div.Visible = true;
                chaild2div.Visible = true;
            }
        }

        protected void txtSalaryAnnum_TextChanged(object sender, EventArgs e)
        {
            int salary1annum = Convert.ToInt32(txtSalaryAnnum.Text);
            int salary1month = salary1annum / 12;
            txtSalaryMonth.Text = salary1month.ToString();
        }
        protected void ddlempextype_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ddlex = ddlempextype.SelectedItem.Text;
            if (ddlex == "Fresher")
            {
                exyearsdiv.Visible = false;
                exlast1compdiv.Visible = false;
                exlast1desidiv.Visible = false;
                exlast1compfromyeardiv.Visible = false;
                exlast1comptoyeardiv.Visible = false;
                exlast1compsalaryannumdiv.Visible = false;
                exlast1compsalarymonthdiv.Visible = false;
                exlast1compmobilediv.Visible = false;
                exlast2compdiv.Visible = false;
                exlast2compdesidiv.Visible = false;
                exlast2compfromyeardiv.Visible = false;
                exlast2comptoyeardiv.Visible = false;
                exlast2compsalaryannumdiv.Visible = false;
                exlast2compsalarymonthdiv.Visible = false;
                exlast2compmobilehdiv.Visible = false;
            }
            else
            {
                exyearsdiv.Visible = true;
                exlast1compdiv.Visible = true;
                exlast1desidiv.Visible = true;
                exlast1compfromyeardiv.Visible = true;
                exlast1comptoyeardiv.Visible = true;
                exlast1compsalaryannumdiv.Visible = true;
                exlast1compsalarymonthdiv.Visible = true;
                exlast1compmobilediv.Visible = true;
                exlast2compdiv.Visible = true;
                exlast2compdesidiv.Visible = true;
                exlast2compfromyeardiv.Visible = true;
                exlast2comptoyeardiv.Visible = true;
                exlast2compsalaryannumdiv.Visible = true;
                exlast2compsalarymonthdiv.Visible = true;
                exlast2compmobilehdiv.Visible = true;

            }
        }

        protected void txtlast1salaryannum_TextChanged(object sender, EventArgs e)
        {
            int salary1annum = Convert.ToInt32(txtlast1salaryannum.Text);
            int salary1month = salary1annum / 12;
            txtlast1salarymonth.Text = salary1month.ToString();
        }
        protected void btnaddemployee_Click(object sender, EventArgs e)
        {
            try
            {
                string connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    string insertQuery = @"
            INSERT INTO [dbo].[Employees]
            (
                [Salutation], [FirstName], [LastName], [Image], [EmployeeCode], [DOB], [DOJ], [Gender], [Company],
                [Branch], [Department], [Designation], [CompanyId], [BranchId], [DepartmentId], [EmployeType], [EmpTypeId],
                [CompanyEmail], [PersonalEmail], [CompanyCellNo], [PersonalCelloNo], [PresentAddress], [PermanentAddress],
                [BloodGroup], [Status], [BankName], [BankAccNo], [IFSC], [PFStatus], [PfStatusId], [PfNo], [Esino],
                [PanNo], [Shift], [ShiftId], [Tocken], [EmpActivaDate], [CasualLeaves], [TaxId], [HighEducation1],
                [EducationYear1], [EducationUniversity1], [HighEducation2], [EducationYear2], [EducationUniversity2],
                [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [UpdatedCount], [DeleteBy], [DeletedDate],
                [DeletedStatus], [RestoredBy], [RestoredDate], [MaritialStatus], [Relation1], [FatherName], [FatherAadhar],
                [Relation2], [MotherName], [MotherAdhar], [Chaild1Name], [Chaild2Name], [EmpExpType], [ExptSalaryAnnum],
                [SalaryAnnum], [SalaryMonth], [Experience], [LastOneCompanyName], [LastOnePrevSalaryAnnum], [LastOnePrevSalaryMonth],
                [LastTwoCompanyName], [LastTwoPrevSalaryAnnum], [LastTwoPrevSalaryMonth], [EditDetailsStatus], [EmpInActiveDate],
                [FatherMobile], [MotherMobile], [BranchCode], [EducationPerventage1], [EducationPerventage2], [Last1CompanyDesignation],
                [Last2CompanyDesignation], [Last1CompanyFromYear], [Last1CompanyToYear], [Last2CompanyFromYear], [Last2CompanyToYear],
                [Last1CompanyMobileNumber], [Last2CompanyMobileYear], [NomineeName], [NomineeRelation], [SpouseName], [NomineeMobile],
                [NomineeAadhar], [ShiftType], [Shift2], [Shift3]
            )
            VALUES
            (
                @Salutation, @FirstName, @LastName, @Image, @EmployeeCode, @DOB, @DOJ, @Gender, @Company, @Branch, @Department,
                @Designation, @CompanyId, @BranchId, @DepartmentId, @EmployeType, @EmpTypeId, @CompanyEmail, @PersonalEmail,
                @CompanyCellNo, @PersonalCelloNo, @PresentAddress, @PermanentAddress, @BloodGroup, @Status, @BankName, @BankAccNo,
                @IFSC, @PFStatus, @PfStatusId, @PfNo, @Esino, @PanNo, @Shift, @ShiftId, @Tocken, @EmpActivaDate, @CasualLeaves,
                @TaxId, @HighEducation1, @EducationYear1, @EducationUniversity1, @HighEducation2, @EducationYear2,
                @EducationUniversity2, @CreatedBy, @CreatedDate, @UpdatedBy, @UpdatedDate, @UpdatedCount, @DeleteBy,
                @DeletedDate, @DeletedStatus, @RestoredBy, @RestoredDate, @MaritialStatus, @Relation1, @FatherName, @FatherAadhar,
                @Relation2, @MotherName, @MotherAdhar, @Chaild1Name, @Chaild2Name, @EmpExpType, @ExptSalaryAnnum, @SalaryAnnum,
                @SalaryMonth, @Experience, @LastOneCompanyName, @LastOnePrevSalaryAnnum, @LastOnePrevSalaryMonth,
                @LastTwoCompanyName, @LastTwoPrevSalaryAnnum, @LastTwoPrevSalaryMonth, @EditDetailsStatus, @EmpInActiveDate,
                @FatherMobile, @MotherMobile, @BranchCode, @EducationPerventage1, @EducationPerventage2, @Last1CompanyDesignation,
                @Last2CompanyDesignation, @Last1CompanyFromYear, @Last1CompanyToYear, @Last2CompanyFromYear, @Last2CompanyToYear,
                @Last1CompanyMobileNumber, @Last2CompanyMobileYear, @NomineeName, @NomineeRelation, @SpouseName, @NomineeMobile,
                @NomineeAadhar, @ShiftType, @Shift2, @Shift3
            );";

                    using (SqlCommand sqlCmd = new SqlCommand(insertQuery, sqlConn))
                    {
                        sqlCmd.Parameters.AddWithValue("@Salutation", ddlsalutation.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@FirstName", txtaddsurname.Text);
                        sqlCmd.Parameters.AddWithValue("@LastName", txtaddlastname.Text);

                        int branchId = Convert.ToInt32(ddladdbranch.SelectedValue);
                        string branchCode = (branchId + 100).ToString();
                        string employeeCode = branchCode;

                        // int img1 = addimage.PostedFile.ContentLength;
                        // byte[] msdata1 = new byte[img1];
                        // addimage.PostedFile.InputStream.Read(msdata1, 0, img1);

                        // // Convert byte array to base64 string
                        // string base64String = Convert.ToBase64String(msdata1);


                        // // Set @Image parameter
                        //// sqlCmd.Parameters.AddWithValue("@Image", base64String);
                        // sqlCmd.Parameters.AddWithValue("@Image", base64String);



                        int imgLength = addimage.PostedFile.ContentLength;
                        byte[] imageData = new byte[imgLength];
                        addimage.PostedFile.InputStream.Read(imageData, 0, imgLength);

                        SqlParameter imageParam = new SqlParameter("@Image", SqlDbType.Image);
                        imageParam.Value = imageData;
                        sqlCmd.Parameters.Add(imageParam);


                        sqlCmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                        sqlCmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(txtadddob.Text));
                        sqlCmd.Parameters.AddWithValue("@DOJ", Convert.ToDateTime(txtadddoj.Text));
                        sqlCmd.Parameters.AddWithValue("@Gender", ddlgender.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@Company", ddladdcompany.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@Branch", ddladdbranch.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@BranchId", ddladdbranch.SelectedValue);
                        sqlCmd.Parameters.AddWithValue("@Department", ddladddepartment.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@Designation", TextBox4.Text); // Ensure TextBox4.Text has value
                        sqlCmd.Parameters.AddWithValue("@CompanyId", ddladdcompany.SelectedValue);
                        sqlCmd.Parameters.AddWithValue("@DepartmentId", ddladddepartment.SelectedValue);
                        sqlCmd.Parameters.AddWithValue("@EmployeType", ddlemptype.SelectedItem.Text);

                        int empTypeId = 0;
                        if (int.TryParse(ddlemptype.SelectedValue, out empTypeId))
                        {
                            sqlCmd.Parameters.AddWithValue("@EmpTypeId", empTypeId);
                        }
                        else
                        {
                            sqlCmd.Parameters.AddWithValue("@EmpTypeId", DBNull.Value);
                        }

                        sqlCmd.Parameters.AddWithValue("@CompanyEmail", TextBox5.Text);
                        sqlCmd.Parameters.AddWithValue("@PersonalEmail", TextBox6.Text);
                        sqlCmd.Parameters.AddWithValue("@CompanyCellNo", txtCompanyCellNo.Text);
                        sqlCmd.Parameters.AddWithValue("@PersonalCelloNo", txtPersonalCelloNo.Text);
                        sqlCmd.Parameters.AddWithValue("@PresentAddress", txtPresentAddress.Text);
                        sqlCmd.Parameters.AddWithValue("@PermanentAddress", txtPermanentAddress.Text);
                        sqlCmd.Parameters.AddWithValue("@BloodGroup", ddlbloodgroup.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@Status", 1);
                        sqlCmd.Parameters.AddWithValue("@BankName", txtBankName.Text);
                        sqlCmd.Parameters.AddWithValue("@BankAccNo", txtBankAccNo.Text);
                        sqlCmd.Parameters.AddWithValue("@IFSC", txtIFSC.Text);
                        sqlCmd.Parameters.AddWithValue("@PFStatus", ddlpfstatus.SelectedItem.Text);

                        if (ddlpfstatus.SelectedValue == "1")
                        {
                            sqlCmd.Parameters.AddWithValue("@PfNo", txtPfNo.Text);
                            sqlCmd.Parameters.AddWithValue("@Esino", txtEsino.Text);
                        }
                        else if (ddlpfstatus.SelectedValue == "2")
                        {
                            sqlCmd.Parameters.AddWithValue("@PfNo", "No");
                            sqlCmd.Parameters.AddWithValue("@Esino", "No");
                        }

                        sqlCmd.Parameters.AddWithValue("@PfStatusId", Convert.ToInt32(ddlpfstatus.SelectedValue));
                        sqlCmd.Parameters.AddWithValue("@PanNo", txtPanNo.Text);
                        sqlCmd.Parameters.AddWithValue("@Shift", txtaddshift3.Text);
                        sqlCmd.Parameters.AddWithValue("@ShiftId", Convert.ToInt32(ddladdshifttype.SelectedValue));
                        sqlCmd.Parameters.AddWithValue("@Tocken", 123456);
                        sqlCmd.Parameters.AddWithValue("@EmpActivaDate", DateTime.Now);
                        sqlCmd.Parameters.AddWithValue("@CasualLeaves", 12);
                        sqlCmd.Parameters.AddWithValue("@TaxId", 123);
                        sqlCmd.Parameters.AddWithValue("@HighEducation1", txtHighEducation1.Text);
                        sqlCmd.Parameters.AddWithValue("@EducationYear1", Convert.ToInt32(txtEducationYear1.Text));
                        sqlCmd.Parameters.AddWithValue("@EducationUniversity1", txtEducationUniversity1.Text);
                        sqlCmd.Parameters.AddWithValue("@HighEducation2", txtHighEducation2.Text);
                        sqlCmd.Parameters.AddWithValue("@EducationYear2", Convert.ToInt32(txtEducationYear2.Text));
                        sqlCmd.Parameters.AddWithValue("@EducationUniversity2", txtEducationUniversity2.Text);
                        sqlCmd.Parameters.AddWithValue("@CreatedBy", Session["Name"].ToString());
                        sqlCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        sqlCmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@UpdatedCount", 0);
                        sqlCmd.Parameters.AddWithValue("@DeleteBy", DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@DeletedDate", DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@DeletedStatus", 1);
                        sqlCmd.Parameters.AddWithValue("@RestoredBy", DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@RestoredDate", DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@MaritialStatus", ddlmaritialstatus.SelectedItem.Text);

                        if (ddlmaritialstatus.SelectedItem.Text == "Single")
                        {
                            sqlCmd.Parameters.AddWithValue("@Chaild1Name", DBNull.Value);
                            sqlCmd.Parameters.AddWithValue("@Chaild2Name", DBNull.Value);
                            sqlCmd.Parameters.AddWithValue("@SpouseName", DBNull.Value);
                        }
                        else
                        {
                            sqlCmd.Parameters.AddWithValue("@Chaild1Name", TextBox33.Text);
                            sqlCmd.Parameters.AddWithValue("@Chaild2Name", TextBox34.Text);
                            sqlCmd.Parameters.AddWithValue("@SpouseName", txtspousename.Text);
                        }

                        sqlCmd.Parameters.AddWithValue("@Relation1", txtRelation1.Text);
                        sqlCmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                        sqlCmd.Parameters.AddWithValue("@FatherAadhar", txtFatherAadhar.Text);
                        sqlCmd.Parameters.AddWithValue("@Relation2", txtRelation2.Text);
                        sqlCmd.Parameters.AddWithValue("@MotherName", txtMotherName.Text);
                        sqlCmd.Parameters.AddWithValue("@MotherAdhar", txtMotherAdhar.Text);
                        sqlCmd.Parameters.AddWithValue("@EmpExpType", ddlempextype.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@ExptSalaryAnnum", txtaddexpectedsalary.Text);

                        if (ddlempextype.SelectedItem.Text == "Fresher")
                        {
                            sqlCmd.Parameters.AddWithValue("@Experience", DBNull.Value);
                            sqlCmd.Parameters.AddWithValue("@LastOneCompanyName", DBNull.Value);
                            sqlCmd.Parameters.AddWithValue("@LastOnePrevSalaryAnnum", DBNull.Value);
                            sqlCmd.Parameters.AddWithValue("@LastOnePrevSalaryMonth", DBNull.Value);
                            sqlCmd.Parameters.AddWithValue("@LastTwoCompanyName", DBNull.Value);
                            sqlCmd.Parameters.AddWithValue("@LastTwoPrevSalaryAnnum", DBNull.Value);
                            sqlCmd.Parameters.AddWithValue("@LastTwoPrevSalaryMonth", DBNull.Value);
                            sqlCmd.Parameters.AddWithValue("@Last1CompanyDesignation", DBNull.Value);
                            sqlCmd.Parameters.AddWithValue("@Last2CompanyDesignation", DBNull.Value);
                            sqlCmd.Parameters.AddWithValue("@Last1CompanyFromYear", DBNull.Value);
                            sqlCmd.Parameters.AddWithValue("@Last1CompanyToYear", DBNull.Value);
                            sqlCmd.Parameters.AddWithValue("@Last2CompanyFromYear", DBNull.Value);
                            sqlCmd.Parameters.AddWithValue("@Last2CompanyToYear", DBNull.Value);
                            sqlCmd.Parameters.AddWithValue("@Last1CompanyMobileNumber", DBNull.Value);
                            sqlCmd.Parameters.AddWithValue("@Last2CompanyMobileYear", DBNull.Value);
                        }
                        else
                        {
                            sqlCmd.Parameters.AddWithValue("@Experience", txtexpirienceyears.Text);
                            sqlCmd.Parameters.AddWithValue("@LastOneCompanyName", txtlast1company.Text);

                            int salary1annum = Convert.ToInt32(txtlast1salaryannum.Text);
                            int monthsalary1 = salary1annum / 12;

                            sqlCmd.Parameters.AddWithValue("@LastOnePrevSalaryAnnum", salary1annum.ToString());
                            sqlCmd.Parameters.AddWithValue("@LastOnePrevSalaryMonth", monthsalary1.ToString());
                            sqlCmd.Parameters.AddWithValue("@LastTwoCompanyName", txtlast2companyname.Text);

                            int salary2annum = Convert.ToInt32(txtlast2companysalaryannum.Text);
                            int monthsalary2 = salary2annum / 12;

                            sqlCmd.Parameters.AddWithValue("@LastTwoPrevSalaryAnnum", salary2annum.ToString());
                            sqlCmd.Parameters.AddWithValue("@LastTwoPrevSalaryMonth", monthsalary2.ToString());

                            sqlCmd.Parameters.AddWithValue("@Last1CompanyDesignation", txtlast1designation.Text);
                            sqlCmd.Parameters.AddWithValue("@Last2CompanyDesignation", txtlast2designation.Text);
                            sqlCmd.Parameters.AddWithValue("@Last1CompanyFromYear", txtlast1fromyear.Text);
                            sqlCmd.Parameters.AddWithValue("@Last1CompanyToYear", txtlast1toyear.Text);
                            sqlCmd.Parameters.AddWithValue("@Last2CompanyFromYear", txtlast2fromyear.Text);
                            sqlCmd.Parameters.AddWithValue("@Last2CompanyToYear", txtlast2toyear.Text);
                            sqlCmd.Parameters.AddWithValue("@Last1CompanyMobileNumber", txtlast1mobile.Text);
                            sqlCmd.Parameters.AddWithValue("@Last2CompanyMobileYear", txtlast2companymobile.Text);
                        }

                        int salaryannum = Convert.ToInt32(txtSalaryAnnum.Text);
                        int monthsalary = salaryannum / 12;

                        sqlCmd.Parameters.AddWithValue("@SalaryAnnum", salaryannum.ToString());
                        sqlCmd.Parameters.AddWithValue("@SalaryMonth", monthsalary.ToString());
                        sqlCmd.Parameters.AddWithValue("@EditDetailsStatus", "Not Edited");
                        sqlCmd.Parameters.AddWithValue("@EmpInActiveDate", DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@FatherMobile", txtFatherMobile.Text);
                        sqlCmd.Parameters.AddWithValue("@MotherMobile", txtMotherMobile.Text);
                        sqlCmd.Parameters.AddWithValue("@BranchCode", branchCode); // Assuming branchCode calculation is correct
                        sqlCmd.Parameters.AddWithValue("@EducationPerventage1", txt1educationpercentage.Text);
                        sqlCmd.Parameters.AddWithValue("@EducationPerventage2", txt2educationpercentage.Text);
                        sqlCmd.Parameters.AddWithValue("@NomineeName", txtnominename.Text);
                        sqlCmd.Parameters.AddWithValue("@NomineeRelation", txtnominerelation.Text);
                        sqlCmd.Parameters.AddWithValue("@NomineeMobile", txtnomineemobile.Text);
                        sqlCmd.Parameters.AddWithValue("@NomineeAadhar", txtnomineeadhar.Text);
                        sqlCmd.Parameters.AddWithValue("@ShiftType", ddladdshifttype.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@Shift2", txtaddshift2.Text);
                        sqlCmd.Parameters.AddWithValue("@Shift3", txtaddshift3.Text);

                        sqlConn.Open();
                        int i = sqlCmd.ExecuteNonQuery();
                        sqlConn.Close();

                        if (i > 0)
                        {
                            Response.Write("<script>alert('Employee Added Successfully')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }


        }

    }
}