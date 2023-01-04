<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="TestResultsDashboard.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:PlaceHolder ID="plcTitle" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageData" runat="server">
    <p>
        The data on this page reflects information gathered on the latest completed suites. For example; 
    if test suite "FOO" has been executed 85 times, we are only seeing the data here of the 85th suite execution.
    </p>
    <p>
        <b>Summary totals for Latest Completed Suite Executions:</b>
        <asp:Table ID="tblSummary" runat="server" CssClass="TblStyle2" />
        <br/>
        <%--<asp:HyperLink ID="lnkIncludeAdhoc" runat="server" Text="Include Adhoc Results" NavigateUrl="~/default.aspx?IncludeAdhoc=true"></asp:HyperLink>--%>
    </p>
    <p>
        <b>Latest Completed Suite Executions:</b>
        <asp:Table ID="tblLatest" runat="server" CssClass="TblStyle2" />
    </p>
</asp:Content>
