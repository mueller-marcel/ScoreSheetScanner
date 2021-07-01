using System.Text.Json;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using ScoreSheetScanner.Recognition.Model;
using Xamarin.Essentials;

namespace ScoreSheetScanner.App.Activities
{
    [Activity(Label = "@string/show_data_results")]
    class ShowDataResultActivity : AppCompatActivity
    {
        /// <summary>
        /// Holds the DataTransferObject of the ScoreSheet
        /// </summary>
        public ScoreSheetDTO ScoreSheetDTO { get; set; }

        public TextInputEditText TextInputEditText_Pairing1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing1Set1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing1Set2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing1Set3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing1Set4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing1Set5 { get; set; }

        public TextInputEditText TextInputEditText_Pairing2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing2Set1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing2Set2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing2Set3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing2Set4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing2Set5 { get; set; }

        public TextInputEditText TextInputEditText_Pairing3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing3Set1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing3Set2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing3Set3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing3Set4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing3Set5 { get; set; }

        public TextInputEditText TextInputEditText_Pairing4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing4Set1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing4Set2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing4Set3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing4Set4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing4Set5 { get; set; }

        public TextInputEditText TextInputEditText_Pairing5 { get; set; }
        public TextInputEditText TextInputEditText_Pairing5Set1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing5Set2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing5Set3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing5Set4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing5Set5 { get; set; }

        public TextInputEditText TextInputEditText_Pairing6 { get; set; }
        public TextInputEditText TextInputEditText_Pairing6Set1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing6Set2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing6Set3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing6Set4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing6Set5 { get; set; }

        public TextInputEditText TextInputEditText_Pairing7 { get; set; }
        public TextInputEditText TextInputEditText_Pairing7Set1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing7Set2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing7Set3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing7Set4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing7Set5 { get; set; }

        public TextInputEditText TextInputEditText_Pairing8 { get; set; }
        public TextInputEditText TextInputEditText_Pairing8Set1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing8Set2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing8Set3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing8Set4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing8Set5 { get; set; }

        public TextInputEditText TextInputEditText_Pairing9 { get; set; }
        public TextInputEditText TextInputEditText_Pairing9Set1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing9Set2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing9Set3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing9Set4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing9Set5 { get; set; }

        public TextInputEditText TextInputEditText_Pairing10 { get; set; }
        public TextInputEditText TextInputEditText_Pairing10Set1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing10Set2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing10Set3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing10Set4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing10Set5 { get; set; }

        public TextInputEditText TextInputEditText_Pairing11 { get; set; }
        public TextInputEditText TextInputEditText_Pairing11Set1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing11Set2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing11Set3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing11Set4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing11Set5 { get; set; }

        public TextInputEditText TextInputEditText_Pairing12 { get; set; }
        public TextInputEditText TextInputEditText_Pairing12Set1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing12Set2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing12Set3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing12Set4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing12Set5 { get; set; }

        public TextInputEditText TextInputEditText_Pairing13 { get; set; }
        public TextInputEditText TextInputEditText_Pairing13Set1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing13Set2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing13Set3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing13Set4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing13Set5 { get; set; }

        public TextInputEditText TextInputEditText_Pairing14 { get; set; }
        public TextInputEditText TextInputEditText_Pairing14Set1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing14Set2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing14Set3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing14Set4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing14Set5 { get; set; }

        public TextInputEditText TextInputEditText_Pairing15 { get; set; }
        public TextInputEditText TextInputEditText_Pairing15Set1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing15Set2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing15Set3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing15Set4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing15Set5 { get; set; }

        public TextInputEditText TextInputEditText_Pairing16 { get; set; }
        public TextInputEditText TextInputEditText_Pairing16Set1 { get; set; }
        public TextInputEditText TextInputEditText_Pairing16Set2 { get; set; }
        public TextInputEditText TextInputEditText_Pairing16Set3 { get; set; }
        public TextInputEditText TextInputEditText_Pairing16Set4 { get; set; }
        public TextInputEditText TextInputEditText_Pairing16Set5 { get; set; }

