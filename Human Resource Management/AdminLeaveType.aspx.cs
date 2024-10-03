using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;


namespace Human_Resource_Management
{
    public partial class AdminLeaveType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLeavesTypes();
            }
        }
        public void BindLeavesTypes()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM LeavesTypeTable where Status='1' ", connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string LeaveId = row["LeaveTypeId"].ToString();
                                string LeaveName = row["LeaveName"].ToString();

                                string Days = row["Days"].ToString();
                                string Reason = row["Reason"].ToString();
                                DateTime? cdob = row["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["CreatedDate"]);
                                string CreatedDate = cdob.HasValue ? cdob.Value.ToString("yyyy-MM-dd") : string.Empty;

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + LeaveId + "</td>");
                                projectHtml.Append("<td>" + LeaveName + "</td>");
                                projectHtml.Append("<td>" + Days + "</td>");
                                projectHtml.Append("<td>" + CreatedDate + "</td>");
                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_leavetype' onclick=\"editleavetype('" + LeaveId + "','" + LeaveName + "','" + Days + "','" + Reason + "','" + CreatedDate + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                projectHtml.Append("<td><a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#delete_leavetype' onclick =\"deleteleave('" + LeaveId + "','" + LeaveName + "')\"><i class='fa-regular fa-trash-can m-r-5' style='color:red;'></i></a></td>");
                                projectHtml.Append("</tr>");

                                LeavesData.Controls.Add(new LiteralControl(projectHtml.ToString()));

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

        protected void btnaddleavetype_Click(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("INSERT INTO LeavesTypeTable(LeaveName,Days,Reason,Status,CreatedBy,CreatedDate) values(@LeaveName,@Days,@Reason,@Status,@CreatedBy,@CreatedDate) ", sqlConn))
                    {
                        sqlCmd.Parameters.AddWithValue("@LeaveName", txtaddleavetype.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Days", txtaddleavedays.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Reason", txtadddescription.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Status", 1);
                        sqlCmd.Parameters.AddWithValue("@CreatedBy", Session["Name"].ToString());
                        sqlCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        int i = sqlCmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Leave Created Successfully...')</script>");
                            BindLeavesTypes();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed...')</script>");
                            BindLeavesTypes();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnleaveupdate_Click(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("UPDATE  LeavesTypeTable set LeaveName =@LeaveName,Days=@Days,Reason=@Reason,UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate Where LeaveTypeId=@LeaveTypeId ", sqlConn))
                    {
                        sqlCmd.Parameters.AddWithValue("@LeaveName", txtedtype.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Days", txteddays.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Reason", txteddesc.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@UpdatedBy", Session["Name"].ToString());
                        sqlCmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                        sqlCmd.Parameters.AddWithValue("@LeaveTypeId", txtedid.Text);
                        int i = sqlCmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Leave Updated Successfully...')</script>");
                            BindLeavesTypes();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed...')</script>");
                            BindLeavesTypes();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btndeleteleave_Click(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("UPDATE  LeavesTypeTable set Status='0' where LeaveTypeId=@LeaveTypeId ", sqlConn))
                    {
                        sqlCmd.Parameters.AddWithValue("@LeaveTypeId", HiddenField1.Value);

                        int i = sqlCmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Leave Deleted Successfully...')</script>");
                            BindLeavesTypes();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed...')</script>");
                            BindLeavesTypes();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //protected void btnupload_Click(object sender, EventArgs e)
        //{
        //    if (FileUpload1.HasFile)
        //    {
        //        String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
        //        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        //        string fileExtension = Path.GetExtension(fileName);

        //        if (fileExtension.ToLower() == ".xlsx")
        //        {
        //            string filePath = Server.MapPath("~/Uploads/") + fileName;
        //            FileUpload1.SaveAs(filePath);

        //            // Set the license context for EPPlus
        //            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set to Commercial if using a commercial license

        //            try
        //            {
        //                using (var package = new ExcelPackage(new FileInfo(filePath)))
        //                {
        //                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
        //                    int rowCount = worksheet.Dimension.Rows;

        //                    using (SqlConnection connection = new SqlConnection(connstrg))
        //                    {
        //                        connection.Open();

        //                        for (int row = 2; row <= rowCount; row++)
        //                        {
        //                            string EmpId = worksheet.Cells[row, 1].Value?.ToString();
        //                            string Salutation = worksheet.Cells[row, 2].Value?.ToString();
        //                            string EmployeeId = worksheet.Cells[row, 3].Value?.ToString();
        //                            string FirstName = worksheet.Cells[row, 4].Value?.ToString();
        //                            string LastName = worksheet.Cells[row, 5].Value?.ToString();
        //                            string imageBase64 = worksheet.Cells[row, 6].Value?.ToString();

        //                            byte[] imageData = null;
        //                            if (!string.IsNullOrEmpty(imageBase64) && imageBase64 != "00")
        //                            {
        //                                try
        //                                {
        //                                    imageData = Convert.FromBase64String(imageBase64);
        //                                }
        //                                catch (FormatException)
        //                                {
        //                                    imageData = null;
        //                                }
        //                            }


        //                            string dateFormat = "yyyy-MM-dd";
        //                            DateTime? parsedDOB = null;
        //                            DateTime? parsedDOJ = null;
        //                            DateTime parsedEmpActivaDate;
        //                            DateTime parsedCreatedDate;
        //                            DateTime parsedUpdatedDate;
        //                            DateTime parsedDeletedDate;
        //                            DateTime parsedRestoredDate;
        //                            DateTime parsedEmpInActiveDate;

        //                            string EmployeeCode = worksheet.Cells[row, 7].Value?.ToString();

        //                            if (!string.IsNullOrEmpty(worksheet.Cells[row, 8].Value?.ToString()))
        //                            {
        //                                if (DateTime.TryParseExact(worksheet.Cells[row, 8].Value?.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsed))
        //                                {
        //                                    parsedDOB = parsed;
        //                                }
        //                            }

        //                            if (!string.IsNullOrEmpty(worksheet.Cells[row, 9].Value?.ToString()))
        //                            {
        //                                if (DateTime.TryParseExact(worksheet.Cells[row, 9].Value?.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsed))
        //                                {
        //                                    parsedDOJ = parsed;
        //                                }
        //                            }
        //                            string Gender = worksheet.Cells[row, 10].Value?.ToString();
        //                            string Company = worksheet.Cells[row, 11].Value?.ToString();
        //                            string Branch = worksheet.Cells[row, 12].Value?.ToString();
        //                            string Department = worksheet.Cells[row, 13].Value?.ToString();
        //                            string Designation = worksheet.Cells[row, 14].Value?.ToString();
        //                            string CompanyId = worksheet.Cells[row, 15].Value?.ToString();
        //                            string BranchId = worksheet.Cells[row, 16].Value?.ToString();
        //                            string DepartmentId = worksheet.Cells[row, 17].Value?.ToString();
        //                            string EmployeType = worksheet.Cells[row, 18].Value?.ToString();
        //                            string EmpTypeId = worksheet.Cells[row, 19].Value?.ToString();
        //                            string CompanyEmail = worksheet.Cells[row, 20].Value?.ToString();
        //                            string PersonalEmail = worksheet.Cells[row, 21].Value?.ToString();
        //                            string CompanyCellNo = worksheet.Cells[row, 22].Value?.ToString();
        //                            string PersonalCelloNo = worksheet.Cells[row, 23].Value?.ToString();
        //                            string PresentAddress = worksheet.Cells[row, 24].Value?.ToString();
        //                            string PermanentAddress = worksheet.Cells[row, 25].Value?.ToString();
        //                            string BloodGroup = worksheet.Cells[row, 26].Value?.ToString();
        //                            string Status = worksheet.Cells[row, 27].Value?.ToString();
        //                            string BankName = worksheet.Cells[row, 28].Value?.ToString();
        //                            string BankAccNo = worksheet.Cells[row, 29].Value?.ToString();
        //                            string IFSC = worksheet.Cells[row, 30].Value?.ToString();
        //                            string PFStatus = worksheet.Cells[row, 31].Value?.ToString();
        //                            string PfStatusId = worksheet.Cells[row, 32].Value?.ToString();
        //                            string PfNo = worksheet.Cells[row, 33].Value?.ToString();
        //                            string Esino = worksheet.Cells[row, 34].Value?.ToString();
        //                            string PanNo = worksheet.Cells[row, 35].Value?.ToString();
        //                            string Shift = worksheet.Cells[row, 36].Value?.ToString();
        //                            string ShiftId = worksheet.Cells[row, 37].Value?.ToString();
        //                            string Tocken = worksheet.Cells[row, 38].Value?.ToString();
        //                            DateTime.TryParseExact(worksheet.Cells[row, 39].Value?.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedEmpActivaDate);
        //                            string CasualLeaves = worksheet.Cells[row, 40].Value?.ToString();
        //                            string TaxId = worksheet.Cells[row, 41].Value?.ToString();
        //                            string HighEducation1 = worksheet.Cells[row, 42].Value?.ToString();
        //                            string EducationYear1 = worksheet.Cells[row, 43].Value?.ToString();
        //                            string EducationUniversity1 = worksheet.Cells[row, 44].Value?.ToString();
        //                            string HighEducation2 = worksheet.Cells[row, 45].Value?.ToString();
        //                            string EducationYear2 = worksheet.Cells[row, 46].Value?.ToString();
        //                            string EducationUniversity2 = worksheet.Cells[row, 47].Value?.ToString();
        //                            string CreatedBy = worksheet.Cells[row, 48].Value?.ToString();
        //                            DateTime.TryParseExact(worksheet.Cells[row, 49].Value?.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedCreatedDate);
        //                            string UpdatedBy = worksheet.Cells[row, 50].Value?.ToString();
        //                            DateTime.TryParseExact(worksheet.Cells[row, 51].Value?.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedUpdatedDate);

                                    
        //                            string UpdatedCount = worksheet.Cells[row, 52].Value?.ToString();
        //                            string DeleteBy = worksheet.Cells[row, 53].Value?.ToString();
        //                            DateTime.TryParseExact(worksheet.Cells[row, 54].Value?.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDeletedDate);
        //                            string DeletedStatus = worksheet.Cells[row, 55].Value?.ToString();
        //                            string RestoredBy = worksheet.Cells[row, 56].Value?.ToString();
        //                            DateTime.TryParseExact(worksheet.Cells[row, 57].Value?.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedRestoredDate);
        //                            string MaritialStatus = worksheet.Cells[row, 58].Value?.ToString();
        //                            string Relation1 = worksheet.Cells[row, 59].Value?.ToString();
        //                            string FatherName = worksheet.Cells[row, 60].Value?.ToString();
        //                            string FatherAadhar = worksheet.Cells[row, 61].Value?.ToString();
        //                            string Relation2 = worksheet.Cells[row, 62].Value?.ToString();
        //                            string MotherName = worksheet.Cells[row, 63].Value?.ToString();
        //                            string MotherAdhar = worksheet.Cells[row, 64].Value?.ToString();
        //                            string Chaild1Name = worksheet.Cells[row, 65].Value?.ToString();
        //                            string Chaild2Name = worksheet.Cells[row, 66].Value?.ToString();
        //                            string EmpExpType = worksheet.Cells[row, 67].Value?.ToString();
        //                            string ExptSalaryAnnum = worksheet.Cells[row, 68].Value?.ToString();
        //                            string SalaryAnnum = worksheet.Cells[row, 69].Value?.ToString();
        //                            string SalaryMonth = worksheet.Cells[row, 70].Value?.ToString();
        //                            string Experience = worksheet.Cells[row, 71].Value?.ToString();
        //                            string LastOneCompanyName = worksheet.Cells[row, 72].Value?.ToString();
        //                            string LastOnePrevSalaryAnnum = worksheet.Cells[row, 73].Value?.ToString();
        //                            string LastOnePrevSalaryMonth = worksheet.Cells[row, 74].Value?.ToString();
        //                            string LastTwoCompanyName = worksheet.Cells[row, 75].Value?.ToString();
        //                            string LastTwoPrevSalaryAnnum = worksheet.Cells[row, 76].Value?.ToString();
        //                            string LastTwoPrevSalaryMonth = worksheet.Cells[row, 77].Value?.ToString();
        //                            string EditDetailsStatus = worksheet.Cells[row, 78].Value?.ToString();
        //                            DateTime.TryParseExact(worksheet.Cells[row, 79].Value?.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedEmpInActiveDate);
        //                            string FatherMobile = worksheet.Cells[row, 80].Value?.ToString();
        //                            string MotherMobile = worksheet.Cells[row, 81].Value?.ToString();
        //                            string BranchCode = worksheet.Cells[row, 82].Value?.ToString();
        //                            string EducationPerventage1 = worksheet.Cells[row, 83].Value?.ToString();
        //                            string EducationPerventage2 = worksheet.Cells[row, 84].Value?.ToString();
        //                            string Last1CompanyDesignation = worksheet.Cells[row, 85].Value?.ToString();
        //                            string Last2CompanyDesignation = worksheet.Cells[row, 86].Value?.ToString();
        //                            string Last1CompanyFromYear = worksheet.Cells[row, 87].Value?.ToString();
        //                            string Last1CompanyToYear = worksheet.Cells[row, 88].Value?.ToString();
        //                            string Last2CompanyFromYear = worksheet.Cells[row, 89].Value?.ToString();
        //                            string Last2CompanyToYear = worksheet.Cells[row, 90].Value?.ToString();
        //                            string Last1CompanyMobileNumber = worksheet.Cells[row, 91].Value?.ToString();
        //                            string Last2CompanyMobileYear = worksheet.Cells[row, 92].Value?.ToString();
        //                            string NomineeName = worksheet.Cells[row, 93].Value?.ToString();
        //                            string NomineeRelation = worksheet.Cells[row, 94].Value?.ToString();
        //                            string SpouseName = worksheet.Cells[row, 95].Value?.ToString();
        //                            string NomineeMobile = worksheet.Cells[row, 96].Value?.ToString();
        //                            string NomineeAadhar = worksheet.Cells[row, 97].Value?.ToString();
        //                            string ShiftType = worksheet.Cells[row, 98].Value?.ToString();
        //                            string Shift2 = worksheet.Cells[row, 99].Value?.ToString();
        //                            string Shift3 = worksheet.Cells[row, 100].Value?.ToString();

        //                            if (!string.IsNullOrWhiteSpace(EmpId))
        //                            {
        //                                string query = "INSERT INTO Employees (EmpId, Salutation, EmployeeId, FirstName, LastName, Image, EmployeeCode, DOB, DOJ, Gender, Company, Branch, Department, Designation, CompanyId, BranchId, DepartmentId, EmployeType, EmpTypeId, CompanyEmail, PersonalEmail, CompanyCellNo, PersonalCelloNo, PresentAddress, PermanentAddress, BloodGroup, Status, BankName, BankAccNo, IFSC, PFStatus, PfStatusId, PfNo, Esino, PanNo, Shift, ShiftId, Tocken, EmpActivaDate, CasualLeaves, TaxId, HighEducation1, EducationYear1, EducationUniversity1, HighEducation2, EducationYear2, EducationUniversity2, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, UpdatedCount, DeleteBy, DeletedDate, DeletedStatus, RestoredBy, RestoredDate, MaritialStatus, Relation1, FatherName, FatherAadhar, Relation2, MotherName, MotherAdhar, Chaild1Name, Chaild2Name, EmpExpType, ExptSalaryAnnum, SalaryAnnum, SalaryMonth, Experience, LastOneCompanyName, LastOnePrevSalaryAnnum, LastOnePrevSalaryMonth, LastTwoCompanyName, LastTwoPrevSalaryAnnum, LastTwoPrevSalaryMonth, EditDetailsStatus, EmpInActiveDate, FatherMobile, MotherMobile, BranchCode, EducationPerventage1, EducationPerventage2, Last1CompanyDesignation, Last2CompanyDesignation, Last1CompanyFromYear, Last1CompanyToYear, Last2CompanyFromYear, Last2CompanyToYear, Last1CompanyMobileNumber, Last2CompanyMobileYear, NomineeName, NomineeRelation, SpouseName, NomineeMobile, NomineeAadhar, ShiftType, Shift2, Shift3) " +
        //                                          "VALUES (@EmpId, @Salutation, @EmployeeId, @FirstName, @LastName, @Image, @EmployeeCode, @DOB, @DOJ, @Gender, @Company, @Branch, @Department, @Designation, @CompanyId, @BranchId, @DepartmentId, @EmployeType, @EmpTypeId, @CompanyEmail, @PersonalEmail, @CompanyCellNo, @PersonalCelloNo, @PresentAddress, @PermanentAddress, @BloodGroup, @Status, @BankName, @BankAccNo, @IFSC, @PFStatus, @PfStatusId, @PfNo, @Esino, @PanNo, @Shift, @ShiftId, @Tocken, @EmpActivaDate, @CasualLeaves, @TaxId, @HighEducation1, @EducationYear1, @EducationUniversity1, @HighEducation2, @EducationYear2, @EducationUniversity2, @CreatedBy, @CreatedDate, @UpdatedBy, @UpdatedDate, @UpdatedCount, @DeleteBy, @DeletedDate, @DeletedStatus, @RestoredBy, @RestoredDate, @MaritialStatus, @Relation1, @FatherName, @FatherAadhar, @Relation2, @MotherName, @MotherAdhar, @Chaild1Name, @Chaild2Name, @EmpExpType, @ExptSalaryAnnum, @SalaryAnnum, @SalaryMonth, @Experience, @LastOneCompanyName, @LastOnePrevSalaryAnnum, @LastOnePrevSalaryMonth, @LastTwoCompanyName, @LastTwoPrevSalaryAnnum, @LastTwoPrevSalaryMonth, @EditDetailsStatus, @EmpInActiveDate, @FatherMobile, @MotherMobile, @BranchCode, @EducationPerventage1, @EducationPerventage2, @Last1CompanyDesignation, @Last2CompanyDesignation, @Last1CompanyFromYear, @Last1CompanyToYear, @Last2CompanyFromYear, @Last2CompanyToYear, @Last1CompanyMobileNumber, @Last2CompanyMobileYear, @NomineeName, @NomineeRelation, @SpouseName, @NomineeMobile, @NomineeAadhar, @ShiftType, @Shift2, @Shift3)";
        //                                using (SqlCommand cmd = new SqlCommand(query, connection))
        //                                {
        //                                    cmd.Parameters.AddWithValue("@EmpId", (object)EmpId ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Salutation", (object)Salutation ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@EmployeeId", (object)EmployeeId ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@FirstName", (object)FirstName ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@LastName", (object)LastName ?? DBNull.Value);
        //                                    cmd.Parameters.Add("@Image", SqlDbType.VarBinary, -1).Value = imageData ?? (object)DBNull.Value;
        //                                    cmd.Parameters.AddWithValue("@EmployeeCode", (object)EmployeeCode ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@DOB", parsedDOB.HasValue ? (object)parsedDOB : DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@DOJ", parsedDOJ.HasValue ? (object)parsedDOJ : DBNull.Value);

        //                                    cmd.Parameters.AddWithValue("@Gender", (object)Gender ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Company", (object)Company ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Branch", (object)Branch ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Department", (object)Department ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Designation", (object)Designation ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@CompanyId", (object)CompanyId ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@BranchId", (object)BranchId ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@DepartmentId", (object)DepartmentId ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@EmployeType", (object)EmployeType ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@EmpTypeId", (object)EmpTypeId ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@CompanyEmail", (object)CompanyEmail ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@PersonalEmail", (object)PersonalEmail ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@CompanyCellNo", (object)CompanyCellNo ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@PersonalCelloNo", (object)PersonalCelloNo ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@PresentAddress", (object)PresentAddress ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@PermanentAddress", (object)PermanentAddress ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@BloodGroup", (object)BloodGroup ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Status", (object)Status ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@BankName", (object)BankName ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@BankAccNo", (object)BankAccNo ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@IFSC", (object)IFSC ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@PFStatus", (object)PFStatus ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@PfStatusId", (object)PfStatusId ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@PfNo", (object)PfNo ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Esino", (object)Esino ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@PanNo", (object)PanNo ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Shift", (object)Shift ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@ShiftId", (object)ShiftId ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Tocken", (object)Tocken ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@EmpActivaDate", parsedEmpActivaDate == default(DateTime) ? (object)DBNull.Value : parsedEmpActivaDate);
        //                                    cmd.Parameters.AddWithValue("@CasualLeaves", (object)CasualLeaves ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@TaxId", (object)TaxId ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@HighEducation1", (object)HighEducation1 ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@EducationYear1", (object)EducationYear1 ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@EducationUniversity1", (object)EducationUniversity1 ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@HighEducation2", (object)HighEducation2 ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@EducationYear2", (object)EducationYear2 ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@EducationUniversity2", (object)EducationUniversity2 ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@CreatedBy", (object)CreatedBy ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@CreatedDate", parsedCreatedDate == default(DateTime) ? (object)DBNull.Value : parsedCreatedDate);
        //                                    cmd.Parameters.AddWithValue("@UpdatedBy", (object)UpdatedBy ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@UpdatedDate", parsedUpdatedDate == default(DateTime) ? (object)DBNull.Value : parsedUpdatedDate);
        //                                    cmd.Parameters.AddWithValue("@UpdatedCount", (object)UpdatedCount ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@DeleteBy", (object)DeleteBy ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@DeletedDate", parsedDeletedDate == default(DateTime) ? (object)DBNull.Value : parsedDeletedDate);
        //                                    cmd.Parameters.AddWithValue("@DeletedStatus", (object)DeletedStatus ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@RestoredBy", (object)RestoredBy ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@RestoredDate", parsedRestoredDate == default(DateTime) ? (object)DBNull.Value : parsedRestoredDate);
        //                                    cmd.Parameters.AddWithValue("@MaritialStatus", (object)MaritialStatus ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Relation1", (object)Relation1 ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@FatherName", (object)FatherName ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@FatherAadhar", (object)FatherAadhar ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Relation2", (object)Relation2 ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@MotherName", (object)MotherName ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@MotherAdhar", (object)MotherAdhar ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Chaild1Name", (object)Chaild1Name ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Chaild2Name", (object)Chaild2Name ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@EmpExpType", (object)EmpExpType ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@ExptSalaryAnnum", (object)ExptSalaryAnnum ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@SalaryAnnum", (object)SalaryAnnum ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@SalaryMonth", (object)SalaryMonth ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Experience", (object)Experience ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@LastOneCompanyName", (object)LastOneCompanyName ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@LastOnePrevSalaryAnnum", (object)LastOnePrevSalaryAnnum ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@LastOnePrevSalaryMonth", (object)LastOnePrevSalaryMonth ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@LastTwoCompanyName", (object)LastTwoCompanyName ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@LastTwoPrevSalaryAnnum", (object)LastTwoPrevSalaryAnnum ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@LastTwoPrevSalaryMonth", (object)LastTwoPrevSalaryMonth ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@EditDetailsStatus", (object)EditDetailsStatus ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@EmpInActiveDate", parsedEmpInActiveDate == default(DateTime) ? (object)DBNull.Value : parsedEmpInActiveDate);
        //                                    cmd.Parameters.AddWithValue("@FatherMobile", (object)FatherMobile ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@MotherMobile", (object)MotherMobile ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@BranchCode", (object)BranchCode ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@EducationPerventage1", (object)EducationPerventage1 ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@EducationPerventage2", (object)EducationPerventage2 ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Last1CompanyDesignation", (object)Last1CompanyDesignation ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Last2CompanyDesignation", (object)Last2CompanyDesignation ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Last1CompanyFromYear", (object)Last1CompanyFromYear ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Last1CompanyToYear", (object)Last1CompanyToYear ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Last2CompanyFromYear", (object)Last2CompanyFromYear ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Last2CompanyToYear", (object)Last2CompanyToYear ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Last1CompanyMobileNumber", (object)Last1CompanyMobileNumber ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Last2CompanyMobileYear", (object)Last2CompanyMobileYear ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@NomineeName", (object)NomineeName ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@NomineeRelation", (object)NomineeRelation ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@SpouseName", (object)SpouseName ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@NomineeMobile", (object)NomineeMobile ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@NomineeAadhar", (object)NomineeAadhar ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@ShiftType", (object)ShiftType ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Shift2", (object)Shift2 ?? DBNull.Value);
        //                                    cmd.Parameters.AddWithValue("@Shift3", (object)Shift3 ?? DBNull.Value);
        //                                    cmd.ExecuteNonQuery();
        //                                    // int i = cmd.ExecuteNonQuery();
        //                                    //if (i > 0)
        //                                    //{
        //                                    //    Response.Write("<script>alert('Uploaded Successfully');</script>");
        //                                    //}
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Response.Write("<script><alert('Id is empty......')</script>");
        //                            }

        //                        }
        //                    }
        //                }
        //            }
        //            catch(Exception ex)
        //            {
        //                throw ex;
        //            }
        //        }
        //        else
        //        {
        //            Response.Write("Please Upload a valid Excel file (.xlsx).");
        //        }

        //    }
        //}

        
    }
}