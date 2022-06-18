using rwaLib.Dal;
using rwaLib.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["user"] != null)
                {
                    Response.Redirect("Dashboard.aspx");
                }

                PanelForma.Visible = true;
                PanelIspis.Visible = false;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {

            try
            {
                if (IsPostBack && ViewState["user"] != null)
                {
                    User u = (User)ViewState["user"];
                    ((IRepo)Application["database"]).AddUser(u);
                    Session["user"] = u;

                    var method = Request.HttpMethod.ToLower();
                    RenderData(u, method);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 0)
                {
                    switch (ex.Errors[0].Number)
                    {
                        case 2627:
                            lblResult.Text = "Pogreška: Korisnik s tim korisničkim imenom postoji!";
                            lblResult.Visible = true;
                            txtUsername.Text = "";
                            break;
                    }
                }
            }

            
            base.OnPreRender(e);
        }

        private void RenderData(User u, string method)
        {
            PanelIspis.Visible = true;
            PanelForma.Visible = false;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='results' class='container p-4'><fieldset>");
            sb.Append($"<legend>{method.ToUpper()} request</legend>");
            sb.Append("<div class='mb-3'><label class='form-label'>Username: </label>");
            sb.Append($"<label class='fw-bold'>{u.Username}</label>");
            sb.Append("</div>");
            sb.Append("<div class='mb-3'><label class='form-label'>Email: </label>");
            sb.Append($"<label class='fw-bold'>{u.Email}</label>");
            sb.Append("</div>");
            sb.Append("<div class='mb-3'><label class='form-label'>PhoneNumber: </label>");
            sb.Append($"<label class='fw-bold'>{u.PhoneNumber}</label>");
            sb.Append("</div>");
            sb.Append("<div class='mb-3'><label class='form-label'>Adress: </label>");
            sb.Append($"<label class='fw-bold'>{u.Address}</label>");
            sb.Append("</div>");
            sb.Append($"<a href='/Dashboard.aspx' class='btn btn-primary'>Continue</a>");
            sb.Append("</fieldset></div>");

            LiteralControl ispis = new LiteralControl(sb.ToString());
            PanelIspis.Controls.Add(ispis);
        }

        protected void Username_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value == "admin")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void btnPosalji_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                User u = new User
                {
                    Email = txtEmail.Text,
                    PasswordHash = Cryptography.HashPassword(txtPassword.Text),
                    PhoneNumber = txtPhoneNumber.Text,
                    Username = txtUsername.Text,
                    Address = txtAddresa.Text
                };
                ViewState["user"] = u;
            }
        }
    }
}