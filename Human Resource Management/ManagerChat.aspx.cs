using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class ManagerChat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayEmployeeList();
                string empId = Request.QueryString["empId"];
                string sessionEmpId = Request.QueryString["SId"];

                if (!string.IsNullOrEmpty(empId) && !string.IsNullOrEmpty(sessionEmpId))
                {
                    DisplayEmployeeDetails(empId, sessionEmpId);
                    UpdateChatStatus(empId, sessionEmpId);
                }
            }
        }
        protected void sendMessage_Click(object sender, EventArgs e)
        {
            string empId = Request.QueryString["empId"];
            string sessionEmpId = Request.QueryString["SId"];

            // Variable to hold the image data
            byte[] imageData = null;

            // Check if a file is selected
            if (FileUpload1.HasFile)
            {
                // Get the file data
                using (var binaryReader = new BinaryReader(FileUpload1.PostedFile.InputStream))
                {
                    imageData = binaryReader.ReadBytes(FileUpload1.PostedFile.ContentLength);
                }
            }

            try
            {
                string connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "INSERT INTO ChatMessages (FromNameId, FromName, ToNameId, ToName, Message, TimeSent, Status, Image) VALUES (@FromNameId, @FromName, @ToNameId, @ToName, @Message, @TimeSent, @Status, @Image)";

                        // Add parameters with explicit types
                        sqlCmd.Parameters.AddWithValue("@FromNameId", sessionEmpId);
                        sqlCmd.Parameters.AddWithValue("@FromName", Session["Name"].ToString());
                        sqlCmd.Parameters.AddWithValue("@ToNameId", empId);
                        sqlCmd.Parameters.AddWithValue("@ToName", DBNull.Value); // Adjust if necessary
                        sqlCmd.Parameters.AddWithValue("@Message", TextBox1.Text);
                        sqlCmd.Parameters.AddWithValue("@TimeSent", DateTime.Now);
                        sqlCmd.Parameters.AddWithValue("@Status", "UnSeen");

                        // Explicitly set the Image parameter with the correct type
                        var imageParam = new SqlParameter("@Image", System.Data.SqlDbType.VarBinary)
                        {
                            Value = (object)imageData ?? DBNull.Value
                        };
                        sqlCmd.Parameters.Add(imageParam);

                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        sqlCmd.ExecuteNonQuery();
                        TextBox1.Text = "";
                    }
                }
                DisplayEmployeeDetails(empId, sessionEmpId);
                DisplayEmployeeList(); // Refresh chat messages after sending a new one
            }
            catch (Exception ex)
            {
                // Handle exception
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            DisplayEmployeeList();
        }
        public void DisplayEmployeeList()
        {
            try
            {
                string loggedInUserId = Session["EmpId"].ToString();
                string searchTerm = TextBox2.Text.Trim();

                // Query to get EmpId, FirstName, and UnseenMessageCount
                string query = @"
        SELECT e.EmpId, e.FirstName, ISNULL(COUNT(cm.Status), 0) AS UnseenMessageCount
        FROM Employees e 
        LEFT JOIN ChatMessages cm 
            ON e.EmpId = cm.FromNameId
            AND cm.ToNameId = @LoggedInUserId 
            AND cm.Status = 'UnSeen' 
        WHERE e.EmpId != @LoggedInUserId";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query += " AND e.FirstName LIKE @SearchTerm";
                }

                query += " GROUP BY e.EmpId, e.FirstName ORDER BY UnseenMessageCount DESC";

                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand sqlCmd = new SqlCommand(query, connection))
                    {
                        sqlCmd.Parameters.AddWithValue("@LoggedInUserId", loggedInUserId);

                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                            sqlCmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                        }

                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                PlaceHolder1.Controls.Add(new LiteralControl("<div>No employees found.</div>"));
                            }
                            else
                            {
                                StringBuilder employeeHtml = new StringBuilder();

                                while (reader.Read())
                                {
                                    string empId = reader["EmpId"].ToString();
                                    string fullName = reader["FirstName"].ToString();
                                    string shortName = fullName.Length > 6 ? fullName.Substring(0, 6) : fullName;
                                    int unseenMessageCount = Convert.ToInt32(reader["UnseenMessageCount"]);

                                    string displayText = unseenMessageCount > 0 ? $"<img src='Images/download1.png' class='msg-image' /> {unseenMessageCount}" : empId;

                                    // Fetch the image separately
                                    byte[] empImage = GetEmployeeImage(empId);

                                    string base64Image = empImage != null ? Convert.ToBase64String(empImage) : "path/to/default/image.jpg";

                                    // Create HTML for each employee, including a button to select
                                    employeeHtml.Append("<div class='userlist' style='box-shadow: rgba(149, 157, 165, 0.2) 0px 8px 24px;background-color:antiquewhite;' ");
                                    employeeHtml.Append($"onclick=\"selectEmployee('{HttpUtility.JavaScriptStringEncode(empId)}')\">");
                                    employeeHtml.Append("<div style='display:flex'>");
                                    employeeHtml.Append("<div class='img-div'>");
                                    employeeHtml.Append($"<img src='data:image/jpeg;base64,{base64Image}' alt='img' class='img-img' />");
                                    employeeHtml.Append("</div>");
                                    employeeHtml.Append("<div class='name-main-div'>");
                                    employeeHtml.Append("<div>" + shortName + "</div>");
                                    employeeHtml.Append("<div>" + displayText + "</div>");
                                    employeeHtml.Append("</div>");
                                    employeeHtml.Append("</div>");
                                    employeeHtml.Append("</div>");
                                }

                                PlaceHolder1.Controls.Add(new LiteralControl(employeeHtml.ToString()));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PlaceHolder1.Controls.Add(new LiteralControl("<div>Error occurred: " + ex.Message + "</div>"));
            }
        }


        private void UpdateChatStatus(string empId, string sessionId)
        {
            try
            {
                int loggedInUserId = Convert.ToInt32(Session["EmpId"]);
                int chatPartnerId = Convert.ToInt32(empId);

                string query = @"UPDATE ChatMessages SET Status = 'Seen' 
        WHERE FromNameId = @ChatPartnerId AND ToNameId = @LoggedInUserId AND Status = 'UnSeen'";

                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(query, connection))
                    {
                        sqlCmd.Parameters.AddWithValue("@ChatPartnerId", chatPartnerId);
                        sqlCmd.Parameters.AddWithValue("@LoggedInUserId", loggedInUserId);
                        connection.Open();
                        sqlCmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                PlaceHolder1.Controls.Add(new LiteralControl("<div>Error occurred: " + ex.Message + "</div>"));
            }
        }

        private byte[] GetEmployeeImage(string empId)
        {
            byte[] imageData = null;

            string imageQuery = "SELECT Image FROM Employees WHERE EmpId = @EmpId";

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                connection.Open();

                using (SqlCommand sqlCmd = new SqlCommand(imageQuery, connection))
                {
                    sqlCmd.Parameters.AddWithValue("@EmpId", empId);

                    var result = sqlCmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        imageData = (byte[])result;
                    }
                }
            }

            return imageData;
        }

        private void DisplayEmployeeDetails(string empId, string sessionEmpId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

            string userQuery = @"SELECT FirstName, Image FROM Employees WHERE EmpId = @empId";
            string messageQuery = @"SELECT * FROM ChatMessages WHERE (FromNameId = @loggedInUserId AND ToNameId = @selectedUserId)
             OR (FromNameId = @selectedUserId AND ToNameId = @loggedInUserId) ORDER BY TimeSent ASC";

            string userName = string.Empty;
            string profileImageBase64 = string.Empty;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Fetch user details
                using (SqlCommand cmd = new SqlCommand(userQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@empId", empId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userName = reader["FirstName"].ToString();

                            // Handle byte array for image
                            byte[] imageBytes = reader["Image"] as byte[];
                            if (imageBytes != null && imageBytes.Length > 0)
                            {
                                profileImageBase64 = Convert.ToBase64String(imageBytes);
                            }
                            else
                            {
                                profileImageBase64 = "default-placeholder-base64-string"; // Placeholder if no image
                            }

                            headerName.Text = userName;
                            headerImage.ImageUrl = $"data:image/jpeg;base64,{profileImageBase64}";
                        }
                    }
                }

                // Fetch chat messages
                using (SqlCommand cmd = new SqlCommand(messageQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@loggedInUserId", sessionEmpId);
                    cmd.Parameters.AddWithValue("@selectedUserId", empId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        LeftChatData.Controls.Clear();
                        RightChatData.Controls.Clear();

                        bool hasMessages = false;

                        while (reader.Read())
                        {
                            hasMessages = true;
                            string message = reader["Message"].ToString();
                            DateTime timeSent = Convert.ToDateTime(reader["TimeSent"]);
                            bool isLoggedInUser = Convert.ToInt32(reader["FromNameId"]) == Convert.ToInt32(sessionEmpId);
                            string status = reader["Status"].ToString();
                            string messageClass = isLoggedInUser ?
                                (status == "Seen" ? "seen-sender" : "unseen-sender") :
                                (status == "Seen" ? "seen-recipient" : "unseen-recipient");

                            // Handle image in message
                            string imageHtml = string.Empty;
                            byte[] messageImageBytes = reader["Image"] as byte[];
                            if (messageImageBytes != null && messageImageBytes.Length > 0)
                            {
                                string messageImageBase64 = Convert.ToBase64String(messageImageBytes);
                                imageHtml = $"<img src='data:image/jpeg;base64,{messageImageBase64}' class='message-image' />";
                            }

                            // Display message with optional image
                            string messageHtml = $@"
<div class='chat-message {messageClass} {(isLoggedInUser ? "right-message" : "left-message")}'>
    <div class='chat-message-text'>{message}</div>
    {imageHtml}
    <div class='chat-message-time'>{timeSent:HH:mm:ss}</div>
</div>";
                            RightChatData.Controls.Add(new Literal { Text = messageHtml });
                        }

                        // If no messages, show the GIF
                        if (!hasMessages)
                        {
                            string noMessagesHtml = @"<div class='no-messages'>
    <img src='Images/giphy.gif' alt='No Messages' style='width:100%;height:420px;opacity:0.7' />
</div>";
                            RightChatData.Controls.Add(new Literal { Text = noMessagesHtml });
                        }
                    }
                }
            }
        }
    }
}