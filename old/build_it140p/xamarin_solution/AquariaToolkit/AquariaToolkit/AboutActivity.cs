﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquariaToolkit
{
    [Activity(Label = "AboutActivity")]
    public class AboutActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_about);

            ImageButton btnBack = FindViewById<ImageButton>(Resource.Id.imageButtonBack);
            btnBack.Click += OnButtonBackClick;
        }

        private void OnButtonBackClick(object sender, EventArgs eventArgs)
        {
            Finish();
        }
    }
}