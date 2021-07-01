using System;
using System.Text.Json;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Plugin.Media;
using Plugin.Media.Abstractions;
using ScoreSheetScanner.App.Activities;
using ScoreSheetScanner.Recognition.Model;
using ScoreSheetScanner.Recognition.Services;
using Xamarin.Essentials;

namespace ScoreSheetScanner.App
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        #region Properties
        /// <summary>
        /// Holds the picture of the document
        /// </summary>
        public ImageView ImageView { get; set; }

        /// <summary>
        /// Holds the functionality for the button to take a picture
        /// </summary>
        public Button TakePicture { get; set; }

        /// <summary>
        /// Holds the functionality for the button to pick a picture
        /// </summary>
        public Button PickPhoto { get; set; }

        /// <summary>
        /// Holds the functionality for the button to show the data
        /// </summary>
        public Button ShowRetrievedData { get; set; }

        /// <summary>
        /// <see cref="TextView"/> to show the welcome message on the startup screen
        /// </summary>
        public TextView WelcomeScreen { get; set; }

        /// <summary>
        /// <see cref="Bitmap"/> holds the picture from the gallery or the camera
        /// </summary>
        public Bitmap ScoreSheet { get; set; }

        /// <summary>
        /// <see cref="ProgressBar"/> is visible after clicking on the ShowRetrievedData-Button
        /// </summary>
        public ProgressBar ProgressBar { get; set; }

        private readonly string scoreSheetName = "scoreSheetDTO";

        #endregion

        #region Life-Cycle-Methods 
        /// <summary>
        /// Life cycle method that gets called when the activity is created
        /// </summary>
        /// <param name="savedInstanceState">Data from the <see cref="Intent"/> that invoked the activity</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Get data from the Intent that invoked the activity
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // Initialize UI-components
            ImageView = FindViewById<ImageView>(Resource.Id.imageViewShowScoreSheet);
            TakePicture = FindViewById<Button>(Resource.Id.btnTakePicture);
            PickPhoto = FindViewById<Button>(Resource.Id.btnPickFromGallery);
            ShowRetrievedData = FindViewById<Button>(Resource.Id.btnToShowData);
            WelcomeScreen = FindViewById<TextView>(Resource.Id.textViewWelcome);
            ProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);

            // Initialize Button handlers
            TakePicture.Click += TakePicture_ClickHandler;
            PickPhoto.Click += PickPhoto_ClickHandler;
            ShowRetrievedData.Click += ShowRetrievedData_ClickHandlerAsync;
        }

        /// <summary>
        /// Forwards the request for permission to the base class
        /// </summary>
        /// <param name="requestCode">The request code of the permission request</param>
        /// <param name="permissions">The name of the permission</param>
        /// <param name="grantResults">The granted permissions</param>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        #endregion

        #region Button Handlers
        /// <summary>
        /// Event handler for the <see cref="View.Click"/> event of the <see cref="TakePicture"/> button
        /// </summary>
        /// <param name="sender">The instance firing the event</param>
        /// <param name="e">The arguments, which come with the event</param>
        private async void TakePicture_ClickHandler(object sender, EventArgs e)
        {
            // Initialize main camera
            await CrossMedia.Current.Initialize();

            // Take Photo and return it
            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Small,
                Name = "scoreSheet.jpg",
                Directory = "sample"
            });
            if (file != null)
            {
                try
                {
                    ScoreSheet = BitmapFactory.DecodeFile(file.Path);
                    if (ScoreSheet != null)
                    {
                        ImageView.SetImageBitmap(ScoreSheet);
                    }
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Ein unerwarteter Fehler ist aufgetreten", ToastLength.Long).Show();
                    return;
                }

                // Change UI
                ShowRetrievedData.Enabled = true;
                ShowRetrievedData.SetTextColor(Color.White);
                WelcomeScreen.Visibility = ViewStates.Invisible;
                ImageView.Visibility = ViewStates.Visible;
            }
        }

        /// <summary>
        /// Event handler for the <see cref="View.Click"/> event of the <see cref="PickPhoto"/> button
        /// </summary>
        /// <param name="sender">The instance firing the event</param>
        /// <param name="e">The arguments, which come with the event</param>
        private async void PickPhoto_ClickHandler(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Large
            });

            if (file != null)
            {
                // Change UI
                ShowRetrievedData.Enabled = true;
                ShowRetrievedData.SetTextColor(Color.White);
                WelcomeScreen.Visibility = ViewStates.Invisible;
                ImageView.Visibility = ViewStates.Visible;

                try
                {
                    // Decode file as bitmap and pass it to the ImageView
                    ScoreSheet = BitmapFactory.DecodeFile(file.Path);
                    if (ScoreSheet != null)
                    {
                        ImageView.SetImageBitmap(ScoreSheet);
                    }
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Es ist ein unerwarteter Fehler aufgetreten.", ToastLength.Long).Show();
                    return;
                }
            }
        }

        /// <summary>
        /// Event handler for the <see cref="View.Click"/> event of the <see cref="ShowRetrievedData"/> button
        /// </summary>
        /// <param name="sender">The instance firing the event</param>
        /// <param name="e">The arguments, which come with the event</param>
        private async void ShowRetrievedData_ClickHandlerAsync(object sender, EventArgs e)
        {
            // Change UI
            ImageView.Visibility = ViewStates.Invisible;
            ProgressBar.Visibility = ViewStates.Visible;
            ShowRetrievedData.Enabled = false;
            ShowRetrievedData.SetTextColor(Color.LightGray);
            PickPhoto.Enabled = false;
            TakePicture.Enabled = false;

            try
            {
                // Get data from ScoreSheet-Image
                OCRCloudCommunicator ocr = new OCRCloudCommunicator();
                ScoreSheetDTO scoreSheetDTO = await Task.Run(() => ocr.RecognizeFontAsync(ScoreSheet));

                // Start new Activity
                Intent intent = new Intent(this, typeof(ChooseGameActivity));
                intent.PutExtra(scoreSheetName, JsonSerializer.Serialize(scoreSheetDTO));
                StartActivity(intent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Toast.MakeText(Application.Context, "Der Spielberichtsbogen konnte nicht ausgelesen werden", ToastLength.Long).Show();
            }
            finally
            {
                // Reset UI of Main activity when finished
                ProgressBar.Visibility = ViewStates.Invisible;
                WelcomeScreen.Visibility = ViewStates.Visible;
                PickPhoto.Enabled = true;
                TakePicture.Enabled = true;
            }
        }
        #endregion
    }
}

