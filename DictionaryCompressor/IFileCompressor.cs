namespace DictionaryCompressor
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    interface IFileCompressor
    {
        void Compress(string path);
    }
}
