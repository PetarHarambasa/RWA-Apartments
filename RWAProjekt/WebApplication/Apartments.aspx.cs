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
    public partial class Apartments : System.Web.UI.Page
    {
        private IList<Apartment> _listOfAllApartments;
        private IList<ApartmentOwner> _listOfAllApartmentOwners;
        private IList<ApartmentStatus> _listOfAllApartmentStatus;
        private IList<City> _listOfAllCity;
        protected void Page_Load(object sender, EventArgs e)
        {
            _listOfAllApartmentOwners = ((IRepo)Application["database"]).LoadApartmentOwner();
            _listOfAllApartmentStatus = ((IRepo)Application["database"]).LoadApartmentStatus();
            _listOfAllCity = ((IRepo)Application["database"]).LoadCity();
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
            Refresh();
        }

        private void Refresh()
        {
            _listOfAllApartments = ((IRepo)Application["database"]).LoadApartments();
            ShowApartments();
            LoadData();
            ClearTxtFields();
        }

        private void ClearTxtFields()
        {
            txtName.Text = "";
            txtNameEng.Text = "";
            txtAddress.Text = "";
            txtPrice.Text = "";
            txtMaxAdults.Text = "";
            txtMaxChildren.Text = "";
            txtTotalRooms.Text = "";
            txtBeachDistance.Text = "";
        }

        protected void lbApartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            var apartmentId = int.Parse(lbApartments.SelectedValue);
            var selectedApartment = _listOfAllApartments.SingleOrDefault(u => u.Id == apartmentId);

            if (selectedApartment != null)
            {

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
            }
        }

        protected void deleteApartment_Click(object sender, EventArgs e)
        {
            var apartmentId = int.Parse(lbApartments.SelectedValue);
            var selectedApartment = _listOfAllApartments.SingleOrDefault(u => u.Id == apartmentId);

            ((IRepo)Application["database"]).DeleteApartment(selectedApartment);
            Refresh();
        }
    }
}