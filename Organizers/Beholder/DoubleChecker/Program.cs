using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DoubleChecker
{
    class Program
    {
        private static string[] extensions = new string[]
		{
			".bmp",
			".jpg",
			".png",
			".raw",
			".tif",
			".wmv",
			".avi",
			".mp4",
			".3gp",
			".mpg",
			".m4v",
			".mkv",
		};

        /// <summary>
        /// Read every file recursevily in path1 and store in dictionary of md5-hash, path.
        /// Read every file recursevily in path2 and store in dictionary of md5-has, path.
        /// List the difference between path1 and path2 by comparing md5-hashes.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            FileDictionary[] dictionaries = new FileDictionary[2];

            Parallel.For(0, 2, i =>
                {
                    Console.WriteLine(i);
                    dictionaries[i] = ScanDirectory(args[i]);
                });
            var result = dictionaries[0] - dictionaries[1];
            ShowDictionary(result);
        }

        private static void ShowDictionary(FileDictionary result)
        {
            result.DumpToConsole();
        }

        private static FileDictionary ScanDirectory(string directoryPath)
        {
            var fileDictionary = new FileDictionary();
            using (MD5 md5 = MD5.Create())
            {
                DoScan(fileDictionary, md5, directoryPath);
            }

            return fileDictionary;
        }

        private static void DoScan(FileDictionary fileDictionary, MD5 md5, string directoryPath)
        {
            foreach (var filePath in Directory.GetFiles(directoryPath))
            {
                if (IncludeFile(filePath))
                {
                    string md5hash = CalcMd5Hash(md5, filePath);
                    fileDictionary.Add(md5hash, filePath);
                }
            }

            foreach (var dirPath in Directory.GetDirectories(directoryPath))
            {
                DoScan(fileDictionary, md5, dirPath);
            }
        }

        private static bool IncludeFile(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            return extensions.Any(x => string.Equals(x, extension, StringComparison.OrdinalIgnoreCase));
        }

        public static string CalcMd5Hash(MD5 md5, string filePath)
        {
            byte[] hash;
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                hash = md5.ComputeHash(stream);
            }
            return BitConverter.ToString(hash).Replace("-", "");
        }

    }
}
