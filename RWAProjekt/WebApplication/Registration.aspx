<%@ Page Title="Registration page" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="WebApplication.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container py-4">

        <asp:Panel ID="PanelIspis" CssClass="container mt-5" runat="server" Visible="False">
            <div class='alert alert-success' role='alert'>
                Registracija je uspješna.
            </div>
        </asp:Panel>
        <asp:Panel ID="PanelForma" runat="server" Visible="True">
            <asp:Label ID="lblResult" runat="server" CssClass="alert alert-danger d-block w-100" Visible="false"></asp:Label>
            <!-- PANEL PORUKA -->
            <!-- // -->
            <fieldset class="p-4">
                <legend>Registration</legend>
                <div class="mb-3">
                    <asp:Label ID="lblUsername" for="txtUsername" class="form-label" runat="server" Text="Username"></asp:Label>
                    <asp:TextBox ID="txtUsername" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                        ControlToValidate="txtUsername" Display="Dynamic" ForeColor="Red">Niste upisali korisničko ime</asp:RequiredFieldValidator>
                    <asp:CustomValidator
                        ID="CustomValidator1"
                        ClientValidationFunction="Username_Validation"
                        runat="server"
                        ControlToValidate="txtUsername"
                        Display="Dynamic"
                        ForeColor="Red"
                        OnServerValidate="Username_ServerValidate">* Zabranjeno korisničko ime</asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblPassword" for="txtPassword" class="form-label" runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="txtPassword" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red">Niste upisali lozinku</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblConfirmPassword" for="txtConfirmPassword" class="form-label" runat="server" Text="Confirm Password"></asp:Label>
                    <asp:TextBox ID="txtConfirmPassword" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtConfirmPassword" Display="Dynamic" ForeColor="Red">Niste upisali potvrdu lozinku</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator3" runat="server"
                        ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"
                        Display="Dynamic"
                        ForeColor="Red">Lozinke u oba polja moraju odgovarati</asp:CompareValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblEmail" for="txtEmail" class="form-label" runat="server" Text="Email"></asp:Label>
                    <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red">Niste upisali email</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblPhoneNumber" for="txtPhoneNumber" class="form-label" runat="server" Text="Phone Number"></asp:Label>
                    <asp:TextBox ID="txtPhoneNumber" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhoneNumber" Display="Dynamic" ForeColor="Red">Niste upisali broj mobitela</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblAddresa" for="txtAddresa" class="form-label" runat="server" Text="Adresa"></asp:Label>
                    <asp:TextBox ID="txtAddresa" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddresa" Display="Dynamic" ForeColor="Red">Niste upisali adresu stanovanja</asp:RequiredFieldValidator>
                </div>
                <asp:Button ID="btnPosalji" runat="server" class="btn btn-warning" Text="Submit" OnClick="btnPosalji_Click" />
            </fieldset>

        </asp:Panel>
        <!-- // -->

        <!-- JQUERY -->
        <script src="Scripts/jquery-3.6.0.min.js"></script>

        <!-- BOOTSTRAP -->
        <script src="Scripts/bootstrap.min.js"></script>

        <!-- USERNAME VALIDATION -->
        <script type="text/javascript">
            function Username_Validation(sender, args) {
                if (args.Value == "admin") {
                    args.IsValid = false;
                } else {
                    args.IsValid = true;
                }
            }
        </script>
    </div>
</asp:Content>
