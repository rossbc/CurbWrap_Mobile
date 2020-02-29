using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurbWrap.MarkupExtensions
{
    [ContentProperty("ResourceName")]
    public class EmbeddedImage : IMarkupExtension
    {
        //Use this class for embedded images
        public string ResourceName { get; set; }
        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            if (String.IsNullOrWhiteSpace(ResourceName)) return null;

            return ImageSource.FromResource(ResourceName);
        }
    }
}
