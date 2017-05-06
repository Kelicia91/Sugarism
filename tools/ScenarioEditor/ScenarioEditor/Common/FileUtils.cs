using System;
using System.IO;
using System.Text;


namespace ScenarioEditor
{
    class FileUtils
    {
        /// <summary>
        /// Extract file name with extension from path.
        /// If path is null, return null.
        /// </summary>
        public static string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        public static bool Exists(string filePath)
        {
            if (File.Exists(filePath))
                return true;
            else
                return false;
        }

        public static bool ReadAllTextAsUTF8(string filePath, out string text)
        {
            return ReadAllText(filePath, Encoding.UTF8, out text);
        }

        public static bool ReadAllText(string filePath, Encoding encoding, out string text)
        {
            text = string.Empty;
            try
            {
                text = File.ReadAllText(filePath, encoding);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return false;
            }

            return true;
        }

        public static bool WriteAllTextAsUTF8(string filePath, string text)
        {
            return WriteAllText(filePath, Encoding.UTF8, text);
        }

        public static bool WriteAllText(string filePath, Encoding encoding, string text)
        {
            try
            {
                File.WriteAllText(filePath, text, encoding);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return false;
            }
            return true;
        }
    }
}
