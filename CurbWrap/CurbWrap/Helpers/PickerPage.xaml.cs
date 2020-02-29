using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurbWrap.Helpers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerPage : ContentPage
    {
        public ListView pickerControl
        {
            get { return picker; }
        }

        public PickerPage()
        {
            
        }
        public PickerPage( HelperFunctions.PickerType pickerType, string pickerValue="")
        {
            InitializeComponent();

            switch (pickerType)
            {
                case HelperFunctions.PickerType.Color:
                    LoadColors();
                    break;
                case HelperFunctions.PickerType.State:
                    LoadStates();
                    break;
                case HelperFunctions.PickerType.Month:
                    LoadMonths();
                    break;
                case HelperFunctions.PickerType.Year:
                    LoadYears();
                    break;
            }

            SetSelectedValue(pickerValue);

        }

        private void SetSelectedValue(string selectedValue)
        {

        }

        //******************    COLOR   ******************************
        public Color GetColor(string colorName)
        {
            //return colorChoice.Color;
            return nameToColor[colorName];
        }

        // Dictionary to get Color from color name.
        protected Dictionary<string, Color> nameToColor = new Dictionary<string, Color>
        {
            { "Aqua", Color.Aqua }, { "Black", Color.Black },
            { "Blue", Color.Blue }, { "Fuchsia", Color.Fuchsia },
            { "Gray", Color.Gray }, { "Green", Color.Green },
            { "Lime", Color.Lime }, { "Maroon", Color.Maroon },
            { "Navy", Color.Navy }, { "Olive", Color.Olive },
            { "Purple", Color.Purple }, { "Red", Color.Red },
            { "Silver", Color.Silver }, { "Teal", Color.Teal },
            { "White", Color.White }, { "Yellow", Color.Yellow }
        };


        private void LoadColors()
        {
            //var template = new DataTemplate(typeof(TextCell));
            //template.SetValue(TextCell.TextColorProperty, Color.Black);
            ////template.SetBinding();
            //picker.ItemTemplate = template;

            this.Title = "Color Of Address Numbers";
            //var items = new List<ColorChoice>();
            var items = new List<string>();
            foreach (string colorName in nameToColor.Keys)
            {
                //items.Add(new ColorChoice{ColorName = colorName });
                items.Add(colorName);
            }

            picker.ItemsSource = items;

        }

        //protected class ColorChoice
        //{
        //    public string ColorName { get; set; }
        //    public Color Color { get; set; }
        //}

        //******************    STATES   ******************************
        public string GetStateValue(string stateName)
        {
            //return state abbreviation;
            return stateInfo[stateName];
        }

        // Dictionary to get Color from color name.
        protected Dictionary<string, string> stateInfo = new Dictionary<string, string>
        {
            {"Alabama","AL"},
            {"Alaska","AK"},
            {"Arizona","AZ"},
            {"Arkansas","AR"},
            {"California","CA"},
            {"Colorado","CO"},
            {"Connecticut","CT"},
            {"Delaware","DE"},
            {"Florida","FL"},
            {"Georgia","GA"},
            {"Hawaii","HI"},
            {"Idaho","ID"},
            {"Illinois","IL"},
            {"Indiana","IN"},
            {"Iowa","IA"},
            {"Kansas","KS"},
            {"Kentucky","KY"},
            {"Louisiana","LA"},
            {"Maine","ME"},
            {"Maryland","MD"},
            {"Massachusetts","MA"},
            {"Michigan","MI"},
            {"Minnesota","MN"},
            {"Mississippi","MS"},
            {"Missouri","MO"},
            {"Montana","MT"},
            {"Nebraska","NE"},
            {"Nevada","NV"},
            {"New Hampshire","NH"},
            {"New Jersey","NJ"},
            {"New Mexico","NM"},
            {"New York","NY"},
            {"North Carolina","NC"},
            {"North Dakota","ND"},
            {"Ohio","OH"},
            {"Oklahoma","OK"},
            {"Oregon","OR"},
            {"Pennsylvania","PA"},
            {"Rhode Island","RI"},
            {"South Carolina","SC"},
            {"South Dakota","SD"},
            {"Tennessee","TN"},
            {"Texas","TX"},
            {"Utah","UT"},
            {"Vermont","VT"},
            {"Virginia","VA"},
            {"Washington","WA"},
            {"West Virginia","WV"},
            {"Wisconsin","WI"},
            {"Wyoming","WY"}
        };

        private void LoadStates()
        {
            this.Title = "US States";
            //var items = new List<ColorChoice>();
            var items = new List<string>();
            foreach (string state in stateInfo.Keys)
            {
                //Add state name
                items.Add(state);
            }

            picker.ItemsSource = items;

        }

        //protected class US_States
        //{
        //    public string stateAbbrev { get; set; }
        //    public string StateName { get; set; }
        //}

        //******************    YEAR   ******************************

        protected List<string> YearList = new List<string>();

        private void LoadYears()
        {
            var startYear = DateTime.Today.Year;
            for (int i = startYear; i < startYear+10; i++)
            {
                YearList.Add(i.ToString());
            }

            picker.ItemsSource = YearList;
        }



        //******************    MONTH   ******************************
        protected Dictionary<string, string> nameToMonth = new Dictionary<string, string>
        {
            { "January", "01"}, { "February", "02" },
            { "March", "03" }, { "April", "04" },
            { "May", "05" }, { "June", "06" },
            { "July", "07" }, { "August", "08" },
            { "September", "09" }, { "October", "10" },
            { "November", "11" }, { "December", "12" }
        };

        private void LoadMonths()
        {
            this.Title = "Calendar Months";
            var items = new List<string>();
            foreach (string month in nameToMonth.Keys)
            {
                //Add month name
                items.Add(month);
            }

            picker.ItemsSource = items;

        }


        public string GetMonthValue(string monthName)
        {
            //return state abbreviation;
            return nameToMonth[monthName];
        }



    }
}