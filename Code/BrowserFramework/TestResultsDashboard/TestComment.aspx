<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="TestComment.aspx.cs" Inherits="TestResultsDashboard.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">Test Comment</asp:Content>
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
                <asp:TableCell runat="server"><asp:Button ID="UpdateComment" Text="Update Comment" runat="server" />   </asp:TableCell>
            </asp:TableRow>
        </asp:Table>                     
    </p>
 </asp:Content>

