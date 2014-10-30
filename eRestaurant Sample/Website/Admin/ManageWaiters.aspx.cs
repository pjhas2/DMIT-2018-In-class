using eRestaurant.BLL;
using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ManageWaiters : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            MessageUserControl.ShowInfo("This form allows you to edit information on new and existing waiters.");
            LoadWaiters();
        }
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(AddWaiter, "Added Waiter", "The new waiter was successfully added.");
    }

    public void AddWaiter()
    {
        Waiter person = new Waiter()
        {
            FirstName = FirstName.Text,
            LastName = LastName.Text,
            Address = Address.Text,
            Phone = Phone.Text,
            HireDate = DateTime.Parse(HireDate.Text)
        };
        DateTime fireDate;
        if (DateTime.TryParse(ReleaseDate.Text, out fireDate))
            person.ReleaseDate = fireDate;

        var controller = new RestaurantAdminController();
        person.WaiterID = controller.AddWaiter(person);
        WaiterID.Text = person.WaiterID.ToString();
        // TODO: Re-populate dropdownlist of waiters. And set the selectvalue as well

    }
    protected void Update_Click(object sender, EventArgs e)
    {
        int temp;
        if (int.TryParse(WaiterID.Text, out temp))
            MessageUserControl.TryRun(UpdateWaiter, "Update Succeeded", "The waiter information was updated.");
        else
            MessageUserControl.ShowInfo("Please lookup a waiter before clicking 'Update'.");
    }

    public void UpdateWaiter()
    {
        Waiter person = new Waiter()
        {
            WaiterID = int.Parse(WaiterID.Text),
            FirstName = FirstName.Text,
            LastName = LastName.Text,
            Address = Address.Text,
            Phone = Phone.Text,
            HireDate = DateTime.Parse(HireDate.Text)
        };
        DateTime firedOn;
        if (DateTime.TryParse(ReleaseDate.Text, out firedOn))
            person.ReleaseDate = firedOn;

        var controller = new RestaurantAdminController();
        controller.UpdateWaiter(person);
        //WaiterID.Text = person.WaiterID.ToString();
        // TODO: Re-populate dropdownlist of waiters. And set the selectvalue as well

    }

    protected void CleanFields()
    {
        WaiterID.Text = "";
        FirstName.Text = "";
        LastName.Text = "";
        Address.Text = "";
        Phone.Text = "";
        HireDate.Text = "";
        ReleaseDate.Text = "";
    }

    protected void Clear_Click(object sender, EventArgs e)
    {
        CleanFields();
        MessageLabel.Text = "Clean Completed";
    }

    
    protected void LoadWaiters()
    {
        try
        {
            List<Waiter> info;
            RestaurantAdminController systemmgr = new RestaurantAdminController();
            info = systemmgr.ListAllWaiters();
            //setup the DDL control
            WaitersDropDown.DataSource = info;
            WaitersDropDown.DataTextField = "FirstName";
            WaitersDropDown.DataValueField = "WaiterID";
            WaitersDropDown.DataBind();
            //Add a prompt line as the 1st line of the DDL
            WaitersDropDown.Items.Insert(0, "Select a Waiter");
        }
        catch(Exception ex)
        {
            MessageLabel.Text = ex.Message;
        }
    }


    protected void ShowWaiter_Click(object sender, EventArgs e)
    {
        if (WaitersDropDown.SelectedIndex == 0)
        {
            MessageLabel.Text = "Please select a Waiter.";         
        }
        else
        {
            try
            {

                Waiter info;

                RestaurantAdminController systemmgr = new RestaurantAdminController();

                info = systemmgr.GetWaiter(int.Parse(WaitersDropDown.SelectedValue));

                if (info == null)
                {
                    MessageLabel.Text = "Waiter ID does not exist on file.";
                    CleanFields();
                }
                else
                {

                    WaiterID.Text = info.WaiterID.ToString();

                    FirstName.Text = info.FirstName;
                    LastName.Text = info.LastName;
                    Phone.Text = info.Phone;
                    Address.Text = info.Address;
                    HireDate.Text = info.HireDate.ToString();
                    ReleaseDate.Text = info.ReleaseDate.ToString();


                }
            }//eof try
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

    }
}