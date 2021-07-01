using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Android.Graphics;
using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using ScoreSheetScanner.Recognition.Model;

namespace ScoreSheetScanner.Recognition.Services
{
    public class OCRCloudCommunicator
    {
        /// <summary>
        /// Holds the client to access azure
        /// </summary>
        public FormRecognizerClient FormRecognizerClient { get; set; }
        /// <summary>
        /// Name of the file to be saved
        /// </summary>
        private readonly string ScoreSheetName = "scoreSheet.jpg";

        /// <summary>
        /// The credential to access the recognition api of azure
        /// </summary>
        private readonly string AzureCredentialKey = "<azure_credential_key>";

        /// <summary>
        /// URL to the form recognizer ressource in azure
        /// </summary>
        private readonly string FormRecognizerUrl = "<recognizer_url>";

        /// <summary>
        /// Id to the model trained for the scoresheet recognition in azure form recognizer
        /// </summary>
        private readonly string ModelId = "<model_id>";

        /// <summary>
        /// Recognizes the text in the bitmap using the model for the scoressheet recognition
        /// </summary>
        /// <param name="bitmap">The bitmap to recognize text in</param>
        /// <returns>A data transfer object containing the recognized text</returns>
        /// <exception cref="Exception">Bubbles up the exception from methods called within this method</exception>
        public async Task<ScoreSheetDTO> RecognizeFontAsync(Bitmap bitmap)
        {
            try
            {
                // Initializations
                ScoreSheetDTO scoreSheetDTO = new ScoreSheetDTO();

                // Login to azure
                FormRecognizerClient = await Task.Run(() => AuthenticateClient());

                // Get path to the document folder and save the bitmap there
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                SaveBitmap(bitmap, documentsPath, ScoreSheetName);

                // Get the path to the saved bitmap file
                string completePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ScoreSheetName);

                using (var fileStream = new FileStream(completePath, FileMode.Open))
                {
                    // Send the filestream and the modelId to azure to get the recognized label values
                    RecognizedFormCollection response = await FormRecognizerClient.StartRecognizeCustomForms(ModelId, fileStream)
                                                                                  .WaitForCompletionAsync();
                    // Transform the answer of azure into a more manageable DTO
                    scoreSheetDTO = await Task.Run(() => GetScoreSheetDTOFromResponse(response));
                }

                return scoreSheetDTO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Performs a login to azure
        /// </summary>
        /// <returns>Returns a client to access the form recognizer</returns>
        /// <exception cref="ArgumentException">Thrown when the credential key or the endpoint url are wrong</exception>
        /// <exception cref="UriFormatException">Thrown when the endpoint url of the form recognizer was in the wrong format</exception>
        private FormRecognizerClient AuthenticateClient()
        {
            try
            {
                // Create credentials using the key and instanciate a client
                AzureKeyCredential credential = new AzureKeyCredential(AzureCredentialKey);
                FormRecognizerClient client = new FormRecognizerClient(new Uri(FormRecognizerUrl), credential);
                return client;
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (UriFormatException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Saves a bitmap to the specified path in the parameters
        /// </summary>
        /// <param name="bitmap">The bitmap to save</param>
        /// <param name="filePath">The path where to save the bitmap to</param>
        /// <param name="fileName">The filename used to save the bitmap</param>
        /// <exception cref="ArgumentException">Thrown when something with the parameters went wrong</exception>
        /// <exception cref="Exception">Thrown when any other fatal error occurs</exception>
        private void SaveBitmap(Bitmap bitmap, string filePath, string fileName)
        {
            try
            {
                // Build the path to the file
                string completePath = System.IO.Path.Combine(filePath, fileName);

                // Save the file to the path
                var stream = new FileStream(completePath, FileMode.Create);

                // Save bitmap using the stream and close the stream
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                stream.Close();
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException($"parameter {e.ParamName} was null or in the wrong format");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Transforms the response from azure into a more manageable instance, which is not that much nested
        /// </summary>
        /// <param name="forms"></param>
        /// <returns>A scoresheet dto with the retrieved data of the photo</returns>
        private ScoreSheetDTO GetScoreSheetDTOFromResponse(RecognizedFormCollection forms)
        {
            // Initialize the new scoresheet DTO
            ScoreSheetDTO scoreSheetDTO = new ScoreSheetDTO();

            // Iterate over forms to assign them to the corresponding field in the Scoresheet DTO
            foreach (RecognizedForm form in forms)
            {
                foreach (var field in form.Fields)
                {
                    // Check if key-value pair does not contain null values
                    if (field.Value == null)
                    {
                        continue;
                    }

                    // Assign label value to correct property of the scoresheet dto
                    switch (field.Value.Name)
                    {
                        case ScoreSheetMappings.HomeTeam:
                            scoreSheetDTO.HomeTeam = field.Value.ValueData.Text ?? string.Empty;
                            break;

                        case ScoreSheetMappings.GuestTeam:
                            scoreSheetDTO.GuestTeam = field.Value.ValueData.Text ?? string.Empty;
                            break;

                        case ScoreSheetMappings.StartTime:
                            scoreSheetDTO.GameStartTime = field.Value.ValueData.Text?.Replace(" ", "") ?? string.Empty;
                            break;

                        case ScoreSheetMappings.GameClass:
                            scoreSheetDTO.GameClass = field.Value.ValueData.Text?.Replace(" ", "") ?? string.Empty;
                            break;

                        case ScoreSheetMappings.GamePlace:
                            scoreSheetDTO.GamePlace = field.Value.ValueData.Text ?? string.Empty;
                            break;

                        case ScoreSheetMappings.Date:
                            scoreSheetDTO.Date = field.Value.ValueData.Text?.Replace(" ", "") ?? string.Empty;
                            break;

                        case ScoreSheetMappings.Group:
                            scoreSheetDTO.Group = field.Value.ValueData.Text ?? string.Empty;
                            break;

                        case ScoreSheetMappings.EndTime:
                            scoreSheetDTO.GameEndTime = field.Value.ValueData.Text?.Replace(" ", "") ?? string.Empty;
                            break;

                        case ScoreSheetMappings.Winner:
                            scoreSheetDTO.Winner = field.Value.ValueData.Text ?? string.Empty;
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.PlayerA):
                            scoreSheetDTO.HomeTeamPlayers.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.PlayerB):
                            scoreSheetDTO.GuestTeamPlayers.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.MatchPairing):
                            scoreSheetDTO.MatchPairings.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.ResultsPairing1):
                            scoreSheetDTO.ResultsPairing.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.ResultsPairing2):
                            scoreSheetDTO.ResultsPairing.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.ResultsPairing3):
                            scoreSheetDTO.ResultsPairing.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.ResultsPairing4):
                            scoreSheetDTO.ResultsPairing.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.ResultsPairing5):
                            scoreSheetDTO.ResultsPairing.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.ResultsPairing6):
                            scoreSheetDTO.ResultsPairing.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.ResultsPairing7):
                            scoreSheetDTO.ResultsPairing.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.ResultsPairing8):
                            scoreSheetDTO.ResultsPairing.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.ResultsPairing9):
                            scoreSheetDTO.ResultsPairing.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.ResultsPairing10):
                            scoreSheetDTO.ResultsPairing.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.ResultsPairing11):
                            scoreSheetDTO.ResultsPairing.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.ResultsPairing12):
                            scoreSheetDTO.ResultsPairing.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.ResultsPairing13):
                            scoreSheetDTO.ResultsPairing.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.ResultsPairing14):
                            scoreSheetDTO.ResultsPairing.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.ResultsPairing15):
                            scoreSheetDTO.ResultsPairing.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        case string LabelName when LabelName.StartsWith(ScoreSheetMappings.ResultsPairing16):
                            scoreSheetDTO.ResultsPairing.Add(field.Value.Name, field.Value.ValueData.Text ?? string.Empty);
                            break;

                        default:
                            continue;
                    }
                }
            }
            SortScoreSheet(scoreSheetDTO);
            return scoreSheetDTO;
        }

        private ScoreSheetDTO SortScoreSheet(ScoreSheetDTO scoreSheetDTO)
        {
            // Regex to check for the correct syntax of the points e.g. +3, -4
            Regex regex = new Regex(@"^(\+|-)\d{1,2}$");
            Dictionary<string, string> newDictionayResultsPairing = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> result in scoreSheetDTO.ResultsPairing)
            {
                result.Value.Replace(" ", "");

                // If retrieved points match the regex they are added to the dictionary,
                // otherwise add empty string
                if (regex.IsMatch(result.Value))
                {
                    newDictionayResultsPairing.Add(result.Key, result.Value);
                }
                else
                {
                    newDictionayResultsPairing.Add(result.Key, string.Empty);
                }
            }

            // Iterate over results
            for (int j = 1; j < 17; j++)
            {
                for (int i = 1; i < 6; i++)
                {
                    // Add empty string if retrieval of the scoresheet didnt work
                    if (!newDictionayResultsPairing.ContainsKey(j + " " + i + ".Satz"))
                    {
                        newDictionayResultsPairing.Add(j + " " + i + ".Satz", string.Empty);
                    }
                }
            }

            // Iterate over home team players to add empty strings when the value couldnt be retrieved
            for (int i = 1; i < 13; i++)
            {
                if (!scoreSheetDTO.HomeTeamPlayers.ContainsKey("Spieler A " + i))
                {
                    scoreSheetDTO.HomeTeamPlayers.Add("Spieler A " + i, string.Empty);
                }
            }

            // Iterate over guest team players to add empty strings when the value couldnt be retrieved
            for (int i = 1; i < 13; i++)
            {
                if (!scoreSheetDTO.GuestTeamPlayers.ContainsKey("Spieler B " + i))
                {
                    scoreSheetDTO.GuestTeamPlayers.Add("Spieler B " + i, string.Empty);
                }
            }

            // Iterate over pairings to add empty strings when the value couldnt be retrieved
            for (int i = 1; i < 17; i++)
            {
                if (!scoreSheetDTO.MatchPairings.ContainsKey("Paarung " + i))
                {
                    scoreSheetDTO.MatchPairings.Add("Paarung " + i, string.Empty);
                }
            }

            // Add dictionary to the scoresheet dto
            scoreSheetDTO.ResultsPairing = newDictionayResultsPairing;
            return scoreSheetDTO;
        }
    }
}