        private readonly string scoreSheetName = "scoreSheetDTO";
        private readonly string Pairing1 = "Paarung 1";
        private readonly string Pairing2 = "Paarung 2";
        private readonly string Pairing3 = "Paarung 3";
        private readonly string Pairing4 = "Paarung 4";
        private readonly string Pairing5 = "Paarung 5";
        private readonly string Pairing6 = "Paarung 6";
        private readonly string Pairing7 = "Paarung 7";
        private readonly string Pairing8 = "Paarung 8";
        private readonly string Pairing9 = "Paarung 9";
        private readonly string Pairing10 = "Paarung 10";
        private readonly string Pairing11 = "Paarung 11";
        private readonly string Pairing12 = "Paarung 12";
        private readonly string Pairing13 = "Paarung 13";
        private readonly string Pairing14 = "Paarung 14";
        private readonly string Pairing15 = "Paarung 15";
        private readonly string Pairing16 = "Paarung 16";
        private readonly string Pairing1Set1 = "1 1.Satz";
        private readonly string Pairing1Set2 = "1 2.Satz";
        private readonly string Pairing1Set3 = "1 3.Satz";
        private readonly string Pairing1Set4 = "1 4.Satz";
        private readonly string Pairing1Set5 = "1 5.Satz";

        private readonly string Pairing2Set1 = "2 1.Satz";
        private readonly string Pairing2Set2 = "2 2.Satz";
        private readonly string Pairing2Set3 = "2 3.Satz";
        private readonly string Pairing2Set4 = "2 4.Satz";
        private readonly string Pairing2Set5 = "2 5.Satz";

        private readonly string Pairing3Set1 = "3 1.Satz";
        private readonly string Pairing3Set2 = "3 2.Satz";
        private readonly string Pairing3Set3 = "3 3.Satz";
        private readonly string Pairing3Set4 = "3 4.Satz";
        private readonly string Pairing3Set5 = "3 5.Satz";

        private readonly string Pairing4Set1 = "4 1.Satz";
        private readonly string Pairing4Set2 = "4 2.Satz";
        private readonly string Pairing4Set3 = "4 3.Satz";
        private readonly string Pairing4Set4 = "4 4.Satz";
        private readonly string Pairing4Set5 = "4 5.Satz";

        private readonly string Pairing5Set1 = "5 1.Satz";
        private readonly string Pairing5Set2 = "5 2.Satz";
        private readonly string Pairing5Set3 = "5 3.Satz";
        private readonly string Pairing5Set4 = "5 4.Satz";
        private readonly string Pairing5Set5 = "5 5.Satz";

        private readonly string Pairing6Set1 = "6 1.Satz";
        private readonly string Pairing6Set2 = "6 2.Satz";
        private readonly string Pairing6Set3 = "6 3.Satz";
        private readonly string Pairing6Set4 = "6 4.Satz";
        private readonly string Pairing6Set5 = "6 5.Satz";

        private readonly string Pairing7Set1 = "7 1.Satz";
        private readonly string Pairing7Set2 = "7 2.Satz";
        private readonly string Pairing7Set3 = "7 3.Satz";
        private readonly string Pairing7Set4 = "7 4.Satz";
        private readonly string Pairing7Set5 = "7 5.Satz";

        private readonly string Pairing8Set1 = "8 1.Satz";
        private readonly string Pairing8Set2 = "8 2.Satz";
        private readonly string Pairing8Set3 = "8 3.Satz";
        private readonly string Pairing8Set4 = "8 4.Satz";
        private readonly string Pairing8Set5 = "8 5.Satz";

        private readonly string Pairing9Set1 = "9 1.Satz";
        private readonly string Pairing9Set2 = "9 2.Satz";
        private readonly string Pairing9Set3 = "9 3.Satz";
        private readonly string Pairing9Set4 = "9 4.Satz";
        private readonly string Pairing9Set5 = "9 5.Satz";

        private readonly string Pairing10Set1 = "10 1.Satz";
        private readonly string Pairing10Set2 = "10 2.Satz";
        private readonly string Pairing10Set3 = "10 3.Satz";
        private readonly string Pairing10Set4 = "10 4.Satz";
        private readonly string Pairing10Set5 = "10 5.Satz";

