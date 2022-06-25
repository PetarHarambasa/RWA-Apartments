using rwaLib.Dal;
using rwaLib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class Apartments : System.Web.UI.Page
    {
        private const string PATH_IMAGE = "./assets/apartments/";
        private IList<Apartment> _listOfAllApartments;
        private IList<ApartmentOwner> _listOfAllApartmentOwners;
        private IList<ApartmentStatus> _listOfAllApartmentStatus;
        private IList<City> _listOfAllCity;
        private IList<Tag> _listOfAllTags;

        protected void Page_Load(object sender, EventArgs e)
        {
            _listOfAllApartmentOwners = ((IRepo)Application["database"]).LoadApartmentOwner();
            _listOfAllApartmentStatus = ((IRepo)Application["database"]).LoadApartmentStatus();
            _listOfAllCity = ((IRepo)Application["database"]).LoadCity();
            _listOfAllTags = ((IRepo)Application["database"]).LoadTags();
            _listOfAllApartments = ((IRepo)Application["database"]).LoadApartments();
            lblResult.Visible = false;

            if (!IsPostBack)
            {
                if (Session["user"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                AppendOwner();
                AppendStatus();
                AppendCity();
                AppendTags();
                LoadData();
                ShowApartments();
            }
        }

        private void LoadData()
        {
            rptApartments.DataSource = _listOfAllApartments;
            rptApartments.DataBind();
        }

        private void AppendCity()
        {
            ddlCity.DataSource = _listOfAllCity;
            ddlCity.DataValueField = "Id";
            ddlCity.DataTextField = "Name";
            ddlCity.DataBind();
        }

        private void AppendTags()
        {
            _listOfAllTags.ToList().ForEach(tag =>
            {
                ListItem tagItem = new ListItem(tag.Name, tag.Id.ToString());
                cblTag.Items.Add(tagItem);
            });
        }

        private void AppendStatus()
        {
            ddlStatus.DataSource = _listOfAllApartmentStatus;
            ddlStatus.DataValueField = "Id";
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataBind();
        }

        private void AppendOwner()
        {
            ddlOwner.DataSource = _listOfAllApartmentOwners;
            ddlOwner.DataValueField = "Id";
            ddlOwner.DataTextField = "Name";
            ddlOwner.DataBind();
        }

        private void ShowApartments()
        {
            lbApartments.DataSource = _listOfAllApartments;
            lbApartments.DataValueField = "Id";
            lbApartments.DataTextField = "Name";
            lbApartments.DataBind();
        }

        private void ShowPictures(IList<ApartmentPicture> pictures)
        {
            repApartmentPictures.DataSource = pictures;
            repApartmentPictures.DataBind();
        }

        protected void updateApartment_Click(object sender, EventArgs e)
        {
            var apartmentId = int.Parse(lbApartments.SelectedValue);
            var selectedApartment = _listOfAllApartments.SingleOrDefault(u => u.Id == apartmentId);

            selectedApartment.Name = txtName.Text;
            selectedApartment.NameEng = txtNameEng.Text;
            selectedApartment.Address = txtAddress.Text;
            selectedApartment.OwnerId = ddlOwner.SelectedIndex + 1;
            selectedApartment.StatusId = ddlStatus.SelectedIndex + 1;
            selectedApartment.CityId = ddlCity.SelectedIndex + 1;
            selectedApartment.Address = txtAddress.Text;
            selectedApartment.Price = Decimal.Parse(txtPrice.Text);
            selectedApartment.MaxAdults = Int32.Parse(txtMaxAdults.Text);
            selectedApartment.MaxChildren = Int32.Parse(txtMaxChildren.Text);
            selectedApartment.TotalRooms = Int32.Parse(txtTotalRooms.Text);
            selectedApartment.BeachDistance = Int32.Parse(txtBeachDistance.Text);

            ((IRepo)Application["database"]).SaveApartment(selectedApartment);
            ((IRepo)Application["database"]).DeleteApartmentTagByApartmentId(selectedApartment.Id);

            foreach (ListItem tagItem in cblTag.Items)
            {
                if (tagItem.Selected)
                {
                    ((IRepo)Application["database"]).AddApartmentTag(new TaggedApartment(apartmentId, Int32.Parse(tagItem.Value)));
                }

                tagItem.Selected = false;
            }

            if (fuUploadMain.HasFile)
            {
                string mainPicture = Path.GetFileName(fuUploadMain.PostedFile.FileName);
                string mainPictureNameOnly = Path.GetFileNameWithoutExtension(fuUploadMain.PostedFile.FileName);
                string mainDirtPath = PATH_IMAGE + mainPicture;
                string mainFullPath = Server.MapPath(mainDirtPath);
                string mainPictureBase64 = streamToBase64(fuUploadMain.PostedFile.InputStream);

                fuUploadMain.SaveAs(mainFullPath);
                ((IRepo)Application["database"]).AddApartmentPicture(new ApartmentPicture(apartmentId, mainDirtPath, mainPictureBase64, mainPictureNameOnly, true));
            }

            if (fuUploadOther.HasFiles)
            {
                foreach (var file in fuUploadOther.PostedFiles)
                {
                    string picture = Path.GetFileName(file.FileName);
                    string nameOnly = Path.GetFileNameWithoutExtension(file.FileName);
                    string dirPath = PATH_IMAGE + picture;
                    string fullPath = Server.MapPath(dirPath);
                    string pictureBase64 = streamToBase64(file.InputStream);

                    fuUploadOther.SaveAs(fullPath);

                    ((IRepo)Application["database"]).AddApartmentPicture(new ApartmentPicture(apartmentId, dirPath, pictureBase64, nameOnly, false));
                }
            }

            Refresh();
        }

        private void Refresh()
        {
            _listOfAllApartments = ((IRepo)Application["database"]).LoadApartments();
            ShowApartments();
            LoadData();
            ClearForm();
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtNameEng.Text = "";
            txtAddress.Text = "";
            txtPrice.Text = "";
            txtMaxAdults.Text = "";
            txtMaxChildren.Text = "";
            txtTotalRooms.Text = "";
            txtBeachDistance.Text = "";
            repApartmentPictures.Dispose();
            repApartmentPictures.DataBind();
            fuUploadMain.Attributes.Clear();
            fuUploadOther.Attributes.Clear();

            foreach (ListItem tagItem in cblTag.Items)
            {
                tagItem.Selected = false;
            }
        }

        private void SetImageValidators(IList<ApartmentPicture> pictures)
        {
            var mainImageExists = pictures.FirstOrDefault(image => image.IsRepresentative);
            validatorUploadMain.Enabled = mainImageExists == null;
            validatorUploadOther.Enabled = pictures.Count == 0;
        }

        protected void lbApartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            var apartmentId = int.Parse(lbApartments.SelectedValue);
            var selectedApartment = _listOfAllApartments.SingleOrDefault(u => u.Id == apartmentId);

            if (selectedApartment != null)
            {
                foreach (ListItem tagItem in cblTag.Items)
                {
                    var tagExists = selectedApartment.Tags.FirstOrDefault(tag => tag.Tag.Id == Int32.Parse(tagItem.Value));
                    tagItem.Selected = tagExists != null;
                }

                ddlOwner.SelectedValue = selectedApartment.OwnerId.ToString();
                ddlStatus.SelectedValue = selectedApartment.StatusId.ToString();
                ddlCity.SelectedValue = selectedApartment.CityId.ToString();

                txtName.Text = selectedApartment.Name;
                txtNameEng.Text = selectedApartment.NameEng;
                txtAddress.Text = selectedApartment.Address;
                txtPrice.Text = selectedApartment.Price.ToString();
                txtMaxAdults.Text = selectedApartment.MaxAdults.ToString();
                txtMaxChildren.Text = selectedApartment.MaxChildren.ToString();
                txtTotalRooms.Text = selectedApartment.TotalRooms.ToString();
                txtBeachDistance.Text = selectedApartment.BeachDistance.ToString();

                ShowPictures(selectedApartment.ApartmentPicture);
                SetImageValidators(selectedApartment.ApartmentPicture);
            }
        }

        protected void deleteApartment_Click(object sender, EventArgs e)
        {
            var apartmentId = int.Parse(lbApartments.SelectedValue);
            var selectedApartment = _listOfAllApartments.SingleOrDefault(u => u.Id == apartmentId);

            ((IRepo)Application["database"]).DeleteApartment(selectedApartment);
            Refresh();
        }

        protected void deleteImage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var imageId = Int32.Parse(btn.CommandArgument);

            ((IRepo)Application["database"]).DeleteApartmentImageById(imageId);
            Refresh();
        }

        private string streamToBase64(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);
            Byte[] bytes = br.ReadBytes((Int32)stream.Length);
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }
    }
}