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
    public partial class Tags : System.Web.UI.Page
    {

        private IList<Tag> _listOfAllTags;
        private IList<TagType> _listOfAllTagTypes;
        protected void Page_Load(object sender, EventArgs e)
        {
            _listOfAllTagTypes = ((IRepo)Application["database"]).LoadTagTypes();
            _listOfAllTags = ((IRepo)Application["database"]).LoadTags();
            lblResult.Visible = false;

            if (!IsPostBack)
            {
                if (Session["user"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                ShowTags();
                toggleForm(false);
                AppendTagTypes();
            }
            else
            {
                toggleForm(true);
            }
        }

        private void AppendTagTypes()
        {
            ddlTagType.DataSource = _listOfAllTagTypes;
            ddlTagType.DataValueField = "Id";
            ddlTagType.DataTextField = "Name";
            ddlTagType.DataBind();
        }

        private void toggleForm(bool status)
        {
            txtName.Enabled = status;
            txtName.CssClass = "form-control";

            txtNameEng.Enabled = status;
            txtNameEng.CssClass = "form-control";

            updateTag.Enabled = status;
            updateTag.CssClass = "btn btn-primary";
        }

        private void ShowTags()
        {
            lbTags.DataSource = _listOfAllTags;
            lbTags.DataValueField = "Id";
            lbTags.DataTextField = "Name";
            lbTags.DataBind();
        }

        protected void lbTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tagId = int.Parse(lbTags.SelectedValue);
            var selectedTag = _listOfAllTags.SingleOrDefault(u => u.Id == tagId);

            if (selectedTag != null)
            {
                txtName.Text = selectedTag.Name;
                txtNameEng.Text = selectedTag.NameEng;
                ddlTagType.SelectedValue = selectedTag.TypeId.ToString();
            }
        }

        protected void updateTag_Click(object sender, EventArgs e)
        {
            var tagId = int.Parse(lbTags.SelectedValue);
            var selectedTag = _listOfAllTags.SingleOrDefault(u => u.Id == tagId);

            selectedTag.Name = txtName.Text;
            selectedTag.NameEng = txtNameEng.Text;
            selectedTag.TypeId = ddlTagType.SelectedIndex + 1;

            ((IRepo)Application["database"]).SaveTag(selectedTag);
            Refresh();
        }

        private void Refresh()
        {
            _listOfAllTags = ((IRepo)Application["database"]).LoadTags();
            ShowTags();
            ClearTxtFields();
        }

        protected void deleteTag_Click(object sender, EventArgs e)
        {
            var tagId = int.Parse(lbTags.SelectedValue);
            var selectedTag = _listOfAllTags.SingleOrDefault(u => u.Id == tagId);

            ((IRepo)Application["database"]).DeleteTag(selectedTag);
            Refresh();
        }

        private void ClearTxtFields()
        {
            txtName.Text = "";
            txtNameEng.Text = "";
        }
    }
}