        private readonly string Pairing11Set1 = "11 1.Satz";
        private readonly string Pairing11Set2 = "11 2.Satz";
        private readonly string Pairing11Set3 = "11 3.Satz";
        private readonly string Pairing11Set4 = "11 4.Satz";
        private readonly string Pairing11Set5 = "11 5.Satz";

        private readonly string Pairing12Set1 = "12 1.Satz";
        private readonly string Pairing12Set2 = "12 2.Satz";
        private readonly string Pairing12Set3 = "12 3.Satz";
        private readonly string Pairing12Set4 = "12 4.Satz";
        private readonly string Pairing12Set5 = "12 5.Satz";

        private readonly string Pairing13Set1 = "13 1.Satz";
        private readonly string Pairing13Set2 = "13 2.Satz";
        private readonly string Pairing13Set3 = "13 3.Satz";
        private readonly string Pairing13Set4 = "13 4.Satz";
        private readonly string Pairing13Set5 = "13 5.Satz";

        private readonly string Pairing14Set1 = "14 1.Satz";
        private readonly string Pairing14Set2 = "14 2.Satz";
        private readonly string Pairing14Set3 = "14 3.Satz";
        private readonly string Pairing14Set4 = "14 4.Satz";
        private readonly string Pairing14Set5 = "14 5.Satz";

        private readonly string Pairing15Set1 = "15 1.Satz";
        private readonly string Pairing15Set2 = "15 2.Satz";
        private readonly string Pairing15Set3 = "15 3.Satz";
        private readonly string Pairing15Set4 = "15 4.Satz";
        private readonly string Pairing15Set5 = "15 5.Satz";

        private readonly string Pairing16Set1 = "16 1.Satz";
        private readonly string Pairing16Set2 = "16 2.Satz";
        private readonly string Pairing16Set3 = "16 3.Satz";
        private readonly string Pairing16Set4 = "16 4.Satz";
        private readonly string Pairing16Set5 = "16 5.Satz";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Get data from the Intent that invoked the activity
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_showResults);
            ScoreSheetDTO = JsonSerializer.Deserialize<ScoreSheetDTO>(Intent.GetStringExtra(scoreSheetName) ?? string.Empty);

