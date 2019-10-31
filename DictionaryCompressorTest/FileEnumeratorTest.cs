using System.IO;
using System.Linq;
using DictionaryCompressor;
using Messerli.Test.Utility;
using Xunit;

namespace DictionaryCompressorTest
{
    public class FileEnumeratorTest
    {
        [Fact]
        public void GetFilesTest()
        {
            var fileName = "File1.lst";
            var testFile = TestFile.Create(fileName);

            using (var testEnvironmentBuilder = new TestEnvironmentBuilder()
                .AddTestFile(testFile)
                .Build())
            {
                FileEnumerator fileEnumerator = new FileEnumerator();
                var files = fileEnumerator.GetFiles(testEnvironmentBuilder.RootDirectory);

                Assert.Single(files);
                Assert.Equal(Path.GetFileName(files.First()), fileName);
            }
        }
    }
}
