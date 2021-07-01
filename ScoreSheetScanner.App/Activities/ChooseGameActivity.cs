using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;
using ScoreSheetScanner.App.Helper;
using ScoreSheetScanner.Cloud;
using ScoreSheetScanner.Cloud.Helper;
using ScoreSheetScanner.Recognition.Model;
using Xamarin.Essentials;
using static Android.App.DatePickerDialog;

namespace ScoreSheetScanner.App.Activities
{
    [Activity(Label = "@string/choose_game")]
    public class ChooseGameActivity : AppCompatActivity, IOnDateSetListener
    {
        public ScoreSheetDTO ScoreSheetDTO { get; set; }
        public Button Button_DateFrom { get; set; }
        public Button Button_DateTo { get; set; }
        public Button Button_Search { get; set; }
        public TextInputEditText TextInputEditText_DateFrom { get; set; }
        public TextInputEditText TextInputEditText_DateTo { get; set; }
        public TextInputEditText TextInputEditText_NumberOfGame { get; set; }
        public ListView ListView_Games { get; set; }
        public ProgressBar ProgressBar { get; set; }

        private readonly string scoreSheetName = "scoreSheetDTO";
        private const int DATE_DIALOG_FROM = 1;
        private const int DATE_DIALOG_TO = 2;
        private bool dateFrom, dateTo = false;
        private int yearFrom, monthFrom, dayFrom, yearTo, monthTo, dayTo;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_chooseGame);
            ScoreSheetDTO = JsonSerializer.Deserialize<ScoreSheetDTO>(Intent.GetStringExtra(scoreSheetName) ?? string.Empty);

            // Initialize UI-Components 
            Button_DateFrom = FindViewById<Button>(Resource.Id.button_choose_date_from);
            Button_DateTo = FindViewById<Button>(Resource.Id.button_choose_date_to);
            Button_Search = FindViewById<Button>(Resource.Id.button_search);

            TextInputEditText_DateFrom = FindViewById<TextInputEditText>(Resource.Id.textEdit_dateFrom);
            TextInputEditText_DateTo = FindViewById<TextInputEditText>(Resource.Id.textEdit_dateTo);
            TextInputEditText_NumberOfGame = FindViewById<TextInputEditText>(Resource.Id.textEdit_numberOFHomeTeam);

            ListView_Games = FindViewById<ListView>(Resource.Id.listView);

            ProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBarChooser);

            //Add Listeners to UI Components
            Button_DateFrom.Click += Button_DateFrom_Click;
            Button_DateTo.Click += Button_DateTo_Click;
            Button_Search.Click += Button_Search_Click;
        }

        /// <summary>
        /// Validates and complements the from and to date
        /// </summary>
        /// <returns>true if the dates could be filled up correctly or are entered correctly</returns>
        private bool ValidateTimeSpan()
        {
            // Check if both dates are empty
            if (string.IsNullOrEmpty(TextInputEditText_DateFrom.Text) && string.IsNullOrEmpty(TextInputEditText_DateTo.Text))
            {
                // Fill up from with today and add one week for the to date
                DateTime time = DateTime.Now;
                DateTime oneWeekLater = time.AddDays(7.0);
                yearFrom = time.Year;
                monthFrom = time.Month;
                dayFrom = time.Day;
                yearTo = oneWeekLater.Year;
                monthTo = oneWeekLater.Month;
                dayTo = oneWeekLater.Day;
                return true;
            }
            // Check if from date is unset and to date is set 
            else if (string.IsNullOrEmpty(TextInputEditText_DateFrom.Text))
            {
                //monthTo = monthTo + 1;
                DateTime toDate = new DateTime(yearTo, monthTo, dayTo);
                DateTime fromDate = toDate.Subtract(TimeSpan.FromDays(7.0));
                yearFrom = fromDate.Year;
                monthFrom = fromDate.Month;
                dayFrom = fromDate.Day;
                return true;
            }
            // Check if to date is unset and from date is set
            else if (string.IsNullOrEmpty(TextInputEditText_DateTo.Text))
            {
                //monthFrom = monthFrom + 1;
                DateTime fromDate = new DateTime(yearFrom, monthFrom, dayFrom);
                DateTime toDate = fromDate.AddDays(7.0);
                yearTo = toDate.Year;
                monthTo = toDate.Month;
                dayTo = toDate.Day;
                return true;
            }
            else
            {
                // Parse both dates
                //monthFrom = monthFrom + 1;
                //monthTo = monthTo + 1;
                DateTime fromDate = new DateTime(yearFrom, monthFrom, dayFrom);
                DateTime toDate = new DateTime(yearTo, monthTo, dayTo);

                // Check if the from date is earlier
                if (DateTime.Compare(fromDate, toDate) >= 0)
                {
                    return false;
                }
                return true;
            }
        }

        private async void Button_Search_Click(object sender, EventArgs e)
        {
            // Check if the club number is given
            if (string.IsNullOrEmpty(TextInputEditText_NumberOfGame.Text))
            {
                Toast.MakeText(this, "Bitte tragen Sie die Vereins-Nr. in das entsprechnde Feld ein.", ToastLength.Short).Show();
                return;
            }

            // Check the entered dates
            if (!ValidateTimeSpan())
            {
                Toast.MakeText(Application.Context, "Anfangsdatum muss vor dem Enddatum liegen", ToastLength.Long).Show();
                return;
            }

            // Set progress bar on visible
            ProgressBar.Visibility = ViewStates.Visible;

            Token accessToken;
            try
            {
                // Get access token
                CloudAuthenticator cloudAuthenticator = new CloudAuthenticator();
                accessToken = await Task.Run(() => cloudAuthenticator.GetToken(Authentication.Federation));
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Bei der Authentifizierung bei der NuScore-Schnittstelle ist ein Fehler aufgetreten.", ToastLength.Long).Show();
                ProgressBar.Visibility = ViewStates.Invisible;
                return;
            }

            MeetingLoader meetingLoader = new MeetingLoader();
            string matchesJson;
            try
            {
                // Get Meetings
                matchesJson = await meetingLoader.GetMeetingsAsync(accessToken, TextInputEditText_NumberOfGame.Text, $"{yearFrom}-{monthFrom}-{dayFrom}", $"{yearTo}-{monthTo}-{dayTo}");
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Bei der Abfrage der Paarungen ist ein Fehler aufgetreten.", ToastLength.Long).Show();
                ProgressBar.Visibility = ViewStates.Invisible;
                return;
            }

            // Get List of MeetingItems from matches
            List<MeetingItem> gameList = ReadOutInformationFromMeetings(matchesJson);
            ProgressBar.Visibility = ViewStates.Invisible;
            var adapter = new ArrayAdapter<MeetingItem>(this, Android.Resource.Layout.SimpleListItem1, gameList);
            ListView_Games.Adapter = adapter;
            ListView_Games.ItemClick += async delegate (object sender, AdapterView.ItemClickEventArgs e)
            {
                MeetingItem item = adapter.GetItem(e.Position);

                string meetingDTOjson = string.Empty;
                string meeting = string.Empty;
                bool matchPlayers = true;
                try
                {
                    // Get the meeting corresponding to the meeting uri
                    meeting = await meetingLoader.GetMeetingAsync(accessToken, item.MeetingUri);
                    string gameCode = GetGameCodeFromMeeting(meeting);

                    CloudAuthenticator authenticator = new CloudAuthenticator();
                    Token livescoringToken = await authenticator.GetToken(Authentication.LiveScoring);

                    // Get the meeting dto
                    meetingDTOjson = await meetingLoader.GetMeetingDTOAsync(livescoringToken, gameCode);
                }
                catch (Exception)
                {
                    Toast.MakeText(this, "Die nu-Score-Schnittstelle hat keinen Gamecode geliefert. Es wird ohne Spieler-Validierung fortgefahren.", ToastLength.Long).Show();
                    matchPlayers = false;
                }

                // Match players
                if (matchPlayers)
                {
                    ScoreSheetDTO.HomeTeamPlayers = GetMatchedPlayersFromMeeting(meetingDTOjson, ScoreSheetDTO.HomeTeamPlayers, TeamType.Home);
                    ScoreSheetDTO.GuestTeamPlayers = GetMatchedPlayersFromMeeting(meetingDTOjson, ScoreSheetDTO.GuestTeamPlayers, TeamType.Guest);
                    JObject meetingJSON = JObject.Parse(meeting);
                    ScoreSheetDTO.HomeTeam = meetingJSON.SelectToken("teamHome").ToString();
                    ScoreSheetDTO.GuestTeam = meetingJSON.SelectToken("teamGuest").ToString();
                    ScoreSheetDTO.Group = meetingJSON.SelectToken("groupName").ToString();
                    ScoreSheetDTO.GameClass = meetingJSON.SelectToken("championshipName").ToString();
                    ScoreSheetDTO.Date = meetingJSON.SelectToken("originalDate").ToString();
                }

                // Start new Activity
                Intent intent = new Intent(this, typeof(ShowDataGeneralActivity));
                intent.PutExtra(scoreSheetName, JsonSerializer.Serialize(ScoreSheetDTO));
                StartActivity(intent);
            };
        }

        private Dictionary<string, string> GetMatchedPlayersFromMeeting(string meetingDTOjson, Dictionary<string, string> recognizedTeamNames, TeamType teamType)
        {
            JObject meetingDTO = JObject.Parse(meetingDTOjson);
            JToken matches = meetingDTO.SelectToken("matches");

            List<string> playerTeamHomeWithDuplicates = new List<string>();
            List<string> playerGuestHomeWithDuplicates = new List<string>();

            foreach (var match in matches)
            {
                if (teamType == TeamType.Home)
                {
                    string playerA1 = $"{match.SelectToken("playerA1.lastname")}";
                    playerTeamHomeWithDuplicates.Add(playerA1);
                    if ((bool)match["doubleMatch"])
                    {
                        string playerA2 = $"{match.SelectToken("playerA2.lastname")}";
                        playerTeamHomeWithDuplicates.Add(playerA2);
                    }
                }
                else if (teamType == TeamType.Guest)
                {
                    string playerB1 = $"{match.SelectToken("playerB1.lastname")}";
                    playerGuestHomeWithDuplicates.Add(playerB1);
                    if ((bool)match["doubleMatch"])
                    {
                        string playerB2 = $"{match.SelectToken("playerB2.lastname")}";
                        playerGuestHomeWithDuplicates.Add(playerB2);
                    }
                }

            }
            List<string> playerTeamHome = playerTeamHomeWithDuplicates.Distinct().ToList();
            List<string> playerGuestHome = playerGuestHomeWithDuplicates.Distinct().ToList();

            PlayerMatcher playerMatch = new PlayerMatcher();
            if (teamType == TeamType.Home)
            {
                return playerMatch.Match(recognizedTeamNames, playerTeamHome);

            }
            else if (teamType == TeamType.Guest)
            {
                return playerMatch.Match(recognizedTeamNames, playerGuestHome);
            }
            return null;
        }

        private string GetGameCodeFromMeeting(string meeting)
        {
            JObject parsedMeeting = JObject.Parse(meeting);
            return (string)parsedMeeting["gameCode"];
        }

        private List<MeetingItem> ReadOutInformationFromMeetings(string matchesJson)
        {
            //Parse matches to JObject for further processing
            JObject matchesParsed = JObject.Parse(matchesJson);

            //Create new List of MeetingItems
            List<MeetingItem> gameList = new List<MeetingItem>();

            // Get meetings
            JToken meetings = matchesParsed.SelectToken("meetings.meetingAbbr");
            foreach (JToken match in meetings)
            {
                MeetingItem item = new MeetingItem
                {
                    HomeTeam = (string)match["teamHome"],
                    GuestTeam = (string)match["teamGuest"],
                    Date = (string)match["originalDate"],
                    MeetingID = (string)match["meetingId"],
                    MeetingUri = (string)match["meetingUri"]
                };
                gameList.Add(item);
            }
            return gameList;
        }

        private void Button_DateTo_Click(object sender, EventArgs e)
        {
            dateTo = true;
            ShowDialog(DATE_DIALOG_TO);
        }

        private void Button_DateFrom_Click(object sender, EventArgs e)
        {
            dateFrom = true;
            ShowDialog(DATE_DIALOG_FROM);
        }

        protected override Dialog OnCreateDialog(int id)
        {
            switch (id)
            {
                case DATE_DIALOG_FROM:
                    return new DatePickerDialog(this, this, DateTime.Now.Year, DateTime.Now.Month -1 , DateTime.Now.Day);
                case DATE_DIALOG_TO:
                    DateTime oneMoreWeek = DateTime.Now.AddDays(7.0);
                    return new DatePickerDialog(this, this, oneMoreWeek.Year, oneMoreWeek.Month -1, oneMoreWeek.Day);
                default:
                    break;
            }
            return null;
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            if (dateFrom)
            {
                dateFrom = false;
                yearFrom = year;
                monthFrom = month + 1;
                dayFrom = dayOfMonth;
                TextInputEditText_DateFrom.Text = dayFrom + "." + (monthFrom) + "." + yearFrom;
            }
            else if (dateTo)
            {
                dateTo = false;
                yearTo = year;
                monthTo = month + 1;
                dayTo = dayOfMonth;
                TextInputEditText_DateTo.Text = dayTo + "." + (monthTo) + "." + yearTo;
            }
        }
    }
}