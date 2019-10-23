using System.Collections.Generic;

namespace Zxcvbn
{
    /// <summary>
    /// The results of zxcvbn's password analysis
    /// </summary>
    // TODO: These should probably be immutable
    public class PasswordMetricResult
    {
        /// <summary>
        /// The number of milliseconds that zxcvbn took to calculate results for this password
        /// </summary>
        public long CalcTime { get; set; }

        /// <summary>
        /// An estimation of the crack time for this password in seconds
        /// </summary>
        public double CrackTime { get; set; }

        /// <summary>
        /// A friendly string for the crack time (like "centuries", "instant", "7 minutes", "14 hours" etc.)
        /// </summary>
        public string CrackTimeDisplay { get; set; }

        /// <summary>
        /// A calculated estimate of how many bits of entropy the password covers, rounded to three decimal places.
        /// </summary>
        public double Entropy { get; set; }

        /// <summary>
        /// The sequence of matches that were used to create the entropy calculation
        /// </summary>
        public IList<PasswordMatch> MatchSequence { get; set; }

        /// <summary>
        /// The password that was used to generate these results
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// A score from 0 to 4 (inclusive), with 0 being least secure and 4 being most secure calculated from crack time:
        /// [0,1,2,3,4] if crack time is less than [10**2, 10**4, 10**6, 10**8, Infinity] seconds.
        /// Useful for implementing a strength meter
        /// </summary>
        public int Score { get; set; }


        private const double GoodPasswordEntropy = 40.0;
        private const double baseBrightness = 191.0;

        /// <summary>
        /// Returns a Color representing how good a password is
        ///  (0 = bad = red, medium = yellow, good = green)
        /// </summary>
        /// <returns>Color between red, yellow, green</returns>
        public System.Drawing.Color PasswordStrengthColor
        {
            get
            {
                // Gradient from red (0.0) yellow (0.5) to green (1.0)
                int red = (int)System.Math.Min(128.0f * (1 - GetNormalizedMetric()) + baseBrightness, 255.0);
                int green = (int)System.Math.Min(128.0f * GetNormalizedMetric() + baseBrightness, 255.0);
                int blue = (int)baseBrightness;

                return System.Drawing.Color.FromArgb(red, green, blue);
            }
        }

        /// <summary>
        /// Returns a normalized strength value between 0.0 and 1.0 
        /// </summary>
        /// <returns>Value between 0.0 WORST and 1.0 GOOD</returns>
        private double GetNormalizedMetric()
        {
            return System.Math.Min(Entropy / GoodPasswordEntropy, 1.0);
        }
    }
}