<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="SuiteDetails.aspx.cs" Inherits="TestResultsDashboard.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">Test Suite Details Report</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageData" runat="server">
    <p>
    <asp:PlaceHolder ID="SuiteDetailsDescription" runat="server" />
    </p>

    <p>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
    </p>

 </asp:Content>
