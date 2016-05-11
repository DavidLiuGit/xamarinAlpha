using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;

namespace XamarinAlpha {
    [Activity(Label = "Xamarin Alpha", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity {
        string convertedString = "";                                        // initialize convertedString to an empty string

        /// <summary>
        /// The following method is called when the activitiy is initialized, and its contents are created
        /// </summary>
        /// <param name="bundle"></param>
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // deleted original content
            // code will go below:            
            Button convert = FindViewById<Button>(Resource.Id.buttonConvert);
            Button call = FindViewById<Button>(Resource.Id.buttonCall);

            // disable call button at the beginning
            call.Enabled = false;

            // add event listener for "Convert!" button
            convert.Click += Convert_Click;

            // add event listener for "Call ...!" button
            call.Click += Call_Click;
        }

        private void Call_Click(object sender, EventArgs e) {
            // create an alert dialog to ask the user if they want to make this call
            var callDiag = new AlertDialog.Builder(this);
            callDiag.SetMessage("Call " + convertedString + "?");

            // set the action/intent that is executed if the user clicks the "OK/Call" button
            callDiag.SetNeutralButton("Call", delegate {
                // create intent to dial phone
                var callIntent = new Intent(Intent.ActionCall);
                callIntent.SetData(Android.Net.Uri.Parse("tel:" + convertedString));        // give the intent it's target to dial
                StartActivity(callIntent);                                      // execute call intent
            });

            // cancel button - delegate no action
            callDiag.SetNegativeButton("Cancel", delegate { });

            // show the alert dialog
            callDiag.Show();
        }

        /// <summary>
        /// called when the "Convert!" button is clicked. This function will parse the input found in the text field
        /// </summary>
        private void Convert_Click(object sender, EventArgs e) {
            convertedString = "";

            // link the call button & text field
            Button call = FindViewById<Button>(Resource.Id.buttonCall);
            EditText numText = FindViewById<EditText>(Resource.Id.textNumber);

            convertedString = Core.PhonewordTranslator.ToNumber(numText.Text);  // covnert the string using the method provided

            if (String.IsNullOrWhiteSpace(convertedString)) {                   // if the conversion appears to be INVALID
                call.Text = "Call!";
                call.Enabled = false;
            } else {                                                            // if the conversion appears to be valid, then append the converted string to the call button, and allow it to be used
                call.Text = "Call " + convertedString + "!";
                call.Enabled = true;
            }
        }
    }
}

