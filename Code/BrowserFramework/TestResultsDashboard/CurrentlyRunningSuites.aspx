<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="CurrentlyRunningSuites.aspx.cs" Inherits="TestResultsDashboard.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">Currently Running Suites View</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageData" runat="server">
    <p>
    The data on this page reflects information gathered on the currently running suites. For example; 
    if test suite "FOO" is currently running on a VM, it will be displayed below.
    </p>
    <p>
        <b>Summary totals for Current Suite Executions:</b>
     <asp:Table ID="tblCurrentSummary" runat="server" CssClass="TblStyle2"/>
    </p>
    <p>

    <b>Current Suite Executions:</b>
     <asp:Table ID="tblCurrent" runat="server" CssClass="TblStyle2"/>
    </p>
</asp:Content>

