
////-----------------------------------------------------------------------
////<copyright file="Functions.cs" company="Interpool">
////     Copyright Interpool. All rights reserved.
//// </copyright>
////-----------------------------------------------------------------------
namespace InterpoolCloudWebRole.Utilities
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
    /// <summary>
    /// Class statement Functions
    /// </summary>    
    public class Functions
    {

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="noticia"> Parameter description for inputList goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public Functions()
        { 
        }
        /// <summary>
        /// Description for Method.</summary>
        /// <param name="noticia"> Parameter description for inputList goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public List<E> SuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();
            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); ////Choose a random object in the list 
                randomList.Add(inputList[randomIndex]); ////add it to the new, random list 
                inputList.RemoveAt(randomIndex); ////remove to avoid duplicates 
            }
            return randomList; ////return the new random list
        }
    }
}