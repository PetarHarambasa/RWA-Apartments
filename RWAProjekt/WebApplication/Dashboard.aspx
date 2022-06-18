<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WebApplication.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="container p-4">
        <fieldset class="container p-4">
            <legend runat="server" >User profile</legend>
            <label>Username:</label>
            <p id="pUsername" runat="server"></p>

            <label>Email:</label>
            <p id="pEmail" runat="server"></p>

            <label>Phone number:</label>
            <p id="pPhoneNumber" runat="server"></p>

            <label>Address:</label>
            <p id="pAddress" runat="server"></p>

        </fieldset>
    </div>

</asp:Content>
