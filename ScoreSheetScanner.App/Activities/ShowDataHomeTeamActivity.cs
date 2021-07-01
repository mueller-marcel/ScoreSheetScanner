using System;
using System.Text.Json;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
using ScoreSheetScanner.Recognition.Model;
using Xamarin.Essentials;

namespace ScoreSheetScanner.App.Activities
{
    [Activity(Label = "@string/show_data_homeTeam")]
    public class ShowDataHomeTeamActivity : AppCompatActivity
    {
        #region Properties
        /// <summary>
        /// Holds the DataTransferObject of the ScoreSheet
        /// </summary>
        public ScoreSheetDTO ScoreSheetDTO { get; set; }

        /// <summary>
        /// Text field representing the first home player in D1
        /// </summary>
        public TextInputEditText TextInputEditText_HomePlayer1D1 { get; set; }

        /// <summary>
        /// Text field representing the second home player in D1
        /// </summary>
        public TextInputEditText TextInputEditText_HomePlayer2D1 { get; set; }

        /// <summary>
        /// Text field representing the first home player in D2
        /// </summary>
        public TextInputEditText TextInputEditText_HomePlayer1D2 { get; set; }

        /// <summary>
        /// Text field representing the second home player in D2
        /// </summary>
        public TextInputEditText TextInputEditText_HomePlayer2D2 { get; set; }

        /// <summary>
        /// Text field representing the first home player in D3
        /// </summary>
        public TextInputEditText TextInputEditText_HomePlayer1D3 { get; set; }

        /// <summary>
        /// Text field representing the second home player in D3
        /// </summary>
        public TextInputEditText TextInputEditText_HomePlayer2D3 { get; set; }

        /// <summary>
        /// Text field representing a home player in E1
        /// </summary>
        public TextInputEditText TextInputEditText_HomePlayerE1 { get; set; }

        /// <summary>
        /// Text field representing a home player in E2
        /// </summary>
        public TextInputEditText TextInputEditText_HomePlayerE2 { get; set; }

        /// <summary>
        /// Text field representing a home player in E3
        /// </summary>
        public TextInputEditText TextInputEditText_HomePlayerE3 { get; set; }

        /// <summary>
        /// Text field representing a home player in E4
        /// </summary>
        public TextInputEditText TextInputEditText_HomePlayerE4 { get; set; }

        /// <summary>
        /// Text field representing a home player in E5
        /// </summary>
        public TextInputEditText TextInputEditText_HomePlayerE5 { get; set; }

        /// <summary>
        /// Text field representing a home player in E6
        /// </summary>
        public TextInputEditText TextInputEditText_HomePlayerE6 { get; set; }

        /// <summary>
        /// Button to get to the overview
        /// </summary>
        public Button BtnNextToGuestTeam { get; set; }

        // Constants for the labels of the scoresheet
        private readonly string PlayerA1 = "Spieler A 1";
        private readonly string PlayerA2 = "Spieler A 2";
        private readonly string PlayerA3 = "Spieler A 3";
        private readonly string PlayerA4 = "Spieler A 4";
        private readonly string PlayerA5 = "Spieler A 5";
        private readonly string PlayerA6 = "Spieler A 6";
        private readonly string PlayerA7 = "Spieler A 7";
        private readonly string PlayerA8 = "Spieler A 8";
        private readonly string PlayerA9 = "Spieler A 9";
        private readonly string PlayerA10 = "Spieler A 10";
        private readonly string PlayerA11 = "Spieler A 11";
        private readonly string PlayerA12 = "Spieler A 12";
        private readonly string scoreSheetName = "scoreSheetDTO";
        #endregion

        /// <summary>
        /// Called when the Activity is started, performs some initializations 
        /// </summary>
        /// <param name="savedInstanceState">Bundle with data from the calling Activity <see cref="ShowDataGeneralActivity"/></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Get data from the Intent that invoked the activity
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_showDataHomeTeam);
            ScoreSheetDTO = JsonSerializer.Deserialize<ScoreSheetDTO>(Intent.GetStringExtra(scoreSheetName) ?? string.Empty);

