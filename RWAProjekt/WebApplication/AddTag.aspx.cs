using rwaLib.Dal;
using rwaLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class AddTag : System.Web.UI.Page
    {
        private IList<TagType> _listOfAllTagTypes;
        protected void Page_Load(object sender, EventArgs e)
        {
            _listOfAllTagTypes = ((IRepo)Application["database"]).LoadTagTypes();
            if (Session["user"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            AppendTagTypes();
        }

        private void AppendTagTypes()
        {
            ddlTagType.DataSource = _listOfAllTagTypes;
            ddlTagType.DataValueField = "Id";
            ddlTagType.DataTextField = "Name";
            ddlTagType.DataBind();
        }

        protected void addTag_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Tag t = new Tag
                {
                    TypeId = ddlTagType.SelectedIndex + 1,
                    Name = txtName.Text,
                    NameEng = txtNameEng.Text,
                };

                ((IRepo)Application["database"]).AddTag(t);
                PanelIspis.Visible = true;

                txtName.Text = "";
                txtNameEng.Text = "";
            }
        }
    }
}