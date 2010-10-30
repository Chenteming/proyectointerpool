

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

    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            log.Text = "";
            log.Text = "Comienzo a procesar el Filter Suspect...";
            InterpoolContainer conteiner = new InterpoolContainer();
            ////Poner el id de facebook que se trae en el loguin cada vez que se conecta.
            IDataManager dm = new DataManager();
            ////string userId = dm.GetLastUserIdFacebook(dm.GetContainer());
            string currentUser = this.TextBoxEmail.Text;
            string userId = dm.GetUserIdFacebookByLoginId(currentUser, dm.GetContainer());
            IProcessController ipc = new ProcessController(dm.GetContainer());
            DataFacebookUser fbud = new DataFacebookUser();
            DataListFacebookUser dlf = ipc.FilterSuspects(userId, fbud);
            Label1.Text = this.getDayOfWeek(dlf.CurrentDate);
            string minutes = "";
            minutes = dlf.CurrentDate.Minute < 10 ? "0" + dlf.CurrentDate.Minute : "" + dlf.CurrentDate.Minute;
            string hour = "";
            hour = dlf.CurrentDate.Hour < 10 ? "0" + dlf.CurrentDate.Hour : "" + dlf.CurrentDate.Hour;
            Label2.Text = hour + ":" + minutes;
            log.Text += Environment.NewLine + "Termino de procesar el Filter Suspect...";
        }
        private string getDayOfWeek(DateTime currentDate)
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            log.Text = "";
            InterpoolContainer container = new InterpoolContainer();
            IDataManager dm = new DataManager();
            ProcessController ipc = new ProcessController(container);
            string currentUser = this.TextBoxEmail.Text;
            string userId = dm.GetUserIdFacebookByLoginId(currentUser, dm.GetContainer());
            DataCity dc;
            if (CheckBox1.Checked)
            {
                log.Text = "Comienzo a procesar el Travel Good a la ciudad: " + ipc.GetNextNode(userId).City.CityName;
                dc = ipc.Travel(userId, ipc.GetNextNode(userId).City.CityName);
            }
            else
            {
                log.Text = "Comienzo a procesar el Travel Wrong...";
                dc = ipc.Travel(userId, "");
            }
            Label1.Text = this.getDayOfWeek(dc.CurrentDate);
            string minutes = "";
            minutes = dc.CurrentDate.Minute < 10 ? "0" + dc.CurrentDate.Minute : "" + dc.CurrentDate.Minute;
            string hour = "";
            hour = dc.CurrentDate.Hour < 10 ? "0" + dc.CurrentDate.Hour : "" + dc.CurrentDate.Hour;
            Label2.Text = hour + ":" + minutes;
            log.Text += Environment.NewLine + "Termino de procesar el Travel...";
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            log.Text = "";
            InterpoolContainer container = new InterpoolContainer();
            IDataManager dm = new DataManager();
            ProcessController ipc = new ProcessController(container);
            string currentUser = this.TextBoxEmail.Text;
            log.Text = "Comienzo a procesar Question Famous... ";
            string userId = dm.GetUserIdFacebookByLoginId(currentUser, dm.GetContainer());
            int numFamous = Int32.Parse(TextBox1.Text);
            DataClue dc = ipc.GetClueByFamous(userId, numFamous);
            log.Text = log.Text + "\n" + "Clue: " + dc.Clue;
            Label1.Text = this.getDayOfWeek(dc.CurrentDate);
            string minutes = "";
            minutes = dc.CurrentDate.Minute < 10 ? "0" + dc.CurrentDate.Minute : "" + dc.CurrentDate.Minute;
            string hour = "";
            hour = dc.CurrentDate.Hour < 10 ? "0" + dc.CurrentDate.Hour : "" + dc.CurrentDate.Hour;
            Label2.Text = hour + ":" + minutes;
            log.Text += Environment.NewLine + "Termino de procesar Question Famous... ";

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            log.Text = "";
            InterpoolContainer conteiner = new InterpoolContainer();
            ////Poner el id de facebook que se trae en el loguin cada vez que se conecta.
            IDataManager dm = new DataManager();
            ////string userId = dm.GetLastUserIdFacebook(dm.GetContainer());
            string currentUser = this.TextBoxEmail.Text;
            log.Text = "Comienzo a procesar Start Game... ";
            string userId = dm.GetUserIdFacebookByLoginId(currentUser, dm.GetContainer());
            IProcessController ipc = new ProcessController(dm.GetContainer());
            DataCity dc;
            ipc.StartGame(userId);
            dc = ipc.GetCurrentCity(userId);
            Label1.Text = this.getDayOfWeek(dc.CurrentDate);
            string minutes = "";
            minutes = dc.CurrentDate.Minute < 10 ? "0" + dc.CurrentDate.Minute : "" + dc.CurrentDate.Minute;
            string hour = "";
            hour = dc.CurrentDate.Hour < 10 ? "0" + dc.CurrentDate.Hour : "" + dc.CurrentDate.Hour; 
            Label2.Text = hour + ":" + minutes;

            minutes = "";
            minutes = dc.DeadLine.Minute < 10 ? "0" + dc.DeadLine.Minute : "" + dc.DeadLine.Minute;
            hour = "";
            hour = dc.DeadLine.Hour < 10 ? "0" + dc.DeadLine.Hour : "" + dc.DeadLine.Hour;
            Label3.Text = this.getDayOfWeek(dc.DeadLine);
            Label4.Text = hour + ":" + minutes;
            log.Text += Environment.NewLine + "Termino de procesar Start Game";
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            log.Text = "";
            log.Text = "Comienzo a procesar el Login... ";
            OAuthFacebook oauth = new OAuthFacebook();
            Response.Redirect(oauth.AuthorizationLinkGet());
            log.Text += Environment.NewLine + "Termino de procesar el Login";
        }
    }
}