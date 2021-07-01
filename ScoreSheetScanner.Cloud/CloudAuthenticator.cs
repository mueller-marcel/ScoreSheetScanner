using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ScoreSheetScanner.Cloud.Exceptions;
using ScoreSheetScanner.Cloud.Helper;
using Xamarin.Essentials;

namespace ScoreSheetScanner.Cloud
{
    public class CloudAuthenticator
    {
        #region Methods
        /// <summary>
        /// Retrieves a new access token
        /// </summary>
        /// <param name="authentication">Indicates whether to authenticate for the livescoring or the federation scope</param>
        /// <returns>An instance of type <see cref="Token"/> holding the access and refresh token</returns>
        /// <exception cref="ArgumentException">Thrown, when <see cref="Authentication"/> param was set wrong or is null</exception>
        /// <exception cref="ArgumentNullException">Thrown, when a parameter of the http-request was null</exception>
        /// <exception cref="HttpRequestException">Thrown, when the http-request was not successful</exception>
        public async Task<Token> GetAccessTokenAsync(Authentication authentication)
        {
            // Declare variables used for the http calls
            string nuScoreScope;
            string authUrl;
            string clientId;
            string clientSecret;

            // Check for which interface the user wants to authenticate
            switch (authentication)
            {
                case Authentication.LiveScoring:
                    authUrl = "<auth_url>";
                    clientId = "<client_id>";
                    clientSecret = "<client_secret>";
                    nuScoreScope = "<scope>";
                    break;
                case Authentication.Federation:
                    authUrl = "<auth_url>";
                    clientId = "<client_id>";
                    clientSecret = "<client_secret>";
                    nuScoreScope = "<scope>";
                    break;
                default:
                    throw new ArgumentException("Argument authentication was set wrong");
            }

            using (var httpClient = new HttpClient())
            {
                // Build the authorization header for OAuth client credentials authentication
                var authenticationHeaders = new Dictionary<string, string>
                {
                    {"grant_type", "client_credentials"},
                    {"client_id", clientId },
                    {"client_secret", clientSecret },
                    {"scope", nuScoreScope}
                };

                HttpResponseMessage response;
                try
                {
                    // Request the authorization together with the headers
                    response = await httpClient.PostAsync(authUrl, new FormUrlEncodedContent(authenticationHeaders));
                }
                catch (ArgumentNullException e)
                {
                    throw new ArgumentNullException($"Argument {e.ParamName} was null while sending the post request");
                }
                catch (HttpRequestException)
                {
                    throw new HttpRequestException("Error while sending the post request");
                }

                // Check if the http response was successful
                if (response.IsSuccessStatusCode)
                {
                    Token token = JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());

                    // After deserialization a token or null is returned so the null case is handled here
                    if (token != null)
                    {
                        // Set Timestamp
                        token.TimeStamp = DateTime.Now;

                        // Set authentication scope
                        token.AuthenticationScope = authentication;

                        return token;
                    }
                    throw new TokenNullException();
                }
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Refreshes an access token
        /// </summary>
        /// <param name="authentication">Indicates whether to authenticate for the livescoring or the federation scope</param>
        /// <param name="refreshToken">The refresh token used to get a new access token</param>
        /// <returns>An instance of type <see cref="Token"/> holding the access and refresh token</returns>
        /// <exception cref="ArgumentException">Thrown, when <see cref="Authentication"/> param was set wrong or is null</exception>
        /// <exception cref="ArgumentNullException">Thrown, when a parameter of the http-request was null</exception>
        /// <exception cref="HttpRequestException">Thrown, when the http-request was not successful</exception>
        public async Task<Token> RefreshAccessTokenAsync(Authentication authentication, string refreshToken)
        {
            // Declarations
            string clientId;
            string clientSecret;
            string authUrl;

            // Check for which interface to refresh the access token
            switch (authentication)
            {
                case Authentication.LiveScoring:
                    authUrl = "<auth_url>";
                    clientId = "<client_id>";
                    clientSecret = "<client_secret>";
                    break;
                case Authentication.Federation:
                    authUrl = "<auth_url>";
                    clientId = "<client_id>";
                    clientSecret = "<client_secret>";
                    break;
                default:
                    throw new ArgumentException("Argument authentication was set wrong");
            }

            using (var httpClient = new HttpClient())
            {
                // Build the authorization header for OAuth client credentials authentication
                var authenticationHeaders = new Dictionary<string, string>
                {
                    {"grant_type", "refresh_token"},
                    {"client_id", clientId },
                    {"client_secret", clientSecret },
                    { "refresh_token", refreshToken }
                };

                HttpResponseMessage response;
                try
                {
                    // Request the authorization together with the headers
                    response = await httpClient.PostAsync(authUrl, new FormUrlEncodedContent(authenticationHeaders));
                }
                catch (ArgumentNullException e)
                {
                    throw new ArgumentNullException($"Argument {e.ParamName} was null while sending the post request");
                }
                catch (HttpRequestException)
                {
                    throw new HttpRequestException("Error while sending the post request");
                }

                // Check if the http response was successful
                if (response.IsSuccessStatusCode)
                {
                    Token token = JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());

                    // After deserialization a token or null is returned so the null case is handled here
                    if (token != null)
                    {
                        // Set Timestamp
                        token.TimeStamp = DateTime.Now;

                        // Set authentication scope
                        token.AuthenticationScope = authentication;

                        return token;
                    }
                    throw new TokenNullException();
                }
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Gets a access token either from the local storage or requests one from nuScore by credentials or refresh token
        /// </summary>
        /// <param name="authentication">Indicates for which interface the access token is requested</param>
        /// <returns>An instance of type <see cref="Token"/> for the further http requests</returns>
        /// <exception cref="Exception">Bubbles up the exceptions from other methods</exception>
        /// <exception cref="JsonException">Thrown, when the json string to deserialize is null or empty</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when the argument for the timespan calculation is out of range</exception>
        public async Task<Token> GetToken(Authentication authentication)
        {
            // Declaration of variables
            Token token;
            TimeSpan timeSpan;

            // Create Access token if no one was created before
            if (!Preferences.ContainsKey(authentication.ToString()))
            {
                try
                {
                    // Generate token
                    token = await GetAccessTokenAsync(authentication);
                }
                catch (Exception e)
                {
                    throw e;
                }

                // Save token local
                Preferences.Set(authentication.ToString(), JsonConvert.SerializeObject(token));

                // Return token
                return token;
            }

            // Get token from local storage
            var tokenJson = Preferences.Get(authentication.ToString(), string.Empty);
            if (!string.IsNullOrEmpty(tokenJson))
            {
                token = JsonConvert.DeserializeObject<Token>(tokenJson);
            }
            else
            {
                throw new JsonException("Retrieval of access token was an empty string");
            }

            try
            {
                // Check if the access token is already expired
                timeSpan = DateTime.Now.Subtract(token.TimeStamp);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Could not determine if the token is already expired");
            }

            // Return token if the time span is less or equal than 297 seconds with 3 seconds buffer for the request
            if (timeSpan.TotalSeconds <= 297)
            {
                return token;
            }

            // Get access token by refresh token if the token older than 5 minutes and less than 10 minutes
            else if (timeSpan.TotalSeconds > 297 && timeSpan.TotalMinutes <= 10)
            {
                try
                {
                    // Get new access token by refresh token
                    token = await RefreshAccessTokenAsync(authentication, token.RefreshToken);

                    // Save token local
                    Preferences.Set(authentication.ToString(), JsonConvert.SerializeObject(token));
                }
                catch (Exception e)
                {
                    throw e;
                }
                return token;
            }
            else
            {
                try
                {
                    // Generate token
                    token = await GetAccessTokenAsync(authentication);

                    // Save token local
                    Preferences.Set(authentication.ToString(), JsonConvert.SerializeObject(token));
                }
                catch (Exception e)
                {
                    throw e;
                }
                return token;
            }
        }
        #endregion
    }
}