            // Initialize UI-components
            //TODO: Exception Handling falls 1 Wert null ist
            try
            {
                TextInputEditText_HomePlayer1D1 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Homeplayer1D1);
                TextInputEditText_HomePlayer1D1.Text = ScoreSheetDTO.HomeTeamPlayers[PlayerA1];
                TextInputEditText_HomePlayer1D1.AfterTextChanged += TextInputEditText_AfterTextChanged;

                TextInputEditText_HomePlayer2D1 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Homeplayer2D1);
                TextInputEditText_HomePlayer2D1.Text = ScoreSheetDTO.HomeTeamPlayers[PlayerA2];
                TextInputEditText_HomePlayer2D1.AfterTextChanged += TextInputEditText_AfterTextChanged;

                TextInputEditText_HomePlayer1D2 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Homeplayer1D2);
                TextInputEditText_HomePlayer1D2.Text = ScoreSheetDTO.HomeTeamPlayers[PlayerA3];
                TextInputEditText_HomePlayer1D2.AfterTextChanged += TextInputEditText_AfterTextChanged;

                TextInputEditText_HomePlayer2D2 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Homeplayer2D2);
                TextInputEditText_HomePlayer2D2.Text = ScoreSheetDTO.HomeTeamPlayers[PlayerA4];
                TextInputEditText_HomePlayer2D2.AfterTextChanged += TextInputEditText_AfterTextChanged;

                TextInputEditText_HomePlayer1D3 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Homeplayer1D3);
                TextInputEditText_HomePlayer1D3.Text = ScoreSheetDTO.HomeTeamPlayers[PlayerA5];
                TextInputEditText_HomePlayer1D3.AfterTextChanged += TextInputEditText_AfterTextChanged;

                TextInputEditText_HomePlayer2D3 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Homeplayer2D3);
                TextInputEditText_HomePlayer2D3.Text = ScoreSheetDTO.HomeTeamPlayers[PlayerA6];
                TextInputEditText_HomePlayer2D3.AfterTextChanged += TextInputEditText_AfterTextChanged;

                TextInputEditText_HomePlayerE1 = FindViewById<TextInputEditText>(Resource.Id.textEdit_HomeplayerE1);
                TextInputEditText_HomePlayerE1.Text = ScoreSheetDTO.HomeTeamPlayers[PlayerA7];
                TextInputEditText_HomePlayerE1.AfterTextChanged += TextInputEditText_AfterTextChanged;

                TextInputEditText_HomePlayerE2 = FindViewById<TextInputEditText>(Resource.Id.textEdit_HomeplayerE2);
                TextInputEditText_HomePlayerE2.Text = ScoreSheetDTO.HomeTeamPlayers[PlayerA8];
                TextInputEditText_HomePlayerE2.AfterTextChanged += TextInputEditText_AfterTextChanged;

                TextInputEditText_HomePlayerE3 = FindViewById<TextInputEditText>(Resource.Id.textEdit_HomeplayerE3);
                TextInputEditText_HomePlayerE3.Text = ScoreSheetDTO.HomeTeamPlayers[PlayerA9];
                TextInputEditText_HomePlayerE3.AfterTextChanged += TextInputEditText_AfterTextChanged;

                TextInputEditText_HomePlayerE4 = FindViewById<TextInputEditText>(Resource.Id.textEdit_HomeplayerE4);
                TextInputEditText_HomePlayerE4.Text = ScoreSheetDTO.HomeTeamPlayers[PlayerA10];
                TextInputEditText_HomePlayerE4.AfterTextChanged += TextInputEditText_AfterTextChanged;

                TextInputEditText_HomePlayerE5 = FindViewById<TextInputEditText>(Resource.Id.textEdit_HomeplayerE5);
                TextInputEditText_HomePlayerE5.Text = ScoreSheetDTO.HomeTeamPlayers[PlayerA11];
                TextInputEditText_HomePlayerE5.AfterTextChanged += TextInputEditText_AfterTextChanged;

                TextInputEditText_HomePlayerE6 = FindViewById<TextInputEditText>(Resource.Id.textEdit_HomeplayerE6);
                TextInputEditText_HomePlayerE6.Text = ScoreSheetDTO.HomeTeamPlayers[PlayerA12];
                TextInputEditText_HomePlayerE6.AfterTextChanged += TextInputEditText_AfterTextChanged;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            BtnNextToGuestTeam = FindViewById<Button>(Resource.Id.btnToPlayerGuest);
            BtnNextToGuestTeam.Click += BtnNextToGuestTeam_Click;
        }
        #region EventHandlers
        /// <summary>
        /// Updates the scoresheet DTO when the text fields are updated
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">Arguments for the event</param>
        private void TextInputEditText_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            // Update the text fields with the data from the scoresheet dto
            TextInputEditText textInputEditText = (TextInputEditText)sender;
            if (textInputEditText == TextInputEditText_HomePlayer1D1) { ScoreSheetDTO.HomeTeamPlayers[PlayerA1] = TextInputEditText_HomePlayer1D1.Text; }
            else if (textInputEditText == TextInputEditText_HomePlayer2D1) { ScoreSheetDTO.HomeTeamPlayers[PlayerA2] = TextInputEditText_HomePlayer2D1.Text; }
            else if (textInputEditText == TextInputEditText_HomePlayer1D2) { ScoreSheetDTO.HomeTeamPlayers[PlayerA3] = TextInputEditText_HomePlayer1D2.Text; }
            else if (textInputEditText == TextInputEditText_HomePlayer2D2) { ScoreSheetDTO.HomeTeamPlayers[PlayerA4] = TextInputEditText_HomePlayer2D2.Text; }
            else if (textInputEditText == TextInputEditText_HomePlayer1D3) { ScoreSheetDTO.HomeTeamPlayers[PlayerA5] = TextInputEditText_HomePlayer1D3.Text; }
            else if (textInputEditText == TextInputEditText_HomePlayer2D3) { ScoreSheetDTO.HomeTeamPlayers[PlayerA6] = TextInputEditText_HomePlayer2D3.Text; }
            else if (textInputEditText == TextInputEditText_HomePlayerE1) { ScoreSheetDTO.HomeTeamPlayers[PlayerA7] = TextInputEditText_HomePlayerE1.Text; }
            else if (textInputEditText == TextInputEditText_HomePlayerE2) { ScoreSheetDTO.HomeTeamPlayers[PlayerA8] = TextInputEditText_HomePlayerE2.Text; }
            else if (textInputEditText == TextInputEditText_HomePlayerE3) { ScoreSheetDTO.HomeTeamPlayers[PlayerA9] = TextInputEditText_HomePlayerE3.Text; }
            else if (textInputEditText == TextInputEditText_HomePlayerE4) { ScoreSheetDTO.HomeTeamPlayers[PlayerA10] = TextInputEditText_HomePlayerE4.Text; }
            else if (textInputEditText == TextInputEditText_HomePlayerE5) { ScoreSheetDTO.HomeTeamPlayers[PlayerA11] = TextInputEditText_HomePlayerE5.Text; }
            else if (textInputEditText == TextInputEditText_HomePlayerE6) { ScoreSheetDTO.HomeTeamPlayers[PlayerA12] = TextInputEditText_HomePlayerE6.Text; }
        }

        /// <summary>
        /// Button that starts the activity <see cref="ShowDataGuestTeamActivity"/>
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">Arguments for the event</param>
        private void BtnNextToGuestTeam_Click(object sender, EventArgs e)
        {
            // Start new Activity
            Intent intent = new Intent(this, typeof(ShowDataGuestTeamActivity));
            intent.PutExtra(scoreSheetName, JsonSerializer.Serialize(ScoreSheetDTO));
            StartActivity(intent);
        }
        #endregion
    }
}