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
    using System.Xml.Linq;
    using Microsoft.Phone.Controls;
    using System.Xml;
    using System.Windows.Resources;
    using System.Reflection;
    using System.Linq;
    using Microsoft.Phone.Shell;

    public class LanguageManager
    {
        private static LanguageManager instance;
        private XDocument xDoc = null;
        private string current = "Spanish";        

        protected LanguageManager()
        {
        }
        
        public XDocument GetXDoc()
        {
            return this.xDoc;
        }

        public void SetXDoc(XDocument document)
        {
            this.xDoc = document;
        }

        public string GetCurrentLanguage()
        {
            return this.current;
        }

        public void SetCurrentLanguage(string language)
        {
            this.current = language;
        }

        public static LanguageManager GetInstance()
        {
            if (instance == null)
            {
                instance = new LanguageManager();
            }
            return instance;
        }

        /// <summary>
        /// Method for translate the page language
        /// </summary>
        /// <param name="page"></param>
        public void TranslatePage(PhoneApplicationPage page)
        {           
            var controls = from us in this.xDoc.Elements("language").Elements("class").Elements("control")
                           where (string)us.Parent.Attribute("name") == page.GetType().Name            
                    select us;
            foreach (var control in controls)
            {   
                if (control.Attribute("name").Value.CompareTo("AppBarMenuItem") == 0)
                    ////FindName not working for AppBarMenuItem                    
                    ((ApplicationBarMenuItem)page.ApplicationBar.MenuItems[int.Parse(control.Attribute("Index").Value) - 1]).Text = control.Attribute("Text").Value;                
                else
                {
                    object item = page.FindName(control.Attribute("name").Value);
                    ////not working for AppBarMenuItem
                    if (item != null)
                    {
                        Type myType = item.GetType();
                        foreach (XAttribute attr in control.Attributes())
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
}
