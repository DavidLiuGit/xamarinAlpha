using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace XamarinAlpha {
    [Activity(Label = "XamarinAlpha", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity {
        //int count = 1;

        /// <summary>
        /// The following method is called when the activitiy is initialized, and its contents are created
        /// </summary>
        /// <param name="bundle"></param>
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            // first change

            // deleted original content
            // code will go below:            
            Button convert = FindViewById<Button>(Resource.Id.buttonConvert);
            Button call = FindViewById<Button>(Resource.Id.buttonCall);

            // disable call button at the beginning
            call.Enabled = false;

            // add event listener for clicking of the "Convert!" button
            convert.Click += Convert_Click;


        }

        /// <summary>
        /// called when the "Convert!" button is clicked. This function will parse the input found in the text field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Convert_Click(object sender, EventArgs e) {
            string convertedString = "";                                        // initialize convertedString to an empty string

            // link the call button & text field
            Button call = FindViewById<Button>(Resource.Id.buttonCall);
            EditText numText = FindViewById<EditText>(Resource.Id.textNumber);

            convertedString = Core.PhonewordTranslator.ToNumber(numText.Text);  // covnert the string using the method provided

            if (String.IsNullOrWhiteSpace(convertedString)) {                   // if the conversion appears to be INVALID
                call.Text = "Call!";
                call.Enabled = false;
            } else {
                call.Text = "Call " + convertedString +"!";
                call.Enabled = true;
            }

            //throw new NotImplementedException();
        }
    }
}

