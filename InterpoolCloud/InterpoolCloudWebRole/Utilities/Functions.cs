﻿
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
        /// Initializes a new instance of the Functions class.</summary>
        public Functions()
        { 
        }

        /// <summary>
        /// Suffle List function
        /// </summary>
        /// <typeparam name="E">Parameter description for E goes here</typeparam>
        /// <param name="inputList">Parameter description for inputList goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();
            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            { 
                //// Choose a random object in the list
                randomIndex = r.Next(0, inputList.Count);
                //// Add it to the new, random list 
                randomList.Add(inputList[randomIndex]);
                //// Remove to avoid duplicates
                inputList.RemoveAt(randomIndex);
            }

            return randomList; ////return the new random list
        }
    }
}