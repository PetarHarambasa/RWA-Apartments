<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="WebApplication.Tags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container p-4">
        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <fieldset class="p-4">
                        <legend>List of Tags</legend>
                        <asp:ListBox runat="server"
                            ID="lbTags"
                            CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="lbTags_SelectedIndexChanged" />
                    </fieldset>
                </div>
            </div>
            <div class="col-sm-6">
                <fieldset class="py-4">
                    <legend>Edit Tags</legend>
                    <asp:Label ID="lblResult" runat="server" CssClass="alert alert-danger d-block w-100"></asp:Label>
                    <div class="mb-3">
                        <asp:Label ID="lblName" class="form-label" runat="server" Text="Name"></asp:Label>
                        <asp:TextBox ID="txtName" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtName" Display="Dynamic" ForeColor="Red">Niste upisali ime</asp:RequiredFieldValidator>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblNameEng" class="form-label" runat="server" Text="Name Eng"></asp:Label>
                        <asp:TextBox ID="txtNameEng" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNameEng" Display="Dynamic" ForeColor="Red">Niste upisali ime</asp:RequiredFieldValidator>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblTagType" for="ddlTagType" class="form-label" runat="server" Text="Tag Type"></asp:Label>
                        <asp:DropDownList ID="ddlTagType" class="form-select" runat="server"></asp:DropDownList>
                    </div>
                    <asp:Button ID="updateTag" class="btn btn-primary" runat="server" Text="Update" OnClick="updateTag_Click" />
                    <asp:Button ID="deleteTag" class="btn btn-danger" runat="server" Text="Delete" OnClick="deleteTag_Click" />
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>
