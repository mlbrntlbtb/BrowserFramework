<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="TestOverride.aspx.cs" Inherits="TestResultsDashboard.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">Test Status Override</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageData" runat="server">
    <asp:HiddenField ID="hTestSuiteID" runat="server" />
    <asp:HiddenField ID="hRunId" runat="server" />
    <asp:HiddenField ID="hResultId" runat="server" />
    <p>
        <asp:Table CssClass="TblStyle1" runat="server" >
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Initials:</asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="Initials" runat="server" MaxLength="3" CssClass="textbox1" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Comment:</asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="Comment" Width="500px" runat="server" Rows="5" MaxLength="175" CssClass="textbox2" TextMode="MultiLine"/></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server"><asp:Button ID="OverideStatus" Text="Override Status" runat="server" />   </asp:TableCell>
            </asp:TableRow>
        </asp:Table>                     
    </p>
 </asp:Content>
