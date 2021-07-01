using System;
using Newtonsoft.Json;

namespace ScoreSheetScanner.Cloud.Helper
{
    public class Token
    {
        /// <summary>
        /// Indicates if the token is for the livescoring scope or the federation/clubs interface
        /// </summary>
        public Authentication AuthenticationScope { get; set; }

        /// <summary>
        /// Holds the access token
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Holds the refresh token
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Timestamp to save when the tokens were generated
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }
}
