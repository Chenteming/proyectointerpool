//-----------------------------------------------------------------------
// <copyright file="GameManager.cs" company="Interpool">
//     Copyright Interpool. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
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

        public DataGameInfo Info { get; set; }

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
            this.Info = new DataGameInfo();
            this.EmitOrder = false;
            this.FromMainPage = false;
            this.BrowserOpened = false;
            this.GetUserInfoTries = 0;
        }

        /// <summary>
        /// Store for the property
        /// </summary>
        public DataUserInfo UserInfo { get; set; }

        /// <summary>
        /// Store for the property
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Store for the property
        /// </summary>
        public bool FromMainPage { get; set; }

        /// <summary>
        /// Store for the property
        /// </summary>
        public bool BrowserOpened { get; set; }

        /// <summary>
        /// Store for the property
        /// </summary>
        public int GetUserInfoTries { get; set; }


        /// <summary>
        /// Store for the property
        /// </summary>
        private string[] filterField;

        public bool Logged { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public bool Vibration { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public DateTime CurrentDateTime { get; set; }

        public DateTime DeadLineDateTime { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public bool ShowAnimation { get; set; }

        /// <summary>
        /// Store for the property
        /// </summary>
        public DataClue Data;

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string PictureLink { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public double Left { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public double Top { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public bool EmitOrder { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string PictureCityLink { get; set; }

        /// <summary>
        /// Store for the property
        /// </summary>
        /// <returns> Return results are described through the returns tag.</returns>
        public static GameManager GetInstance()
        {
            if (instance == null)
            {
                instance = new GameManager();
            }

            return instance;
        }

        /// <summary>
        /// Description of the class 
        /// </summary>
        /// <param name="position">Parameter description for position goes here</param>
        /// <param name="name">Parameter description for name goes here</param>
        public void AddCity(int position, string name)
        ////Add the new city in the list
        {
            this.cities.Insert(position, name);
        }

        /// <summary>
        /// Description of the class 
        /// </summary>
        /// <param name="position">Parameter description for position goes here</param>
        /// <param name="name">Parameter description for name goes here</param>
        public void AddClue(int position, string name)
        ////Add the new clue in the list
        {
            this.clues.Insert(position, name);
        }

        /// <summary>
        /// Description of the class 
        /// </summary>
        /// <param name="position">Parameter description for position goes here</param>
        /// <param name="name">Parameter description for name goes here</param>
        public void AddFamous(int position, string name)
        ////Add the new famous the list
        {
            this.famous[position] = name;
        }

        /// <summary>
        /// Description of the class 
        /// </summary>
        /// <param name="city">Parameter description for city goes here</param>
        public void SetCurrentCity(string city)
        {
            this.currentCity = city;
        }

        /// <summary>
        /// Description of the class 
        /// </summary>
        /// <param name="list">Parameter description for list goes here</param>
        public void SetCurrentCities(List<string> list)
        {
            this.cities = list;
        }

        /// <summary>
        /// Description of the class 
        /// </summary>
        /// <returns> Return results are described through the returns tag.</returns>
        public List<string> GetCities()
        {
            return this.cities;
        }

        /// <summary>
        /// Description of the class 
        /// </summary>
        /// <returns> Return results are described through the returns tag.</returns>
        public List<string> GetClues()
        {
            return this.clues;
        }

        /// <summary>
        /// Description of the class 
        /// </summary>
        /// <returns> Return results are described through the returns tag.</returns>
        public string GetCurrentCity()
        ////Return the current city
        {
            return this.currentCity;
        }

        /// <summary>
        /// Description of the class 
        /// </summary>
        /// <returns> Return results are described through the returns tag.</returns>
        public List<string> GetSuspects()
        {
            return this.suspects;
        }

        /// <summary>
        /// Description of the class 
        /// </summary>
        /// <param name="list">Parameter description for list goes here</param>
        public void SetSuspectsList(List<string> list)
        {
            this.suspects = list;
        }

        /// <summary>
        /// Description of the class 
        /// </summary>
        /// <param name="gameObjectNumber">Parameter description for gameObjectNumber goes here</param>
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

        /// <summary>
        /// Description of the class 
        /// </summary>
        /// <returns> Return results are described through the returns tag.</returns>
        public int GetNumber()
        ////returns the number of the Actual famous
        {
            return this.number;
        }

        /// <summary>
        /// Description of the class 
        /// </summary>
        /// <returns> Return results are described through the returns tag.</returns>
        public int GetCurrentFamous()
        {
            return this.currentFamous;
        }

        /// <summary>
        /// Description of the class 
        /// </summary>
        /// <param name="field">Parameter description for field goes here</param>
        /// <param name="position">Parameter description for position goes here</param>
        public void AddFilterField(string field, int position)
        {
            this.filterField[position] = field;
        }

        /// <summary>
        /// Description of the class 
        /// </summary>
        /// <returns> Return results are described through the returns tag.</returns>
        public string[] GetFilterField()
        {
            return this.filterField;
        }

        
    }
}