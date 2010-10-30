namespace WP7
{
    using System;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using System.Collections.Generic;
    using Microsoft.Phone.Controls;
    using WP7.Utilities;

    public class GameManager
    {
        private static GameManager instance;
        private string currentCity;
        private List<string> cities;
        private List<string> clues;
        private string[] famous;
        private List<string> suspects;
        private int[] famousIndex = { -1, -1, -1 };
        private int number = 0;
        private int currentFamous = -1;
        ////0 = first_name
        ////1 = last_name 
        ////2 =  birthday
        ////3 = hometown
        ////4 = gender
        ////5 = music
        ////6 = cinema
        private GameManager()
        {
            this.cities = new List<string>(Constants.MAX_CITIES);
            this.famous = new string[Constants.MAX_FAMOUS];
            this.filterField = new string[Constants.MAX_FILTERFIELD];
            this.clues = new List<string>();
            this.suspects = new List<string>();
            this.Logged = false;
        }

        public string UserId { get; set; }

        private string[] filterField;

		public bool Logged { get; set; }

		public string UserEmail { get; set; }

        public static GameManager getInstance()
        {
            if (instance == null)
                instance = new GameManager();
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
        ////Return all the cities	
        {
            return this.cities;
        }

        public List<string> GetClues()
        ////Return all the clues	
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
            if (famousIndex[gameObjectNumber] == -1)
            {
                this.number++;
                this.famousIndex[gameObjectNumber] = number;
                this.currentFamous = number;
            }
            else
                this.currentFamous = famousIndex[gameObjectNumber];
        }

        public int GetNumber()
        ////returns the number of the actual famous
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