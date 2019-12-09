using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PTSZ
{
    public class FilesHelper
    {
        public static string SelectFile(string path) {
            List<string> filePaths = Directory.GetFiles(path).ToList().OrderBy(q => q).ToList();

            Console.WriteLine("Select a file:");
            Console.WriteLine();

            for ( int i = 0; i < filePaths.Count; i++ )
            {
                Console.WriteLine(String.Format( "{0}: {1}", i, filePaths[i] )) ;
            }

            string result = Console.ReadLine();

            int selection = Convert.ToInt32(result);

            if (selection >= filePaths.Count) {
                Console.Clear();
                return SelectFile(path);
            }

            return filePaths[selection];
        }
    }
}
