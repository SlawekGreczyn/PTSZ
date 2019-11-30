using System;
using System.IO;


namespace PTSZ
{
    class Program
    {
        static void Main(string[] args)
        {
            int option;
            string filePath;
            Instance instance;

            do {
                Console.Clear();

                option = DisplayMenu();

                Console.Clear();

                switch (option)
                {
                    case 1:
                        Directory.CreateDirectory("Instances");
                        Instance.GenerateSetToFiles();
                        break;
                    case 2:
                        Directory.CreateDirectory("Results");
                        filePath = FilesHelper.SelectFile("Instances");
                        instance = Instance.FromFile(filePath);
                        SolverGreedy.RunAndSave(instance, filePath.Replace(".txt", ".GREEDY.out.txt").Replace("Instances", "Results"));
                        break;
                    case 3:
                        Directory.CreateDirectory("Results");
                        filePath = FilesHelper.SelectFile("Instances");
                        instance = Instance.FromFile(filePath);
                        string filePathResults = FilesHelper.SelectFile("Results");
                        int delayTime;
                        bool isValid = Validator.Validate( instance, filePathResults, out delayTime );
                        Console.WriteLine(String.Format( "Validator resilt {0}! - delay is equal to {1}", isValid ? "PASS" : "FAILED", delayTime ));
                        Console.ReadLine();
                        break;
                }
            } while (option != 0);
        }

        static public int DisplayMenu()
        {
            Console.WriteLine("PTSZ Program");
            Console.WriteLine();
            Console.WriteLine("1. Generate data.");
            Console.WriteLine("2. Load and solve from file - greedy");
            Console.WriteLine("3. Verify solution");
            Console.WriteLine("0. Exit");
            string result = Console.ReadLine();

            return Convert.ToInt32(result);
        }
    }
}
