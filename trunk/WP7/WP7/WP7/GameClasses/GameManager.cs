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

namespace WP7
{
    public class GameManager
    {
        private static GameManager instance;
        private string currentCity;
        private List<string> cities;
        private List<string> clues;
        private string[] famous;
        private List<string> suspects;
        private int[] famousIndex = {-1,-1,-1} ;
        private int number = 0;
        private int currentFamous = -1;
        public string userId { get; set; }

        public static GameManager getInstance()
        {
            if (instance == null)
                instance = new GameManager();
            return instance;
        }

        private GameManager()
        {
            cities = new List<string>(Constants.MAX_CITIES);
            famous = new string[Constants.MAX_FAMOUS];
            clues = new List<string>();
            suspects = new List<string>();
        }

        public void AddCity(int position, string name)
        // Add the new city in the list
        {
            cities.Insert(position, name);
        }

        public void AddClue(int position, string name)
        // Add the new clue in the list
        {
            clues.Insert(position, name);
        }

        public void AddFamous(int position, string name)
        // Add the new famous the list
        {
           
            famous[position] = name;
        }

        public void SetCurrentCity(string city)
        {
            currentCity = city;
        }

        public void SetCurrentCities(List<string> list)
        {
            cities = list;
        }

        public List<string> GetCities()
        // Return all the cities	
        {
            return cities;
        }

        public List<string> GetClues()
        // Return all the clues	
        {
            return clues;
        }

        public string GetCurrentCity()
        // Return the current city
        {
            return currentCity;
        }

        public List<string> GetSuspects()
        {
            return suspects;
        }

        public void SetSuspectsList(List<string> list)
        {
            suspects = list;
        }

        public void SetFamousIndex(int gameObjectNumber)
        // famousIndex[0] = phoneFamous        
        // famousIndex[1] = newspaperFamous   (1,2,3) famousNumber
        // famousIndex[2] = computerFamous
        {
            if (famousIndex[gameObjectNumber] == -1)
            {
                number++;
                famousIndex[gameObjectNumber] = number;
                currentFamous = number;
            }
            else
                currentFamous = famousIndex[gameObjectNumber];
        }
        public int GetNumber()
        // returns the number of the actual famous
        {
            return number;
        }

        public int GetCurrentFamous()
        {
            return currentFamous;
        }
       }
}