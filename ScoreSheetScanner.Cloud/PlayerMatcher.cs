using System;
using System.Collections.Generic;
using System.Linq;
using Fastenshtein;

namespace ScoreSheetScanner.Cloud
{
    public class PlayerMatcher
    {
        /// <summary>
        /// Compares the recognized names with the names from nuScore and corrects them by the Levenshtein distance
        /// </summary>
        /// <param name="recognizedNamesDictionary">A dictionary containing A1,B1 etc. as key and the names as value</param>
        /// <param name="downloadedNames">A <see cref="List{string}"/> with the names from nuScore</param>
        /// <returns>An <see cref="Dictionary{String, String}"/> with the corrected names</returns>
        public Dictionary<string, string> MatchPlayers(Dictionary<string, string> recognizedNamesDictionary, List<string> downloadedNames)
        {
            // Copy dictionary
            Dictionary<string, string> workingDictionary = new Dictionary<string, string>();

            // Iterate over all recognized names 
            foreach (var recognizedName in recognizedNamesDictionary)
            {
                // Define a levenshtein engine and an array to save the distances to each downloaded name
                Levenshtein levenshtein = new Levenshtein(recognizedName.Value);
                int[] distances = new int[downloadedNames.Count];

                // Iterate over the downloaded names and save the distance to the array
                for (int i = 0; i < downloadedNames.Count; i++)
                {
                    distances[i] = levenshtein.DistanceFrom(downloadedNames.ElementAt(i));
                }

                // If the minimum distance is longer than the name itself it will be skipped
                if (distances.Min() >= recognizedName.Value.Length)
                {
                    workingDictionary.Add(recognizedName.Key, recognizedName.Value);
                    continue;
                }
                else
                {
                    // Get the index of the minimum
                    int indexOfMinimum = Array.IndexOf(distances, distances.Min());

                    // Get the element to replace from the downloaded names using the minimum index
                    string nameToReplace = downloadedNames.ElementAt(indexOfMinimum);

                    // Replace value in the dictionary
                    workingDictionary.Add(recognizedName.Key, nameToReplace);
                }
            }

            return workingDictionary;
        }
        public Dictionary<string, string> Match(Dictionary<string, string> recognizedNamesDictionary, List<string> downloadedNames)
        {
            Dictionary<string, string> workingDictionary = new Dictionary<string, string>();
            foreach (var recognizedName in recognizedNamesDictionary)
            {
                Levenshtein levenshtein = new Levenshtein(recognizedName.Value);
                int levenshteinDistance = 100;
                foreach (var downloadedName in downloadedNames)
                {
                    levenshteinDistance = levenshtein.DistanceFrom(downloadedName);
                    if (levenshteinDistance < 3)
                    {
                        workingDictionary.Add(recognizedName.Key, downloadedName);
                        break;
                    }
                }
                if (levenshteinDistance >= 3)
                {
                    workingDictionary.Add(recognizedName.Key, recognizedName.Value);
                }
            }
            return workingDictionary;
        }
    }

}
