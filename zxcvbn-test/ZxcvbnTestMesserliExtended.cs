using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zxcvbn;
using Zxcvbn.Matcher;

namespace zxcvbn_test
{
    [TestClass]
    public class ZxcvbnTestMesserliExtended
    {
        private const string CategoryUnitTest = "UnitTest";

        [TestMethod]
        [TestCategory(CategoryUnitTest)]
        [Priority(1)]
        public void EvaluatesEmptyPasswordWithAnEntropyOfZero()
        {
            var passwordChecker = new PasswordMetric();

            var result = passwordChecker.EvaluatePassword("");

            Assert.AreEqual(0.0, result.Entropy);
        }

        [TestMethod]
        [TestCategory(CategoryUnitTest)]
        [Priority(1)]
        public void EvaluatesPasswordEntropy()
        {
            var passwordChecker = new PasswordMetric();

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
        public void LoadsMesserliDictionary()
        {
            var passwordChecker = new PasswordMetric();
            var password = "Messerli";
            var metric = passwordChecker.EvaluatePassword(password);

            foreach (var match in metric.MatchSequence)
            {
                Assert.AreEqual(10, match.Cardinality);
                Assert.AreEqual(password, match.Token);
                Assert.AreEqual(0, match.Begin);
                Assert.AreEqual(password.Length - 1, match.End);

                var dictionaryMatch = match as DictionaryMatch;

                Assert.AreNotEqual(null, dictionaryMatch);
                Assert.AreEqual("messerli", dictionaryMatch.DictionaryName);
                Assert.AreEqual(1, dictionaryMatch.UppercaseEntropy);


            }
        }

        [TestMethod]
        [TestCategory(CategoryUnitTest)]
        [Priority(1)]
        public void MatchesSwissGermanKeyboardPattern()
        {
            var swissKeyboardPatterns = new List<string> { "löäü'", "ö-.,m" };

            var passwordChecker = new PasswordMetric();

            foreach (var pattern in swissKeyboardPatterns)
            {
                var metric = passwordChecker.EvaluatePassword(pattern);

                foreach (var match in metric.MatchSequence)
                {
                    var spatialMatch = match as SpatialMatch;

                    Assert.AreNotEqual(null, spatialMatch);
                    Assert.AreEqual("Swiss German", spatialMatch.Graph);
                    Assert.AreEqual(0, spatialMatch.ShiftedCount);
                    Assert.AreEqual(2, spatialMatch.Turns);

                    Assert.AreEqual(0, match.Cardinality);
                    Assert.AreEqual(0, match.Begin);
                    Assert.AreEqual(pattern.Length - 1, match.End);
                    Assert.AreEqual(pattern, match.Token);
                }
            }
        }

        [TestMethod]
        [TestCategory(CategoryUnitTest)]
        [Priority(1)]
        public void MatchesFrenchKeyboardPattern()
        {
            var frenchKeyboardPatterns = new List<string> { "*ùmlkj", "bn,;:!" };

            var passwordChecker = new PasswordMetric();

            foreach (var pattern in frenchKeyboardPatterns)
            {
                var metric = passwordChecker.EvaluatePassword(pattern);

                foreach (var match in metric.MatchSequence)
                {
                    var spatialMatch = match as SpatialMatch;

                    Assert.AreNotEqual(null, spatialMatch);
                    Assert.AreEqual("French", spatialMatch.Graph);
                    Assert.AreEqual(0, spatialMatch.ShiftedCount);
                    Assert.AreEqual(1, spatialMatch.Turns);

                    Assert.AreEqual(0, match.Cardinality);
                    Assert.AreEqual(0, match.Begin);
                    Assert.AreEqual(pattern.Length - 1, match.End);
                    Assert.AreEqual(pattern, match.Token);
                }
            }
        }
    }
}


