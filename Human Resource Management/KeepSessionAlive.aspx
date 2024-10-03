<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KeepSessionAlive.aspx.cs" Inherits="Human_Resource_Management.KeepSessionAlive" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
    <script type="text/javascript">
    // Interval in milliseconds (e.g., 5 minutes)
    var keepAliveInterval = 1 * 60 * 1000; // 5 minutes

    function keepSessionAlive() {
        var xhr = new XMLHttpRequest();
        xhr.open('GET', 'KeepSessionAlive.aspx', true);
        xhr.send();
    }

    // Set interval to call keepSessionAlive function periodically
    setInterval(keepSessionAlive, keepAliveInterval);
    </script>

</body>
</html>
