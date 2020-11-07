using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Identity.Client;

namespace MobChatApp.Droid
{
    [Activity]
    [IntentFilter(new [] { Intent.ActionView},
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault},
        DataHost = "auth",
        DataScheme = "msal8d851549-9c94-4f28-9092-b076089985f9")]

    public class MsalActivity : BrowserTabActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
    }
}