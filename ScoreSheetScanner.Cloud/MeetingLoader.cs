using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ScoreSheetScanner.Cloud.Helper;

namespace ScoreSheetScanner.Cloud
{
    public class MeetingLoader
    {
        #region Methods
        /// <summary>
        /// Gets the meetings according to a time span and the clubnumber
        /// </summary>
        /// <param name="token">An instance of type <see cref="Token"/> to hold the access token</param>
        /// <param name="clubNumber">The number of the club to get matches from</param>
        /// <param name="fromDate">The beginning of the time span in format: yyyy-mm-dd</param>
        /// <param name="toDate">The end of the time span in format: yyyy-mm-dd</param>
        /// <returns>The json containing the meetings of the club in the parametrized time span</returns>
        /// <exception cref="ObjectDisposedException">Thrown, if the response was disposed before it could be read</exception>
        /// <exception cref="ProtocolViolationException">Thrown, if there is no response</exception>
        /// <exception cref="ArgumentException">Thrown, if the charset could not be retrieved correctly from the response</exception>
        public async Task<string> GetMeetingsAsync(Token token, string clubNumber, string fromDate, string toDate)
        {
            string content;
            Stream responseStream;
            Encoding responseEncoding;

            // Build the request
            string requestUrl = $"https://ttde-portal.liga.nu/rs/2014/federations/TTBW/clubs/{clubNumber}/meetings?fromDate={fromDate}&toDate={toDate}";
            var httpRequest = WebRequest.CreateHttp(requestUrl);
            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = $"Bearer {token.AccessToken}";

            // Perform the GET-Method and cast it to a HttpWebResponse
            var response = (HttpWebResponse)await httpRequest.GetResponseAsync();

            // Check if request was successful
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    // Get a stream of the response
                    responseStream = response.GetResponseStream();
                }
                catch (ObjectDisposedException e)
                {
                    throw e;
                }
                catch (ProtocolViolationException e)
                {
                    throw e;
                }

                try
                {
                    // Retrieve the encoding
                    responseEncoding = Encoding.GetEncoding(response.CharacterSet);
                }
                catch (ArgumentException e)
                {
                    throw e;
                }

                // Read the content
                using (var streamReader = new StreamReader(responseStream, responseEncoding))
                {
                    content = streamReader.ReadToEnd();
                }
                return content;
            }
            else
            {
                throw new HttpRequestException($"Http-Request was not successful. statuscode: {response.StatusCode}");
            }
        }

        /// <summary>
        /// Gets the meeting by the meetingId
        /// </summary>
        /// <param name="token">Holds the <see cref="Token.AccessToken"/> for the authorization</param>
        /// <param name="meetingUri">Identifies the meeting</param>
        /// <returns>A meetingReportDTO in JSON</returns>
        /// <exception cref="ArgumentNullException">Thrown, when the <see cref="HttpRequestMessage"/> was null</exception>
        /// <exception cref="InvalidOperationException">Thrown, if the <see cref="HttpRequestMessage"/> was already sent by the <see cref="HttpClient"/></exception>
        /// <exception cref="HttpRequestException">Thrown, if the <see cref="HttpResponseMessage"/> does not have a successful status code</exception>
        public async Task<string> GetMeetingAsync(Token token, string meetingUri)
        {
            // Declaration
            HttpResponseMessage response;

            // Configure request url
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, meetingUri);

            // Set headers
            httpRequest.Headers.Add("Accept", "application/json");
            httpRequest.Headers.Add("Connection", "keep-alive");
            httpRequest.Headers.Add("Authorization", $"Bearer {token.AccessToken}");

            // Set up a httpClient to send the request
            using (var httpClient = new HttpClient())
            {
                try
                {
                    // Try to send the request
                    response = await httpClient.SendAsync(httpRequest);
                }
                catch (ArgumentNullException e)
                {
                    throw e;
                }
                catch (InvalidOperationException e)
                {
                    throw e;
                }
                catch (HttpRequestException e)
                {
                    throw e;
                }
            }

            // Check if the http request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the content and return it
                string responseJson = await response.Content.ReadAsStringAsync();
                return responseJson;
            }

            // When 401 is returned throw exception due to an invalid access token
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new HttpRequestException("Access Token was invalid");
            }

            // If no common error matches thrown an exception with an unknown error
            throw new HttpRequestException("Unknown http error");
        }

        /// <summary>
        /// Retrieves the meetingDTO using the gamecode
        /// </summary>
        /// <param name="token">Holds a valid access token and the refresh token</param>
        /// <param name="gameCode">The gamecode needed to identify the correct meetingDTO to retrieve</param>
        /// <returns>A meetingDTO as JSON</returns>
        public async Task<string> GetMeetingDTOAsync(Token token, string gameCode)
        {
            // Declarations
            string content;
            Stream responseStream;
            Encoding responseEncoding;

            // Configure request url
            string requestUrl = $"https://ttde-portal.liga.nu/nuliga/rs/2018/meetingentry/report/{gameCode}";

            // Configure http request
            var httpRequest = WebRequest.CreateHttp(requestUrl);
            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = $"Bearer {token.AccessToken}";

            // Send http request and return response asynchronously
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)await httpRequest.GetResponseAsync();

            }
            catch (Exception e)
            {
                throw e;
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    // Get a stream of the response
                    responseStream = response.GetResponseStream();
                }
                catch (ObjectDisposedException e)
                {
                    throw e;
                }
                catch (ProtocolViolationException e)
                {
                    throw e;
                }

                try
                {
                    // Retrieve the encoding
                    responseEncoding = Encoding.GetEncoding(response.CharacterSet);
                }
                catch (ArgumentException e)
                {
                    throw e;
                }

                // Read the content
                using (var streamReader = new StreamReader(responseStream, responseEncoding))
                {
                    content = streamReader.ReadToEnd();
                }
                return content;
            }
            else
            {
                throw new HttpRequestException($"Http-Request was not successful. statuscode: {response.StatusCode}");
            }
        }
        #endregion
    }
}
