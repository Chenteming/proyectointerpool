//-----------------------------------------------------------------------
// <copyright file="TestPage.aspx.cs" company="Interpool">
//     Copyright Interpool. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace InterpoolCloudWebRole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using InterpoolCloudWebRole.Controller;
    using InterpoolCloudWebRole.Data;
    using InterpoolCloudWebRole.Datatypes;
    using InterpoolCloudWebRole.FacebookCommunication;
    using InterpoolCloudWebRole.Utilities;

    /// <summary>
    /// form for test
    /// </summary>
    public partial class WebForm1 : System.Web.UI.Page
    {
        /// <summary>
        /// page load function
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// test filter suspect
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            this.log.Text = String.Empty;
            this.log.Text = "Comienzo a procesar el Filter Suspect...";
            InterpoolContainer conteiner = new InterpoolContainer();
            ////Poner el id de facebook que se trae en el loguin cada vez que se conecta.
            IDataManager dm = new DataManager();
            ////string userId = dm.GetLastUserIdFacebook(dm.GetContainer());
            string currentUser = this.TextBoxEmail.Text;
            string userId = dm.GetUserIdFacebookByLoginId(currentUser, dm.GetContainer());
            IProcessController ipc = new ProcessController(dm.GetContainer());
            DataFacebookUser fbud = new DataFacebookUser();
            DataListFacebookUser dlf = ipc.FilterSuspects(userId, fbud);
            this.Label1.Text = this.GetDayOfWeek(dlf.CurrentDate);
            string minutes = String.Empty;
            minutes = dlf.CurrentDate.Minute < 10 ? "0" + dlf.CurrentDate.Minute : String.Empty + dlf.CurrentDate.Minute;
            string hour = String.Empty;
            hour = dlf.CurrentDate.Hour < 10 ? "0" + dlf.CurrentDate.Hour : String.Empty + dlf.CurrentDate.Hour;
            this.Label2.Text = hour + ":" + minutes;
            this.log.Text += Environment.NewLine + "Termino de procesar el Filter Suspect...";
        }

        /// <summary>
        /// test Travel
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            this.log.Text = String.Empty;
            InterpoolContainer container = new InterpoolContainer();
            IDataManager dm = new DataManager();
            ProcessController ipc = new ProcessController(container);
            string currentUser = this.TextBoxEmail.Text;
            string userId = dm.GetUserIdFacebookByLoginId(currentUser, dm.GetContainer());
            DataCity dc;
            if (this.CheckBox1.Checked)
            {
                this.log.Text = "Comienzo a procesar el Travel Good a la ciudad: " + ipc.GetNextNode(userId).City.CityName;
                dc = ipc.Travel(userId, ipc.GetNextNode(userId).City.CityName);
            }
            else
            {
                this.log.Text = "Comienzo a procesar el Travel Wrong...";
                dc = ipc.Travel(userId, String.Empty);
            }

            this.Label1.Text = this.GetDayOfWeek(dc.CurrentDate);
            string minutes = String.Empty;
            minutes = dc.CurrentDate.Minute < 10 ? "0" + dc.CurrentDate.Minute : String.Empty + dc.CurrentDate.Minute;
            string hour = String.Empty;
            hour = dc.CurrentDate.Hour < 10 ? "0" + dc.CurrentDate.Hour : String.Empty + dc.CurrentDate.Hour;
            this.Label2.Text = hour + ":" + minutes;
            this.log.Text += Environment.NewLine + "Termino de procesar el Travel...";
        }

        /// <summary>
        /// test get clue famous
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void Button3_Click(object sender, EventArgs e)
        {
            this.log.Text = String.Empty;
            InterpoolContainer container = new InterpoolContainer();
            IDataManager dm = new DataManager();
            ProcessController ipc = new ProcessController(container);
            string currentUser = this.TextBoxEmail.Text;
            this.log.Text = "Comienzo a procesar Question Famous... ";
            string userId = dm.GetUserIdFacebookByLoginId(currentUser, dm.GetContainer());
            int numFamous = Int32.Parse(this.TextBox1.Text);
            DataClue dc = ipc.GetClueByFamous(userId, numFamous);
            this.log.Text += Environment.NewLine + "Clue: " + dc.Clue;
            this.Label1.Text = this.GetDayOfWeek(dc.CurrentDate);
            string minutes = String.Empty;
            minutes = dc.CurrentDate.Minute < 10 ? "0" + dc.CurrentDate.Minute : String.Empty + dc.CurrentDate.Minute;
            string hour = String.Empty;
            hour = dc.CurrentDate.Hour < 10 ? "0" + dc.CurrentDate.Hour : String.Empty + dc.CurrentDate.Hour;
            this.Label2.Text = hour + ":" + minutes;
            this.log.Text += Environment.NewLine + "Termino de procesar Question Famous... ";
        }

        /// <summary>
        /// test start game
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void Button4_Click(object sender, EventArgs e)
        {
            this.log.Text = String.Empty;
            InterpoolContainer conteiner = new InterpoolContainer();
            ////Poner el id de facebook que se trae en el loguin cada vez que se conecta.
            IDataManager dm = new DataManager();
            ////string userId = dm.GetLastUserIdFacebook(dm.GetContainer());
            string currentUser = this.TextBoxEmail.Text;
            this.log.Text = "Comienzo a procesar Start Game... ";
            string userId = dm.GetUserIdFacebookByLoginId(currentUser, dm.GetContainer());
            IProcessController ipc = new ProcessController(dm.GetContainer());
            DataCity dc;
            ipc.StartGame(userId);
            dc = ipc.GetCurrentCity(userId);
            this.Label1.Text = this.GetDayOfWeek(dc.CurrentDate);
            string minutes = String.Empty;
            minutes = dc.CurrentDate.Minute < 10 ? "0" + dc.CurrentDate.Minute : String.Empty + dc.CurrentDate.Minute;
            string hour = String.Empty;
            hour = dc.CurrentDate.Hour < 10 ? "0" + dc.CurrentDate.Hour : String.Empty + dc.CurrentDate.Hour;
            this.Label2.Text = hour + ":" + minutes;
            minutes = String.Empty;
            minutes = dc.DeadLine.Minute < 10 ? "0" + dc.DeadLine.Minute : String.Empty + dc.DeadLine.Minute;
            hour = String.Empty;
            hour = dc.DeadLine.Hour < 10 ? "0" + dc.DeadLine.Hour : String.Empty + dc.DeadLine.Hour;
            this.Label3.Text = this.GetDayOfWeek(dc.DeadLine);
            this.Label4.Text = hour + ":" + minutes;
            this.log.Text += Environment.NewLine + "Termino de procesar Start Game";
        }

        /// <summary>
        /// test login
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void Button5_Click(object sender, EventArgs e)
        {
            this.log.Text = String.Empty;
            this.log.Text = "Comienzo a procesar el Login... ";
            OAuthFacebook oauth = new OAuthFacebook();
            Response.Redirect(oauth.AuthorizationLinkGet());
            this.log.Text += Environment.NewLine + "Termino de procesar el Login";
        }

        /// <summary>
        /// Get Day of week 
        /// </summary>
        /// <param name="currentDate">Parameter description for currentDate goes here</param>
        /// <returns>
        /// the day of the week</returns>
        private string GetDayOfWeek(DateTime currentDate)
        {
            switch (currentDate.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    return "Friday";
                case DayOfWeek.Monday:
                    return "Monday";
                case DayOfWeek.Saturday:
                    return "Saturday";
                case DayOfWeek.Sunday:
                    return "Sunday";
                case DayOfWeek.Thursday:
                    return "Thursday";
                case DayOfWeek.Tuesday:
                    return "Tuesday";
                case DayOfWeek.Wednesday:
                    return "Wednesday";
                default:
                    return "chupame el escroto";
            }
        }

        /// <summary>
        /// Get Day of week 
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for sender e here</param>
        private void Button6_Click(object sender, EventArgs e)
        {
            this.log.Text = String.Empty;
            InterpoolContainer container = new InterpoolContainer();
            IDataManager dm = new DataManager();
            ProcessController ipc = new ProcessController(container);
            string currentUser = this.TextBoxEmail.Text;
            this.log.Text = "Comienzo a procesar Question Famous... ";
            string userId = dm.GetUserIdFacebookByLoginId(currentUser, ipc.GetContainer());
            User user = dm.GetUserByIdFacebook(ipc.GetContainer(), userId).First();
            ipc.DeleteGame(user);
        }
    }
}