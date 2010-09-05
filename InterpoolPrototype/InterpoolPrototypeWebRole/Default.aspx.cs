using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.Services.Client;


namespace InterpoolPrototypeWebRole
{
    public partial class _Default : System.Web.UI.Page
    {
        private HelloWorldEntities context;
       // private Uri svcUri = new Uri("http://localhost:4069/WcfDataService.svc");


        protected void Page_Load(object sender, EventArgs e)
        {
            //context = new HelloWorldEntities();
            //List<City> cities = new List<City>(context.Cities);
            //foreach (City c in cities)
            //{
            //    citiesList.Items.Add(new ListItem(String.Concat(c.Name, " - ", c.CountryName), c.ID.ToString()));
            //}
        }

        protected void citiesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Define a LINQ query that returns information
            // about the selected person.
            var info = (from p in context.Cities
                        where p.ID == Convert.ToInt32(citiesList.SelectedItem.Value)
                        select p).FirstOrDefault();

            //Display information about the person
            infoLabel.Text = String.Concat("ID: ", info.ID.ToString(), " ",
                                            "Name: ", info.Name);

        }
    }
}
