using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Zxcvbn.Matcher;

namespace Zxcvbn
{
    /// <summary>
    /// <para>This matcher factory will use all of the default password matchers.</para>
    /// 
    /// <para>Default dictionary matchers use the built-in word lists: passwords, english, male_names, female_names, surnames</para>
    /// <para>Also matching against: user data, all dictionaries with l33t substitutions</para>
    /// <para>Other default matchers: repeats, sequences, digits, years, dates, spatial</para>
    /// 
    /// <para>See <see cref="IMatcher"/> and the classes that implement it for more information on each kind of pattern matcher.</para>
    /// </summary>
    class DefaultMatcherFactory : IMatcherFactory
    {
        List<IMatcher> matchers;

        private IEnumerable<string> DictionaryResources()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            string folderName = string.Format("{0}.Resources.Folder", executingAssembly.GetName().Name);
            return executingAssembly.GetManifestResourceNames().Where(path => path.StartsWith("zxcvbn.Dictionaries"));

        }

        /// <summary>
        /// Create a matcher factory that uses the default list of pattern matchers
        /// </summary>
        public DefaultMatcherFactory()
        {
            var dictionaryMatchers = new List<DictionaryMatcher>();

            foreach (var resource in DictionaryResources())
            {
                var name = resource.Split('.').Skip(2).First();
                dictionaryMatchers.Add(new DictionaryMatcher(name, resource));
            }

            matchers = new List<IMatcher> {
                new RepeatMatcher(),
                new SequenceMatcher(),
                new RegexMatcher("\\d{3,}", 10, true, "digits"),
                new RegexMatcher("19\\d\\d|200\\d|201\\d", 119, false, "year"),
                new DateMatcher(),
                new SpatialMatcher()
            };

            matchers.AddRange(dictionaryMatchers);
            matchers.Add(new L33tMatcher(dictionaryMatchers));
        }

        /// <summary>
        /// Get instances of pattern matchers, adding in per-password matchers on userInputs (and userInputs with l33t substitutions)
        /// </summary>
        /// <param name="userInputs">Enumerable of user information</param>
        /// <returns>Enumerable of matchers to use</returns>
        public IEnumerable<IMatcher> CreateMatchers(IEnumerable<string> userInputs)
        {
            var userInputDict = new DictionaryMatcher("user_inputs", userInputs);
            var leetUser = new L33tMatcher(userInputDict);

            return matchers.Union(new List<IMatcher> { userInputDict, leetUser });
        }
    }
}
