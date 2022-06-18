using rwaLib.Dal;
using rwaLib.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class Users : System.Web.UI.Page
    {
        private IList<User> _listOfAllUsers;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            _listOfAllUsers = ((IRepo)Application["database"]).LoadUsers();
            lblResult.Visible = false;

            if (!IsPostBack)
            {
                ShowUsers();
                toggleForm(false);
            }
            else
            {
                toggleForm(true);
            }
        }

        private void toggleForm(bool status)
        {
            txtUsername.Enabled = status;
            txtUsername.CssClass = "form-control";

            txtEmail.Enabled = status;
            txtEmail.CssClass = "form-control";

            txtPhoneNumber.Enabled = status;
            txtPhoneNumber.CssClass = "form-control";

            txtAddress.Enabled = status;
            txtAddress.CssClass = "form-control";

            updateUser.Enabled = status;
            updateUser.CssClass = "btn btn-primary";
        }

        private void ShowUsers()
        {
            lbUsers.DataSource = _listOfAllUsers;
            lbUsers.DataValueField = "Id";
            lbUsers.DataTextField = "Username";
            lbUsers.DataBind();
        }

        protected void lbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var userId = int.Parse(lbUsers.SelectedValue);
            var selectedUser = _listOfAllUsers.SingleOrDefault(u => u.Id == userId);

            if (selectedUser != null)
            {
                txtUsername.Text = selectedUser.Username;
                txtEmail.Text = selectedUser.Email;
                txtPhoneNumber.Text = selectedUser.PhoneNumber;
                txtAddress.Text = selectedUser.Address;
            }
        }

        protected void updateUser_Click(object sender, EventArgs e)
        {
            var userId = int.Parse(lbUsers.SelectedValue);
            var selectedUser = _listOfAllUsers.SingleOrDefault(u => u.Id == userId);

            selectedUser.Username = txtUsername.Text;
            selectedUser.Email = txtEmail.Text;
            selectedUser.PhoneNumber = txtPhoneNumber.Text;
            selectedUser.Address = txtAddress.Text;

            try
            {
                // Update DB
                ((IRepo)Application["database"]).SaveUser(selectedUser);

                // Update SESSION
                if (((User)Session["user"]).Id == userId)
                {
                    Session["user"] = selectedUser;
                }

                Response.Redirect(Request.Url.LocalPath);
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 0)
                {
                    switch (ex.Errors[0].Number)
                    {
                        case 2627: // Constraint violation
                            lblResult.Text = "Pogreška: Korisnik s tim korisničkim imenom postoji!";
                            lblResult.Visible = true;
                            txtUsername.Text = "";
                            break;
                    }
                }
            }
        }
    }
}