using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobChatApp.Components;
using MobChatApp.Droid.Components;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(OutlineButton), typeof(OutlineButtonRenderer))]
namespace MobChatApp.Droid.Components
{
    class OutlineButtonRenderer : ButtonRenderer
    {
        public OutlineButtonRenderer(Context context) : base(context)
        {
        }
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
        }
    }
}