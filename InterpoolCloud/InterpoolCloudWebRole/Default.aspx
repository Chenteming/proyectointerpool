<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="Default.aspx.cs" Inherits="InterpoolCloudWebRole._Default" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>



<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>



<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">


    
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeOut="600">


        </asp:ScriptManager>
   
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            
            <ProgressTemplate>

            <center>
				<img alt="" src="/ajax-loader.gif"  style="width: 150px; height: 150px" />


                </center>
          </ProgressTemplate>

        </asp:UpdateProgress>


        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>               
                <center>
        <div>

                <br />
                <asp:Label ID="LabelEmail" runat="server" Text="Email Facebook"></asp:Label>
                <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
              <p>
        
     <table>
     <tr>
        <td>   
        <asp:Button ID="StartGame" runat="server" onclick="StartGame_Click" 
        CssClass="boton"  Text="StartGame" Width="150px" Height="40px" />
        </td>      
       <td>    
        <asp:Button ID="Newsfamous" runat="server" onclick="NewsFamous_Click" 
            CssClass="boton" Text="News Famous" Width="150px" Height="40px" />
       </td>  
     </tr> 
     <tr>
        <td>      
            <asp:Button ID="Login" runat="server"  
              CssClass="boton" Text="Login" Width="150px" Height="40px" 
                onclick="Login_Click" />
        </td> 
        <td> 
        <asp:Button ID="DeleteGame" runat="server" Text="Delete Game" 
            CssClass="boton"  Width="150px" Height="40px" onclick="DeleteGame_Click" />

        </td> 
        </tr> 
        <tr>
        <td>
            <asp:Button ID="PruebaGetCities" runat="server" CssClass="boton" Text="Prueba GetCities"  Width="150px" Height="40px" onclick="PruebaGetCities_Click" />
        </td>
        <td>
            <asp:Button ID="PruebaTravel" runat="server" 
            CssClass="boton" Text="Prueba Travel" Width="150px" Height="40px" 

                onclick="PruebaTravel_Click" />
        </td>
        </tr>
        <tr>
        <td>
            <asp:Button ID="PruebaEOA" runat="server" 


            CssClass="boton" Text="Prueba EOA"  Width="150px" Height="40px" 
                onclick="PruebaEOA_Click" />
        </td>
        <td>
                <asp:Button ID="PruebaArrestar" runat="server" 
                 CssClass="boton"   Text="Prueba Arrestar" Width="150px" Height="40px" 




                    onclick="PruebaArrestar_Click" />
        </td>
        </tr>
        <tr><td>
            <asp:Label CssClass="labelInfo" ID="pruebaGetCitieslabel" runat="server" Text=""></asp:Label>
        </td>
        </tr>

      </table>   
    </p>
    <p>
    <br>
    <asp:Label ID="labelInfo" runat="server" Text=""></asp:Label>
    </p>
        </div>
        </center>
        
            </ContentTemplate>
         </asp:UpdatePanel>



        <asp:UpdatePanel runat="server" id="UpdatePanel2">
</asp:UpdatePanel>

    
</asp:Content>