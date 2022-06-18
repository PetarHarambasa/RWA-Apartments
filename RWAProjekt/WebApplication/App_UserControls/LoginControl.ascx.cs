using rwaLib.Dal;
using rwaLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.App_UserControls
{
    public partial class LoginControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PanelForma.Visible = true;
                PanelIspis.Visible = false;
            }
            if (Session["user"] != null)
            {
                Response.Redirect("Dashboard.aspx");
            }
        }
         
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var username = txtUsername.Text;
                var password = Cryptography.HashPassword(txtPassword.Text);

                
                User user = ((DBRepo)Application["database"]).AuthUser(username, password);


                if (user == null)
                {
                    PanelIspis.Visible = true;
                    PanelForma.Visible = true;
                }
                else
                {
                    Session["user"] = user;
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }
    }
}