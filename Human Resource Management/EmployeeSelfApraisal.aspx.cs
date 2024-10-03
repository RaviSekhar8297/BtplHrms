using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Presentation;
using Human_Resource_Management.Roles.Employee;
using System.Diagnostics;
using AjaxControlToolkit;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2019.Word.Cid;

namespace Human_Resource_Management
{
    public partial class EmployeeSelfApraisal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BasicInfo();
                ProExcellenceData();
                CalculateAndDisplayResult();
                CalculateAndDisplayResult2();
               // DataBindTextBoxes();
                DataBindTextBoxes2();
                RosHeadsDataBindTextBoxes();
            }
            if (!IsPostBack)
            {
                PersonalUpdateDataSelf();
                GainedAndTotalPoints();              
            }
        }
          
        public void BasicInfo()
        {
            try
            {
                if (Session["EmpId"] != null && Session["EmpId"].ToString() != "")
                {
                    using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE EmpId = @EmployeeId", connection1))
                        {
                            sqlcmd.Parameters.AddWithValue("@EmployeeId", Session["EmpId"].ToString());
                            connection1.Open();
                            using (SqlDataReader myReader = sqlcmd.ExecuteReader())
                            {
                                if (myReader.Read())
                                {
                                    txtname.Text = myReader["LastName"].ToString();
                                    txtdepartment.Text = myReader["Department"].ToString();
                                    txtdesignation.Text = myReader["Designation"].ToString();
                                    txtqualification.Text = myReader["HighEducation1"].ToString();
                                    txtempid.Text = myReader["EmpId"].ToString();
                                    DateTime dateOfJoining = Convert.ToDateTime(myReader["DOJ"]);
                                    txtdoj.Text = dateOfJoining.ToString("yyyy-MM-dd");
                                    DateTime dateOfBirth = Convert.ToDateTime(myReader["DOB"]);
                                    txtdob.Text = dateOfBirth.ToString("yyyy-MM-dd");
                                    txtprevex.Text = myReader["Experience"].ToString();
                                    txtro1.Text = myReader["LastName"].ToString();
                                    txtro2.Text = myReader["LastName"].ToString();
                                    txtro3.Text = myReader["LastName"].ToString();
                                    txtro4.Text = myReader["LastName"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void ProExcellenceData()
        {
            if (Session["EmpId"] != null && Session["EmpId"].ToString() != "")
            {
                TextBox7.ReadOnly = true;
                TextBox8.ReadOnly = true;
                TextBox9.ReadOnly = true;
                TextBox10.ReadOnly = true;
                TextBox17.ReadOnly = true;
                TextBox18.ReadOnly = true;
                try
                {
                    int currentYear = DateTime.Now.Year;
                    using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM AppricialsSelfROS WHERE EmpId = @EmployeeId", connection1))
                        {
                            sqlcmd.Parameters.AddWithValue("@EmployeeId", Session["EmpId"].ToString());
                            sqlcmd.Parameters.AddWithValue("@Year", currentYear.ToString());
                            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
                            System.Data.DataTable dt = new System.Data.DataTable();
                            sqlda.Fill(dt);
                            int RowCount = dt.Rows.Count;
                            connection1.Close();
                            int i = 0;                           
                            if (RowCount > 0)
                            {
                                string ProExellenceSelfStatus = dt.Rows[i]["ProExellenceSelfStatus"].ToString().Trim();  //
                                string ProQuantitySelf = dt.Rows[i]["ProQuantitySelf"].ToString().Trim();
                                string ProTATSelf = dt.Rows[i]["ProTATSelf"].ToString().Trim();
                                string ProPMSSelf = dt.Rows[i]["ProPMSSelf"].ToString().Trim();
                                string ProTPDSelf = dt.Rows[i]["ProTPDSelf"].ToString().Trim();
                                string ProTeamKnowledgeSelf = dt.Rows[i]["ProTeamKnowledgeSelf"].ToString().Trim();
                                string ProCommunicationSelf = dt.Rows[i]["ProCommunicationSelf"].ToString().Trim();


                                string ProExellenceROSStatus = dt.Rows[i]["ProExellenceROSStatus"].ToString().Trim();  //
                                string ProQuantityROS = dt.Rows[i]["ProQuantityROS"].ToString().Trim();
                                string ProTATROS = dt.Rows[i]["ProTATROS"].ToString().Trim();
                                string ProPMSROS = dt.Rows[i]["ProPMSROS"].ToString().Trim();
                                string ProTPDROS = dt.Rows[i]["ProTPDROS"].ToString().Trim();
                                string ProTeamKnowledgeROS = dt.Rows[i]["ProTeamKnowledgeROS"].ToString().Trim();
                                string ProCommunicationROS = dt.Rows[i]["ProCommunicationROS"].ToString().Trim();

                                TextBox7.Text = ProQuantityROS;
                                TextBox8.Text = ProTATROS;

                                string PersExcellenceSelfStatus = dt.Rows[i]["PersExcellenceSelfStatus"].ToString().Trim();  //
                                string PersPlanUnplanLeavesSelf = dt.Rows[i]["PersPlanUnplanLeavesSelf"].ToString().Trim();
                                string PersExTimeConsciousnessSelf = dt.Rows[i]["PersExTimeConsciousnessSelf"].ToString().Trim();
                                string PersExTeamColaborationSelf = dt.Rows[i]["PersExTeamColaborationSelf"].ToString().Trim();
                                string PersExProfessionalismSelf = dt.Rows[i]["PersExProfessionalismSelf"].ToString().Trim();
                                string PersExAdpoliciesSelf = dt.Rows[i]["PersExAdpoliciesSelf"].ToString().Trim();
                                string PersExSpecialEffortsSelf = dt.Rows[i]["PersExSpecialEffortsSelf"].ToString().Trim();
                                string PersExTrainingUtilizationSelf = dt.Rows[i]["PersExTrainingUtilizationSelf"].ToString().Trim();

                                string PersExcellenceROSStatus = dt.Rows[i]["PersExcellenceROSStatus"].ToString().Trim();  //
                                string PersPlanUnplanLeavesROS = dt.Rows[i]["PersPlanUnplanLeavesROS"].ToString().Trim();
                                string PersExTimeConsciousnessROS = dt.Rows[i]["PersExTimeConsciousnessROS"].ToString().Trim();
                                string PersExTeamColaborationROS = dt.Rows[i]["PersExTeamColaborationROS"].ToString().Trim();
                                string PersExProfessionalismROS = dt.Rows[i]["PersExProfessionalismROS"].ToString().Trim();
                                string PersExAdpoliciesROS = dt.Rows[i]["PersExAdpoliciesROS"].ToString().Trim();
                                string PersExSpecialEffortsROS = dt.Rows[i]["PersExSpecialEffortsROS"].ToString().Trim();
                                string PersExTrainingUtilizationROS = dt.Rows[i]["PersExTrainingUtilizationROS"].ToString().Trim();

                                //string Year = dt.Rows[i]["Year"].ToString().Trim();
                                IEnumerable<int> years = dt.AsEnumerable().Select(row => int.Parse(row["Year"].ToString().Trim()));
                                int maxYear = years.Max();
                                if (ProExellenceSelfStatus == "1" && maxYear == currentYear)
                                {
                                    TextBox[] textBoxes = { TextBox1, TextBox2, TextBox3, TextBox4, TextBox5, TextBox6,  };

                                    string[] columnNames = { "ProQuantitySelf", "ProTATSelf", "ProPMSSelf", "ProTPDSelf", "ProTeamKnowledgeSelf", "ProCommunicationSelf" };

                                    for (int j = 0; j < columnNames.Length; j++)
                                    {
                                        string value = dt.Rows[i][columnNames[j]].ToString().Trim();

                                        if (ProExellenceSelfStatus == "1" && !string.IsNullOrEmpty(value))
                                        {
                                            textBoxes[j].ReadOnly = true;
                                            textBoxes[j].Text = value;
                                        }
                                        else
                                        {
                                            textBoxes[j].ReadOnly = false;
                                        }
                                    }
                                }                               
                                else
                                {
                                    InsertProExcellenceAppricials();
                                }
                            }
                            else
                            {
                                InsertProExcellenceAppricials();
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
        private void CalculateAndDisplayResult()
        {
            TextBox[] defaultTextBoxes = { txtdefaluttextbox1, txtdefaluttextbox2, txtdefaluttextbox3, txtdefaluttextbox4, txtdefaluttextbox5, txtdefaluttextbox6 };
            TextBox[] textBoxes = { TextBox1, TextBox2, TextBox3, TextBox4, TextBox5, TextBox6 };
            TextBox[] resultTextBoxes = { TextBox11, TextBox12, TextBox13, TextBox14, TextBox15, TextBox16 };

            for (int i = 0; i < defaultTextBoxes.Length; i++)
            {
                int defaultValue = string.IsNullOrEmpty(defaultTextBoxes[i].Text) ? 0 : Convert.ToInt32(defaultTextBoxes[i].Text);
                int userInputValue = string.IsNullOrEmpty(textBoxes[i].Text) ? 0 : Convert.ToInt32(textBoxes[i].Text);
                int percentageValue = (userInputValue * defaultValue) / 100;
                resultTextBoxes[i].Text = percentageValue.ToString();
            }
            int totalSum = 0;
            foreach (TextBox textBox in resultTextBoxes)
            {
                totalSum += string.IsNullOrEmpty(textBox.Text) ? 0 : Convert.ToInt32(textBox.Text);
            }
            txttotalscore.Text = totalSum.ToString();

            int txtdefaultcountTotal = 0;
            foreach (TextBox textBox in defaultTextBoxes)
            {
                txtdefaultcountTotal += string.IsNullOrEmpty(textBox.Text) ? 0 : Convert.ToInt32(textBox.Text);
            }

            txtdefaultcount.Text = txtdefaultcountTotal.ToString();

            int totalPer = (int)((totalSum / (double)txtdefaultcountTotal) * 100);
            txtpercentagetotal.Text = totalPer.ToString() + "%";
        }
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox currentTextBox = (TextBox)sender;
            int index = int.Parse(currentTextBox.ID.Substring(currentTextBox.ID.Length - 1)) - 1;
            TextBox[] textBoxes = { TextBox1, TextBox2, TextBox3, TextBox4, TextBox5, TextBox6  }; // enter values textboxes
            TextBox[] defaultTextBoxes = { txtdefaluttextbox1, txtdefaluttextbox2, txtdefaluttextbox3, txtdefaluttextbox4, txtdefaluttextbox5, txtdefaluttextbox6 }; // default values textboxes
            TextBox[] resultTextBoxes = { TextBox11, TextBox12, TextBox13, TextBox14, TextBox15, TextBox16 }; // data display in this textboxes

            int defaultValue = Convert.ToInt32(defaultTextBoxes[index].Text);
            int inputValue = Convert.ToInt32(currentTextBox.Text);
            int percentageValue = (inputValue * defaultValue) / 100;

            TextBox resultTextBox = resultTextBoxes[index];
            resultTextBox.Text = percentageValue.ToString();

            string vv = percentageValue.ToString();
          //  ProExcellenceData();
            ClientScript.RegisterStartupScript(this.GetType(), "log", "logToConsole('" + vv + "');", true);

            if (Session["EmpId"] != null && Session["EmpId"].ToString() != "")
            {
                try
                {
                    using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        string updateQuery = "UPDATE AppricialsSelfROS SET ";
                        // Construct the SQL UPDATE statement based on the index of the TextBox
                        switch (index)
                        {
                            case 0:
                                updateQuery += "ProQuantitySelf = @Value";
                                break;
                            case 1:
                                updateQuery += "ProTATSelf = @Value";
                                break;
                            case 2:
                                updateQuery += "ProPMSSelf = @Value";
                                break;
                            case 3:
                                updateQuery += "ProTPDSelf = @Value";
                                break;
                            case 4:
                                updateQuery += "ProTeamKnowledgeSelf = @Value";
                                break;
                            case 5:
                                updateQuery += "ProCommunicationSelf = @Value";
                                break;

                        }
                        updateQuery += " WHERE EmpId = @EmployeeId";

                        using (SqlCommand sqlcmd = new SqlCommand(updateQuery, connection1))
                        {
                            sqlcmd.Parameters.AddWithValue("@EmployeeId", Session["EmpId"].ToString());
                            sqlcmd.Parameters.AddWithValue("@Value", currentTextBox.Text);
                            connection1.Open();
                            sqlcmd.ExecuteNonQuery();                                                     
                        }
                        connection1.Close();

                        // Call methods after the update
                        ProExcellenceData();
                        CalculateAndDisplayResult();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

       protected void Text_TextChanged(object sender, EventArgs e)
       {
            TextBox currentTextBox = (TextBox)sender;

            // Define the array of TextBoxes you want to work with
            TextBox[] textBoxes = { TextBox19, TextBox20, TextBox21, TextBox22, TextBox23, TextBox24, TextBox25 };

            // Find the index of the current TextBox in the array
            int index = Array.IndexOf(textBoxes, currentTextBox);

            // Define the database field names corresponding to each TextBox index
            string[] fieldNames = { "PersPlanUnplanLeavesSelf", "PersExTimeConsciousnessSelf", "PersExTeamColaborationSelf", "PersExProfessionalismSelf", "PersExAdpoliciesSelf", "PersExSpecialEffortsSelf", "PersExTrainingUtilizationSelf" };

            // Use the index to determine which field to update in the database
            UpdateDatabaseField(fieldNames[index], currentTextBox.Text);

            // Your existing code
            TextBox[] defaultTextBoxes = { TextBox26, TextBox34, TextBox35, TextBox36, TextBox37, TextBox38, TextBox39 }; // default values textboxes
            TextBox[] resultTextBoxes = { TextBox27, TextBox28, TextBox29, TextBox30, TextBox31, TextBox32, TextBox33 };
            int defaultValue = Convert.ToInt32(defaultTextBoxes[index].Text);
            int inputValue = Convert.ToInt32(currentTextBox.Text);
            int percentageValue = (inputValue * defaultValue) / 100;

            TextBox resultTextBox = resultTextBoxes[index];
            resultTextBox.Text = percentageValue.ToString();

            string vv = percentageValue.ToString();
          //  ProExcellenceData2();
            ClientScript.RegisterStartupScript(this.GetType(), "log", "logToConsole('" + vv + "');", true);

            if (Session["EmpId"] != null && Session["EmpId"].ToString() != "")
            {
                try
                {
                    switch (index)
                    {
                        case 0:
                            UpdateDatabaseField("PersPlanUnplanLeavesSelf", currentTextBox.Text);
                            break;
                        case 1:
                            UpdateDatabaseField("PersExTimeConsciousnessSelf", currentTextBox.Text);
                            break;
                        case 2:
                            UpdateDatabaseField("PersExTeamColaborationSelf", currentTextBox.Text);
                            break;
                        case 3:
                            UpdateDatabaseField("PersExProfessionalismSelf", currentTextBox.Text);
                            break;
                        case 4:
                            UpdateDatabaseField("PersExAdpoliciesSelf", currentTextBox.Text);
                            break;
                        case 5:
                            UpdateDatabaseField("PersExSpecialEffortsSelf", currentTextBox.Text);
                            break;
                        case 6:
                            UpdateDatabaseField("PersExTrainingUtilizationSelf", currentTextBox.Text);
                            break;
                        default:
                            // Handle if index is out of range
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

       }

        public void InsertProExcellenceAppricials()
        {
            if (Session["EmpId"] != null && Session["EmpId"].ToString() != "")
            {
                try
                {
                    using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        connection1.Open();
                        //string value = null;
                        String Status = "1";
                        int currentYear = DateTime.Now.Year;
                        SqlCommand cmd = new SqlCommand("INSERT INTO AppricialsSelfROS(EmpId, Name, Company, Branch, Department, Designation, ProExellenceSelfStatus, ProQuantitySelf, ProTATSelf, ProPMSSelf, ProTPDSelf, ProTeamKnowledgeSelf, ProCommunicationSelf, ProExellenceROSStatus, ProQuantityROS, ProTATROS, ProPMSROS, ProTPDROS, ProTeamKnowledgeROS, ProCommunicationROS, PersExcellenceSelfStatus, PersPlanUnplanLeavesSelf, PersExTimeConsciousnessSelf, PersExTeamColaborationSelf, PersExProfessionalismSelf, PersExAdpoliciesSelf, PersExSpecialEffortsSelf, PersExTrainingUtilizationSelf, PersExcellenceROSStatus, PersPlanUnplanLeavesROS, PersExTimeConsciousnessROS, PersExTeamColaborationROS, PersExProfessionalismROS, PersExAdpoliciesROS, PersExSpecialEffortsROS, PersExTrainingUtilizationROS, Year) VALUES (@EmpId, @Name, @Company, @Branch, @Department, @Designation, @ProExellenceSelfStatus, @ProQuantitySelf, @ProTATSelf, @ProPMSSelf, @ProTPDSelf, @ProTeamKnowledgeSelf, @ProCommunicationSelf, @ProExellenceROSStatus, @ProQuantityROS, @ProTATROS, @ProPMSROS, @ProTPDROS, @ProTeamKnowledgeROS, @ProCommunicationROS, @PersExcellenceSelfStatus, @PersPlanUnplanLeavesSelf, @PersExTimeConsciousnessSelf, @PersExTeamColaborationSelf, @PersExProfessionalismSelf, @PersExAdpoliciesSelf, @PersExSpecialEffortsSelf, @PersExTrainingUtilizationSelf, @PersExcellenceROSStatus, @PersPlanUnplanLeavesROS, @PersExTimeConsciousnessROS, @PersExTeamColaborationROS, @PersExProfessionalismROS, @PersExAdpoliciesROS, @PersExSpecialEffortsROS, @PersExTrainingUtilizationROS, @Year)", connection1);

                        cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                        cmd.Parameters.AddWithValue("@Name", Session["Name"].ToString());
                        cmd.Parameters.AddWithValue("@Company", Session["CompanyName"].ToString());
                        cmd.Parameters.AddWithValue("@Branch", Session["BranchName"].ToString());
                        cmd.Parameters.AddWithValue("@Department", Session["DepartmentName"].ToString());
                        cmd.Parameters.AddWithValue("@Designation", Session["Designation"].ToString());
                        cmd.Parameters.AddWithValue("@ProExellenceSelfStatus", Status.ToString());
                        cmd.Parameters.AddWithValue("@ProQuantitySelf", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProTATSelf", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProPMSSelf", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProTPDSelf", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProTeamKnowledgeSelf", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProCommunicationSelf", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProExellenceROSStatus", Status.ToString());
                        cmd.Parameters.AddWithValue("@ProQuantityROS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProTATROS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProPMSROS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProTPDROS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProTeamKnowledgeROS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProCommunicationROS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PersExcellenceSelfStatus", Status.ToString());
                        cmd.Parameters.AddWithValue("@PersPlanUnplanLeavesSelf", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PersExTimeConsciousnessSelf", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PersExTeamColaborationSelf", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PersExProfessionalismSelf", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PersExAdpoliciesSelf", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PersExSpecialEffortsSelf", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PersExTrainingUtilizationSelf", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PersExcellenceROSStatus", Status.ToString());
                        cmd.Parameters.AddWithValue("@PersPlanUnplanLeavesROS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PersExTimeConsciousnessROS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PersExTeamColaborationROS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PersExProfessionalismROS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PersExAdpoliciesROS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PersExSpecialEffortsROS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PersExTrainingUtilizationROS", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Year", currentYear.ToString());

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public void ProExcellenceData2()
        {
            if (Session["EmpId"] != null && Session["EmpId"].ToString() != "")
            {
                try
                {
                    using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM AppricialsSelfROS WHERE EmpId = @EmployeeId", connection1))
                        {
                            sqlcmd.Parameters.AddWithValue("@EmployeeId", Session["EmpId"].ToString());
                            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
                            System.Data.DataTable dt = new System.Data.DataTable();
                            sqlda.Fill(dt);
                            int RowCount = dt.Rows.Count;
                            connection1.Close();
                            int i = 0;
                            int currentYear = DateTime.Now.Year;
                            if (RowCount > 0)
                            {
                                string ProExellenceSelfStatus = dt.Rows[i]["ProExellenceSelfStatus"].ToString().Trim();  //
                                string ProQuantitySelf = dt.Rows[i]["ProQuantitySelf"].ToString().Trim();
                                string ProTATSelf = dt.Rows[i]["ProTATSelf"].ToString().Trim();
                                string ProPMSSelf = dt.Rows[i]["ProPMSSelf"].ToString().Trim();
                                string ProTPDSelf = dt.Rows[i]["ProTPDSelf"].ToString().Trim();
                                string ProTeamKnowledgeSelf = dt.Rows[i]["ProTeamKnowledgeSelf"].ToString().Trim();
                                string ProCommunicationSelf = dt.Rows[i]["ProCommunicationSelf"].ToString().Trim();


                                string ProExellenceROSStatus = dt.Rows[i]["ProExellenceROSStatus"].ToString().Trim();  //
                                string ProQuantityROS = dt.Rows[i]["ProQuantityROS"].ToString().Trim();
                                string ProTATROS = dt.Rows[i]["ProTATROS"].ToString().Trim();
                                string ProPMSROS = dt.Rows[i]["ProPMSROS"].ToString().Trim();
                                string ProTPDROS = dt.Rows[i]["ProTPDROS"].ToString().Trim();
                                string ProTeamKnowledgeROS = dt.Rows[i]["ProTeamKnowledgeROS"].ToString().Trim();
                                string ProCommunicationROS = dt.Rows[i]["ProCommunicationROS"].ToString().Trim();

                                string PersExcellenceSelfStatus = dt.Rows[i]["PersExcellenceSelfStatus"].ToString().Trim();  //
                                string PersPlanUnplanLeavesSelf = dt.Rows[i]["PersPlanUnplanLeavesSelf"].ToString().Trim();
                                string PersExTimeConsciousnessSelf = dt.Rows[i]["PersExTimeConsciousnessSelf"].ToString().Trim();
                                string PersExTeamColaborationSelf = dt.Rows[i]["PersExTeamColaborationSelf"].ToString().Trim();
                                string PersExProfessionalismSelf = dt.Rows[i]["PersExProfessionalismSelf"].ToString().Trim();
                                string PersExAdpoliciesSelf = dt.Rows[i]["PersExAdpoliciesSelf"].ToString().Trim();
                                string PersExSpecialEffortsSelf = dt.Rows[i]["PersExSpecialEffortsSelf"].ToString().Trim();
                                string PersExTrainingUtilizationSelf = dt.Rows[i]["PersExTrainingUtilizationSelf"].ToString().Trim();

                                string PersExcellenceROSStatus = dt.Rows[i]["PersExcellenceROSStatus"].ToString().Trim();  //
                                string PersPlanUnplanLeavesROS = dt.Rows[i]["PersPlanUnplanLeavesROS"].ToString().Trim();
                                string PersExTimeConsciousnessROS = dt.Rows[i]["PersExTimeConsciousnessROS"].ToString().Trim();
                                string PersExTeamColaborationROS = dt.Rows[i]["PersExTeamColaborationROS"].ToString().Trim();
                                string PersExProfessionalismROS = dt.Rows[i]["PersExProfessionalismROS"].ToString().Trim();
                                string PersExAdpoliciesROS = dt.Rows[i]["PersExAdpoliciesROS"].ToString().Trim();
                                string PersExSpecialEffortsROS = dt.Rows[i]["PersExSpecialEffortsROS"].ToString().Trim();
                                string PersExTrainingUtilizationROS = dt.Rows[i]["PersExTrainingUtilizationROS"].ToString().Trim();

                                IEnumerable<int> years = dt.AsEnumerable().Select(row => int.Parse(row["Year"].ToString().Trim()));
                                int maxYear = years.Max();
                                if (ProExellenceSelfStatus == "1" && maxYear == currentYear)
                                {
                                    TextBox[] textBoxes = { TextBox19, TextBox20, TextBox21, TextBox22, TextBox23, TextBox24, TextBox25 };

                                    string[] columnNames = { "PersPlanUnplanLeavesSelf", "PersExTimeConsciousnessSelf", "PersExTeamColaborationSelf", "PersExProfessionalismSelf", "PersExAdpoliciesSelf", "PersExSpecialEffortsSelf", "PersExTrainingUtilizationSelf" };

                                    for (int j = 0; j < columnNames.Length; j++)
                                    {
                                        string value = dt.Rows[i][columnNames[j]].ToString().Trim();

                                        if (ProExellenceSelfStatus == "1" && !string.IsNullOrEmpty(value))
                                        {
                                            textBoxes[j].ReadOnly = true;
                                            textBoxes[j].Text = value;
                                        }
                                        else
                                        {
                                            textBoxes[j].ReadOnly = false;
                                        }
                                    }
                                }
                                
                                else
                                {
                                    InsertProExcellenceAppricials();
                                }
                            }
                            else
                            {
                                InsertProExcellenceAppricials();
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

        private void CalculateAndDisplayResult2()
        {
            TextBox[] defaultTextBoxes = { TextBox26, TextBox34, TextBox35, TextBox36, TextBox37, TextBox38, TextBox39 };
            TextBox[] textBoxes = { TextBox19, TextBox20, TextBox21, TextBox22, TextBox23, TextBox24, TextBox25 };
            TextBox[] resultTextBoxes = { TextBox27, TextBox28, TextBox29, TextBox30, TextBox31, TextBox32, TextBox33 };

            for (int i = 0; i < defaultTextBoxes.Length; i++)
            {
                int defaultValue = string.IsNullOrEmpty(defaultTextBoxes[i].Text) ? 0 : Convert.ToInt32(defaultTextBoxes[i].Text);
                int userInputValue = string.IsNullOrEmpty(textBoxes[i].Text) ? 0 : Convert.ToInt32(textBoxes[i].Text);
                int percentageValue = (userInputValue * defaultValue) / 100;
                resultTextBoxes[i].Text = percentageValue.ToString();
            }
            int totalSum = 0;
            foreach (TextBox textBox in resultTextBoxes)
            {
                totalSum += string.IsNullOrEmpty(textBox.Text) ? 0 : Convert.ToInt32(textBox.Text);
            }
            TextBox42.Text = totalSum.ToString();

            int txtdefaultcountTotal = 0;
            foreach (TextBox textBox in defaultTextBoxes)
            {
                txtdefaultcountTotal += string.IsNullOrEmpty(textBox.Text) ? 0 : Convert.ToInt32(textBox.Text);
            }

            TextBox40.Text = txtdefaultcountTotal.ToString();

            int totalPer = (int)((totalSum / (double)txtdefaultcountTotal) * 100);
            TextBox41.Text = totalPer.ToString() + "%";
        }


        private void UpdateDatabaseField(string fieldName, string value)
        {
            try
            {
                using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    string updateQuery = $"UPDATE AppricialsSelfROS SET {fieldName} = @Value WHERE EmpId = @EmployeeId AND Year = (SELECT MAX(Year) FROM AppricialsSelfROS)";

                    using (SqlCommand sqlcmd = new SqlCommand(updateQuery, connection1))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmployeeId", Session["EmpId"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Value", value);
                        connection1.Open();
                        sqlcmd.ExecuteNonQuery();
                        ProExcellenceData2();
                        CalculateAndDisplayResult2();
                    }
                }
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }

        public void DataBindTextBoxes()
        {
            try
            {
                using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM AppricialsSelfROS WHERE EmpId = @EmployeeId", connection1))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmployeeId", Session["EmpId"].ToString());
                        SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        sqlda.Fill(dt);
                        int RowCount = dt.Rows.Count;
                        connection1.Close();
                        int i = 0;
                        int currentYear = DateTime.Now.Year;
                        if (RowCount > 0)
                        {
                            object proQuantityROSValue = dt.Rows[i]["ProQuantityROS"];
                            int ProQuantityROS = proQuantityROSValue == DBNull.Value ? 0 : Convert.ToInt32(proQuantityROSValue);

                            object ProTATROSvalue = dt.Rows[i]["ProTATROS"];
                            int ProTATROS = ProTATROSvalue == DBNull.Value ? 0 : Convert.ToInt32(ProTATROSvalue);

                            object proPMSROSValue = dt.Rows[i]["ProPMSROS"];
                            int ProPMSROS = proPMSROSValue == DBNull.Value ? 0 : Convert.ToInt32(proPMSROSValue);

                            object proTPDROSValue = dt.Rows[i]["ProTPDROS"];
                            int ProTPDROS = proTPDROSValue == DBNull.Value ? 0 : Convert.ToInt32(proTPDROSValue);

                            object proTeamKnowledgeROSValue = dt.Rows[i]["ProTeamKnowledgeROS"];
                            int ProTeamKnowledgeROS = proTeamKnowledgeROSValue == DBNull.Value ? 0 : Convert.ToInt32(proTeamKnowledgeROSValue);

                            object proCommunicationROSValue = dt.Rows[i]["ProCommunicationROS"];
                            int ProCommunicationROS = proCommunicationROSValue == DBNull.Value ? 0 : Convert.ToInt32(proCommunicationROSValue);

                            object persPlanUnplanLeavesROSValue = dt.Rows[i]["PersPlanUnplanLeavesROS"];
                            int PersPlanUnplanLeavesROS = persPlanUnplanLeavesROSValue == DBNull.Value ? 0 : Convert.ToInt32(persPlanUnplanLeavesROSValue);

                            object persExTimeConsciousnessROSValue = dt.Rows[i]["PersExTimeConsciousnessROS"];
                            int PersExTimeConsciousnessROS = persExTimeConsciousnessROSValue == DBNull.Value ? 0 : Convert.ToInt32(persExTimeConsciousnessROSValue);

                            object persExTeamColaborationROSValue = dt.Rows[i]["PersExTeamColaborationROS"];
                            int PersExTeamColaborationROS = persExTeamColaborationROSValue == DBNull.Value ? 0 : Convert.ToInt32(persExTeamColaborationROSValue);

                            object persExProfessionalismROSValue = dt.Rows[i]["PersExProfessionalismROS"];
                            int PersExProfessionalismROS = persExProfessionalismROSValue == DBNull.Value ? 0 : Convert.ToInt32(persExProfessionalismROSValue);

                            object persExAdpoliciesROSValue = dt.Rows[i]["PersExAdpoliciesROS"];
                            int PersExAdpoliciesROS = persExAdpoliciesROSValue == DBNull.Value ? 0 : Convert.ToInt32(persExAdpoliciesROSValue);

                            object persExSpecialEffortsROSValue = dt.Rows[i]["PersExSpecialEffortsROS"];
                            int PersExSpecialEffortsROS = persExSpecialEffortsROSValue == DBNull.Value ? 0 : Convert.ToInt32(persExSpecialEffortsROSValue);

                            object persExTrainingUtilizationROSValue = dt.Rows[i]["PersExTrainingUtilizationROS"];
                            int PersExTrainingUtilizationROS = persExTrainingUtilizationROSValue == DBNull.Value ? 0 : Convert.ToInt32(persExTrainingUtilizationROSValue);


                            TextBox7.Text = ProQuantityROS.ToString();
                            TextBox8.Text = ProTATROS.ToString();
                            TextBox9.Text = ProPMSROS.ToString();
                            TextBox10.Text = ProTPDROS.ToString();
                            TextBox17.Text = ProTeamKnowledgeROS.ToString();
                            TextBox18.Text = ProCommunicationROS.ToString();

                            TextBox43.Text = PersPlanUnplanLeavesROS.ToString();
                            TextBox45.Text = PersExTimeConsciousnessROS.ToString();
                            TextBox47.Text = PersExTeamColaborationROS.ToString();
                            TextBox49.Text = PersExProfessionalismROS.ToString();
                            TextBox51.Text = PersExAdpoliciesROS.ToString();
                            TextBox53.Text = PersExSpecialEffortsROS.ToString();
                            TextBox55.Text = PersExTrainingUtilizationROS.ToString();

                            TextBox[] defaultTextBoxes = { TextBox26, TextBox34, TextBox35, TextBox36, TextBox37, TextBox38, TextBox39 };
                            TextBox[] textBoxes = { TextBox43, TextBox45, TextBox47, TextBox49, TextBox51, TextBox53, TextBox55 };
                            int[] textBoxValues = new int[textBoxes.Length];
                            TextBox[] resultTextBoxes = { TextBox44, TextBox46, TextBox48, TextBox50, TextBox52, TextBox54, TextBox56 };

                            for (int k = 0; k < defaultTextBoxes.Length; k++)
                            {
                                int defaultValue = string.IsNullOrEmpty(defaultTextBoxes[k].Text) ? 0 : Convert.ToInt32(defaultTextBoxes[k].Text);
                                int userInputValue = string.IsNullOrEmpty(textBoxes[k].Text) ? 0 : Convert.ToInt32(textBoxes[k].Text);
                                int percentageValue = (userInputValue * defaultValue) / 100;
                                resultTextBoxes[k].Text = percentageValue.ToString();
                            }


                            int totalSum = 0;
                            foreach (TextBox textBox in resultTextBoxes)
                            {
                                totalSum += string.IsNullOrEmpty(textBox.Text) ? 0 : Convert.ToInt32(textBox.Text);
                            }
                            TextBox58.Text = totalSum.ToString();

                            int txtdefaultcountTotal = 0;
                            foreach (TextBox textBox in textBoxes)
                            {
                                txtdefaultcountTotal += string.IsNullOrEmpty(textBox.Text) ? 0 : Convert.ToInt32(textBox.Text);
                            }

                            int totalPer = (int)((totalSum / (double)txtdefaultcountTotal) * 100);
                            TextBox57.Text = totalPer.ToString() + "%";
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public void DataBindTextBoxes2()
        {
            try
            {
                using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection1.Open();
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM AppricialsSelfROS WHERE EmpId = @EmployeeId", connection1))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmployeeId", Session["EmpId"].ToString());
                        SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        sqlda.Fill(dt);
                        int RowCount = dt.Rows.Count;
                        connection1.Close();

                        if (RowCount == 1)
                        {
                            int ProQuantityROS = ConvertToInt(dt.Rows[0]["ProQuantityROS"]);
                            int ProTATROS = ConvertToInt(dt.Rows[0]["ProTATROS"]);
                            int ProPMSROS = ConvertToInt(dt.Rows[0]["ProPMSROS"]);
                            int ProTPDROS = ConvertToInt(dt.Rows[0]["ProTPDROS"]);
                            int ProTeamKnowledgeROS = ConvertToInt(dt.Rows[0]["ProTeamKnowledgeROS"]);
                            int ProCommunicationROS = ConvertToInt(dt.Rows[0]["ProCommunicationROS"]);
                            int PersPlanUnplanLeavesROS = ConvertToInt(dt.Rows[0]["PersPlanUnplanLeavesROS"]);
                            int PersExTimeConsciousnessROS = ConvertToInt(dt.Rows[0]["PersExTimeConsciousnessROS"]);
                            int PersExTeamColaborationROS = ConvertToInt(dt.Rows[0]["PersExTeamColaborationROS"]);
                            int PersExProfessionalismROS = ConvertToInt(dt.Rows[0]["PersExProfessionalismROS"]);
                            int PersExAdpoliciesROS = ConvertToInt(dt.Rows[0]["PersExAdpoliciesROS"]);
                            int PersExSpecialEffortsROS = ConvertToInt(dt.Rows[0]["PersExSpecialEffortsROS"]);
                            int PersExTrainingUtilizationROS = ConvertToInt(dt.Rows[0]["PersExTrainingUtilizationROS"]);

                            TextBox7.Text = ProQuantityROS.ToString();
                            TextBox8.Text = ProTATROS.ToString();
                            TextBox9.Text = ProPMSROS.ToString();
                            TextBox10.Text = ProTPDROS.ToString();
                            TextBox17.Text = ProTeamKnowledgeROS.ToString();
                            TextBox18.Text = ProCommunicationROS.ToString();

                            TextBox43.Text = PersPlanUnplanLeavesROS.ToString();
                            TextBox45.Text = PersExTimeConsciousnessROS.ToString();
                            TextBox47.Text = PersExTeamColaborationROS.ToString();
                            TextBox49.Text = PersExProfessionalismROS.ToString();
                            TextBox51.Text = PersExAdpoliciesROS.ToString();
                            TextBox53.Text = PersExSpecialEffortsROS.ToString();
                            TextBox55.Text = PersExTrainingUtilizationROS.ToString();

                            CalculateAndBindPercentages();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log it as needed
                throw ex;
            }
        }

        private int ConvertToInt(object value)
        {
            if (value == DBNull.Value || string.IsNullOrEmpty(value.ToString()))
            {
                return 0;
            }

            return Convert.ToInt32(value);
        }

        private void CalculateAndBindPercentages()
        {
            TextBox[] defaultTextBoxes = { TextBox26, TextBox34, TextBox35, TextBox36, TextBox37, TextBox38, TextBox39 };
            TextBox[] textBoxes = { TextBox43, TextBox45, TextBox47, TextBox49, TextBox51, TextBox53, TextBox55 };
            TextBox[] resultTextBoxes = { TextBox44, TextBox46, TextBox48, TextBox50, TextBox52, TextBox54, TextBox56 };

            for (int k = 0; k < defaultTextBoxes.Length; k++)
            {
                int defaultValue = ConvertToInt(defaultTextBoxes[k].Text);
                int userInputValue = ConvertToInt(textBoxes[k].Text);
                int percentageValue = (userInputValue * defaultValue) / 100;
                resultTextBoxes[k].Text = percentageValue.ToString();
            }

            int totalSum = 0;
            foreach (TextBox textBox in resultTextBoxes)
            {
                totalSum += ConvertToInt(textBox.Text);
            }
            TextBox58.Text = totalSum.ToString();

            int txtdefaultcountTotal = 0;
            foreach (TextBox textBox in defaultTextBoxes)
            {
                txtdefaultcountTotal += ConvertToInt(textBox.Text);
            }

            int totalPer = (int)((totalSum / (double)txtdefaultcountTotal) * 100);
            TextBox57.Text = totalPer.ToString() + "%";

            TextBox[] defaultTextBoxes1 = { txtdefaluttextbox1, txtdefaluttextbox2, txtdefaluttextbox3, txtdefaluttextbox4, txtdefaluttextbox5, txtdefaluttextbox6 };
            TextBox[] textBoxes1 = { TextBox7, TextBox8, TextBox9, TextBox10, TextBox17, TextBox18 };
            TextBox[] resultTextBoxes1 = { TextBox59, TextBox60, TextBox61, TextBox62, TextBox63, TextBox64 };

            for (int k = 0; k < defaultTextBoxes1.Length; k++)
            {
                int defaultValue = ConvertToInt(defaultTextBoxes1[k].Text);
                int userInputValue = ConvertToInt(textBoxes1[k].Text);
                int percentageValue = (userInputValue * defaultValue) / 100;
                resultTextBoxes1[k].Text = percentageValue.ToString();
            }

            int totalSum1 = 0;
            foreach (TextBox textBox in resultTextBoxes1)
            {
                totalSum1 += ConvertToInt(textBox.Text);
            }
            TextBox66.Text = totalSum1.ToString();

            int txtdefaultcountTotal1 = 0;
            foreach (TextBox textBox in defaultTextBoxes1)
            {
                txtdefaultcountTotal1 += ConvertToInt(textBox.Text);
            }

            int totalPer1 = (int)((totalSum1 / (double)txtdefaultcountTotal1) * 100);
            TextBox65.Text = totalPer1.ToString() + "%";
        }


        protected void TextDropValue_TextChanged(object sender, EventArgs e)
        {
            TextBox currentTextBox = (TextBox)sender;

            // Define the array of TextBoxes you want to work with
            TextBox[] textBoxes = { TextBox68, TextBox69, TextBox70, TextBox71, TextBox72, TextBox73 };

            // Find the index of the current TextBox in the array
            int index = Array.IndexOf(textBoxes, currentTextBox);

            // Define arrays for DropDownLists and TextBoxes
            DropDownList[] dropDowns = { DropDownList1, DropDownList2, DropDownList3, DropDownList4, DropDownList5, DropDownList6 };
           
            string selectedValue = dropDowns[index].SelectedItem?.Text ?? "";

            string statusColumnName = "";
            string detailsColumnName = "";

            switch (index)
            {
                case 0:
                    statusColumnName = "WorkIssueStatus";
                    detailsColumnName = "WorkIssueDetails";
                    break;
                case 1:
                    statusColumnName = "LeaveIssueStatus";
                    detailsColumnName = "LeaveIssueDetails";
                    break;
                case 2:
                    statusColumnName = "StabilityIssueStatus";
                    detailsColumnName = "StabilityIssueDetails";
                    break;
                case 3:
                    statusColumnName = "NonSupAttitudeStatus";
                    detailsColumnName = "NonSupAttitudeDetails";
                    break;
                case 4:
                    statusColumnName = "SpecificNotePointStatus";
                    detailsColumnName = "SpecificNotePointDetails";
                    break;
                case 5:
                    statusColumnName = "OverallPerformanceStatus";
                    detailsColumnName = "OverallPerformanceDetails";
                    break;
                default:
                    break;
            }

            string details = textBoxes[index].Text;

            DropDownValueUpdate(statusColumnName, detailsColumnName, selectedValue, details);
        }

        private void DropDownValueUpdate(string statusColumnName, string detailsColumnName, string status, string details)
        {
            string employeeId = Session["EmpId"].ToString();
            string updateQuery = $"UPDATE RosHodsSign SET {statusColumnName} = @Status, {detailsColumnName} = @Details WHERE Id = @EmployeeId and Year=@Year";
            int CurrentYear = DateTime.Now.Year;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Details", details);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@Year", CurrentYear);

                    connection.Open();
                    if(status == "Yes" || status == "No")
                    {
                        cmd.ExecuteNonQuery();
                        RosHeadsDataBindTextBoxes();
                    }
                    else
                    {
                        Response.Write("<script>alert('Please select Yes or No....')</script>");
                        RosHeadsDataBindTextBoxes();
                    }
                    
                }
            }
        }
        private void LogError(string errorMessage)
        {
            System.Diagnostics.Trace.TraceError(errorMessage);
        }

        public void RosHeadsDataBindTextBoxes()
        {
            if (Session["EmpId"] != null && Session["EmpId"].ToString() != "" )
            {
                try
                {
                    int currentYear = DateTime.Now.Year;
                    using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM RosHodsSign WHERE Id = @EmployeeId", connection1))
                        {
                            sqlcmd.Parameters.AddWithValue("@EmployeeId", Session["EmpId"].ToString());
                            sqlcmd.Parameters.AddWithValue("@Year", currentYear.ToString());
                            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
                            System.Data.DataTable dt = new System.Data.DataTable();
                            sqlda.Fill(dt);
                            int RowCount = dt.Rows.Count;
                            connection1.Close();
                            int i = 0;                           
                            if (RowCount > 0)
                            {
                                IEnumerable<int> years = dt.AsEnumerable().Select(row => int.Parse(row["Year"].ToString().Trim()));
                                int maxYear = years.Max();
                                if (maxYear == currentYear)
                                {
                                    DropDownList[] dropDowns = { DropDownList1, DropDownList2, DropDownList3, DropDownList4, DropDownList5, DropDownList6 };
                                    TextBox[] textBoxes = { TextBox68, TextBox69, TextBox70, TextBox71, TextBox72, TextBox73 };
                                    string[] columnNames = { "WorkIssueStatus", "WorkIssueDetails", "LeaveIssueStatus", "LeaveIssueDetails", "StabilityIssueStatus", "StabilityIssueDetails", "NonSupAttitudeStatus", "NonSupAttitudeDetails", "SpecificNotePointStatus", "SpecificNotePointDetails", "OverallPerformanceStatus", "OverallPerformanceDetails" };

                                    for (int j = 0; j < columnNames.Length; j += 2) 
                                    {
                                        string statusColumnName = columnNames[j];
                                        string detailsColumnName = columnNames[j + 1];
                                        string status = dt.Rows[i][statusColumnName]?.ToString().Trim(); 
                                        string details = dt.Rows[i][detailsColumnName]?.ToString().Trim(); 

                                        if (!string.IsNullOrEmpty(status))
                                        {
                                            dropDowns[j / 2].SelectedValue = status;
                                            textBoxes[j / 2].Text = details;                                           
                                            textBoxes[j / 2].ReadOnly = true;
                                            dropDowns[j / 2].Enabled = false;
                                        }
                                        else
                                        {
                                            dropDowns[j / 2].SelectedIndex = 0; 
                                            textBoxes[j / 2].Text = ""; 
                                            textBoxes[j / 2].ReadOnly = false;
                                            dropDowns[j / 2].Enabled = true;
                                        }                                       
                                       // textBoxes[j / 2].ReadOnly = ProExellenceSelfStatus == "1";
                                    }

                                }
                                else
                                {
                                    RosHodsSignInsert();
                                }
                            }
                            else
                            {
                                RosHodsSignInsert();
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


        protected void TextpersonalUpdate_TextChanged(object sender, EventArgs e)
        {
            TextBox currentTextBox = (TextBox)sender;

            // Define the array of TextBoxes you want to work with
            TextBox[] textBoxes = { TextBox74, TextBox75, TextBox76, TextBox77, TextBox78, TextBox79, TextBox80, TextBox81 };

            // Find the index of the current TextBox in the array
            int index = Array.IndexOf(textBoxes, currentTextBox);

            // Define arrays for DropDownLists and TextBoxes
            DropDownList[] dropDowns = { DropDownList7, DropDownList8, DropDownList9, DropDownList10, DropDownList11, DropDownList12, DropDownList13, DropDownList14 };

            string selectedValue = dropDowns[index].SelectedItem?.Text ?? "";

            string statusColumnName = "";
            string detailsColumnName = "";

            switch (index)
            {
                case 0:
                    statusColumnName = "PersonalMarriedStatus";
                    detailsColumnName = "PersonalMarriedDetails";
                    break;
                case 1:
                    statusColumnName = "PersonalHigherStudiesStatus";
                    detailsColumnName = "PersonalHigherStudiesDetails";
                    break;
                case 2:
                    statusColumnName = "PersonalHealthIssuesStatus";
                    detailsColumnName = "PersonalHealthIssuesDetails";
                    break;
                case 3:
                    statusColumnName = "PersonalOthersStatus";
                    detailsColumnName = "PersonalOthersDetails";
                    break;
                case 4:
                    statusColumnName = "PersonalMarriagePlansStatus";
                    detailsColumnName = "PersonalMarriagePlansDetails";
                    break;
                case 5:
                    statusColumnName = "PersonalPlansForHigherStudyStatus";
                    detailsColumnName = "PersonalPlansForHigherStudyDetails";
                    break;
                case 6:
                    statusColumnName = "PersonalCertificationPlansStatus";
                    detailsColumnName = "PersonalCertificationPlansDetails";
                    break;
                case 7:
                    statusColumnName = "PersonalOthers2Status";
                    detailsColumnName = "PersonalOthers2Details";
                    break;
                default:
                    break;
            }

            string details = textBoxes[index].Text;

            PersonalUpdateData(statusColumnName, detailsColumnName, selectedValue, details);
        }
        private void PersonalUpdateData(string statusColumnName, string detailsColumnName, string status, string details)
        {
            string employeeId = Session["EmpId"].ToString();
            string updateQuery = $"UPDATE AllSelfdata SET {statusColumnName} = @Status, {detailsColumnName} = @Details WHERE EmpId = @EmployeeId";

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Details", details);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    PersonalUpdateDataSelf();
                }
            }
        }

        public void PersonalUpdateDataSelf()
        {
            if (Session["EmpId"] != null && Session["EmpId"].ToString() != "")
            {
                try
                {
                    using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM AllSelfdata WHERE EmpId = @EmployeeId", connection1))
                        {
                            sqlcmd.Parameters.AddWithValue("@EmployeeId", Session["EmpId"].ToString());
                            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
                            System.Data.DataTable dt = new System.Data.DataTable();
                            sqlda.Fill(dt);
                            int RowCount = dt.Rows.Count;
                            connection1.Close();
                            int i = 0;
                            int currentYear = DateTime.Now.Year;
                            if (RowCount > 0)
                            {
                                IEnumerable<int> years = dt.AsEnumerable().Select(row => int.Parse(row["Year"].ToString().Trim()));
                                int maxYear = years.Max();
                                if (maxYear == currentYear)
                                {
                                    DropDownList[] dropDowns = { DropDownList7, DropDownList8, DropDownList9, DropDownList10, DropDownList11, DropDownList12, DropDownList13, DropDownList14 };
                                    TextBox[] textBoxes = { TextBox74, TextBox75, TextBox76, TextBox77, TextBox78, TextBox79, TextBox80, TextBox81 };
                                    string[] columnNames = { "PersonalMarriedStatus", "PersonalMarriedDetails", "PersonalHigherStudiesStatus", "PersonalHigherStudiesDetails", "PersonalHealthIssuesStatus", "PersonalHealthIssuesDetails", "PersonalOthersStatus", "PersonalOthersDetails", "PersonalMarriagePlansStatus", "PersonalMarriagePlansDetails", "PersonalPlansForHigherStudyStatus", "PersonalPlansForHigherStudyDetails", "PersonalCertificationPlansStatus", "PersonalCertificationPlansDetails", "PersonalOthers2Status", "PersonalOthers2Details" };

                                    for (int j = 0; j < columnNames.Length; j += 2)
                                    {
                                        string statusColumnName = columnNames[j];
                                        string detailsColumnName = columnNames[j + 1];
                                        string status = dt.Rows[i][statusColumnName]?.ToString().Trim();
                                        string details = dt.Rows[i][detailsColumnName]?.ToString().Trim();

                                        if (!string.IsNullOrEmpty(status))
                                        {
                                            dropDowns[j / 2].SelectedValue = status;
                                            textBoxes[j / 2].Text = details;
                                            textBoxes[j / 2].ReadOnly = true;
                                            dropDowns[j / 2].Enabled = false;
                                        }
                                        else
                                        {
                                            dropDowns[j / 2].SelectedIndex = 0;
                                            textBoxes[j / 2].Text = "";
                                            textBoxes[j / 2].ReadOnly = false;
                                            dropDowns[j / 2].Enabled = true;
                                        }
                                        // textBoxes[j / 2].ReadOnly = ProExellenceSelfStatus == "1";
                                    }

                                }

                                else
                                {
                                     PersonalInsertDataSelf();
                                }
                            }
                            else
                            {
                                PersonalInsertDataSelf();
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


        protected void PointsHodComments_TextChanged(object sender, EventArgs e)
        {
            TextBox currentTextBox = (TextBox)sender;
          
            TextBox[] textBoxes = { TextBox92, TextBox93, TextBox94, TextBox95, TextBox96 };
            int index = Array.IndexOf(textBoxes, currentTextBox);

            string RosCommnet = "";

            switch (index) // Use index1 to determine which TextBox in textBoxes1 was edited
            {
                case 0:                   
                    RosCommnet = "KRAsRosComment";
                    break;
                case 1:
                    RosCommnet = "ProfessionalRosComment";
                    break;
                case 2:
                    RosCommnet = "PersonalRosComment";
                    break;
                case 3:
                    RosCommnet = "SpecialRosComment";
                    break;
                case 4:
                    RosCommnet = "OverallRosComment";
                    break;
                default:
                    break;
            }
            string enteredValue2 = currentTextBox.Text;
            string employeeId = Session["EmpId"].ToString();
            string updateQuery = $"UPDATE RosHodsSign SET {RosCommnet} = @RosCommnet WHERE Id = @EmployeeId";

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@RosCommnet", enteredValue2);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    GainedAndTotalPoints();
                }
            }
        }

        protected void PointsGained_TextChanged(object sender, EventArgs e)
        {
            TextBox currentTextBox = (TextBox)sender;

            TextBox[] textBoxes1 = { TextBox82, TextBox83, TextBox84, TextBox85 };
            TextBox[] textBoxes2 = { TextBox87, TextBox88, TextBox89, TextBox90 };

            int index2 = Array.IndexOf(textBoxes2, currentTextBox);

            string GainedPointsColumn = "";

            switch (index2)
            {
                case 0:
                    GainedPointsColumn = "KRAsGainedPoints";
                    break;
                case 1:
                    GainedPointsColumn = "ProfessionalGainedPoints";
                    break;
                case 2:
                    GainedPointsColumn = "PersonalGainedPoints";
                    break;
                case 3:
                    GainedPointsColumn = "SpecialGainedPoints";
                    break;             
                default:
                    break;
            }
            int currentYear = DateTime.Now.Year;
            string enteredValue2 = currentTextBox.Text;
            string employeeId = Session["EmpId"].ToString();
            string updateQuery = $"UPDATE RosHodsSign SET {GainedPointsColumn} = @GainedPoints WHERE Id = @EmployeeId and Year=@Year";

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@GainedPoints", enteredValue2);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@Year", currentYear);

                    TextBox correspondingTextBox = textBoxes1[index2];

                    // Perform validations using the corresponding TextBox
                    if (!IsValidInput(correspondingTextBox, enteredValue2))
                    {
                       
                        return; // Exit the method without updating the database
                    }

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    GainedAndTotalPoints();
                }
            }
        }



        public void GainedAndTotalPoints()
        {
            if (Session["EmpId"] != null && Session["EmpId"].ToString() != "")
            {
                try
                {
                    using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM RosHodsSign WHERE Id = @EmployeeId", connection1))
                        {
                            sqlcmd.Parameters.AddWithValue("@EmployeeId", Session["EmpId"].ToString());
                            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
                            System.Data.DataTable dt = new System.Data.DataTable();
                            sqlda.Fill(dt);
                            int RowCount = dt.Rows.Count;
                            connection1.Close();
                            int i = 0;
                            int currentYear = DateTime.Now.Year;
                            if (RowCount > 0)
                            {
                                object KRAsTotalPointsValue = dt.Rows[i]["KRAsTotalPoints"];
                                int KRAsTotalPoints = KRAsTotalPointsValue == DBNull.Value ? 0 : Convert.ToInt32(KRAsTotalPointsValue);

                                object KRAsGainedPointsValue = dt.Rows[i]["KRAsGainedPoints"];
                                int KRAsGainedPoints = KRAsGainedPointsValue == DBNull.Value ? 0 : Convert.ToInt32(KRAsGainedPointsValue);


                                object ProfessionalTotalPointsValue = dt.Rows[i]["ProfessionalTotalPoints"];
                                int ProfessionalTotalPoints = ProfessionalTotalPointsValue == DBNull.Value ? 0 : Convert.ToInt32(ProfessionalTotalPointsValue);

                                object ProfessionalGainedPointsValue = dt.Rows[i]["ProfessionalGainedPoints"];
                                int ProfessionalGainedPoints = ProfessionalGainedPointsValue == DBNull.Value ? 0 : Convert.ToInt32(ProfessionalGainedPointsValue);


                                object PersonalTotalPointsValue = dt.Rows[i]["PersonalTotalPoints"];
                                int PersonalTotalPoints = PersonalTotalPointsValue == DBNull.Value ? 0 : Convert.ToInt32(PersonalTotalPointsValue);

                                object PersonalGainedPointsValue = dt.Rows[i]["PersonalGainedPoints"];
                                int PersonalGainedPoints = PersonalGainedPointsValue == DBNull.Value ? 0 : Convert.ToInt32(PersonalGainedPointsValue);


                                object SpecialTotalPointsValue = dt.Rows[i]["SpecialTotalPoints"];
                                int SpecialTotalPoints = SpecialTotalPointsValue == DBNull.Value ? 0 : Convert.ToInt32(SpecialTotalPointsValue);

                                object SpecialGainedPointsValue = dt.Rows[i]["SpecialGainedPoints"];
                                int SpecialGainedPoints = SpecialGainedPointsValue == DBNull.Value ? 0 : Convert.ToInt32(SpecialGainedPointsValue);

                                TextBox82.Text = KRAsTotalPoints.ToString();
                                TextBox87.Text = KRAsGainedPoints.ToString();
                                TextBox83.Text = ProfessionalTotalPoints.ToString();
                                TextBox88.Text = ProfessionalGainedPoints.ToString();
                                TextBox84.Text = PersonalTotalPoints.ToString();
                                TextBox89.Text = PersonalGainedPoints.ToString();
                                TextBox85.Text = SpecialTotalPoints.ToString();
                                TextBox90.Text = SpecialGainedPoints.ToString();


                                TextBox[] textboxes11 = { TextBox82, TextBox83, TextBox84, TextBox85 };
                                TextBox[] textboxes12 = { TextBox87, TextBox88, TextBox89, TextBox90 };


                                int totalPointsSum = textboxes11.Sum(tb => string.IsNullOrEmpty(tb.Text) ? 0 : int.Parse(tb.Text.Trim()));
                                TextBox86.Text = totalPointsSum.ToString();

                                int gainedPointsSum = textboxes12.Sum(tb => string.IsNullOrEmpty(tb.Text) ? 0 : int.Parse(tb.Text.Trim()));
                                TextBox91.Text = gainedPointsSum.ToString();


                                IEnumerable<int> years = dt.AsEnumerable().Select(row => int.Parse(row["Year"].ToString().Trim()));
                                int maxYear = years.Max();
                                if ( maxYear == currentYear)
                                {
                                    TextBox[] textBoxes = { TextBox82, TextBox87, TextBox92,TextBox83, TextBox88, TextBox93,TextBox84, TextBox89, TextBox94,TextBox85, TextBox90,  TextBox95};
                                    string[] columnNames = { "KRAsTotalPoints", "KRAsGainedPoints", "KRAsRosComment", "ProfessionalTotalPoints", "ProfessionalGainedPoints", "ProfessionalRosComment","PersonalTotalPoints", "PersonalGainedPoints", "PersonalRosComment","SpecialTotalPoints", "SpecialGainedPoints", "SpecialRosComment"};

                                    for (int j = 0; j < columnNames.Length; j++)
                                    {
                                        string value = dt.Rows[i][columnNames[j]].ToString().Trim();

                                        if ( !string.IsNullOrEmpty(value))
                                        {
                                            textBoxes[j].ReadOnly = true;
                                            textBoxes[j].Text = value;
                                        }
                                        else
                                        {
                                            textBoxes[j].ReadOnly = false;
                                        }
                                    }
                                }
                                else
                                {
                                    RosHodsSignInsert();
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


        private bool IsValidInput(TextBox correspondingTextBox, string enteredValue)
        {
            if (string.IsNullOrEmpty(correspondingTextBox.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Before The TextBox..')", true);
                return false;
            }

            int correspondingValue = Convert.ToInt32(correspondingTextBox.Text);
            int enteredIntValue = Convert.ToInt32(enteredValue);
            if (enteredIntValue >= correspondingValue)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Less Than " + correspondingValue + " Value..');", true);
                return false;
            }

            return true;
        }

        

        protected void PointsTotal_TextChanged(object sender, EventArgs e)
        {
            TextBox currentTextBox = (TextBox)sender;
            TextBox[] textBoxes1 = { TextBox82, TextBox83, TextBox84, TextBox85 };
            int index1 = Array.IndexOf(textBoxes1, currentTextBox);
            string TotalPointsColumn = "";

            switch (index1)
            {
                case 0:
                    TotalPointsColumn = "KRAsTotalPoints";
                    break;
                case 1:
                    TotalPointsColumn = "ProfessionalTotalPoints";
                    break;
                case 2:
                    TotalPointsColumn = "PersonalTotalPoints";
                    break;
                case 3:
                    TotalPointsColumn = "SpecialTotalPoints";
                    break;              
                default:
                    break;
            }

            string enteredValue2 = currentTextBox.Text;
            string employeeId = Session["EmpId"].ToString();
            string updateQuery = $"UPDATE RosHodsSign SET {TotalPointsColumn} = @TotalPointsColumn WHERE Id = @EmployeeId";

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@TotalPointsColumn", enteredValue2);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    // Calculate the overall total points by summing up the values of other columns
                    //int overallTotalPoints = textBoxes1.Where((tb, index) => index != 4) // Exclude OverallTotalPoints column
                    //                                     .Sum(tb => string.IsNullOrEmpty(tb.Text) ? 0 : int.Parse(tb.Text.Trim()));

                    // If the OverallTotalPoints column is being updated, use the calculated value
                    //if (TotalPointsColumn == "OverallTotalPoints")
                    //{
                    //    cmd.Parameters["@TotalPointsColumn"].Value = overallTotalPoints;
                    //}

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    GainedAndTotalPoints();
                }
            }

        }

        public void RosHodsSignInsert()
        {
            try
            {
                using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection1.Open();
                    int currentYear = DateTime.Now.Year;
                    using (SqlCommand sqlcmd = new SqlCommand("INSERT INTO RosHodsSign(Id,Name,Company,Branch,Department,Designation,WorkIssueStatus,WorkIssueDetails,LeaveIssueStatus,LeaveIssueDetails," +
                        "StabilityIssueStatus,StabilityIssueDetails,NonSupAttitudeStatus,NonSupAttitudeDetails,specificNotePointStatus,specificNotePointDetails,OverallPerformanceStatus,OverallPerformanceDetails," +
                        "KRAsTotalPoints,KRAsGainedPoints,KRAsRosComment,ProfessionalTotalPoints,ProfessionalGainedPoints,ProfessionalRosComment,PersonalTotalPoints,PersonalGainedPoints,PersonalRosComment," +
                        "SpecialTotalPoints,SpecialGainedPoints,SpecialRosComment,OverallTotalPoints,OverallGainedPoints,OverallRosComment,Year)" +
                        " values(@Id,@Name,@Company,@Branch,@Department,@Designation,@WorkIssueStatus,@WorkIssueDetails,@LeaveIssueStatus,@LeaveIssueDetails,@StabilityIssueStatus,@StabilityIssueDetails," +
                        "@NonSupAttitudeStatus,@NonSupAttitudeDetails,@specificNotePointStatus,@specificNotePointDetails,@OverallPerformanceStatus,@OverallPerformanceDetails," +
                        "@KRAsTotalPoints,@KRAsGainedPoints,@KRAsRosComment,@ProfessionalTotalPoints,@ProfessionalGainedPoints,@ProfessionalRosComment,@PersonalTotalPoints," +
                        "@PersonalGainedPoints,@PersonalRosComment,@SpecialTotalPoints,@SpecialGainedPoints,@SpecialRosComment," +
                        "@OverallTotalPoints,@OverallGainedPoints,@OverallRosComment,@Year)", connection1))
                    {
                        sqlcmd.Parameters.AddWithValue("@Id", Session["EmpId"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Name", Session["Name"].ToString()); 
                        sqlcmd.Parameters.AddWithValue("@Company", Session["CompanyName"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Branch", Session["BranchName"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Department", Session["DepartmentName"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Designation", Session["Designation"].ToString());
                        sqlcmd.Parameters.AddWithValue("@WorkIssueStatus", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@WorkIssueDetails", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@LeaveIssueStatus", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@LeaveIssueDetails", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@StabilityIssueStatus", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@StabilityIssueDetails", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@NonSupAttitudeStatus", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@NonSupAttitudeDetails", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@specificNotePointStatus", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@specificNotePointDetails", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@OverallPerformanceStatus", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@OverallPerformanceDetails", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@KRAsTotalPoints", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@KRAsGainedPoints", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@KRAsRosComment", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@ProfessionalTotalPoints", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@ProfessionalGainedPoints", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@ProfessionalRosComment", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalTotalPoints", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalGainedPoints", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalRosComment", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@SpecialTotalPoints", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@SpecialGainedPoints", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@SpecialRosComment", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@OverallTotalPoints", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@OverallGainedPoints", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@OverallRosComment", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@Year", currentYear.ToString());
                        sqlcmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }   

        public void PersonalInsertDataSelf()
        {
            try
            {
                using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection1.Open();
                    int currentYear = DateTime.Now.Year;
                    using (SqlCommand sqlcmd = new SqlCommand("INSERT INTO AllSelfdata(EmpId,Name,Company,Branch,Department,Designation,SpecialInitiativesBySelf,SpecialInitiativesRosCom,SpecialInitiativesHodCom,CommentsBySelf, CommentsRos, CommentsHod, CommentsStrengths, " +
                        "CommentsAreasImprovement, AppraiseeStrengthsRos, AppraiseeAreasImprovementRos,AppraiseeStrengthsHod, AppraiseeAreasImprovementHod, PersonalGoalsLastYear, PersonalGoalsPresentYear, PersonalMarriedStatus, PersonalMarriedDetails,PersonalHigherStudiesStatus, PersonalHigherStudiesDetails, PersonalHealthIssuesStatus, PersonalHealthIssuesDetails, PersonalOthersStatus, PersonalOthersDetails,PersonalMarriagePlansStatus, PersonalMarriagePlansDetails, PersonalPlansForHigherStudyStatus, PersonalPlansForHigherStudyDetails, PersonalCertificationPlansStatus,PersonalCertificationPlansDetails, PersonalOthers2Status, PersonalOthers2Details, TrainingBySelf, TrainingRos, TrainingHod, OthersBySelf, OthersRos,OthersHod, Year)" +
                        " values(@EmpId,@Name,@Company,@Branch,@Department,@Designation,@SpecialInitiativesBySelf,@SpecialInitiativesRosCom,@SpecialInitiativesHodCom,@CommentsBySelf,@CommentsRos,@CommentsHod,@CommentsStrengths,@CommentsAreasImprovement," +
                        "@AppraiseeStrengthsRos,@AppraiseeAreasImprovementRos,@AppraiseeStrengthsHod,@AppraiseeAreasImprovementHod,@PersonalGoalsLastYear,@PersonalGoalsPresentYear,@PersonalMarriedStatus,@PersonalMarriedDetails,@PersonalHigherStudiesStatus," +
                        "@PersonalHigherStudiesDetails,@PersonalHealthIssuesStatus,@PersonalHealthIssuesDetails,@PersonalOthersStatus,@PersonalOthersDetails,@PersonalMarriagePlansStatus,@PersonalMarriagePlansDetails," +
                        "@PersonalPlansForHigherStudyStatus,@PersonalPlansForHigherStudyDetails,@PersonalCertificationPlansStatus,@PersonalCertificationPlansDetails,@PersonalOthers2Status,@PersonalOthers2Details,@TrainingBySelf,@TrainingRos,@TrainingHod,@OthersBySelf,@OthersRos,@OthersHod,@Year)", connection1))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Name", Session["Name"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Company", Session["CompanyName"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Branch", Session["BranchName"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Department", Session["DepartmentName"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Designation", Session["Designation"].ToString());
                        sqlcmd.Parameters.AddWithValue("@SpecialInitiativesBySelf", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@SpecialInitiativesRosCom", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@SpecialInitiativesHodCom", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@CommentsBySelf", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@CommentsRos", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@CommentsHod", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@CommentsStrengths", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@CommentsAreasImprovement", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@AppraiseeStrengthsRos", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@AppraiseeAreasImprovementRos", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@AppraiseeStrengthsHod", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@AppraiseeAreasImprovementHod", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalGoalsLastYear", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalGoalsPresentYear", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalMarriedStatus", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalMarriedDetails", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalHigherStudiesStatus", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalHigherStudiesDetails", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalHealthIssuesStatus", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalHealthIssuesDetails", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalOthersStatus", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalOthersDetails", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalMarriagePlansStatus", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalMarriagePlansDetails", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalPlansForHigherStudyStatus", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalPlansForHigherStudyDetails", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalCertificationPlansStatus", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalCertificationPlansDetails", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalOthers2Status", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@PersonalOthers2Details", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@TrainingBySelf", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@TrainingRos", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@TrainingHod", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@OthersBySelf", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@OthersRos", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@OthersHod", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@Year", currentYear.ToString());
                        sqlcmd.ExecuteNonQuery();
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