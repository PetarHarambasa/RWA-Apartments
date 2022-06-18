using rwaLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            User u = (User)Session["user"];
            pUsername.InnerText = u.Username;
            pEmail.InnerText = u.Email;
            pPhoneNumber.InnerText = u.PhoneNumber;
            pAddress.InnerText = u.Address;
            base.OnPreRender(e);
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
}