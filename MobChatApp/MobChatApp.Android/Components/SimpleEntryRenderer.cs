using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobChatApp.Components;
using MobChatApp.Droid.Components;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SimpleEntry), typeof(SimpleEntryRenderer))]
namespace MobChatApp.Droid.Components
{
    public class SimpleEntryRenderer: EntryRenderer
    {
        public SimpleEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if(Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
                
            }
        }
    }
}