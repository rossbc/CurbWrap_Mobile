using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CurbWrap.Helpers
{
    public class EntryExt : Entry
    {
        public const string ReturnKeyPropertyName = "ReturnKeyType";

        public EntryExt() { }

        public static readonly BindableProperty ReturnKeyTypeProperty = BindableProperty.Create(
            propertyName: ReturnKeyPropertyName,
            returnType: typeof(ReturnKeyTypes),
            declaringType: typeof(EntryExt),
            defaultValue: ReturnKeyTypes.Done);

        public ReturnKeyTypes ReturnKeyType
        {
            get
            {
                var rtn = (ReturnKeyTypes) GetValue(ReturnKeyTypeProperty);
                //if (rtn == ReturnKeyTypes.Continue) return ReturnKeyTypes.Default;
                return rtn;
            }
            set { SetValue(ReturnKeyTypeProperty, value); }
        }
    }

    // Not all of these are supported on Android, consult EntryEditText.ImeOptions
    //Continue is not supported
    public enum ReturnKeyTypes : int
    {
        Default,
        None,
        Go,
        Search,
        Send,
        Next,
        Done,
        Previous
    }


    //Supported 
    // - ImeNull = 0,
    // - Unspecified = 0,
    // - None = 1,
    // - Go = 2,
    // - Search = 3,
    // - Send = 4,
    // - Next = 5,
    // - Done = 6,
    // - Previous = 7,
    // - ImeMaskAction = 255, // 0x000000FF

    
    //public enum ReturnKeyTypes : int
    //{
    //    Default,
    //    Go,
    //    Google,
    //    Join,
    //    Next,
    //    Route,
    //    Search,
    //    Send,
    //    Yahoo,
    //    Done,
    //    EmergencyCall,
    //    Continue
    //}


}
