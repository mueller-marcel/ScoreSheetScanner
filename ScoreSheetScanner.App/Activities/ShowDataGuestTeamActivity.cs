using System;
using System.Text.Json;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Text;
using Android.Widget;
using ScoreSheetScanner.Recognition.Model;
using Xamarin.Essentials;

namespace ScoreSheetScanner.App.Activities
{
    [Activity(Label = "@string/show_data_guestTeam")]
    public class ShowDataGuestTeamActivity : AppCompatActivity
    {
        #region Properties

        /// <summary>
        /// Holds the DTO with the retrieved data from the scoresheet dto
        /// </summary>
        public ScoreSheetDTO ScoreSheetDTO { get; set; }

        /// <summary>
        /// Text field representing the first guest player in D1
        /// </summary>
        public TextInputEditText TextInputEditText_GuestPlayer1D1 { get; set; }

        /// <summary>
        /// Text field representing the second guest player in D1
        /// </summary>
        public TextInputEditText TextInputEditText_GuestPlayer2D1 { get; set; }

        /// <summary>
        /// Text field representing the first guest player in D2
        /// </summary>
        public TextInputEditText TextInputEditText_GuestPlayer1D2 { get; set; }

        /// <summary>
        /// Text field representing the second guest player in D2
        /// </summary>
        public TextInputEditText TextInputEditText_GuestPlayer2D2 { get; set; }

        /// <summary>
        /// Text field representing the first guest player in D3
        /// </summary>
        public TextInputEditText TextInputEditText_GuestPlayer1D3 { get; set; }

        /// <summary>
        /// Text field representing the second guest player in D3
        /// </summary>
        public TextInputEditText TextInputEditText_GuestPlayer2D3 { get; set; }

        /// <summary>
        /// Text field representing a guest player in E1
        /// </summary>
        public TextInputEditText TextInputEditText_GuestPlayerE1 { get; set; }

        /// <summary>
        /// Text field representing a guest player in E2
        /// </summary>
        public TextInputEditText TextInputEditText_GuestPlayerE2 { get; set; }

        /// <summary>
        /// Text field representing a guest player in E3
        /// </summary>
        public TextInputEditText TextInputEditText_GuestPlayerE3 { get; set; }

        /// <summary>
        /// Text field representing a guest player in E4
        /// </summary>
        public TextInputEditText TextInputEditText_GuestPlayerE4 { get; set; }

        /// <summary>
        /// Text field representing a guest player in E5
        /// </summary>
        public TextInputEditText TextInputEditText_GuestPlayerE5 { get; set; }

        /// <summary>
        /// Text field representing a guest player in E6
        /// </summary>
        public TextInputEditText TextInputEditText_GuestPlayerE6 { get; set; }

        /// <summary>
        /// Button to get to the overview
        /// </summary>
        public Button btnToOverview { get; set; }

        // Constants for the labels of the scoresheet
        private readonly string PlayerB1 = "Spieler B 1";
        private readonly string PlayerB2 = "Spieler B 2";
        private readonly string PlayerB3 = "Spieler B 3";
        private readonly string PlayerB4 = "Spieler B 4";
        private readonly string PlayerB5 = "Spieler B 5";
        private readonly string PlayerB6 = "Spieler B 6";
        private readonly string PlayerB7 = "Spieler B 7";
        private readonly string PlayerB8 = "Spieler B 8";
        private readonly string PlayerB9 = "Spieler B 9";
        private readonly string PlayerB10 = "Spieler B 10";
        private readonly string PlayerB11 = "Spieler B 11";
        private readonly string PlayerB12 = "Spieler B 12";
        private readonly string scoreSheetName = "scoreSheetDTO";
        #endregion

        /// <summary>
        /// Called when the Activity is started, performs some initializations 
        /// </summary>
        /// <param name="savedInstanceState">Bundle with data from the calling Activity <see cref="ShowDataHomeTeamActivity"/></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Get data from the Intent that invoked the activity
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_showDataGuestTeam);
            ScoreSheetDTO = JsonSerializer.Deserialize<ScoreSheetDTO>(Intent.GetStringExtra(scoreSheetName) ?? string.Empty);

