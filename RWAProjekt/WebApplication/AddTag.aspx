<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="AddTag.aspx.cs" Inherits="WebApplication.AddTag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container py-4">
        <asp:Panel ID="PanelForma" runat="server" Visible="True">
            <asp:Label ID="lblResults" runat="server" CssClass="alert alert-danger d-block w-100" Visible="false"></asp:Label>
            <fieldset class="p-4">
                <legend>Add Tag</legend>
                <div class="mb-3">
                    <asp:Label ID="lblName" class="form-label" runat="server" Text="Name"></asp:Label>
                    <asp:TextBox ID="txtName" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtName" Display="Dynamic" ForeColor="Red">Niste upisali ime</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblNameEng" class="form-label" runat="server" Text="NameEng"></asp:Label>
                    <asp:TextBox ID="txtNameEng" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNameEng" Display="Dynamic" ForeColor="Red">Niste upisali ime na engleskom</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                        <asp:Label ID="lblTagType" for="ddlTagType" class="form-label" runat="server" Text="Tag Type"></asp:Label>
                        <asp:DropDownList ID="ddlTagType" class="form-select" runat="server"></asp:DropDownList>
                    </div>
                <asp:Button ID="addTag" class="btn btn-primary" runat="server" Text="Add" OnClick="addTag_Click" />
            </fieldset>
        </asp:Panel>
    </div>
    <!-- PANEL PORUKA -->
    <asp:Panel ID="PanelIspis" CssClass="container mt-5" runat="server" Visible="False">
        <div class='alert alert-success' role='alert'>
            Uspješno dodan tag.
        </div>
    </asp:Panel>
</asp:Content>
