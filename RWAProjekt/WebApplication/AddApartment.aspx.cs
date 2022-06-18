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
    public partial class AddApartment : System.Web.UI.Page
    {
        private IList<ApartmentOwner> _listOfAllApartmentOwners;
        private IList<ApartmentStatus> _listOfAllApartmentStatus;
        private IList<City> _listOfAllCity;
        protected void Page_Load(object sender, EventArgs e)
        {
            _listOfAllApartmentOwners = ((IRepo)Application["database"]).LoadApartmentOwner();
            _listOfAllApartmentStatus = ((IRepo)Application["database"]).LoadApartmentStatus();
            _listOfAllCity = ((IRepo)Application["database"]).LoadCity();
            if (Session["user"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            AppendOwner();
            AppendStatus();
            AppendCity();
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
                    Price = Int32.Parse(txtPrice.Text),
                    MaxAdults = Int32.Parse(txtMaxAdults.Text),
                    MaxChildren = Int32.Parse(txtMaxChildren.Text),
                    TotalRooms = Int32.Parse(txtTotalRooms.Text),
                    BeachDistance = Int32.Parse(txtBeachDistance.Text),
                };

                ((IRepo)Application["database"]).AddApartment(a);
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
}