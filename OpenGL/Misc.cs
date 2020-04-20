using System.IO;

namespace OpenGL
{
    public static class Misc
    {
        public static string GetFileContentAsString(string fileName)
        {
            CheckFileExists(fileName);
            return File.ReadAllText(fileName);
        }

        public static byte[] GetFileContentAsBytes(string fileName)
        {
            CheckFileExists(fileName);
            return File.ReadAllBytes(fileName);
        }

        private static void CheckFileExists(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("File was not found.");
            }
        }
    }
}