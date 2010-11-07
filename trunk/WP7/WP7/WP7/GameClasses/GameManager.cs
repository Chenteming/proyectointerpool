namespace WP7
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using Microsoft.Phone.Controls;
    using WP7.ServiceReference;
    using WP7.Utilities;

    /// <summary>
    /// Class Description GameManager
    /// </summary>
    public class GameManager
    {
        /// <summary>
        /// Store for the property
        /// </summary>
        private static GameManager instance;

        /// <summary>
        /// Store for the property
        /// </summary>
        private string currentCity;

        /// <summary>
        /// Store for the property
        /// </summary>
        private List<string> cities;

        /// <summary>
        /// Store for the property
        /// </summary>
        private List<string> clues;

        /// <summary>
        /// Store for the property
        /// </summary>
        private string[] famous;

        /// <summary>
        /// Store for the property
        /// </summary>
        private List<string> suspects;

        /// <summary>
        /// Store for the property
        /// </summary>
        private int[] famousIndex = { -1, -1, -1 };

        /// <summary>
        /// Store for the property
        /// </summary>
        private int number = 0;

        /// <summary>
        /// Store for the property
        /// </summary>
        private int currentFamous = -1;

        ////0 = first_name
        ////1 = last_name 
        ////2 =  birthday
        ////3 = hometown
        ////4 = gender
        ////5 = music
        ////6 = cinema

        /// <summary>
        /// Initializes a new instance of the GameManager class.</summary>
        private GameManager()
        {
            this.cities = new List<string>(Constants.MaxCities);
            this.famous = new string[Constants.MaxFamous];
            this.filterField = new string[Constants.MaxFilterfield];
            this.clues = new List<string>();
            this.suspects = new List<string>();
            this.Logged = false;
            this.Vibration = false;
            this.ShowAnimation = false;
            this.Data = new DataClue();
            this.EmitOrder = false;
        }

        public string UserId { get; set; }

        /// <summary>
        /// Store for the property
        /// </summary>
        private string[] filterField;

        public bool Logged { get; set; }

        public string UserEmail { get; set; }

        public bool Vibration { get; set; }

        public DateTime CurrentDateTime { get; set; }

        public DateTime DeadLineDateTime { get; set; }

        public bool ShowAnimation { get; set; }

        /// <summary>
        /// Store for the property
        /// </summary>
        public DataClue Data;

        public string PictureLink { get; set; }

        public double  Left { get; set; }

        public double Top { get; set; }

        public bool EmitOrder { get; set; }

        public string PictureCityLink { get; set; }

        public static GameManager getInstance()
        {
            if (instance == null)
            {
                instance = new GameManager();
            }

            return instance;
        }

        public void AddCity(int position, string name)
        ////Add the new city in the list
        {
            this.cities.Insert(position, name);
        }

        public void AddClue(int position, string name)
        ////Add the new clue in the list
        {
            this.clues.Insert(position, name);
        }

        public void AddFamous(int position, string name)
        ////Add the new famous the list
        {
            this.famous[position] = name;
        }

        public void SetCurrentCity(string city)
        {
            this.currentCity = city;
        }

        public void SetCurrentCities(List<string> list)
        {
            this.cities = list;
        }

        public List<string> GetCities()
        {
            return this.cities;
        }

        public List<string> GetClues()
        {
            return this.clues;
        }

        public string GetCurrentCity()
        ////Return the current city
        {
            return this.currentCity;
        }

        public List<string> GetSuspects()
        {
            return this.suspects;
        }

        public void SetSuspectsList(List<string> list)
        {
            this.suspects = list;
        }

        public void SetFamousIndex(int gameObjectNumber)
        ////famousIndex[0] = phoneFamous        
        ////famousIndex[1] = newspaperFamous   (1,2,3) famousNumber
        ////famousIndex[2] = computerFamous
        {
            if (this.famousIndex[gameObjectNumber] == -1)
            {
                this.number++;
                this.famousIndex[gameObjectNumber] = this.number;
                this.currentFamous = this.number;
            }
            else
            {
                this.currentFamous = this.famousIndex[gameObjectNumber];
            }
        }

        public int GetNumber()
        ////returns the number of the Actual famous
        {
            return this.number;
        }

        public int GetCurrentFamous()
        {
            return this.currentFamous;
        }

        public void AddFilterField(string field, int position)
        {
            this.filterField[position] = field;
        }

        public string[] GetFilterField()
        {
            return this.filterField;
        }
    }
}