            // Initialize UI-components
            // Pairing 1
            TextInputEditText_Pairing1 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Pairing1);
            TextInputEditText_Pairing1.Text = ScoreSheetDTO.MatchPairings[Pairing1];
            TextInputEditText_Pairing1Set1 = FindViewById<TextInputEditText>(Resource.Id.Pairing1Set1);
            TextInputEditText_Pairing1Set1.Text = ScoreSheetDTO.ResultsPairing[Pairing1Set1];
            TextInputEditText_Pairing1Set2 = FindViewById<TextInputEditText>(Resource.Id.Pairing1Set2);
            TextInputEditText_Pairing1Set2.Text = ScoreSheetDTO.ResultsPairing[Pairing1Set2];
            TextInputEditText_Pairing1Set3 = FindViewById<TextInputEditText>(Resource.Id.Pairing1Set3);
            TextInputEditText_Pairing1Set3.Text = ScoreSheetDTO.ResultsPairing[Pairing1Set3];
            TextInputEditText_Pairing1Set4 = FindViewById<TextInputEditText>(Resource.Id.Pairing1Set4);
            TextInputEditText_Pairing1Set4.Text = ScoreSheetDTO.ResultsPairing[Pairing1Set4];
            TextInputEditText_Pairing1Set5 = FindViewById<TextInputEditText>(Resource.Id.Pairing1Set5);
            TextInputEditText_Pairing1Set5.Text = ScoreSheetDTO.ResultsPairing[Pairing1Set5];

            // Pairing 2
            TextInputEditText_Pairing2 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Pairing2);
            TextInputEditText_Pairing2.Text = ScoreSheetDTO.MatchPairings[Pairing2];
            TextInputEditText_Pairing2Set1 = FindViewById<TextInputEditText>(Resource.Id.Pairing2Set1);
            TextInputEditText_Pairing2Set1.Text = ScoreSheetDTO.ResultsPairing[Pairing2Set1];
            TextInputEditText_Pairing2Set2 = FindViewById<TextInputEditText>(Resource.Id.Pairing2Set2);
            TextInputEditText_Pairing2Set2.Text = ScoreSheetDTO.ResultsPairing[Pairing2Set2];
            TextInputEditText_Pairing2Set3 = FindViewById<TextInputEditText>(Resource.Id.Pairing2Set3);
            TextInputEditText_Pairing2Set3.Text = ScoreSheetDTO.ResultsPairing[Pairing2Set3];
            TextInputEditText_Pairing2Set4 = FindViewById<TextInputEditText>(Resource.Id.Pairing2Set4);
            TextInputEditText_Pairing2Set4.Text = ScoreSheetDTO.ResultsPairing[Pairing2Set4];
            TextInputEditText_Pairing2Set5 = FindViewById<TextInputEditText>(Resource.Id.Pairing2Set5);
            TextInputEditText_Pairing2Set5.Text = ScoreSheetDTO.ResultsPairing[Pairing2Set5];

            // Pairing 3
            TextInputEditText_Pairing3 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Pairing3);
            TextInputEditText_Pairing3.Text = ScoreSheetDTO.MatchPairings[Pairing3];
            TextInputEditText_Pairing3Set1 = FindViewById<TextInputEditText>(Resource.Id.Pairing3Set1);
            TextInputEditText_Pairing3Set1.Text = ScoreSheetDTO.ResultsPairing[Pairing3Set1];
            TextInputEditText_Pairing3Set2 = FindViewById<TextInputEditText>(Resource.Id.Pairing3Set2);
            TextInputEditText_Pairing3Set2.Text = ScoreSheetDTO.ResultsPairing[Pairing3Set2];
            TextInputEditText_Pairing3Set3 = FindViewById<TextInputEditText>(Resource.Id.Pairing3Set3);
            TextInputEditText_Pairing3Set3.Text = ScoreSheetDTO.ResultsPairing[Pairing3Set3];
            TextInputEditText_Pairing3Set4 = FindViewById<TextInputEditText>(Resource.Id.Pairing3Set4);
            TextInputEditText_Pairing3Set4.Text = ScoreSheetDTO.ResultsPairing[Pairing3Set4];
            TextInputEditText_Pairing3Set5 = FindViewById<TextInputEditText>(Resource.Id.Pairing3Set5);
            TextInputEditText_Pairing3Set5.Text = ScoreSheetDTO.ResultsPairing[Pairing3Set5];

            // Pairing 4
            TextInputEditText_Pairing4 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Pairing4);
            TextInputEditText_Pairing4.Text = ScoreSheetDTO.MatchPairings[Pairing4];
            TextInputEditText_Pairing4Set1 = FindViewById<TextInputEditText>(Resource.Id.Pairing4Set1);
            TextInputEditText_Pairing4Set1.Text = ScoreSheetDTO.ResultsPairing[Pairing4Set1];
            TextInputEditText_Pairing4Set2 = FindViewById<TextInputEditText>(Resource.Id.Pairing4Set2);
            TextInputEditText_Pairing4Set2.Text = ScoreSheetDTO.ResultsPairing[Pairing4Set2];
            TextInputEditText_Pairing4Set3 = FindViewById<TextInputEditText>(Resource.Id.Pairing4Set3);
            TextInputEditText_Pairing4Set3.Text = ScoreSheetDTO.ResultsPairing[Pairing4Set3];
            TextInputEditText_Pairing4Set4 = FindViewById<TextInputEditText>(Resource.Id.Pairing4Set4);
            TextInputEditText_Pairing4Set4.Text = ScoreSheetDTO.ResultsPairing[Pairing4Set4];
            TextInputEditText_Pairing4Set5 = FindViewById<TextInputEditText>(Resource.Id.Pairing4Set5);
            TextInputEditText_Pairing4Set5.Text = ScoreSheetDTO.ResultsPairing[Pairing4Set5];

            // Pairing 5
            TextInputEditText_Pairing5 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Pairing5);
            TextInputEditText_Pairing5.Text = ScoreSheetDTO.MatchPairings[Pairing5];
            TextInputEditText_Pairing5Set1 = FindViewById<TextInputEditText>(Resource.Id.Pairing5Set1);
            TextInputEditText_Pairing5Set1.Text = ScoreSheetDTO.ResultsPairing[Pairing5Set1];
            TextInputEditText_Pairing5Set2 = FindViewById<TextInputEditText>(Resource.Id.Pairing5Set2);
            TextInputEditText_Pairing5Set2.Text = ScoreSheetDTO.ResultsPairing[Pairing5Set2];
            TextInputEditText_Pairing5Set3 = FindViewById<TextInputEditText>(Resource.Id.Pairing5Set3);
            TextInputEditText_Pairing5Set3.Text = ScoreSheetDTO.ResultsPairing[Pairing5Set3];
            TextInputEditText_Pairing5Set4 = FindViewById<TextInputEditText>(Resource.Id.Pairing5Set4);
            TextInputEditText_Pairing5Set4.Text = ScoreSheetDTO.ResultsPairing[Pairing5Set4];
            TextInputEditText_Pairing5Set5 = FindViewById<TextInputEditText>(Resource.Id.Pairing5Set5);
            TextInputEditText_Pairing5Set5.Text = ScoreSheetDTO.ResultsPairing[Pairing5Set5];

            // Pairing 6
            TextInputEditText_Pairing6 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Pairing6);
            TextInputEditText_Pairing6.Text = ScoreSheetDTO.MatchPairings[Pairing6];
            TextInputEditText_Pairing6Set1 = FindViewById<TextInputEditText>(Resource.Id.Pairing6Set1);
            TextInputEditText_Pairing6Set1.Text = ScoreSheetDTO.ResultsPairing[Pairing6Set1];
            TextInputEditText_Pairing6Set2 = FindViewById<TextInputEditText>(Resource.Id.Pairing6Set2);
            TextInputEditText_Pairing6Set2.Text = ScoreSheetDTO.ResultsPairing[Pairing6Set2];
            TextInputEditText_Pairing6Set3 = FindViewById<TextInputEditText>(Resource.Id.Pairing6Set3);
            TextInputEditText_Pairing6Set3.Text = ScoreSheetDTO.ResultsPairing[Pairing6Set3];
            TextInputEditText_Pairing6Set4 = FindViewById<TextInputEditText>(Resource.Id.Pairing6Set4);
            TextInputEditText_Pairing6Set4.Text = ScoreSheetDTO.ResultsPairing[Pairing6Set4];
            TextInputEditText_Pairing6Set5 = FindViewById<TextInputEditText>(Resource.Id.Pairing6Set5);
            TextInputEditText_Pairing6Set5.Text = ScoreSheetDTO.ResultsPairing[Pairing6Set5];

            // Pairing 7
            TextInputEditText_Pairing7 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Pairing7);
            TextInputEditText_Pairing7.Text = ScoreSheetDTO.MatchPairings[Pairing7];
            TextInputEditText_Pairing7Set1 = FindViewById<TextInputEditText>(Resource.Id.Pairing7Set1);
            TextInputEditText_Pairing7Set1.Text = ScoreSheetDTO.ResultsPairing[Pairing7Set1];
            TextInputEditText_Pairing7Set2 = FindViewById<TextInputEditText>(Resource.Id.Pairing7Set2);
            TextInputEditText_Pairing7Set2.Text = ScoreSheetDTO.ResultsPairing[Pairing7Set2];
            TextInputEditText_Pairing7Set3 = FindViewById<TextInputEditText>(Resource.Id.Pairing7Set3);
            TextInputEditText_Pairing7Set3.Text = ScoreSheetDTO.ResultsPairing[Pairing7Set3];
            TextInputEditText_Pairing7Set4 = FindViewById<TextInputEditText>(Resource.Id.Pairing7Set4);
            TextInputEditText_Pairing7Set4.Text = ScoreSheetDTO.ResultsPairing[Pairing7Set4];
            TextInputEditText_Pairing7Set5 = FindViewById<TextInputEditText>(Resource.Id.Pairing7Set5);
            TextInputEditText_Pairing7Set5.Text = ScoreSheetDTO.ResultsPairing[Pairing7Set5];

            // Pairing 8
            TextInputEditText_Pairing8 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Pairing8);
            TextInputEditText_Pairing8.Text = ScoreSheetDTO.MatchPairings[Pairing8];
            TextInputEditText_Pairing8Set1 = FindViewById<TextInputEditText>(Resource.Id.Pairing8Set1);
            TextInputEditText_Pairing8Set1.Text = ScoreSheetDTO.ResultsPairing[Pairing8Set1];
            TextInputEditText_Pairing8Set2 = FindViewById<TextInputEditText>(Resource.Id.Pairing8Set2);
            TextInputEditText_Pairing8Set2.Text = ScoreSheetDTO.ResultsPairing[Pairing8Set2];
            TextInputEditText_Pairing8Set3 = FindViewById<TextInputEditText>(Resource.Id.Pairing8Set3);
            TextInputEditText_Pairing8Set3.Text = ScoreSheetDTO.ResultsPairing[Pairing8Set3];
            TextInputEditText_Pairing8Set4 = FindViewById<TextInputEditText>(Resource.Id.Pairing8Set4);
            TextInputEditText_Pairing8Set4.Text = ScoreSheetDTO.ResultsPairing[Pairing8Set4];
            TextInputEditText_Pairing8Set5 = FindViewById<TextInputEditText>(Resource.Id.Pairing8Set5);
            TextInputEditText_Pairing8Set5.Text = ScoreSheetDTO.ResultsPairing[Pairing8Set5];

            // Pairing 9
            TextInputEditText_Pairing9 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Pairing9);
            TextInputEditText_Pairing9.Text = ScoreSheetDTO.MatchPairings[Pairing9];
            TextInputEditText_Pairing9Set1 = FindViewById<TextInputEditText>(Resource.Id.Pairing9Set1);
            TextInputEditText_Pairing9Set1.Text = ScoreSheetDTO.ResultsPairing[Pairing9Set1];
            TextInputEditText_Pairing9Set2 = FindViewById<TextInputEditText>(Resource.Id.Pairing9Set2);
            TextInputEditText_Pairing9Set2.Text = ScoreSheetDTO.ResultsPairing[Pairing9Set2];
            TextInputEditText_Pairing9Set3 = FindViewById<TextInputEditText>(Resource.Id.Pairing9Set3);
            TextInputEditText_Pairing9Set3.Text = ScoreSheetDTO.ResultsPairing[Pairing9Set3];
            TextInputEditText_Pairing9Set4 = FindViewById<TextInputEditText>(Resource.Id.Pairing9Set4);
            TextInputEditText_Pairing9Set4.Text = ScoreSheetDTO.ResultsPairing[Pairing9Set4];
            TextInputEditText_Pairing9Set5 = FindViewById<TextInputEditText>(Resource.Id.Pairing9Set5);
            TextInputEditText_Pairing9Set5.Text = ScoreSheetDTO.ResultsPairing[Pairing9Set5];

            // Pairing 10
            TextInputEditText_Pairing10 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Pairing10);
            TextInputEditText_Pairing10.Text = ScoreSheetDTO.MatchPairings[Pairing10];
            TextInputEditText_Pairing10Set1 = FindViewById<TextInputEditText>(Resource.Id.Pairing10Set1);
            TextInputEditText_Pairing10Set1.Text = ScoreSheetDTO.ResultsPairing[Pairing10Set1];
            TextInputEditText_Pairing10Set2 = FindViewById<TextInputEditText>(Resource.Id.Pairing10Set2);
            TextInputEditText_Pairing10Set2.Text = ScoreSheetDTO.ResultsPairing[Pairing10Set2];
            TextInputEditText_Pairing10Set3 = FindViewById<TextInputEditText>(Resource.Id.Pairing10Set3);
            TextInputEditText_Pairing10Set3.Text = ScoreSheetDTO.ResultsPairing[Pairing10Set3];
            TextInputEditText_Pairing10Set4 = FindViewById<TextInputEditText>(Resource.Id.Pairing10Set4);
            TextInputEditText_Pairing10Set4.Text = ScoreSheetDTO.ResultsPairing[Pairing10Set4];
            TextInputEditText_Pairing10Set5 = FindViewById<TextInputEditText>(Resource.Id.Pairing10Set5);
            TextInputEditText_Pairing10Set5.Text = ScoreSheetDTO.ResultsPairing[Pairing10Set5];

            // Pairing 11
            TextInputEditText_Pairing11 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Pairing11);
            TextInputEditText_Pairing11.Text = ScoreSheetDTO.MatchPairings[Pairing11];
            TextInputEditText_Pairing11Set1 = FindViewById<TextInputEditText>(Resource.Id.Pairing11Set1);
            TextInputEditText_Pairing11Set1.Text = ScoreSheetDTO.ResultsPairing[Pairing11Set1];
            TextInputEditText_Pairing11Set2 = FindViewById<TextInputEditText>(Resource.Id.Pairing11Set2);
            TextInputEditText_Pairing11Set2.Text = ScoreSheetDTO.ResultsPairing[Pairing11Set2];
            TextInputEditText_Pairing11Set3 = FindViewById<TextInputEditText>(Resource.Id.Pairing11Set3);
            TextInputEditText_Pairing11Set3.Text = ScoreSheetDTO.ResultsPairing[Pairing11Set3];
            TextInputEditText_Pairing11Set4 = FindViewById<TextInputEditText>(Resource.Id.Pairing11Set4);
            TextInputEditText_Pairing11Set4.Text = ScoreSheetDTO.ResultsPairing[Pairing11Set4];
            TextInputEditText_Pairing11Set5 = FindViewById<TextInputEditText>(Resource.Id.Pairing11Set5);
            TextInputEditText_Pairing11Set5.Text = ScoreSheetDTO.ResultsPairing[Pairing11Set5];

            // Pairing 12
            TextInputEditText_Pairing12 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Pairing12);
            TextInputEditText_Pairing12.Text = ScoreSheetDTO.MatchPairings[Pairing12];
            TextInputEditText_Pairing12Set1 = FindViewById<TextInputEditText>(Resource.Id.Pairing12Set1);
            TextInputEditText_Pairing12Set1.Text = ScoreSheetDTO.ResultsPairing[Pairing12Set1];
            TextInputEditText_Pairing12Set2 = FindViewById<TextInputEditText>(Resource.Id.Pairing12Set2);
            TextInputEditText_Pairing12Set2.Text = ScoreSheetDTO.ResultsPairing[Pairing12Set2];
            TextInputEditText_Pairing12Set3 = FindViewById<TextInputEditText>(Resource.Id.Pairing12Set3);
            TextInputEditText_Pairing12Set3.Text = ScoreSheetDTO.ResultsPairing[Pairing12Set3];
            TextInputEditText_Pairing12Set4 = FindViewById<TextInputEditText>(Resource.Id.Pairing12Set4);
            TextInputEditText_Pairing12Set4.Text = ScoreSheetDTO.ResultsPairing[Pairing12Set4];
            TextInputEditText_Pairing12Set5 = FindViewById<TextInputEditText>(Resource.Id.Pairing12Set5);
            TextInputEditText_Pairing12Set5.Text = ScoreSheetDTO.ResultsPairing[Pairing12Set5];

            // Pairing 13
            TextInputEditText_Pairing13 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Pairing13);
            TextInputEditText_Pairing13.Text = ScoreSheetDTO.MatchPairings[Pairing13];
            TextInputEditText_Pairing13Set1 = FindViewById<TextInputEditText>(Resource.Id.Pairing13Set1);
            TextInputEditText_Pairing13Set1.Text = ScoreSheetDTO.ResultsPairing[Pairing13Set1];
            TextInputEditText_Pairing13Set2 = FindViewById<TextInputEditText>(Resource.Id.Pairing13Set2);
            TextInputEditText_Pairing13Set2.Text = ScoreSheetDTO.ResultsPairing[Pairing13Set2];
            TextInputEditText_Pairing13Set3 = FindViewById<TextInputEditText>(Resource.Id.Pairing13Set3);
            TextInputEditText_Pairing13Set3.Text = ScoreSheetDTO.ResultsPairing[Pairing13Set3];
            TextInputEditText_Pairing13Set4 = FindViewById<TextInputEditText>(Resource.Id.Pairing13Set4);
            TextInputEditText_Pairing13Set4.Text = ScoreSheetDTO.ResultsPairing[Pairing13Set4];
            TextInputEditText_Pairing13Set5 = FindViewById<TextInputEditText>(Resource.Id.Pairing13Set5);
            TextInputEditText_Pairing13Set5.Text = ScoreSheetDTO.ResultsPairing[Pairing13Set5];

            // Pairing 14
            TextInputEditText_Pairing14 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Pairing14);
            TextInputEditText_Pairing14.Text = ScoreSheetDTO.MatchPairings[Pairing14];
            TextInputEditText_Pairing14Set1 = FindViewById<TextInputEditText>(Resource.Id.Pairing14Set1);
            TextInputEditText_Pairing14Set1.Text = ScoreSheetDTO.ResultsPairing[Pairing14Set1];
            TextInputEditText_Pairing14Set2 = FindViewById<TextInputEditText>(Resource.Id.Pairing14Set2);
            TextInputEditText_Pairing14Set2.Text = ScoreSheetDTO.ResultsPairing[Pairing14Set2];
            TextInputEditText_Pairing14Set3 = FindViewById<TextInputEditText>(Resource.Id.Pairing14Set3);
            TextInputEditText_Pairing14Set3.Text = ScoreSheetDTO.ResultsPairing[Pairing14Set3];
            TextInputEditText_Pairing14Set4 = FindViewById<TextInputEditText>(Resource.Id.Pairing14Set4);
            TextInputEditText_Pairing14Set4.Text = ScoreSheetDTO.ResultsPairing[Pairing14Set4];
            TextInputEditText_Pairing14Set5 = FindViewById<TextInputEditText>(Resource.Id.Pairing14Set5);
            TextInputEditText_Pairing14Set5.Text = ScoreSheetDTO.ResultsPairing[Pairing14Set5];

            // Pairing 15
            TextInputEditText_Pairing15 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Pairing15);
            TextInputEditText_Pairing15.Text = ScoreSheetDTO.MatchPairings[Pairing15];
            TextInputEditText_Pairing15Set1 = FindViewById<TextInputEditText>(Resource.Id.Pairing15Set1);
            TextInputEditText_Pairing15Set1.Text = ScoreSheetDTO.ResultsPairing[Pairing15Set1];
            TextInputEditText_Pairing15Set2 = FindViewById<TextInputEditText>(Resource.Id.Pairing15Set2);
            TextInputEditText_Pairing15Set2.Text = ScoreSheetDTO.ResultsPairing[Pairing15Set2];
            TextInputEditText_Pairing15Set3 = FindViewById<TextInputEditText>(Resource.Id.Pairing15Set3);
            TextInputEditText_Pairing15Set3.Text = ScoreSheetDTO.ResultsPairing[Pairing15Set3];
            TextInputEditText_Pairing15Set4 = FindViewById<TextInputEditText>(Resource.Id.Pairing15Set4);
            TextInputEditText_Pairing15Set4.Text = ScoreSheetDTO.ResultsPairing[Pairing15Set4];
            TextInputEditText_Pairing15Set5 = FindViewById<TextInputEditText>(Resource.Id.Pairing15Set5);
            TextInputEditText_Pairing15Set5.Text = ScoreSheetDTO.ResultsPairing[Pairing15Set5];

            // Pairing 16
            TextInputEditText_Pairing16 = FindViewById<TextInputEditText>(Resource.Id.textEdit_Pairing16);
            TextInputEditText_Pairing16.Text = ScoreSheetDTO.MatchPairings[Pairing16];
            TextInputEditText_Pairing16Set1 = FindViewById<TextInputEditText>(Resource.Id.Pairing16Set1);
            TextInputEditText_Pairing16Set1.Text = ScoreSheetDTO.ResultsPairing[Pairing16Set1];
            TextInputEditText_Pairing16Set2 = FindViewById<TextInputEditText>(Resource.Id.Pairing16Set2);
            TextInputEditText_Pairing16Set2.Text = ScoreSheetDTO.ResultsPairing[Pairing16Set2];
            TextInputEditText_Pairing16Set3 = FindViewById<TextInputEditText>(Resource.Id.Pairing16Set3);
            TextInputEditText_Pairing16Set3.Text = ScoreSheetDTO.ResultsPairing[Pairing16Set3];
            TextInputEditText_Pairing16Set4 = FindViewById<TextInputEditText>(Resource.Id.Pairing16Set4);
            TextInputEditText_Pairing16Set4.Text = ScoreSheetDTO.ResultsPairing[Pairing16Set4];
            TextInputEditText_Pairing16Set5 = FindViewById<TextInputEditText>(Resource.Id.Pairing16Set5);
            TextInputEditText_Pairing16Set5.Text = ScoreSheetDTO.ResultsPairing[Pairing16Set5];


        }
    }
}