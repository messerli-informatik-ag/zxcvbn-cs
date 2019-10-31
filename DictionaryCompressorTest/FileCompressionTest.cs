using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using DictionaryCompressor;
using Xunit;

namespace DictionaryCompressorTest
{
    public class FileCompressionTest
    {
        [SuppressMessage("ReSharper", "StringLiteralTypo", Justification = "Lorem ipsum uses words that do not exist")]
        private static byte[] Data => Encoding.UTF8.GetBytes(
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer sit amet erat ut massa ultricies molestie commodo quis erat. " +
            "Praesent tincidunt, nibh et vestibulum pretium, ipsum lorem congue lectus, a posuere lacus enim at mauris. Aliquam gravida ex a mi fringilla tincidunt. Mauris ac urna finibus, finibus lorem in, elementum arcu. " +
            "Aenean tincidunt metus non feugiat porta. Vestibulum sollicitudin in diam eget fermentum. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. " +
            "Integer est sapien, pretium nec volutpat vel, malesuada ut odio. Nam finibus iaculis viverra. Morbi faucibus, ipsum condimentum convallis auctor, metus ex condimentum lacus, in elementum nibh odio varius libero. " +
            "Proin nec felis eu neque ultrices pretium. Morbi porttitor sem quis viverra sollicitudin. Morbi vel ante ornare nisl porta convallis id id risus. Pellentesque iaculis venenatis dolor aliquet malesuada.");

        [Fact]
        public void Compress()
        {
            var fileCompression = new FileCompression();
            using (var streamToCompress = new MemoryStream(Data))
            using (var compressedMemoryStream = new MemoryStream())
            {
                fileCompression.Compress(streamToCompress, compressedMemoryStream);
                var compressedBytes = compressedMemoryStream.ToArray();
                Assert.NotEqual(Data, compressedBytes);
            }
        }
    }
}
