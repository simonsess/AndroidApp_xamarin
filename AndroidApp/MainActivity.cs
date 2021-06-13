using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using System.Net.Http;

namespace AndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Button sendButton = FindViewById<Button>(Resource.Id.button1);
                sendButton.Click -= SendOnClick;

                Button clearButton = FindViewById<Button>(Resource.Id.button2);
                clearButton.Click -= ClearOnClick;
            }
            base.Dispose(disposing);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            TextView output = FindViewById<TextView>(Resource.Id.text);
            output.Text = "Hi there!";

            Button sendButton = FindViewById<Button>(Resource.Id.button1);
            sendButton.Text = "SEND";
            sendButton.Click += SendOnClick;

            Button clearButton = FindViewById<Button>(Resource.Id.button2);
            clearButton.Text = "CLEAR";
            clearButton.Click += ClearOnClick;
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
        private void ClearOnClick(object sender, EventArgs args)
        {
            TextView output = FindViewById<TextView>(Resource.Id.text);
            output.Text = string.Empty;
        }
        private void SendOnClick(object sender, EventArgs args)
        {
            EditText input = FindViewById<EditText>(Resource.Id.editText1);
            GetResponse(input.Text);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private async void GetResponse(string url)
        {
            Uri uri = new Uri(string.Format(url, string.Empty));
            var httpClient = new HttpClient();
            
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                TextView output = FindViewById<TextView>(Resource.Id.text);
                output.Text = content;
            }
        }
    }
}

