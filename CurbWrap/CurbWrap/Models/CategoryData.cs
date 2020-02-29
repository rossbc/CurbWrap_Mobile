using System;
using System.Collections.Generic;
using System.Text;

namespace CurbWrap.Models
{
    public class CategoryData
    {
        public string Version { get; set; }
        public double SubscriptionPrice { get; set; }
        public double Price { get; set; }
        public double CustomPrice { get; set; }
        public string BaseUrl { get; set; }
        public List<NewsItem> News { get; set; }
        public List<Fundraiser> Fundraisers { get; set; }
        public List<Category>Categories { get; set; }
    }

    public class Category
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public List<cwImage> cwImages;
    }

    public class cwImage
    {
        public string Name { get; set; }
        public string Tags { get; set; }
        public string ImageUrl { get; set; }
        public string DefaultColor { get; set; }
    }

    public class NewsItem
    {
        public string Date { get; set; }
        public string Text { get; set; }
    }

    public class Fundraiser
    {
        public string FundraiserCode { get; set; }
        public string ExpiresOn { get; set; }
        public string Price { get; set; }
        public string OrderEmail { get; set; }
    }
}
