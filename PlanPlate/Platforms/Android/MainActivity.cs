﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Firebase.Crashlytics;
using Plugin.Permissions;
using System.Diagnostics;

namespace PlanPlate
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Firebase.FirebaseApp.InitializeApp(this);

            FirebaseCrashlytics.Instance.SetCrashlyticsCollectionEnabled(true);

            FirebaseCrashlytics.Instance.SetUserId("user1234rr56");

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
