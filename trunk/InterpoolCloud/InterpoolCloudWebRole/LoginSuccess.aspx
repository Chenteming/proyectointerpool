<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginSuccess.aspx.cs" Inherits="InterpoolCloudWebRole.LoginSuccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type='text/javascript'>
        function init() {
            var ventana = window.self;
            ventana.opener = window.self;
            ventana.close();
        }
     </script>
</head>
<body onload="init()">
    <p>
        Login success. Press back button to return to the game.
    </p>
</body>
</html>
