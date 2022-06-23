﻿using rwaLib.Dal;
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
    public partial class AddApartment : System.Web.UI.Page
    {
        private const string PATH_IMAGE = "./assets/apartments/";
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
            if (Session["user"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            AppendOwner();
            AppendStatus();
            AppendCity();
            AppendTags();
        }
        private void AppendCity()
        {
            ddlCity.DataSource = _listOfAllCity;
            ddlCity.DataValueField = "Id";
            ddlCity.DataTextField = "Name";
            ddlCity.DataBind();
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

        private void AppendTags()
        {
            _listOfAllTags.ToList().ForEach(tag =>
            {
                ListItem tagItem = new ListItem(tag.Name, tag.Id.ToString());
                cblTag.Items.Add(tagItem);
            });
        }

        protected void addApartment_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Apartment a = new Apartment
                {
                    Name = txtName.Text,
                    NameEng = txtNameEng.Text,
                    OwnerId = ddlOwner.SelectedIndex + 1,
                    TypeId = 999,
                    StatusId = ddlStatus.SelectedIndex + 1,
                    CityId = ddlCity.SelectedIndex + 1,
                    Address = txtAddress.Text,
                    Price = Decimal.Parse(txtPrice.Text),
                    MaxAdults = Int32.Parse(txtMaxAdults.Text),
                    MaxChildren = Int32.Parse(txtMaxChildren.Text),
                    TotalRooms = Int32.Parse(txtTotalRooms.Text),
                    BeachDistance = Int32.Parse(txtBeachDistance.Text),
                };

                var apartmentId = ((IRepo)Application["database"]).AddApartment(a);

                if (apartmentId != 0)
                {
                    foreach (ListItem tagItem in cblTag.Items)
                    {
                        if (tagItem.Selected)
                        {
                            ((IRepo)Application["database"]).AddApartmentTag(new TaggedApartment(apartmentId, Int32.Parse(tagItem.Value)));
                        }

                        tagItem.Selected = false;
                    }

                    string mainPicture = Path.GetFileName(fuUploadMain.PostedFile.FileName);
                    string mainPictureNameOnly = Path.GetFileNameWithoutExtension(fuUploadMain.PostedFile.FileName);
                    string mainFullPath = Server.MapPath(PATH_IMAGE + mainPicture);
                    string mainPictureBase64 = streamToBase64(fuUploadMain.PostedFile.InputStream);

                    fuUploadMain.SaveAs(mainFullPath);
                    ((IRepo)Application["database"]).AddApartmentPicture(new ApartmentPicture(apartmentId, mainFullPath, mainPictureBase64, mainPictureNameOnly, true));

                    foreach (var file in fuUploadOther.PostedFiles)
                    {
                        string picture = Path.GetFileName(file.FileName);
                        string nameOnly = Path.GetFileNameWithoutExtension(file.FileName);
                        string fullPath = Server.MapPath(PATH_IMAGE + picture);
                        string pictureBase64 = streamToBase64(file.InputStream);

                        fuUploadOther.SaveAs(fullPath);

                        ((IRepo)Application["database"]).AddApartmentPicture(new ApartmentPicture(apartmentId, fullPath, pictureBase64, nameOnly, false));
                    }

                    fuUploadMain.Attributes.Clear();
                    fuUploadOther.Attributes.Clear();

                    PanelIspis.Visible = true;

                    txtName.Text = "";
                    txtNameEng.Text = "";
                    txtAddress.Text = "";
                    txtPrice.Text = "";
                    txtMaxAdults.Text = "";
                    txtMaxChildren.Text = "";
                    txtTotalRooms.Text = "";
                    txtBeachDistance.Text = "";
                }

            }
        }
        private string streamToBase64(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);
            Byte[] bytes = br.ReadBytes((Int32)stream.Length);
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }
    }
}