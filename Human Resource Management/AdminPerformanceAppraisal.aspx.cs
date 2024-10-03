using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class AdminperformanceAppraisal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAppraisals();
                UpdateStatus();
            }
        }
        public void BindAppraisals()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM AppricialsSelfROS WHERE (YEAR(Year) = YEAR(GETDATE()))";
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            AllPerformance.Controls.Clear();
                            int RowCount = 1;
                            while (reader.Read())
                            {
                                string Id = reader["EmpId"].ToString();
                                string Name = reader["Name"].ToString();
                                // self enter values
                                string ProExellenceSelfStatus = reader["ProExellenceSelfStatus"].ToString();
                                string ProQuantitySelf = reader["ProQuantitySelf"].ToString();
                                string ProTATSelf = reader["ProTATSelf"].ToString();
                                string ProPMSSelf = reader["ProPMSSelf"].ToString();
                                string ProTPDSelf = reader["ProTPDSelf"].ToString();
                                string ProTeamKnowledgeSelf = reader["ProTeamKnowledgeSelf"].ToString();
                                string ProCommunicationSelf = reader["ProCommunicationSelf"].ToString();

                                // ros enter values
                               
                                string ProQuantityROS = reader["ProQuantityROS"].ToString();
                                string ProTATROS = reader["ProTATROS"].ToString();
                                string ProPMSROS = reader["ProPMSROS"].ToString();
                                string ProTPDROS = reader["ProTPDROS"].ToString();
                                string ProTeamKnowledgeROS = reader["ProTeamKnowledgeROS"].ToString();
                                string ProCommunicationROS = reader["ProCommunicationROS"].ToString();

                                TextBox3.Text = ProQuantityROS;
                                TextBox5.Text = ProTATROS;
                                TextBox7.Text = ProPMSROS;
                                TextBox10.Text = ProTPDROS;
                                TextBox12.Text = ProTeamKnowledgeROS;
                                TextBox14.Text = ProCommunicationROS;

                                string PersExcellenceSelfStatus = reader["PersExcellenceSelfStatus"].ToString();
                                string PersPlanUnplanLeavesSelf = reader["PersPlanUnplanLeavesSelf"].ToString();
                                string PersExTimeConsciousnessSelf = reader["PersExTimeConsciousnessSelf"].ToString();
                                string PersExTeamColaborationSelf = reader["PersExTeamColaborationSelf"].ToString();
                                string PersExProfessionalismSelf = reader["PersExProfessionalismSelf"].ToString();
                                string PersExAdpoliciesSelf = reader["PersExAdpoliciesSelf"].ToString();
                                string PersExSpecialEffortsSelf = reader["PersExSpecialEffortsSelf"].ToString();
                                string PersExTrainingUtilizationSelf = reader["PersExTrainingUtilizationSelf"].ToString();

                                string PersPlanUnplanLeavesROS = reader["PersPlanUnplanLeavesROS"].ToString();
                                string PersExTimeConsciousnessROS = reader["PersExTimeConsciousnessROS"].ToString();
                                string PersExTeamColaborationROS = reader["PersExTeamColaborationROS"].ToString();
                                string PersExProfessionalismROS = reader["PersExProfessionalismROS"].ToString();
                                string PersExAdpoliciesROS = reader["PersExAdpoliciesROS"].ToString();
                                string PersExSpecialEffortsROS = reader["PersExSpecialEffortsROS"].ToString();
                                string PersExTrainingUtilizationROS = reader["PersExTrainingUtilizationROS"].ToString();


                                TextBox15.Text = PersPlanUnplanLeavesROS;
                                TextBox18.Text = PersExTimeConsciousnessROS;
                                TextBox20.Text = PersExTeamColaborationROS;
                                TextBox22.Text = PersExProfessionalismROS;
                                TextBox24.Text = PersExAdpoliciesROS;
                                TextBox26.Text = PersExSpecialEffortsROS;
                                TextBox28.Text = PersExTrainingUtilizationROS;


                                if (ProExellenceSelfStatus == "1" && PersExcellenceSelfStatus == "1") 
                                {
                                    StringBuilder projectHtml = new StringBuilder();
                                    projectHtml.Append("<tr>");
                                    projectHtml.Append("<td>" + Id + "</td>");
                                    projectHtml.Append("<td><a href='#'>" + Name + " </a></td>");
                                    projectHtml.Append("<td>" + ProQuantitySelf + "</td>");
                                    projectHtml.Append("<td>" + ProTATSelf + "</td>");
                                    projectHtml.Append("<td>" + ProPMSSelf + "</td>");
                                    projectHtml.Append("<td>" + ProTPDSelf + "</td>");
                                    projectHtml.Append("<td>" + ProTeamKnowledgeSelf + "</td>");
                                    projectHtml.Append("<td>" + ProCommunicationSelf + "</td>");
                                    projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_appraisal' onclick =\"editappraisal('" + Id + "','" + Name + "','" + ProQuantitySelf + "','" + PersPlanUnplanLeavesSelf + "','" + ProTATSelf + "','" + PersExTimeConsciousnessSelf + "','" + ProPMSSelf + "','" + PersExTeamColaborationSelf + "','" + ProTPDSelf + "','" + PersExProfessionalismSelf + "','" + ProTeamKnowledgeSelf + "','" + PersExAdpoliciesSelf + "','" + ProCommunicationSelf + "','" + PersExSpecialEffortsSelf + "','"+ PersExTrainingUtilizationSelf + "')\"><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
                                   
                                    // projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#delete_resignation'data-hyid='" + Id + "'><i class='fa-regular fa-trash-can m-r-5' style='color:#ab0c19;'></i></a></td>");
                                    projectHtml.Append("</tr>");
                                    //   projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_resignation' onclick =\"editResign('" + Id + "')\"  ></a></td>");
                                    AllPerformance.Controls.Add(new LiteralControl(projectHtml.ToString()));

                                    RowCount++;
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

        public void UpdateStatus()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    int year = DateTime.Now.Year;

                    // Create a list to store the data to be updated
                    var updateList = new List<(string Id, bool IsProSelfComplete, bool IsPersSelfComplete)>();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM AppricialsSelfROS WHERE (YEAR(Year) = YEAR(GETDATE()))", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string Id = reader["EmpId"].ToString();

                                // Self-entered values
                                string ProQuantitySelf = reader["ProQuantitySelf"].ToString();
                                string ProTATSelf = reader["ProTATSelf"].ToString();
                                string ProPMSSelf = reader["ProPMSSelf"].ToString();
                                string ProTPDSelf = reader["ProTPDSelf"].ToString();
                                string ProTeamKnowledgeSelf = reader["ProTeamKnowledgeSelf"].ToString();
                                string ProCommunicationSelf = reader["ProCommunicationSelf"].ToString();

                                string PersPlanUnplanLeavesSelf = reader["PersPlanUnplanLeavesSelf"].ToString();
                                string PersExTimeConsciousnessSelf = reader["PersExTimeConsciousnessSelf"].ToString();
                                string PersExTeamColaborationSelf = reader["PersExTeamColaborationSelf"].ToString();
                                string PersExProfessionalismSelf = reader["PersExProfessionalismSelf"].ToString();
                                string PersExAdpoliciesSelf = reader["PersExAdpoliciesSelf"].ToString();
                                string PersExSpecialEffortsSelf = reader["PersExSpecialEffortsSelf"].ToString();
                                string PersExTrainingUtilizationSelf = reader["PersExTrainingUtilizationSelf"].ToString();

                                bool isProSelfComplete = !string.IsNullOrEmpty(ProQuantitySelf) && !string.IsNullOrEmpty(ProTATSelf) &&
                                                         !string.IsNullOrEmpty(ProPMSSelf) && !string.IsNullOrEmpty(ProTPDSelf) &&
                                                         !string.IsNullOrEmpty(ProTeamKnowledgeSelf) && !string.IsNullOrEmpty(ProCommunicationSelf);

                                bool isPersSelfComplete = !string.IsNullOrEmpty(PersPlanUnplanLeavesSelf) && !string.IsNullOrEmpty(PersExTimeConsciousnessSelf) &&
                                                          !string.IsNullOrEmpty(PersExTeamColaborationSelf) && !string.IsNullOrEmpty(PersExProfessionalismSelf) &&
                                                          !string.IsNullOrEmpty(PersExAdpoliciesSelf) && !string.IsNullOrEmpty(PersExSpecialEffortsSelf) &&
                                                          !string.IsNullOrEmpty(PersExTrainingUtilizationSelf);

                                updateList.Add((Id, isProSelfComplete, isPersSelfComplete));
                            }
                        }
                    }

                    // Perform updates after the reader is closed
                    foreach (var item in updateList)
                    {
                        if (item.IsProSelfComplete)
                        {
                            using (SqlCommand cmdup1 = new SqlCommand("UPDATE AppricialsSelfROS SET ProExellenceSelfStatus='1' WHERE EmpId=@EmpId AND Year=@Year", connection))
                            {
                                cmdup1.Parameters.AddWithValue("@EmpId", item.Id);
                                cmdup1.Parameters.AddWithValue("@Year", year);
                                cmdup1.ExecuteNonQuery();
                                BindAppraisals();
                            }
                        }

                        if (item.IsPersSelfComplete)
                        {
                            using (SqlCommand cmdup2 = new SqlCommand("UPDATE AppricialsSelfROS SET PersExcellenceSelfStatus='1' WHERE EmpId=@EmpId AND Year=@Year", connection))
                            {
                                cmdup2.Parameters.AddWithValue("@EmpId", item.Id);
                                cmdup2.Parameters.AddWithValue("@Year", year);
                                cmdup2.ExecuteNonQuery();
                                BindAppraisals();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the status.", ex);
            }
        }

        protected void btnpersonal_Click(object sender, EventArgs e)
        {
            int year = DateTime.Now.Year;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("update AppricialsSelfROS set ProQuantityROS=@ProQuantityROS,ProTATROS=@ProTATROS,ProPMSROS=@ProPMSROS,ProTPDROS=@ProTPDROS,ProTeamKnowledgeROS=@ProTeamKnowledgeROS,ProCommunicationROS=@ProCommunicationROS where EmpId=@EmpId and Year=@Year ", connection))
                    {
                        command.Parameters.AddWithValue("@ProQuantityROS", TextBox3.Text);
                        command.Parameters.AddWithValue("@ProTATROS", TextBox5.Text);
                        command.Parameters.AddWithValue("@ProPMSROS", TextBox7.Text);
                        command.Parameters.AddWithValue("@ProTPDROS", TextBox10.Text);
                        command.Parameters.AddWithValue("@ProTeamKnowledgeROS", TextBox12.Text);
                        command.Parameters.AddWithValue("@ProCommunicationROS", TextBox14.Text);
                        command.Parameters.AddWithValue("@EmpId", HiddenField1.Value);
                        command.Parameters.AddWithValue("@Year", year);
                        int i = command.ExecuteNonQuery();
                        BindAppraisals();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnprofesional_Click(object sender, EventArgs e)
        {
            int year = DateTime.Now.Year;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("update AppricialsSelfROS set PersPlanUnplanLeavesROS=@PersPlanUnplanLeavesROS,PersExTimeConsciousnessROS=@PersExTimeConsciousnessROS,PersExTeamColaborationROS=@PersExTeamColaborationROS,PersExProfessionalismROS=@PersExProfessionalismROS,PersExAdpoliciesROS=@PersExAdpoliciesROS,PersExSpecialEffortsROS=@PersExSpecialEffortsROS,PersExTrainingUtilizationROS=@PersExTrainingUtilizationROS where EmpId=@EmpId and Year=@Year ", connection))
                    {
                        command.Parameters.AddWithValue("@PersPlanUnplanLeavesROS", TextBox15.Text);
                        command.Parameters.AddWithValue("@PersExTimeConsciousnessROS", TextBox18.Text);
                        command.Parameters.AddWithValue("@PersExTeamColaborationROS", TextBox20.Text);
                        command.Parameters.AddWithValue("@PersExProfessionalismROS", TextBox22.Text);
                        command.Parameters.AddWithValue("@PersExAdpoliciesROS", TextBox24.Text);
                        command.Parameters.AddWithValue("@PersExSpecialEffortsROS", TextBox26.Text);
                        command.Parameters.AddWithValue("@PersExTrainingUtilizationROS", TextBox28.Text);
                        command.Parameters.AddWithValue("@EmpId", HiddenField1.Value);
                        command.Parameters.AddWithValue("@Year", year);
                        command.ExecuteNonQuery();
                        BindAppraisals();
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