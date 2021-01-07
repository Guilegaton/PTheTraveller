using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using Plugin.Messaging;
using Xamarin.Essentials;

namespace PrushkaTheTraveller
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            Android.Support.V7.App.ActionBarDrawerToggle toggle = new Android.Support.V7.App.ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            var notificationBtn = FindViewById<Button>(Resource.Id.notificationBTN);
            notificationBtn.Click += OnScheduleClick;

        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                // Handle the camera action
            }
            else if (id == Resource.Id.nav_gallery)
            {

            }
            else if (id == Resource.Id.nav_slideshow)
            {

            }
            else if (id == Resource.Id.nav_manage)
            {

            }
            else if (id == Resource.Id.nav_share)
            {

            }
            else if (id == Resource.Id.nav_send)
            {

            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        CancellationTokenSource cts;

        async void GetCurrentLocation(object sender, EventArgs eventArgs)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    var textViewer = FindViewById<TextView>(Resource.Id.locationTV);
                    textViewer.Text = $"Latitude: { location.Latitude}, Longitude: { location.Longitude}, Altitude: { location.Altitude}, Accuracy: {location.Accuracy}";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        async void OnScheduleClick(object sender, EventArgs eventArgs)
        {

            //Services.GeolocationService geolocationService = new Services.GeolocationService();
            //var location = await geolocationService.GetGeolocation();

            //Services.NotificationService notificationService = new Services.NotificationService((NotificationManager)GetSystemService(NotificationService));
            //notificationService.PushNotification(this, "test", $"Location: {location.Altitude}, {location.Latitude}, {location.Longitude}.\n\rAccuracy: {location.Accuracy}");

            Services.SmsService smsService = new Services.SmsService();
            smsService.SendSms("test", "+380682502081");
            //await SendSms("test message", "+380682502081");
        }

        //public async Task SendSms(string messageText, string recipient)
        //{
        //    if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.SendSms) == Permission.Granted)
        //    {
        //        SmsManager smsManager = SmsManager.Default;
        //        // Split text message content (SMS length limit)
        //        IList<string> divideContents = smsManager.DivideMessage(messageText);

        //        foreach (string text in divideContents)
        //        {
        //            smsManager.SendTextMessage(recipient, null, text, null, null);
        //        }
        //    }
        //    else
        //    {
        //        ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.SendSms }, 1);
        //    }
        //}
    }
}

