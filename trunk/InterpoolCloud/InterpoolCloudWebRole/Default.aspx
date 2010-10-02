<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="InterpoolCloudWebRole._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to ASP.NET!
    </h2>
    <p>
        To learn more about ASP.NET visit <a href="http://www.asp.net" title="ASP.NET Website">www.asp.net</a>.</p>
    <p>
        &nbsp;<asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="StartGame" />
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="Newsfamois" />
        <asp:Button ID="ButtonLogin" runat="server" OnClick="ButtonLogin_Click" Text="Login" />

        <asp:Button ID="Button3" runat="server" Text="DeleteGame" 
            onclick="Button3_Click" />

        <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
            Text="Prueba GetCities" />

        <asp:Button ID="Button5" runat="server" onclick="Button5_Click" 
            Text="Prueba Travel" />

        <asp:Button ID="Button6" runat="server" onclick="Button6_Click" 
            Text="Prueba EOA" />

        <asp:Button ID="Button7" runat="server" onclick="Button6_Click" 
            Text="Prueba Arrestar" />

    </p>
    <p>
        <asp:Label ID="pruebaGetCities" runat="server"></asp:Label>
    </p>
    <p>
        You can also find <a href="http://go.microsoft.com/fwlink/?LinkID=152368&amp;clcid=0x409"
            title="MSDN ASP.NET Docs">documentation on ASP.NET at MSDN</a>.
    </p>
</asp:Content>
