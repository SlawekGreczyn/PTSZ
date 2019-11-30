using System;
using System.IO;

namespace PTSZ
{
    public class FilesHelper
    {
        public static string SelectFile(string path) {
            string[] filePaths = Directory.GetFiles(path);

            Console.WriteLine("Wybierz plik");
            Console.WriteLine();

            for ( int i = 0; i < filePaths.Length; i++ )
            {
                Console.WriteLine(String.Format( "{0}: {1}", i, filePaths[i] )) ;
            }

            string result = Console.ReadLine();

            int selection = Convert.ToInt32(result);

            if (selection >= filePaths.Length) {
                Console.Clear();
                return SelectFile(path);
            }

            return filePaths[selection];
        }
    }
}
