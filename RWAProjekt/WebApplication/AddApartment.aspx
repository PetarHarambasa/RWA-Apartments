<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="AddApartment.aspx.cs" Inherits="WebApplication.AddApartment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container py-4">
        <asp:Panel ID="PanelForma" runat="server" Visible="True">
            <asp:Label ID="lblResults" runat="server" CssClass="alert alert-danger d-block w-100" Visible="false"></asp:Label>
            <fieldset class="p-4">
                <legend>Add Apartment</legend>
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
                    <asp:Label ID="lblOwner" for="ddlOwner" class="form-label" runat="server" Text="Owner"></asp:Label>
                    <asp:DropDownList ID="ddlOwner" class="form-select" runat="server"></asp:DropDownList>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblStatus" for="ddlStatus" class="form-label" runat="server" Text="Status"></asp:Label>
                    <asp:DropDownList ID="ddlStatus" class="form-select" runat="server"></asp:DropDownList>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblCity" for="ddlCity" class="form-label" runat="server" Text="City"></asp:Label>
                    <asp:DropDownList ID="ddlCity" class="form-select" runat="server"></asp:DropDownList>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblTag" for="cblTag" class="form-label" runat="server" Text="Tag"></asp:Label>
                    <asp:CheckBoxList ID="cblTag" class="form-select table_tag" runat="server"></asp:CheckBoxList>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblAddress" for="txtAddress" class="form-label" runat="server" Text="Address"></asp:Label>
                    <asp:TextBox ID="txtAddress" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddress" Display="Dynamic" ForeColor="Red">Niste upisali adresu</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblPrice" for="txtPrice" class="form-label" runat="server" Text="Price"></asp:Label>
                    <asp:TextBox ID="txtPrice" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPrice" Display="Dynamic" ForeColor="Red">Niste upisali cijenu</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblMaxAdults" for="txtMaxAdults" class="form-label" runat="server" Text="Max Adults"></asp:Label>
                    <asp:TextBox ID="txtMaxAdults" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMaxAdults" Display="Dynamic" ForeColor="Red">Niste upisali broj odraslih osoba</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblMaxChildren" for="txtMaxChildren" class="form-label" runat="server" Text="Max Children"></asp:Label>
                    <asp:TextBox ID="txtMaxChildren" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMaxChildren" Display="Dynamic" ForeColor="Red">Niste upisali broj djece</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblTotalRooms" for="txtTotalRooms" class="form-label" runat="server" Text="Total Rooms"></asp:Label>
                    <asp:TextBox ID="txtTotalRooms" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtTotalRooms" Display="Dynamic" ForeColor="Red">Niste upisali broj soba</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblBeachDistance" for="txtBeachDistance" class="form-label" runat="server" Text="Beach Distance"></asp:Label>
                    <asp:TextBox ID="txtBeachDistance" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtBeachDistance" Display="Dynamic" ForeColor="Red">Niste upisali udaljenost od plaže</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label for="fuUploadMain" class="form-label" runat="server">Representative image</asp:Label>
                    <asp:FileUpload ID="fuUploadMain" runat="server" class="form-control" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fuUploadMain" Display="Dynamic" ForeColor="Red">Niste uploadli sliku</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label for="fuUploadOther" class="form-label" runat="server">Other images</asp:Label>
                    <asp:FileUpload ID="fuUploadOther" multiple="multiple" runat="server" class="form-control" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="fuUploadOther" Display="Dynamic" ForeColor="Red">Niste uplodali jos neku sliku</asp:RequiredFieldValidator>
                </div>
                <asp:Button ID="addApartment" class="btn btn-primary" runat="server" Text="Add" OnClick="addApartment_Click" />
            </fieldset>
        </asp:Panel>
    </div>
    <!-- // -->

    <!-- PANEL PORUKA -->
    <asp:Panel ID="PanelIspis" CssClass="container mt-5" runat="server" Visible="False">
        <div class='alert alert-success' role='alert'>
            Uspješno dodan apartman.
        </div>
    </asp:Panel>
    <!-- // -->
</asp:Content>
