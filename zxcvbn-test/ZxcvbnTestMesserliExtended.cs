﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.IO.Compression;

namespace zxcvbn_test
{
    [TestClass]
    public class ZxcvbnTest_MesserliExtended
    {
        private const string CategoryUnitTest = "UnitTest";

        [Ignore]
        [TestMethod]
        public void CompressDictionaries()
        {
            // Running this test creates the compressed dictionaries for the zxcvbn resources
            var basePath = Directory.GetCurrentDirectory();
            var dictionaryPath = Path.GetFullPath(Path.Combine(basePath, "..\\..\\..\\..\\RMIProd\\Main\\ExternalLibraries\\zxcvbn\\Dictionaries\\Uncompressed"));

            DirectoryInfo directorySelected = new DirectoryInfo(dictionaryPath);

            foreach (FileInfo file in directorySelected.GetFiles("*.lst"))
            {
                using (FileStream originalFileStream = file.OpenRead())
                {
                    if ((File.GetAttributes(file.FullName) & FileAttributes.Hidden)
                        != FileAttributes.Hidden & file.Extension != ".cmp")
                    {
                        var compressedFileName = Path.GetFullPath(Path.Combine(file.DirectoryName, "..", file.Name + ".cmp"));
                        using (FileStream compressedFileStream = File.Create(compressedFileName))
                        {
                            using (DeflateStream compressionStream = new DeflateStream(compressedFileStream, CompressionLevel.Optimal))
                            {
                                originalFileStream.CopyTo(compressionStream);
                            }
                        }
                    }
                }
            }

        }

        [TestMethod]
        [TestCategory(CategoryUnitTest)]
        [Priority(1)]
        public void ZxcvbnEmptyPasswordMetric()
        {
            var passwordChecker = new Zxcvbn.Zxcvbn();

            var result = passwordChecker.EvaluatePassword("");

            Assert.AreEqual(0.0, result.Entropy);
        }

        [TestMethod]
        [TestCategory(CategoryUnitTest)]
        [Priority(1)]
        public void ZxcvbnPasswordMetric()
        {
            var passwordChecker = new Zxcvbn.Zxcvbn();

            var a = passwordChecker.EvaluatePassword("a");
            var aaaa = passwordChecker.EvaluatePassword("aaaa");
            var abab = passwordChecker.EvaluatePassword("abab");
            var ABab = passwordChecker.EvaluatePassword("ABab");

            Assert.IsTrue(aaaa.Entropy > a.Entropy);
            Assert.IsTrue(abab.Entropy > aaaa.Entropy);
            Assert.IsTrue(ABab.Entropy > abab.Entropy);
        }

        [TestMethod]
        [TestCategory(CategoryUnitTest)]
        [Priority(1)]
        public void DictionaryMatcherTest()
        {
            // We test if the dictionary matcher loads the messerli dictionary
            var passwordChecker = new Zxcvbn.Zxcvbn();
            var password = "Messerli";
            var metric = passwordChecker.EvaluatePassword(password);

            foreach (var match in metric.MatchSequence)
            {
                // Match
                Assert.AreEqual(10, match.Cardinality);
                Assert.AreEqual(password, match.Token);
                Assert.AreEqual(0, match.i);
                Assert.AreEqual(password.Length - 1, match.j);

                // Dictionary Match
                var dictionaryMatch = match as Zxcvbn.Matcher.DictionaryMatch;

                Assert.AreNotEqual(null, dictionaryMatch);
                Assert.AreEqual("messerli", dictionaryMatch.DictionaryName);
                Assert.AreEqual(1, dictionaryMatch.UppercaseEntropy);


            }
        }

        [TestMethod]
        [TestCategory(CategoryUnitTest)]
        [Priority(1)]
        public void SwissSpatialMatcherTest()
        {
            // We test if the pattern matcher matches swiss keyboards
            var swissKeyboardPatterns = new System.Collections.Generic.List<string> { "löäü'", "ö-.,m" };

            var passwordChecker = new Zxcvbn.Zxcvbn();

            foreach (var pattern in swissKeyboardPatterns)
            {
                var metric = passwordChecker.EvaluatePassword(pattern);

                foreach (var match in metric.MatchSequence)
                {
                    // Dictionary Match
                    var spatialMatch = match as Zxcvbn.Matcher.SpatialMatch;

                    Assert.AreNotEqual(null, spatialMatch);
                    Assert.AreEqual("Swiss German", spatialMatch.Graph);
                    Assert.AreEqual(0, spatialMatch.ShiftedCount);
                    Assert.AreEqual(2, spatialMatch.Turns);

                    // Match
                    Assert.AreEqual(0, match.Cardinality);
                    Assert.AreEqual(0, match.i);
                    Assert.AreEqual(pattern.Length - 1, match.j);
                    Assert.AreEqual(pattern, match.Token);
                }
            }
        }

        [TestMethod]
        [TestCategory(CategoryUnitTest)]
        [Priority(1)]
        public void FrenchSpatialMatcherTest()
        {
            // We test if the pattern matcher matches swiss keyboards
            var frenchKeyboardPatterns = new System.Collections.Generic.List<string> { "*ùmlkj", "bn,;:!" };

            var passwordChecker = new Zxcvbn.Zxcvbn();

            foreach (var pattern in frenchKeyboardPatterns)
            {
                var metric = passwordChecker.EvaluatePassword(pattern);

                foreach (var match in metric.MatchSequence)
                {
                    // Dictionary Match
                    var spatialMatch = match as Zxcvbn.Matcher.SpatialMatch;

                    Assert.AreNotEqual(null, spatialMatch);
                    Assert.AreEqual("French", spatialMatch.Graph);
                    Assert.AreEqual(0, spatialMatch.ShiftedCount);
                    Assert.AreEqual(1, spatialMatch.Turns);

                    // Match
                    Assert.AreEqual(0, match.Cardinality);
                    Assert.AreEqual(0, match.i);
                    Assert.AreEqual(pattern.Length - 1, match.j);
                    Assert.AreEqual(pattern, match.Token);
                }
            }
        }
    }
}

