using System.Collections.Generic;

namespace ScoreSheetScanner.Recognition.Model
{
    public class ScoreSheetDTO
    {
        /// <summary>
        /// Holds the name of the home team
        /// </summary>
        public string HomeTeam { get; set; }

        /// <summary>
        /// Holds the name of the guest team
        /// </summary>
        public string GuestTeam { get; set; }

        /// <summary>
        /// Holds the game class
        /// </summary>
        public string GameClass { get; set; }

        /// <summary>
        /// Holds the name of the place where the match is located
        /// </summary>
        public string GamePlace { get; set; }

        /// <summary>
        /// Holds the start time of the match
        /// </summary>
        public string GameStartTime { get; set; }

        /// <summary>
        /// Holds the end time of the match
        /// </summary>
        public string GameEndTime { get; set; }

        /// <summary>
        /// Holds the date of the match
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Holds the group of the teams
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Holds the winning team
        /// </summary>
        public string Winner { get; set; }

        /// <summary>
        /// Holds the names of the home team players
        /// </summary>
        public Dictionary<string, string> HomeTeamPlayers { get; set; }

        /// <summary>
        /// Holds the names of the guest team players
        /// </summary>
        public Dictionary<string, string> GuestTeamPlayers { get; set; }

        /// <summary>
        /// Holds Match Pairings
        /// </summary>
        public Dictionary<string, string> MatchPairings { get; set; }
        public Dictionary<string, string> ResultsPairing { get; set; }


        #region Constructors
        /// <summary>
        /// Standard constructor that initializes both <see cref="HomeTeamPlayers"/> and <see cref="GuestTeamPlayers"/>
        /// </summary>
        public ScoreSheetDTO()
        {
            HomeTeamPlayers = new Dictionary<string, string>();
            GuestTeamPlayers = new Dictionary<string, string>();
            MatchPairings = new Dictionary<string, string>();
            ResultsPairing = new Dictionary<string, string>();
        }
        #endregion
    }
}