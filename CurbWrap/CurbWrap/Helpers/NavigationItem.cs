using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CurbWrap.Helpers
{
    public class NavigationItem
    {
        public NavigationItem()
        {
                Tabs = new List<ShellContent>();
        }

        public string Title { get; set; }              //This pairs with ShellItem.Title
        public DataTemplate Template { get; set; }     //This pairs with ShellItem.ContentTemplate
        public ImageSource Image { get; set; }         //This pairs with ShellItem.Icon
        public string Route { get; set; }              //This pairs with ShellItem.Route
        public List<ShellContent> Tabs { get; set; }   //This pairs with ShellItem.Tab


        public bool IsSecured { get; set; }
        public int SortOrder { get; set; }

        public bool HasImage => Image != null;         //These are optional, and just for validation purpose
        public bool HasContent => Tabs != null;        //These are optional, and just for validation purpose
    }
}
