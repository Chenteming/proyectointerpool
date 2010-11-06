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
              <p>
     <table>
     <tr>
        
       <td>    
        <asp:Button ID="Newsfamous" runat="server" onclick="NewsFamous_Click" 
            CssClass="boton" Text="News Famous" Width="150px" Height="40px" />
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