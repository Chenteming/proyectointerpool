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


namespace WP7
{
    public class LanguageManager
    {
        private static LanguageManager Instance;
        private XDocument XDoc = null;
        private String current = "Spanish";        

        protected LanguageManager()
        {

        }
        
        public XDocument GetXDoc()
        {
            return XDoc;
        }

        public void SetXDoc(XDocument document)
        {
            XDoc = document;
        }

        public String GetCurrentLanguage()
        {
            return current;
        }

        public void SetCurrentLanguage(String language)
        {
            current = language;
        }

        public static LanguageManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new LanguageManager();
            }
            return Instance;
        }

        public void TranslatePage(PhoneApplicationPage page)
        {           
            var controls = from us in XDoc.Elements("language").Elements("class").Elements("control")
                           where (String)us.Parent.Attribute("name") == page.GetType().Name            
                    select us;
            foreach (var control in controls)
            {   
                if (control.Attribute("name").Value.CompareTo("AppBarMenuItem")==0)                
                    // FindName not working for AppBarMenuItem                    
                    ((ApplicationBarMenuItem)page.ApplicationBar.MenuItems[int.Parse(control.Attribute("Index").Value) - 1]).Text = control.Attribute("Text").Value;                
                else
                {
                    object item = page.FindName(control.Attribute("name").Value);
                    // not working for AppBarMenuItem
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
