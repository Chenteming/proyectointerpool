//-----------------------------------------------------------------------
// <copyright file="LanguageManager.cs" company="Interpool">
//     Copyright Interpool. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace WP7
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Resources;
    using System.Windows.Shapes;
    using System.Xml;
    using System.Xml.Linq;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;

    /// <summary>
    /// Class Description LanguageManager
    /// </summary>
    public class LanguageManager
    {
        /// <summary>
        /// Store for the property
        /// </summary>
        private static LanguageManager instance;

        /// <summary>
        /// Store for the property
        /// </summary>
        private XDocument xdoc = null;

        /// <summary>
        /// Store for the property
        /// </summary>
        private string current = "Spanish";

        /// <summary>
        /// Initializes a new instance of the LanguageManager class.</summary>
        protected LanguageManager()
        {
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public XDocument GetXDoc()
        {
            return this.xdoc;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="document"> Parameter description for document goes here</param>
        public void SetXDoc(XDocument document)
        {
            this.xdoc = document;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public string GetCurrentLanguage()
        {
            return this.current;
        }

        /// <summary>
        /// Description of the class 
        /// </summary>
        /// <param name="language">Parameter description for language goes here</param>
        public void SetCurrentLanguage(string language)
        {
            this.current = language;
        }

        /// <summary>
        /// Method for translate the page language
        /// </summary>
        /// <param name="page">Parameter description for page goes here</param>
        public void TranslatePage(PhoneApplicationPage page)
        {           
            var controls = from us in this.xdoc.Elements("language").Elements("class").Elements("control")
                           where (string)us.Parent.Attribute("name") == page.GetType().Name            
                    select us;
            foreach (var control in controls)
            {
                if (control.Attribute("name").Value.CompareTo("AppBarMenuItem") == 0)
                {
                    ////FindName not working for  AppBarMenuItem                    
                    ((ApplicationBarMenuItem)page.ApplicationBar.MenuItems[int.Parse(control.Attribute("Index").Value) - 1]).Text = control.Attribute("Text").Value;
                }
                else
                {
                    object item = page.FindName(control.Attribute("name").Value);
                    ////not working for AppBarMenuItem
                    if (item != null)
                    {
                        Type myType = item.GetType();
                        foreach (XAttribute attr in control.Attributes())
                        {
                            if (attr.Name.LocalName.CompareTo("name") != 0)
                            {
                                PropertyInfo pinfo = myType.GetProperty(attr.Name.LocalName);
                                pinfo.SetValue(item, attr.Value, null);
                            }
                        }
                    }
                }               
            }
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public static LanguageManager GetInstance()
        {
            if (instance == null)
            {
                instance = new LanguageManager();
            }

            return instance;
        }
    }
}
