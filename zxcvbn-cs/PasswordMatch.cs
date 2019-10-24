using System;

namespace Zxcvbn
{
    /// <summary>
    /// <para>A single match that one of the pattern matchers has made against the password being tested.</para>
    ///
    /// <para>Some pattern matchers implement subclasses of match that can provide more information on their specific results.</para>
    ///
    /// <para>Matches must all have the <see cref="Pattern"/>, <see cref="Token"/>, <see cref="Entropy"/>, <see cref="Begin"/> and
    /// <see cref="End"/> fields (i.e. all but the <see cref="Cardinality"/> field, which is optional) set before being returned from the matcher
    /// in which they are created.</para>
    /// </summary>
    // TODO: These should probably be immutable
    public class PasswordMatch
    {
        /// <summary>
        /// Some pattern matchers can associate the cardinality of the set of possible matches that the
        /// entropy calculation is derived from. Not all matchers provide a value for cardinality.
        /// </summary>
        public int Cardinality { get; set; }

        /// <summary>
        /// The entropy that this portion of the password covers using the current pattern matching technique
        /// </summary>
        public double Entropy { get; set; }

        // The following are more internal measures, but may be useful to consumers
        /// <summary>
        /// The start index in the password string of the matched token.
        /// </summary>
        public int Begin { get; set; }

        /// <summary>
        /// The end index in the password string of the matched token.
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// The name of the pattern matcher used to generate this match
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// The portion of the password that was matched
        /// </summary>
        public string Token { get; set; }

        public override string ToString()
        {
            if (Cardinality > 0)
            {
                return String.Format("Matched '{0}' with {1} from {2} to {3} with {4}bit Entropy based on {5} Elements.", Token, Pattern, Begin, End, Entropy, Cardinality);
            }

            return String.Format("Matched '{0}' with {1} from {2} to {3} with {4}bit Entropy.", Token, Pattern, Begin, End, Entropy);
        }
    }
}