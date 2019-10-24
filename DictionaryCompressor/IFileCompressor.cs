namespace DictionaryCompressor
{
    public interface IFileCompressor
    {
        void CompressFile(string filePath, string output);

        void CompressDirectory(string directoryPath, string output);
    }
}