            // Initialize UI-components          
            TextInputEditText_GuestPlayer1D1 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Guestplayer1D1);
            TextInputEditText_GuestPlayer1D1.Text = ScoreSheetDTO.GuestTeamPlayers[PlayerB1];
            TextInputEditText_GuestPlayer1D1.AfterTextChanged += TextInputEditText_Guest_AfterTextChanged;

            //Try Catch Block weil das Label öfters Mal null ist
            //TODO: Exception-Handling für alle Komponenten falls mal nichts ausgelesen wird
            try
            {
                TextInputEditText_GuestPlayer2D1 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Guestplayer2D1);
                TextInputEditText_GuestPlayer2D1.Text = ScoreSheetDTO.GuestTeamPlayers[PlayerB2];
                TextInputEditText_GuestPlayer2D1.AfterTextChanged += TextInputEditText_Guest_AfterTextChanged;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            TextInputEditText_GuestPlayer1D2 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Guestplayer1D2);
            TextInputEditText_GuestPlayer1D2.Text = ScoreSheetDTO.GuestTeamPlayers[PlayerB3];
            TextInputEditText_GuestPlayer1D2.AfterTextChanged += TextInputEditText_Guest_AfterTextChanged;

            TextInputEditText_GuestPlayer2D2 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Guestplayer2D2);
            TextInputEditText_GuestPlayer2D2.Text = ScoreSheetDTO.GuestTeamPlayers[PlayerB4];
            TextInputEditText_GuestPlayer2D2.AfterTextChanged += TextInputEditText_Guest_AfterTextChanged;

            TextInputEditText_GuestPlayer1D3 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Guestplayer1D3);
            TextInputEditText_GuestPlayer1D3.Text = ScoreSheetDTO.GuestTeamPlayers[PlayerB5];
            TextInputEditText_GuestPlayer1D3.AfterTextChanged += TextInputEditText_Guest_AfterTextChanged;

            TextInputEditText_GuestPlayer2D3 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Guestplayer2D3);
            TextInputEditText_GuestPlayer2D3.Text = ScoreSheetDTO.GuestTeamPlayers[PlayerB6];
            TextInputEditText_GuestPlayer2D3.AfterTextChanged += TextInputEditText_Guest_AfterTextChanged;

            TextInputEditText_GuestPlayerE1 = FindViewById<TextInputEditText>(Resource.Id.textEdit_GuestplayerE1);
            TextInputEditText_GuestPlayerE1.Text = ScoreSheetDTO.GuestTeamPlayers[PlayerB7];
            TextInputEditText_GuestPlayerE1.AfterTextChanged += TextInputEditText_Guest_AfterTextChanged;

            TextInputEditText_GuestPlayerE2 = FindViewById<TextInputEditText>(Resource.Id.textEdit_GuestplayerE2);
            TextInputEditText_GuestPlayerE2.Text = ScoreSheetDTO.GuestTeamPlayers[PlayerB8];
            TextInputEditText_GuestPlayerE2.AfterTextChanged += TextInputEditText_Guest_AfterTextChanged;

            TextInputEditText_GuestPlayerE3 = FindViewById<TextInputEditText>(Resource.Id.textEdit_GuestplayerE3);
            TextInputEditText_GuestPlayerE3.Text = ScoreSheetDTO.GuestTeamPlayers[PlayerB9];
            TextInputEditText_GuestPlayerE3.AfterTextChanged += TextInputEditText_Guest_AfterTextChanged;

            TextInputEditText_GuestPlayerE4 = FindViewById<TextInputEditText>(Resource.Id.textEdit_GuestplayerE4);
            TextInputEditText_GuestPlayerE4.Text = ScoreSheetDTO.GuestTeamPlayers[PlayerB10];
            TextInputEditText_GuestPlayerE4.AfterTextChanged += TextInputEditText_Guest_AfterTextChanged;

            TextInputEditText_GuestPlayerE5 = FindViewById<TextInputEditText>(Resource.Id.textEdit_GuestplayerE5);
            TextInputEditText_GuestPlayerE5.Text = ScoreSheetDTO.GuestTeamPlayers[PlayerB11];
            TextInputEditText_GuestPlayerE5.AfterTextChanged += TextInputEditText_Guest_AfterTextChanged;

            TextInputEditText_GuestPlayerE6 = FindViewById<TextInputEditText>(Resource.Id.textEdit_GuestplayerE6);
            TextInputEditText_GuestPlayerE6.Text = ScoreSheetDTO.GuestTeamPlayers[PlayerB12];
            TextInputEditText_GuestPlayerE6.AfterTextChanged += TextInputEditText_Guest_AfterTextChanged;

            btnToOverview = FindViewById<Button>(Resource.Id.btnToOverview);
            btnToOverview.Click += BtnToOverview_Click;
        }

        private void BtnToOverview_Click(object sender, EventArgs e)
        {
            // Start new Activity
            Intent intent = new Intent(this, typeof(ShowDataResultActivity));
            intent.PutExtra(scoreSheetName, JsonSerializer.Serialize(ScoreSheetDTO));
            StartActivity(intent);
        }

        /// <summary>
        /// Updates the scoresheet DTO when the text fields are updated
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">Arguments for the event</param>
        private void TextInputEditText_Guest_AfterTextChanged(object sender, AfterTextChangedEventArgs e)
        {
            // Update every textinput field with the data from the dto
            TextInputEditText textInputEditText = (TextInputEditText)sender;
            if (textInputEditText == TextInputEditText_GuestPlayer1D1) { ScoreSheetDTO.GuestTeamPlayers[PlayerB1] = TextInputEditText_GuestPlayer1D1.Text; }
            else if (textInputEditText == TextInputEditText_GuestPlayer2D1) { ScoreSheetDTO.GuestTeamPlayers[PlayerB2] = TextInputEditText_GuestPlayer2D1.Text; }
            else if (textInputEditText == TextInputEditText_GuestPlayer1D2) { ScoreSheetDTO.GuestTeamPlayers[PlayerB3] = TextInputEditText_GuestPlayer1D2.Text; }
            else if (textInputEditText == TextInputEditText_GuestPlayer2D2) { ScoreSheetDTO.GuestTeamPlayers[PlayerB4] = TextInputEditText_GuestPlayer2D2.Text; }
            else if (textInputEditText == TextInputEditText_GuestPlayer1D3) { ScoreSheetDTO.GuestTeamPlayers[PlayerB5] = TextInputEditText_GuestPlayer1D3.Text; }
            else if (textInputEditText == TextInputEditText_GuestPlayer2D3) { ScoreSheetDTO.GuestTeamPlayers[PlayerB6] = TextInputEditText_GuestPlayer2D3.Text; }
            else if (textInputEditText == TextInputEditText_GuestPlayerE1) { ScoreSheetDTO.GuestTeamPlayers[PlayerB7] = TextInputEditText_GuestPlayerE1.Text; }
            else if (textInputEditText == TextInputEditText_GuestPlayerE2) { ScoreSheetDTO.GuestTeamPlayers[PlayerB8] = TextInputEditText_GuestPlayerE2.Text; }
            else if (textInputEditText == TextInputEditText_GuestPlayerE3) { ScoreSheetDTO.GuestTeamPlayers[PlayerB9] = TextInputEditText_GuestPlayerE3.Text; }
            else if (textInputEditText == TextInputEditText_GuestPlayerE4) { ScoreSheetDTO.GuestTeamPlayers[PlayerB10] = TextInputEditText_GuestPlayerE4.Text; }
            else if (textInputEditText == TextInputEditText_GuestPlayerE5) { ScoreSheetDTO.GuestTeamPlayers[PlayerB11] = TextInputEditText_GuestPlayerE5.Text; }
            else if (textInputEditText == TextInputEditText_GuestPlayerE6) { ScoreSheetDTO.GuestTeamPlayers[PlayerB12] = TextInputEditText_GuestPlayerE6.Text; }
        }
    }
}