﻿using Microsoft.Identity.Client;
using MobChat.Application.Models.ViewModels;
using MobChat.Application.Services;
using MobChatApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobChatApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        //AuthenticationResult authenticationResult;
        
        public AppShell()
        {
            InitializeComponent();
            
        }

    }

}