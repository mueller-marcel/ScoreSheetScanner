using System.Collections.Generic;
using Fastenshtein;
using NUnit.Framework;

namespace NUnitTestProject
{
    public class Tests
    {
        /// <summary>
        /// Matches the recognized player names with the cloud data
        /// </summary>
        /// <param name="recognizedNamesDictionary">The names from the scoresheet</param>
        /// <param name="downloadedNames">The names from the cloud</param>
        /// <returns>A <see cref="Dictionary{string, string}"/> with the corrected names</returns>
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
                    if (levenshteinDistance < 5)
                    {
                        workingDictionary.Add(recognizedName.Key, downloadedName);
                        break;
                    }
                }
                if (levenshteinDistance >= 5)
                {
                    workingDictionary.Add(recognizedName.Key, recognizedName.Value);
                }
            }
            return workingDictionary;
        }

        private Dictionary<string, string> recognizedNames;
        private Dictionary<string, string> expectedNames;
        List<string> downloadedNames;

        /// <summary>
        /// Sets up the unit test
        /// </summary>
        [SetUp]
        public void SetupBeforeEachTest()
        {
            recognizedNames = new Dictionary<string, string>();
            expectedNames = new Dictionary<string, string>();
            downloadedNames = new List<string>();
        }

        [Test]
        public void Test1()
        {
            expectedNames.Add("A1", "Josua Stricker");
            expectedNames.Add("A2", "Marcel M�ller");
            expectedNames.Add("A3", "Sven Stoll");

            recognizedNames.Add("A1", "Josua Stricker");
            recognizedNames.Add("A2", "Marcel M�ller");
            recognizedNames.Add("A3", "Sven Stoll");

            downloadedNames.Add("Josua Stricker");
            downloadedNames.Add("Marcel M�ller");
            downloadedNames.Add("Sven Stoll");

            Dictionary<string, string> result = Match(recognizedNames, downloadedNames);
            Assert.AreEqual(expectedNames, result);
        }

        [Test]
        public void PlayerMatching_Success()
        {
            expectedNames.Add("A1", "Max Mustermann");
            expectedNames.Add("A2", "Maria Weber");
            expectedNames.Add("A3", "Susanne Bieber");

            recognizedNames.Add("A1", "MaX Mostermann");
            recognizedNames.Add("A2", "Maria Weber");
            recognizedNames.Add("A3", "sUsanneBieber");

            downloadedNames.Add("Max Mustermann");
            downloadedNames.Add("Susanne Bieber");

            Dictionary<string, string> result = Match(recognizedNames, downloadedNames);
            Assert.AreEqual(expectedNames, result);
        }

        [Test]
        public void Test3()
        {
            expectedNames.Add("A1", "Josua Stricker");
            expectedNames.Add("A2", "Marcel M�ller");
            expectedNames.Add("A3", "Sven Stoll");

            recognizedNames.Add("A1", "JOsua Stricker");
            recognizedNames.Add("A2", "MarKKK M�ller");
            recognizedNames.Add("A3", "Sven Stoll");

            downloadedNames.Add("Josua Stricker");
            downloadedNames.Add("Marcel M�ller");

            Dictionary<string, string> result = Match(recognizedNames, downloadedNames);
            Assert.AreEqual(expectedNames, result);
        }

        [Test]
        public void Test4()
        {
            expectedNames.Add("A1", "Josua Stricker");
            expectedNames.Add("A2", "Marcel M�ller");
            expectedNames.Add("A3", "Sven Stoll");

            recognizedNames.Add("A1", "Josua Stricker");
            recognizedNames.Add("A2", "Marcel M�ller");
            recognizedNames.Add("A3", "Sven Stoll");

            downloadedNames.Add("JOSUA STRICKER");
            downloadedNames.Add("Marcel Stoll");

            Dictionary<string, string> result = Match(recognizedNames, downloadedNames);
            Assert.AreEqual(expectedNames, result);
        }

        [Test]
        public void Test5()
        {
            expectedNames.Add("A1", "Josua Stricker");
            expectedNames.Add("A2", "Marcel M�ller");
            expectedNames.Add("A3", "Max Mustermann");

            recognizedNames.Add("A1", "JOsuA Stricker");
            recognizedNames.Add("A2", "Marcelo M�ller");
            recognizedNames.Add("A3", "Max Mustermann");

            downloadedNames.Add("Josua Stricker");
            downloadedNames.Add("Marcel M�ller");
            downloadedNames.Add("Max Thum");

            Dictionary<string, string> result = Match(recognizedNames, downloadedNames);
            Assert.AreEqual(expectedNames, result);
        }
    }
}