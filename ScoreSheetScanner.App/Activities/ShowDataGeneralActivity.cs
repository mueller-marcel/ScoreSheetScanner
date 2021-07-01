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
    [Activity(Label = "@string/show_data_general")]
    public class ShowDataGeneralActivity : AppCompatActivity
    {
        #region Properties
        /// <summary>
        /// Holds the DataTransferObject of the ScoreSheet
        /// </summary>
        public ScoreSheetDTO scoreSheetDTO { get; set; }

        /// <summary>
        /// Holds the TextInputField of the Home Team
        /// </summary>
        public TextInputEditText TextInputEditText_HomeTeam { get; set; }

        /// <summary>
        /// Holds the TextInputField of the Guest Team
        /// </summary>
        public TextInputEditText TextInputEditText_GuestTeam { get; set; }

        /// <summary>
        /// Holds the TextInputField of the Game-Place
        /// </summary>
        public TextInputEditText TextInputEditText_GamePlace { get; set; }

        /// <summary>
        /// Holds the TextInputField of the Game-Class
        /// </summary>
        public TextInputEditText TextInputEditText_GameClass { get; set; }

        /// <summary>
        /// Holds the TextInputField of the Game-Start-Time
        /// </summary>
        public TextInputEditText TextInputEditText_GameStartTime { get; set; }

        /// <summary>
        /// Holds the TextInputField of the Game-Time
        /// </summary>
        public TextInputEditText TextInputEditText_GameDate { get; set; }

        /// <summary>
        /// Holds the TextInputField of the Game-Group
        /// </summary>
        public TextInputEditText TextInputEditText_GameGroup { get; set; }

        /// <summary>
        /// Holds the TextInputField of the Game-End-Time
        /// </summary>
        public TextInputEditText TextInputEditText_GameEndTime { get; set; }

        /// <summary>
        /// Holds the TextInputField of the Game-Winner
        /// </summary>
        public TextInputEditText TextInputEditText_GameWinner { get; set; }

        /// <summary>
        /// Holds the Button to go to the next Activity
        /// </summary>
        public Button BtnNextToHomeTeam { get; set; }

        /// <summary>
        /// Name of the scoressheet dto
        /// </summary>
        private readonly string scoreSheetName = "scoreSheetDTO";
        #endregion

        /// <summary>
        /// Life cycle method that gets called when the activity is created
        /// </summary>
        /// <param name="savedInstanceState">Data from the <see cref="Intent"/> that invoked the activity</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Get data from the Intent that invoked the activity
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_showDataGeneral);
            scoreSheetDTO = JsonSerializer.Deserialize<ScoreSheetDTO>(Intent.GetStringExtra(scoreSheetName) ?? string.Empty);

            // Initialize UI-components, set content from the Scoresheet DTO and add event handlers for the update of the text fields
            TextInputEditText_HomeTeam = FindViewById<TextInputEditText>(Resource.Id.textEdit_homeTeam);
            TextInputEditText_HomeTeam.Text = scoreSheetDTO.HomeTeam;
            TextInputEditText_HomeTeam.AfterTextChanged += TextInputEditText_AfterTextChanged;

            TextInputEditText_GuestTeam = FindViewById<TextInputEditText>(Resource.Id.textEdit_guestTeam);
            TextInputEditText_GuestTeam.Text = scoreSheetDTO.GuestTeam;
            TextInputEditText_GuestTeam.AfterTextChanged += TextInputEditText_AfterTextChanged;

            TextInputEditText_GamePlace = FindViewById<TextInputEditText>(Resource.Id.textEdit_gamePlace);
            TextInputEditText_GamePlace.Text = scoreSheetDTO.GamePlace;
            TextInputEditText_GamePlace.AfterTextChanged += TextInputEditText_AfterTextChanged;

            TextInputEditText_GameClass = FindViewById<TextInputEditText>(Resource.Id.textEdit_gameClass);
            TextInputEditText_GameClass.Text = scoreSheetDTO.GameClass;
            TextInputEditText_GameClass.AfterTextChanged += TextInputEditText_AfterTextChanged;

            TextInputEditText_GameStartTime = FindViewById<TextInputEditText>(Resource.Id.textEdit_gameStartTime);
            TextInputEditText_GameStartTime.Text = scoreSheetDTO.GameStartTime;
            TextInputEditText_GameStartTime.AfterTextChanged += TextInputEditText_AfterTextChanged;

            TextInputEditText_GameDate = FindViewById<TextInputEditText>(Resource.Id.textEdit_gameDate);
            TextInputEditText_GameDate.Text = scoreSheetDTO.Date;
            TextInputEditText_GameDate.AfterTextChanged += TextInputEditText_AfterTextChanged;

            TextInputEditText_GameGroup = FindViewById<TextInputEditText>(Resource.Id.textEdit_gameGroup);
            TextInputEditText_GameGroup.Text = scoreSheetDTO.Group;
            TextInputEditText_GameGroup.AfterTextChanged += TextInputEditText_AfterTextChanged;

            TextInputEditText_GameEndTime = FindViewById<TextInputEditText>(Resource.Id.textEdit_gameEndTime);
            TextInputEditText_GameEndTime.Text = scoreSheetDTO.GameEndTime;
            TextInputEditText_GameEndTime.AfterTextChanged += TextInputEditText_AfterTextChanged;

            TextInputEditText_GameWinner = FindViewById<TextInputEditText>(Resource.Id.textEdit_gameWinner);
            TextInputEditText_GameWinner.Text = scoreSheetDTO.Winner;
            TextInputEditText_GameWinner.AfterTextChanged += TextInputEditText_AfterTextChanged;

            BtnNextToHomeTeam = FindViewById<Button>(Resource.Id.btnToPlayerHome);
            BtnNextToHomeTeam.Click += BtnNextToHomeTeam_Click;
        }

        #region EventHandler
        /// <summary>
        /// Updates the scoresheet DTO when the text fields are updated
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">Arguments for the event</param>
        private void TextInputEditText_AfterTextChanged(object sender, AfterTextChangedEventArgs e)
        {
            // Update every textinput field with the data from the dto
            TextInputEditText textInputEditText = (TextInputEditText)sender;
            if (textInputEditText == TextInputEditText_HomeTeam) { scoreSheetDTO.HomeTeam = TextInputEditText_HomeTeam.Text; }
            else if (textInputEditText == TextInputEditText_GuestTeam) { scoreSheetDTO.GuestTeam = TextInputEditText_GuestTeam.Text; }
            else if (textInputEditText == TextInputEditText_GamePlace) { scoreSheetDTO.GamePlace = TextInputEditText_GamePlace.Text; }
            else if (textInputEditText == TextInputEditText_GameClass) { scoreSheetDTO.GameClass = TextInputEditText_GameClass.Text; }
            else if (textInputEditText == TextInputEditText_GameStartTime) { scoreSheetDTO.GameStartTime = TextInputEditText_GameStartTime.Text; }
            else if (textInputEditText == TextInputEditText_GameDate) { scoreSheetDTO.Date = TextInputEditText_GameDate.Text; }
            else if (textInputEditText == TextInputEditText_GameGroup) { scoreSheetDTO.Group = TextInputEditText_GameGroup.Text; }
            else if (textInputEditText == TextInputEditText_GameEndTime) { scoreSheetDTO.GameEndTime = TextInputEditText_GameEndTime.Text; }
            else if (textInputEditText == TextInputEditText_GameWinner) { scoreSheetDTO.Winner = TextInputEditText_GameWinner.Text; }
        }

        /// <summary>
        /// Event handler for the <see cref="View.Click"/> event of the <see cref="BtnNextToHomeTeam"/> button
        /// </summary>
        /// <param name="sender">The instance firing the event</param>
        /// <param name="e">The arguments, which come with the event</param>
        private void BtnNextToHomeTeam_Click(object sender, System.EventArgs e)
        {
            //Start New Activity
            Intent intent = new Intent(this, typeof(ShowDataHomeTeamActivity));
            intent.PutExtra(scoreSheetName, JsonSerializer.Serialize(scoreSheetDTO));
            StartActivity(intent);

        }
        #endregion
    }
}